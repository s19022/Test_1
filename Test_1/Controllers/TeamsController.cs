using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_1.Exceptions;
using Test_1.Resources;

namespace Test_1.Controllers
{
    [Route("api/teams")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        public readonly ITeamsDbService service;

        public TeamsController(ITeamsDbService _service)
        {
            service = _service;
        }

        [HttpGet("{id}")]
        public IActionResult getTeamMember(int id)
        {
            try
            {
                var teamMember = service.GetTeamMember(id);
                return Ok(teamMember);
            }
            catch (NoSuchTeamMemberException ex)
            {
                return BadRequest("No member with id: " + id);
            }
        }
    }
}