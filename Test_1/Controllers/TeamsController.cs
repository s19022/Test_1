using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_1.DTO.Request;
using Test_1.Exceptions;
using Test_1.Resources;

namespace Test_1.Controllers
{
    [Route("api/teams")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        public readonly ITeamsDbService teamService;

        public readonly IProjectServiceDb projectService;

        public TeamsController(ITeamsDbService _service, IProjectServiceDb _projectService)
        {
            teamService = _service;
            projectService = _projectService;

        }

        [HttpGet("{id}")]
        public IActionResult getTeamMember(int id)
        {
            try
            {
                var teamMember = teamService.GetTeamMember(id);
                return Ok(teamMember);
            }
            catch (NoSuchTeamMemberException ex)
            {
                return BadRequest("No member with id: " + id);
            }
        }

        [HttpDelete("{projectId}")]
        public IActionResult DeleteProject(int projectId)
        {
            try
            {
                projectService.DeleteProject(new ProjectDeleteRequest{id = projectId});
            }catch(NoSuchProjectException ex)
            {
                return BadRequest("No such project");
            }
            return Ok();
        }
    }
}