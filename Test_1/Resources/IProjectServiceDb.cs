using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_1.DTO.Request;

namespace Test_1.Resources
{
    public interface IProjectServiceDb
    {
        public void DeleteProject(projectDeleteRequest request);
    }
}
