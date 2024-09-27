using DocumentManager.Domain.Exceptions.Base;


namespace DocumentManager.Domain.Exceptions;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(int documentId)
        : base($"The user with the identifier {documentId} was not found.")
    {
    }
}






