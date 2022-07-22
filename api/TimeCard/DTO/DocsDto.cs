using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO
{
    public class DocsDto
    {
        public int? DomainId { get; set; }
        public int? ClientId { get; set; }
        public string DocumentName { get; set; }
        public int? CategoryId { get; set; }
        public int? CategoryParentId { get; set; }
        
        public string CategoryName { get; set; }
        public int? ProjectId { get; set; }
        public int? UserId { get; set; }
        public string Details { get; set; }
        public string Description { get; set; }
        
    }

    public class parentCatList{
        public int? CategoryParentId { get; set; }
        public string ParentCategoryName { get; set; }
        public List<subCatList> subCategory { get; set; }

    }

    public class subCatList{
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<docsList> DocLists { get; set; }

    }

    public class docsList{
        public int DocumentId { get; set; }
        public int? DomainId { get; set; }
        public int? ClientId { get; set; }
        public string DocumentName { get; set; }
        public int? ProjectId { get; set; }
        public int? UserId { get; set; }
        public string Details { get; set; }
        public string Description { get; set; }
    }

    public class parentCat {
        public int DomainId { get; set; }
        public string? Name { get; set; }
        public int? Id { get; set; }
        
    }

    
}