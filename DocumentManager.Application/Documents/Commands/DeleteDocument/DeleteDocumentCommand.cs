using DocumentManager.Application.Abstractions.Messaging;


namespace DocumentManager.Application.Documents.Commands.DeleteDocument;

public sealed record DeleteDocumentCommand(int Id, int DeletingUserId) : ICommand<Unit>;
