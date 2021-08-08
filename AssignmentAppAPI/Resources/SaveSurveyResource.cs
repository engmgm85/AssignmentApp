using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentAppDAL.Models;

namespace AssignmentAppAPI.Resources
{
    public class SaveSurveyResource
    {
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public int NumberOfKids { get; set; }
        public List<SurveyHearSourceResource> SurveyResources{ get;set;}

        public SaveSurveyResource ()
        {
            SurveyResources = new List<SurveyHearSourceResource>();

        }
    }
}
