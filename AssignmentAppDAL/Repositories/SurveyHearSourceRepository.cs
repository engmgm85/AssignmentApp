using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentAppDAL.Models;

using Microsoft.EntityFrameworkCore;


namespace AssignmentAppDAL.Repositories
{
    public class SurveyHearSourceRepository : Repository<SurveyHearSource>,ISurveyHearSourceRepository
    {
        public SurveyHearSourceRepository(AssignmentAppDataContext context)
        : base(context)
        { }
        private AssignmentAppDataContext AssignmentAppDataContext
        {
            get { return Context as AssignmentAppDataContext; }
        }

      
    }
}
