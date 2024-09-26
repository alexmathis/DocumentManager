using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManager.Application.Documents.Commands.DeleteDocument;

public class DeleteDocumentCommand : IRequest<Unit>  // Unit represents a void type
{
    public int Id { get; set; }
}