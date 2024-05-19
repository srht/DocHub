using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Common.DTO
{
    public class CategoryDto:IDataTransfer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<CategoryDto>? SubCategories { get; set; }
        public CategoryDto? Parent { get; set; }
    }
}
