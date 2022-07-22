using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.DTO

{
    public class ApprovalDto
    {
        public string Username { get; set; }

        public int TotalSubmitted { get; set; }
        public bool Approved { get; set; }

        public int TotalRejected { get; set; }

    }
        public class ApprovalInnerData
        {
            public string Client { get; set; }

            public string Project { get; set; }
            public DateTime Date { get; set; }

            public float BillHours { get; set; }
            public float NonBillHours { get; set; }

        }

    }

