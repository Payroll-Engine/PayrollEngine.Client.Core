using System;
using System.Collections;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace PayrollEngine.Client.Exchange;

/// <summary>Custom YAML type converter for dictionary support</summary>
public class YamlDictionaryTypeConverter : IYamlTypeConverter
{
    /// <inheritdoc />
    public bool Accepts(Type type)
    {
        return type.IsGenericType &&
               type.GetGenericTypeDefinition() == typeof(Dictionary<,>);
    }

    /// <inheritdoc />
    public object ReadYaml(IParser parser, Type type, ObjectDeserializer serializer)
    {
        // dictionary instance
        var dictionary = (IDictionary)Activator.CreateInstance(type);
        var genericArguments = type.GetGenericArguments();
        var valueType = genericArguments.Length > 1 ? genericArguments[1] : typeof(object);

        // mapping start
        parser.Consume<MappingStart>();

        // Read key-value pairs until mapping end
        while (parser.Current is not MappingEnd)
        {
            // key as scalar
            var keyEvent = parser.Consume<Scalar>();
            var key = keyEvent.Value;

            // value recursively
            var value = ReadValue(parser, valueType);

            // add to dictionary
            dictionary![key] = value;
        }

        // mapping end
        parser.Consume<MappingEnd>();

        return dictionary;
    }

    /// <inheritdoc />
    public void WriteYaml(IEmitter emitter, object value, Type type, ObjectSerializer serializer)
    {
        var dictionary = (IDictionary)value;

        // mapping start with modern parameters (no anchor, no explicit tag)
        emitter.Emit(new MappingStart(null, null, true, MappingStyle.Any));

        foreach (DictionaryEntry entry in dictionary!)
        {
            // key
            emitter.Emit(new Scalar(entry.Key.ToString() ?? string.Empty));

            // value
            WriteValue(emitter, entry.Value, serializer);
        }

        // mapping end
        emitter.Emit(new MappingEnd());
    }

    /// <summary>
    /// Read parser value
    /// </summary>
    /// <param name="parser">YAML parser</param>
    /// <param name="type">Value type</param>
    private object ReadValue(IParser parser, Type type)
    {
        var current = parser.Current;
        switch (current)
        {
            case Scalar scalar:
                parser.MoveNext();

                if (string.IsNullOrWhiteSpace(scalar.Value))
                {
                    return null;
                }

                try
                {
                    // simple conversion for basic types
                    return type == typeof(object) ? scalar.Value : Convert.ChangeType(scalar.Value, type);
                }
                catch
                {
                    return scalar.Value;
                }
            case SequenceStart:
                return ReadSequence(parser);
            case MappingStart:
                return ReadMapping(parser);
            default:
                throw new InvalidOperationException($"Cannot read value of type {type.Name} from {current?.GetType().Name}");
        }
    }

    private object ReadSequence(IParser parser)
    {
        var list = new List<object>();
        parser.Consume<SequenceStart>();
        while (parser.Current is not SequenceEnd)
        {
            var value = ReadValue(parser, typeof(object));
            list.Add(value);
        }
        parser.Consume<SequenceEnd>();
        return list;
    }

    private object ReadMapping(IParser parser)
    {
        var dict = new Dictionary<object, object>();
        parser.Consume<MappingStart>();
        while (parser.Current is not MappingEnd)
        {
            var keyEvent = parser.Consume<Scalar>();
            var key = keyEvent.Value;

            var value = ReadValue(parser, typeof(object));
            dict[key] = value;
        }
        parser.Consume<MappingEnd>();
        return dict;
    }

    private void WriteValue(IEmitter emitter, object value, ObjectSerializer serializer)
    {
        // empty
        if (value == null)
        {
            emitter.Emit(new Scalar(null, null, string.Empty, ScalarStyle.Plain, true, false));
            return;
        }

        // nested dictionaries
        var valueType = value.GetType();
        if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
        {
            WriteYaml(emitter, value, valueType, serializer);
        }
        // sequences/lists
        else if (value is IEnumerable enumerable && value is not string)
        {
            WriteSequence(emitter, enumerable, serializer);
        }
        // all others
        else
        {
            serializer(value, valueType);
        }
    }

    private void WriteSequence(IEmitter emitter, IEnumerable enumerable, ObjectSerializer serializer)
    {
        // emit sequence start with modern parameters
        emitter.Emit(new SequenceStart(null, null, true, SequenceStyle.Any));
        foreach (var item in enumerable)
        {
            WriteValue(emitter, item, serializer);
        }
        emitter.Emit(new SequenceEnd());
    }
}
