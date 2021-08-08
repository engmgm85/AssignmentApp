using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AssignmentAppDAL.Models;
using AssignmentAppDAL;

namespace AssignmentAppDAL.Repositories
{
    public class SurveyRepository : Repository<Survey>,ISurveyRepository
    {

        public SurveyRepository(AssignmentAppDataContext context)
          : base(context)
        { }
        private AssignmentAppDataContext AssignmentAppDataContext
        {
            get { return Context as AssignmentAppDataContext; }
        }

        public async Task<Survey> GetSurveyByIdAsync(int id)
        {
            return await AssignmentAppDataContext.Surveys
                              .SingleOrDefaultAsync(m => m.Id == id); ;
        }
    }
}
