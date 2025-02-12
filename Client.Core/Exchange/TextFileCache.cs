using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PayrollEngine.Client.Exchange;

/// <summary>Cache for text files</summary>
public class TextFileCache
{
    // shared text files by file name
    private readonly Dictionary<string, string> sharedTextFiles = new();

    /// <summary>
    /// Reads a text file as string
    /// </summary>
    /// <param name="fileName">Name of the file</param>
    /// <returns>The file content as string</returns>
    public string ReadTextFile(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException(nameof(fileName));
        }
        if (!File.Exists(fileName))
        {
            throw new PayrollException($"Missing file {new FileInfo(fileName).FullName}.");
        }

        // use cache
        if (sharedTextFiles.TryGetValue(fileName, out var file))
        {
            return file;
        }

        // file read
        var builder = new StringBuilder();
        foreach (var line in File.ReadAllLines(fileName))
        {
            builder.AppendLine(line);
        }
        var text = builder.ToString();

        // update cache
        sharedTextFiles[fileName] = text;

        return text;
    }

}