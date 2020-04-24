using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_1.DTO.Response
{
    public class GetTaskResponse
    {
        public string name { get; set; }

        public string description { get; set; }

        public DateTime deadline { get; set; }

        public string projectName { get; set; }
    }
}
