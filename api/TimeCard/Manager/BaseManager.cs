using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;
using TimeCard.Models;

namespace TimeCard.Manager
{
    public class BaseManager
    {
        protected readonly TimeCardAppContext DataContext;
        protected readonly BaseDataAccess _baseDataAccess;

       
        protected BaseManager()
        {
            if (DataContext == null)
            {
                DataContext = new TimeCardAppContext();
            }
            _baseDataAccess = new BaseDataAccess(DataContext);
        }

        protected BaseManager(TimeCardAppContext dataContext)
        {
            DataContext = dataContext;
            _baseDataAccess = new BaseDataAccess(DataContext);
        }
    }
}

