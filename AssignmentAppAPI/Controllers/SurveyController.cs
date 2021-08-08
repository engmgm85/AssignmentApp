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
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyService _surveyService;
        private readonly IMapper _mapper;

        public SurveyController(ISurveyService surveyService, IMapper mapper)
        {
            this._mapper = mapper;
            this._surveyService = surveyService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<SurveyResource>>> GetAllSurveys()
        {
            var surveys = await _surveyService.GetAll();
           
             var surveyResources = _mapper.Map<IEnumerable<Survey>, IEnumerable<SurveyResource>>(surveys);
            
            return Ok(surveyResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SurveyResource>> GetSurveyById(int id)
        {
            var survey = await _surveyService.GetSurveyById(id);
            var surveyResource = _mapper.Map<Survey, SurveyResource>(survey);

            return Ok(surveyResource);
        }

        [HttpPost("")]
        public async Task<ActionResult<SurveyResource>> CreateSurvey([FromBody] SaveSurveyResource saveSurveyResource)
        {
            var validator = new SaveSurveyResourceValidator();
            var validationResult = await validator.ValidateAsync(saveSurveyResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); 

            var surveyToCreate = _mapper.Map<SaveSurveyResource, Survey>(saveSurveyResource);

            var newSurvey = await _surveyService.CreateSurvey(surveyToCreate);
            
            
           
            saveSurveyResource.SurveyResources.ForEach(c => c.SurveyId = newSurvey.Id);
            List<SurveyHearSource> surveyshearsourceList = new List<SurveyHearSource>();
            saveSurveyResource.SurveyResources.ForEach(c => surveyshearsourceList.Add(new SurveyHearSource() { HearSourceId = c.HearSourceId, SurveyId = c.SurveyId }));
            var SurveyCommit = await _surveyService.SaveSurveyHear(surveyshearsourceList);
            var survey = await _surveyService.GetSurveyById(newSurvey.Id);

            var surveyResource = _mapper.Map<Survey, SurveyResource>(survey);

            return Ok(surveyResource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SurveyResource>> UpdateSurvey(int id, [FromBody] SaveSurveyResource saveSurveyResource)
        {
            var validator = new SaveSurveyResourceValidator();
            var validationResult = await validator.ValidateAsync(saveSurveyResource);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var surveyToBeUpdate = await _surveyService.GetSurveyById(id);

            if (surveyToBeUpdate == null)
                return NotFound();

            var survey = _mapper.Map<SaveSurveyResource, Survey>(saveSurveyResource);

            await _surveyService.UpdateSurvey(surveyToBeUpdate, survey);

            var updatedSurvey = await _surveyService.GetSurveyById(id);
            var updatedSurveyResource = _mapper.Map<Survey, SurveyResource>(updatedSurvey);

            return Ok(updatedSurveyResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurvey(int id)
        {
            if (id == 0)
                return BadRequest();

            var survey = await _surveyService.GetSurveyById(id);

            if (survey == null)
                return NotFound();

            await _surveyService.DeleteSurvey(survey);

            return NoContent();
        }
    }
}
