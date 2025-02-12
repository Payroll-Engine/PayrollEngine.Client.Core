using System;
using System.IO;

namespace PayrollEngine.Client.Exchange;

/// <summary>Binary file</summary>
public static class BinaryFile
{
    /// <summary>Reads a binary file as encoded string</summary>
    /// <param name="fileName">Name of the file</param>
    /// <returns>The file content as base 64 string</returns>
    public static string Read(string fileName)
    {
        if (string.IsNullOrWhiteSpace(fileName))
        {
            throw new ArgumentException(nameof(fileName));
        }
        if (!File.Exists(fileName))
        {
            throw new PayrollException($"Missing file {new FileInfo(fileName).FullName}.");
        }

        using var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using var reader = new BinaryReader(stream);
        var bytes = reader.ReadBytes((int)stream.Length);
        return Convert.ToBase64String(bytes);
    }
}