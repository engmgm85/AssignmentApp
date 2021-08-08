using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AssignmentAppDAL.Services;
using AssignmentAppAPI.Resources;
using AssignmentAppAPI.Validations;
using AssignmentAppDAL.Models;

namespace AssignmentAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HearSourceController : ControllerBase
    {
        private readonly IHearSourceService _hearSourceService;
        private readonly IMapper _mapper;

        public HearSourceController(IHearSourceService hearSourceService, IMapper mapper)
        {
            this._mapper = mapper;
            this._hearSourceService = hearSourceService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<HeareSourceRssource>>> GetAllHearSource()
        {
            var HearSources = await _hearSourceService.GetAll();
            var HearSourceResources = _mapper.Map<IEnumerable<HearSource>, IEnumerable<HeareSourceRssource>>(HearSources);

            return Ok(HearSourceResources);
        }

      
    }
}
