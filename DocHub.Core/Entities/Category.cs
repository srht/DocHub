using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocHub.Core.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Category? Parent { get; set; }
        public List<Category>? SubCategories { get; set; }
        public List<DDocument>? Documents { get; set; }
        public bool IsDeleted { get; set; }
    }
}
