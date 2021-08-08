using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentAppDAL.Models;

namespace AssignmentAppDAL.Services
{
    public interface ISurveyService
    {
        Task<IEnumerable<Survey>> GetAll();
        Task<Survey> GetSurveyById(int id);
      
        Task<Survey> CreateSurvey(Survey newSurvey);

        Task<List<SurveyHearSource>> SaveSurveyHear(List<SurveyHearSource> SurveyHearSource);
        Task UpdateSurvey(Survey surveyToBeUpdated, Survey survey);
        Task DeleteSurvey(Survey survey);
    }
}
