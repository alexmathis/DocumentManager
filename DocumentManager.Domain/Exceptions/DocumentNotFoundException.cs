using System;
using DocumentManager.Domain.Exceptions.Base;

namespace DocumentManager.Domain.Exceptions;

public sealed class DocumentNotFoundException : NotFoundException
{
    public DocumentNotFoundException(int documentId)
        : base($"The document with the identifier {documentId} was not found.")
    {
    }
}