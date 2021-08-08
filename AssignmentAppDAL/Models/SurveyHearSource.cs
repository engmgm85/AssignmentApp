using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentAppDAL.Models
{
   public class SurveyHearSource
    {
        public int SurveyId { get; set; }
        public int HearSourceId { get; set; }

        public virtual HearSource HearSource { get; set; }
        public virtual Survey Survey { get; set; }
    }
    
}
