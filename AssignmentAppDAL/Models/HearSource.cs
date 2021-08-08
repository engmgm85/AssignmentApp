using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentAppDAL.Models
{
   public  class HearSource
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
        public int ChoiceOrder { get; set; }
        public virtual ICollection<SurveyHearSource> SurveyHearSources { get; set; } 
        
        public HearSource()
        {
            SurveyHearSources = new HashSet<SurveyHearSource>();
        }

    }
}
