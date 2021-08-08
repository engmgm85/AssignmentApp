using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentAppAPI.Resources
{
    public class SurveyResource
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public int NumberOfKids { get; set; }

        public virtual ICollection<SurveyHearSourceResource> SurveyHearSources { get; set; }

        public SurveyResource()
        {
            SurveyHearSources = new HashSet<SurveyHearSourceResource>();
        }

    }
}
