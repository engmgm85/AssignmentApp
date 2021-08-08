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
   public class HearSourceService : IHearSourceService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HearSourceService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<HearSource>> GetAll()
        {
            var result = await _unitOfWork.HearSources
                .GetAllAsync();
         return   result.OrderBy(c=>c.ChoiceOrder);
        }

        public async Task<HearSource> GetHearSourceById(int id)
        {
            return await _unitOfWork.HearSources
                .GetByIdAsync(id);
        }
        public async Task<HearSource> CreateHearSource(HearSource newHearSource)
    {
        await _unitOfWork.HearSources.AddAsync(newHearSource);
        await _unitOfWork.CommitAsync();
        return newHearSource;
    }

    public async Task DeleteHearSource(HearSource hearSource)
    {
        _unitOfWork.HearSources.Remove(hearSource);
        await _unitOfWork.CommitAsync();
    }

  

  

   

    public async Task UpdateHearSource(HearSource hearSourceToBeUpdated, HearSource hearSource)
    {
            hearSourceToBeUpdated.ChoiceOrder = hearSource.ChoiceOrder;
            hearSourceToBeUpdated.Id = hearSource.Id;
            hearSourceToBeUpdated.Title = hearSource.Title;
            
            await _unitOfWork.CommitAsync();
    }
    }
}
