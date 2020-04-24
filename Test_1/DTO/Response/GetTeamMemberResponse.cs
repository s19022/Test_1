using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_1.DTO.Response
{
    public class GetTeamMemberResponse
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }

        public List<GetTaskResponse> assigned { get; set; }

        public List<GetTaskResponse> created { get; set; }

    }
}
