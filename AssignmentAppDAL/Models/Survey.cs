using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentAppDAL.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public int NumberOfKids { get; set; }

        public virtual ICollection<SurveyHearSource> SurveyHearSources { get; set; }

        public Survey()
        {
            SurveyHearSources = new HashSet<SurveyHearSource>();
        }

    }
}
