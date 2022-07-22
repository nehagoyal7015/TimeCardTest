using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;
using TimeCard.Models;

namespace TimeCard.DataAcess
{
    public class DocsDataAccess : BaseDataAccess
    {
        public DocsDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {
        }

        public List<parentCatList> GetDocs()
        {
            
            var documents = (from doc in DataContext.Documents
                             join docCategory in DataContext.DocumentCategories on doc.CategoryId equals docCategory.CategoryId
                             join docParentCategory in DataContext.DocumentCategories on docCategory.CategoryParentId equals docParentCategory.CategoryId
                             select new 
                {
                    DocumentId = doc.DocumentId,
                    DomainId = doc.DomainId,
                    ClientId = doc.ClientId,
                    DocumentName = doc.DocumentName,
                    CategoryId = doc.CategoryId,
                    CategoryName = docCategory.CategoryName,
                    CategoryParentId = docCategory.CategoryParentId,
                    ParentCategoryName = docParentCategory.CategoryName,
                    ProjectId = doc.ProjectId,
                    UserId = doc.UserId,
                    Details = doc.Details,
                    Description = doc.Description,
                }).ToList();
            
            var dLists = new List<parentCatList>();
            foreach (var parentCat in documents.ToList().GroupBy(s => new { s.CategoryParentId, s.ParentCategoryName })){
                var cat = new parentCatList() {CategoryParentId = parentCat.Key.CategoryParentId, ParentCategoryName = parentCat.Key.ParentCategoryName};
                cat.subCategory = new List<subCatList>();
                foreach (var category in parentCat.ToList().GroupBy(s=> new {s.CategoryId, s.CategoryName} ))
                {
                    var subCat = new subCatList() {CategoryId = category.Key.CategoryId, CategoryName = category.Key.CategoryName};
                    subCat.DocLists = new List<docsList>();
                    foreach (var docmt in category.ToList())
                    {
                        subCat.DocLists.Add(new docsList
                        {
                            DocumentId = docmt.DocumentId,
                            DomainId = docmt.DomainId,
                            ClientId = docmt.ClientId,
                            DocumentName = docmt.DocumentName,
                            ProjectId = docmt.ProjectId,
                            UserId = docmt.UserId,
                            Details = docmt.Details,
                            Description = docmt.Description

                        });
                    } 
                    cat.subCategory.Add(subCat);
                }
                dLists.Add(cat);
            }
            
            return dLists;
        }

        public bool AddDocument(DocsDto doc)
        {
            var catego = (from docAllCat in DataContext.DocumentCategories where docAllCat.DomainId == doc.DomainId
                            select new {
                                catId = docAllCat.CategoryId,
                                catName = docAllCat.CategoryName
                            }).ToList();
            var value = false;
            foreach (var item in catego)
            {
                if (item.catName == doc.CategoryName)
                {
                    value = true;
                    var info = new Document();
                    
                    info.DomainId = doc.DomainId;
                    info.ClientId = doc.ClientId;
                    info.DocumentName = doc.DocumentName;
                    info.CategoryId = item.catId;
                    info.ProjectId = doc.ProjectId;
                    info.UserId = doc.UserId;
                    info.Details = doc.Details;
                    info.Description = doc.Description;
                    info.ModifiedBy = 1;
                    info.ModifiedOn = DateTime.Now;
                    info.CreatedBy = 1;
                    info.CreatedOn = DateTime.Now;

                        DataContext.Documents.Add(info);
                        if (DataContext.SaveChanges() == 1)
                            return true;
                        else
                            return false;
                }
            }

            if(value != true){
                var docCatInfo = new DocumentCategory();
                docCatInfo.CategoryName = doc.CategoryName;
                docCatInfo.CategoryParentId = doc.CategoryParentId;
                docCatInfo.DomainId = doc.DomainId;
                docCatInfo.ModifiedBy = 1;
                docCatInfo.ModifiedOn = DateTime.Now;
                docCatInfo.CreatedBy = 1;
                docCatInfo.CreatedOn = DateTime.Now;

                DataContext.DocumentCategories.Add(docCatInfo);
                DataContext.SaveChanges();

                var newCat = (from docCatNew in DataContext.DocumentCategories where docCatNew.CategoryName == doc.CategoryName
                                && docCatNew.DomainId == doc.DomainId
                                select new {
                                    CategoryId = docCatNew.CategoryId,
                                    CategoryName = docCatNew.CategoryName
                                }).FirstOrDefault();

                var info = new Document();
                    
                    info.DomainId = doc.DomainId;
                    info.ClientId = doc.ClientId;
                    info.DocumentName = doc.DocumentName;
                    info.CategoryId = newCat.CategoryId;
                    info.ProjectId = doc.ProjectId;
                    info.UserId = doc.UserId;
                    info.Details = doc.Details;
                    info.Description = doc.Description;
                    info.ModifiedBy = 1;
                    info.ModifiedOn = DateTime.Now;
                    info.CreatedBy = 1;
                    info.CreatedOn = DateTime.Now;

                        DataContext.Documents.Add(info);
                        if (DataContext.SaveChanges() == 1)
                            return true;
                        else
                            return false;
            }

            else
                throw new ArgumentException("Parameters provided are incorrect");
            
        }

        public List<parentCat> GetParent(int domainId, string catname)
        {
            if(catname == "Domain"){
            var parent = (from appDomain in DataContext.AppDomains where appDomain.DomainId == domainId
                            select new parentCat
                {
                    DomainId = appDomain.DomainId,
                    Id = appDomain.DomainId,
                    Name = appDomain.DomainName
                }).ToList();

            return parent;
            }

            if(catname == "Client"){
                var parent = (from clients in DataContext.Clients where clients.DomainId == domainId
                                select new parentCat
                {
                    DomainId = clients.DomainId,
                    Id = clients.ClientId,
                    Name = clients.ClientName
                }).ToList();

            return parent;
            }

            if(catname == "Project") {
                var parent = (from proj in DataContext.Projects where proj.DomainId == domainId
                                select new parentCat
                {
                    DomainId = proj.DomainId,
                    Id = proj.ProjectId,
                    Name = proj.Name
                }).ToList();

            return parent;
            }

            else
                throw new ArgumentException("Parameter not found");
        }
    }
}