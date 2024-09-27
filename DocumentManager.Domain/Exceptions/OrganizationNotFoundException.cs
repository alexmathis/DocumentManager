using DocumentManager.Domain.Exceptions.Base;


namespace DocumentManager.Domain.Exceptions;

public sealed class OrganizationNotFoundException : NotFoundException
{
    public OrganizationNotFoundException(int documentId)
        : base($"The organization with the identifier {documentId} was not found.")
    {
    }
}