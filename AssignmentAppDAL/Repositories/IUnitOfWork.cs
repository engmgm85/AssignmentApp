using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentAppDAL.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ISurveyRepository Surveys { get; }
        IHearSourceRepository HearSources { get; }
        ISurveyHearSourceRepository SurveyHears { get; }
        Task<int> CommitAsync();
    }
}
