using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentAppDAL.Models;
using AssignmentAppDAL.Services;
using AssignmentAppDAL.Repositories;
using AssignmentAppDAL;

namespace AssignmentAppDAL.Services
{
   public class SurveyService : ISurveyService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SurveyService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Survey>> GetAll()
        {
            return await _unitOfWork.Surveys
                .GetAllAsync();
        }
        public async Task<Survey> CreateSurvey(Survey newSurvey)
    {
        await _unitOfWork.Surveys.AddAsync(newSurvey);
            
        await _unitOfWork.CommitAsync();
        return newSurvey;
    }

        public async Task<List<SurveyHearSource>> SaveSurveyHear(List<SurveyHearSource> newSurveyHearSource)
        {
            await _unitOfWork.SurveyHears.AddRangeAsync(newSurveyHearSource);

            await _unitOfWork.CommitAsync();
            return newSurveyHearSource;
        }

        public async Task DeleteSurvey(Survey survey)
    {
        _unitOfWork.Surveys.Remove(survey);
        await _unitOfWork.CommitAsync();
    }

  

    public async Task<Survey> GetSurveyById(int id)
    {
        return await _unitOfWork.Surveys
            .GetSurveyByIdAsync(id);
    }

   

    public async Task UpdateSurvey(Survey surveyToBeUpdated, Survey survey)
    {
            surveyToBeUpdated.FullName = survey.FullName;
            surveyToBeUpdated.Birthdate = survey.Birthdate;
            surveyToBeUpdated.Gender = survey.Gender;
            surveyToBeUpdated.NumberOfKids = survey.NumberOfKids;
            await _unitOfWork.CommitAsync();
    }
    }
}
