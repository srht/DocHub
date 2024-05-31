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
        Task<List<DocumentDto>> GetDocumentsAsync(string query);
        Task<List<DocumentDto>> GetDocumentsByCategoryAsync(int categoryId);
        Task<DocumentDto> GetDocumentAsync(Guid id);
        Task AddDocumentAsync(DocumentDto document);
        Task UpdateDocumentAsync(DocumentDto document);
        Task DeleteDocumentAsync(Guid id);
        
    }
}
