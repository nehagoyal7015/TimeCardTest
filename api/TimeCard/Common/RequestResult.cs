using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;

namespace TimeCard.Common
{
    public class RequestResult<T>
    {
        public string messageType { get; set; }
        public bool Success { get; set; }
        public string message { get; set; }
        public T Data {get; set; }        
    }

    public class KeyValuePair
    {
        public int Key { get; set; }
        public bool Value { get; set; }
        public string Description { get; set; }
    }
}
