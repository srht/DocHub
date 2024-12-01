using DocHub.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DocHub.Common.DTO
{
    public class DocumentDto:IDataTransfer
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<DDocFilePathDto> FilePaths { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public DocumentTypes? DocumentType { get; set; } = DocumentTypes.Text;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<TagDto>? Tags { get; set; }
        public List<CategoryDto>? Categories { get; set; }
    }

    public class DDocFilePathDto
    {
        public string FilePath { get; set; }
    }
}
