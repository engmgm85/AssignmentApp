using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentAppDAL.Models;

namespace AssignmentAppDAL.Services
{
    public interface IHearSourceService
    {
        Task<IEnumerable<HearSource>> GetAll();
        Task<HearSource> GetHearSourceById(int id);
      
        Task<HearSource> CreateHearSource(HearSource newHearSource);
        Task UpdateHearSource(HearSource hearSourceToBeUpdated, HearSource hearSource);
        Task DeleteHearSource(HearSource hearSource);
    }
}
