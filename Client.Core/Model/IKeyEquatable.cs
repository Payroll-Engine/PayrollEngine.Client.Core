using System;

namespace PayrollEngine.Client.Model
{
    /// <summary>Model equitable</summary>
    /// <typeparam name="T"></typeparam>
    public interface IKeyEquatable<T> : IEquatable<T>
    {
        /// <summary>Test for the same item key</summary>
        bool EqualKey(T compare);
    }
}
