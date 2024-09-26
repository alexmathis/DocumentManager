using System;

namespace DocumentManager.Domain.Exceptions.Base;

public abstract class BadRequestException : Exception
{
    protected BadRequestException(string message)
        : base(message)
    {
    }
}