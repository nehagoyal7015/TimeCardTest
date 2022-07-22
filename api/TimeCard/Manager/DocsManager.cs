using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Manager
{
    public class DocsManager : BaseManager, IDocs
    {
        private readonly DocsDataAccess _docsDataAccess;
        public DocsManager(DocsDataAccess docsDataAccess)
        {
            _docsDataAccess = docsDataAccess;
        }

        public List<parentCatList> GetDocs()
        {
            return _docsDataAccess.GetDocs();
        }

        public bool AddDocument(DocsDto docInfo)
        {
            return _docsDataAccess.AddDocument(docInfo);
        }

        public List<parentCat> GetParent(int domainId, string catname)
        {
            return _docsDataAccess.GetParent(domainId,catname);
        }
    }
}