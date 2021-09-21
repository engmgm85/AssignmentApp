using MediatR;
using MyfutureModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyfutureData;
using System.Threading;
using System.Data;
using System.ComponentModel.DataAnnotations;


namespace MyfutureAPI.Queries
{

    public class VacancydetailQuery : IRequest<Webresponse<Vacancydetail>>
    {
        public Vacancydetailfilter filter { get; set; }

        public class VacancydetailQueryHandler : IRequestHandler<VacancydetailQuery, Webresponse<Vacancydetail>>
        {
            private readonly IDatabaseConnectionFactory _db;

            public VacancydetailQueryHandler(IDatabaseConnectionFactory db)
            {
                _db = db;
            }
            public async Task<Webresponse<Vacancydetail>> Handle(VacancydetailQuery request, CancellationToken cancellationToken)
            {
                Webresponse<Vacancydetail> _result = new Webresponse<Vacancydetail>
                {
                    status = APIStatus.processing,
                    message = "Initiaing db request",
                    data = null
                };

                using (var conn = await _db.CreateConnectionAsync())
                {
                    _result.data = conn.Query<Vacancydetail>("SP_GetMain_OpportunityDetail_Extended",
                        new
                        {
                            universityid = request.filter.universityid,
                            jobuuid = request.filter.jobuuid,
                            uuid = request.filter.uuid,
                            usertype = request.filter.usertype
                        }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (_result.data != null)
                    {
                        _result.status = APIStatus.success;
                        _result.message = "recordfound";

                        _result.data.educations = conn.Query<Lookup>("SP_Get_JobOpportunityEducations",
                            new
                            {
                                rid = _result.data.jobid
                            },
                            commandType: CommandType.StoredProcedure).ToList();

                        _result.data.skills = conn.Query<Lookup>("SP_Get_JobOpportunitySkills",
                            new
                            {
                                rid = _result.data.jobid
                            },
                            commandType: CommandType.StoredProcedure).ToList();

                        _result.data.competences = conn.Query<Lookup>("SP_Get_JobOpportunityAbilities",
                           new
                           {
                               rid = _result.data.jobid
                           },
                           commandType: CommandType.StoredProcedure).ToList();


                    }
                }


                return _result;

            }
        }

    }
}
