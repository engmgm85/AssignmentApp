using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentAppDAL.Repositories;

namespace AssignmentAppDAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AssignmentAppDataContext _context;
        private SurveyRepository _surveyRepository;
        private HearSourceRepository _hearSourceRepository;
        private SurveyHearSourceRepository _surveyHearSourceRepository;
        public UnitOfWork(AssignmentAppDataContext context)
        {
            this._context = context;
        }
        public ISurveyRepository Surveys => _surveyRepository = _surveyRepository ?? new SurveyRepository(_context);
        public IHearSourceRepository HearSources => _hearSourceRepository = _hearSourceRepository ?? new HearSourceRepository(_context);
        public ISurveyHearSourceRepository SurveyHears => _surveyHearSourceRepository = _surveyHearSourceRepository ?? new SurveyHearSourceRepository(_context);
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
   

    
}
