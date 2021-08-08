using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentAppDAL.Models;

namespace AssignmentAppDAL.Repositories
{
    public interface ISurveyRepository : IRepository<Survey>
    {
       
        Task<Survey> GetSurveyByIdAsync(int id);
      
    }
}
