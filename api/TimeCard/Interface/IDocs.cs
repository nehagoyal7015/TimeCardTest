using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;

namespace TimeCard.Interface
{
    public interface IDocs
    {
        List<parentCatList> GetDocs();

        bool AddDocument(DocsDto docInfo);
        List<parentCat> GetParent(int domainId, string catname);
    }
}