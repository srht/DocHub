using DocHub.Common.DTO;
using DocHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Service.Abstracts
{
    public interface IDocumentsService
    {
        List<DocumentDto> GetDocuments(string query);
        DocumentDto GetDocument(Guid id);
        void AddDocument(DocumentDto document);
        void UpdateDocument(DocumentDto document);
        void DeleteDocument(Guid id);
        
    }
}
