using System.Collections.Generic;
using DocumentManager.Domain.Exceptions.Base;


namespace DocumentManager.Application.Exceptions;

public sealed class ValidationException : BadRequestException
{
    public ValidationException(Dictionary<string, string[]> errors)
        : base("Validation errors occurred") =>
        Errors = errors;

    public Dictionary<string, string[]> Errors { get; }
}
