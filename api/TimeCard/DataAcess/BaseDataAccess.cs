using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.Models;

namespace TimeCard.DataAcess
{
    public class BaseDataAccess
    {
        public TimeCardAppContext DataContext;

        public BaseDataAccess(TimeCardAppContext context)
        {
            DataContext = context;
        }

        
        public void Commit()
        {
            DataContext.SaveChanges();
        }
    }
}

