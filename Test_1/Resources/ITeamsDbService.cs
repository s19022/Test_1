using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_1.DTO.Response;

namespace Test_1.Resources
{
    public interface ITeamsDbService
    {
        public List<GetTaskResponse> GetAssignedTasks(int id);


        public List<GetTaskResponse> GetCreatedTasks(int id);

        public GetTeamMemberResponse GetTeamMember(int id);
    }
}
