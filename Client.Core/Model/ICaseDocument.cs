using System;

namespace PayrollEngine.Client.Model;

/// <summary>The case document client object</summary>
public interface ICaseDocument : IModel, IEquatable<ICaseDocument>
{
    /// <summary>The document name</summary>
    string Name { get; set; }

    /// <summary>The document content</summary>
    string Content { get; set; }

    /// <summary>The document file name</summary>
    string ContentFile { get; set; }

    /// <summary>The document content type</summary>
    string ContentType { get; set; }
}