using DocHub.Common.DTO;
using DocHub.Common.Enums;
using DocHub.Core.Entities;
using DocHub.Data.Abstracts;
using DocHub.Service.Abstracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Service
{
    public class DocumentService : IDocumentsService
    {
        public DocumentService(IDocumentRepository documentRepository)
        {
            DocumentRepository = documentRepository;
        }

        public IDocumentRepository DocumentRepository { get; }
        public ILogger Logger { get; }

        public void AddDocument(DocumentDto documentDto)
        {
            var documentDb = new DDocument();
            documentDb.Id = Guid.NewGuid();
            documentDb.Title = documentDto.Title;
            documentDb.DocumentType = documentDto.DocumentType.HasValue ? documentDto.DocumentType.Value : DocumentTypes.Text;
            documentDb.Description = documentDto.Description;
            documentDb.FilePath = documentDto.FilePath;
            documentDb.CreatedAt = DateTime.Now;
            documentDb.Tags = documentDto.Tags?.Select(i=>new Tag { Id=i.Id, Name=i.Name })?.ToList();

            DocumentRepository.Insert(documentDb);
           
        }

        public void DeleteDocument(Guid id)
        {
            DocumentRepository.SoftDelete(id);
        }

        public DocumentDto GetDocument(Guid id)
        {
            var documentDb=DocumentRepository.GetObjectById(id);
            var document = new DocumentDto();
            document.Id=documentDb.Id;
            document.Title = documentDb.Title;
            document.UpdatedAt = documentDb.UpdatedAt;
            document.DocumentType = documentDb.DocumentType;
            document.Description = documentDb.Description;
            document.FilePath = documentDb.FilePath;
            document.CreatedAt = documentDb.CreatedAt;  
            document.Tags=documentDb.Tags?.Select(i => new TagDto { Id = i.Id, Name = i.Name })?.ToList();
            return document;
        }

        public List<DocumentDto> GetDocuments()
        {
            var documents = DocumentRepository.GetList().Select(documentDb => new DocumentDto
            {
                Id=documentDb.Id,
                Title=documentDb.Title,
                UpdatedAt = documentDb.UpdatedAt,
                DocumentType = documentDb.DocumentType,
                Description = documentDb.Description,
                FilePath = documentDb.FilePath,
                CreatedAt = documentDb.CreatedAt,
                Tags = documentDb.Tags?.Select(i => new TagDto { Id = i.Id, Name = i.Name })?.ToList()
            }).ToList();

            return documents;
        }
    public void UpdateDocument(DocumentDto documentDto)
        {
            var documentDb = new DDocument();
            documentDb.Id = documentDto.Id;
            documentDb.Title=documentDto.Title;
            documentDb.UpdatedAt = documentDto.UpdatedAt;
            documentDb.DocumentType = documentDto.DocumentType.HasValue? documentDto.DocumentType.Value:DocumentTypes.Text;
            documentDb.Description = documentDto.Description;
            documentDb.FilePath = documentDto.FilePath;
            documentDb.CreatedAt = documentDto.CreatedAt;
            documentDb.Tags = documentDto.Tags?.Select(i => new Tag { Id = i.Id, Name = i.Name })?.ToList();
            DocumentRepository.Update(documentDb);
        }
    }
}
