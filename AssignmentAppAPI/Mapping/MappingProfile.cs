using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AssignmentAppAPI.Resources;
using AssignmentAppDAL.Models;

namespace AssignmentAppAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Survey, SurveyResource>();
            CreateMap<HearSource, HeareSourceRssource>();
            CreateMap<SurveyHearSource, SurveyHearSourceResource> ();

            // Resource to Domain
            CreateMap<SurveyResource, Survey>();
            CreateMap<SaveSurveyResource, Survey>();
            CreateMap< SurveyHearSourceResource, SurveyHearSource>();
            CreateMap<SaveSurveyHearSourceResource, SurveyHearSource>();
            CreateMap<SaveHearSource, HearSource>();
            CreateMap<HeareSourceRssource, HearSource>();
        }
    }
}
