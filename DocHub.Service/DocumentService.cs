using AutoMapper;
using DocHub.Common.DTO;
using DocHub.Common.Enums;
using DocHub.Core.Entities;
using DocHub.Data.Abstracts;
using DocHub.Data.Migrations;
using DocHub.Service.Abstracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DocHub.Service
{
    public class DocumentService : IDocumentsService
    {
        public DocumentService(IDocumentRepository documentRepository, ICategoriesRepository categoriesRepository, ITagsRepository tagsRepository, IMapper mapper)
        {
            DocumentRepository = documentRepository;
            CategoriesRepository = categoriesRepository;
            TagsRepository = tagsRepository;
            Mapper = mapper;
        }

        public IDocumentRepository DocumentRepository { get; }
        public ICategoriesRepository CategoriesRepository { get; }
        public ITagsRepository TagsRepository { get; }
        public IMapper Mapper { get; }
        public ILogger Logger { get; }

        public async Task AddDocumentAsync(DocumentDto documentDto)
        {
            var documentDb = Mapper.Map<DDocument>(documentDto);
            documentDb.DocumentType = documentDto.DocumentType.HasValue ? documentDto.DocumentType.Value : DocumentTypes.Text;
            documentDb.CreatedAt = DateTime.Now;
            documentDb.Categories = null;
            documentDb.Tags = null;

            if (documentDto.FilePaths.Any())
            {
                    documentDb.FilePaths = new List<DDocFilePath>();

                var files = documentDto.FilePaths.Select(i => new DDocFilePath { FilePath = i.FilePath });
                documentDb.FilePaths.AddRange(files);
            }

            if (documentDto.Tags != null && documentDto.Tags.Any())
            {
                documentDb.Tags = new List<Tag>();
                var broughtTags = documentDto.Tags;
                foreach (var c in broughtTags)
                {
                    var tagDb = await TagsRepository.GetObjectByIntIdAsync(c.Id);
                    if (tagDb != null)
                        documentDb.Tags.Add(tagDb);
                }
            }

            if (documentDto.Categories != null && documentDto.Categories.Any())
            {

                documentDb.Categories = new List<Category>();
                var broughtCategories = documentDto.Categories;
                foreach (var c in broughtCategories)
                {
                    var categoryDb = await CategoriesRepository.GetObjectByIntIdAsync(c.Id);

                    if (categoryDb != null)
                        documentDb.Categories.Add(categoryDb);
                }
            }

            await DocumentRepository.InsertAsync(documentDb);
            
           
        }

        public async Task UpdateDocumentAsync(DocumentDto documentDto)
        {
            if (documentDto == null || documentDto.Id == null)
                throw new Exception("No document passed to update");

            var documentDb = await DocumentRepository.GetDocumentByIdAsync(documentDto.Id.Value);
            if (documentDb == null)
                throw new Exception("Document not found with document id: " + documentDto.Id);

            if (documentDb.Categories == null)
                documentDb.Categories = new List<Category>();
            if (!string.IsNullOrEmpty(documentDto.Title))
                documentDb.Title = documentDto.Title;
            documentDb.UpdatedAt = DateTime.Now;
            documentDb.DocumentType = documentDto.DocumentType.HasValue ? documentDto.DocumentType.Value : DocumentTypes.Text;
            if (!string.IsNullOrEmpty(documentDto.Description))
                documentDb.Description = documentDto.Description;
            if (documentDto.FilePaths.Any())
            {
                documentDb.FilePaths = new List<DDocFilePath>();

                var files = documentDto.FilePaths.Select(i => new DDocFilePath { FilePath = i.FilePath });
                documentDb.FilePaths.AddRange(files);
            }

            if (documentDto.Tags != null && documentDto.Tags.Any())
            {
                documentDb.Tags = new List<Tag>();
                var broughtTags = documentDto.Tags;
                foreach (var c in broughtTags)
                {
                    var tagDb = await TagsRepository.GetObjectByNameAsync(c.Name);
                    if (tagDb == null)
                        tagDb = new Tag { Name = c.Name };
                        documentDb.Tags.Add(tagDb);
                }
            }

            if (documentDto.Categories != null && documentDto.Categories.Any())
            {

                documentDb.Categories = new List<Category>();
                var broughtCategories = documentDto.Categories;
                foreach (var c in broughtCategories)
                {
                    var categoryDb = await CategoriesRepository.GetObjectByIntIdAsync(c.Id);

                    if (categoryDb != null)
                        documentDb.Categories.Add(categoryDb);
                }
            }

            await DocumentRepository.UpdateAsync(documentDb);
        }

        public async Task DeleteDocumentAsync(Guid id)
        {
            await DocumentRepository.SoftDeleteAsync(id);
        }

        public async Task<DocumentDto> GetDocumentAsync(Guid id)
        {
            var documentDb=DocumentRepository.GetDocumentById(id);
            var document = Mapper.Map<DocumentDto>(documentDb);
            return document;
        }

        public async Task<List<DocumentDto>> GetDocumentsAsync(string query="")
        {
            query = query.ToLower();
            var docs = DocumentRepository.QueryList("Tags", "Categories");
            /*
            var documents = DocumentRepository.QueryList("Tags","Categories")
                .Where(i=>i.Title.ToLower().Contains(query)|| i.Tags.Any(t => t.Name.ToLower().Contains(query)))
                .Where(i=>!i.IsDeleted).Select(documentDb =>Mapper.Map<DocumentDto>(documentDb)).ToList();
            */
            var documents=docs.Select(documentDb => Mapper.Map<DocumentDto>(documentDb)).ToList();
            return documents;
        }

        public List<DocumentDto> GetDocumentsByCategory(int categoryId)
        {
            var docList = DocumentRepository.GetList("Tags", "Categories");
            var documents = docList
                .Where(i => i.Categories.Any(c => c.Id == categoryId)).Where(i => !i.IsDeleted)
                //.Select(documentDb => Mapper.Map<DocumentDto>(documentDb)).ToList();
                .ToList();
            var mappedDocs = documents.Select(d => Mapper.Map<DocumentDto>(d)).ToList();

            return mappedDocs;
        }

      
    }
}
