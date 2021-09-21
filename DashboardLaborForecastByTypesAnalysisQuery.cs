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
using Refit;
using MyfutureAPI.Abstractions;

namespace MyfutureAPI.Queries
{

    
    public class DashboardLaborForecastByTypesAnalysisQuery : IRequest<Webresponse<DashboardAnalysisForType>>
    {

        public DashboardInputExtendedForFilter input { get; set; }
        public class DashboardLaborForecastByTypesAnalsysHandler : IRequestHandler<DashboardLaborForecastByTypesAnalysisQuery, Webresponse<DashboardAnalysisForType>>
        {
            private readonly IDatabaseConnectionFactory _db;

            public DashboardLaborForecastByTypesAnalsysHandler(IDatabaseConnectionFactory db)
            {
                _db = db;
            }
            public async Task<Webresponse<DashboardAnalysisForType>> Handle(DashboardLaborForecastByTypesAnalysisQuery request, CancellationToken cancellationToken)
            {
                Webresponse<DashboardAnalysisForType> _result = new Webresponse<DashboardAnalysisForType>
                {
                    status = APIStatus.processing,
                    message = "Initiaing db request",
                    data = new DashboardAnalysisForType()
                };

                using (var conn = await _db.CreateConnectionAsync())
                {
                    _result.data.bysector = conn.Query<TypesbySector>("SP_Dashboard_Stats_Extended",
                        new
                        {
                            universityid = request.input.universityid,
                            uuid = request.input.uuid,
                            usertype = request.input.usertype,
                            stattype = 6, //6
                            typeid=request.input.typeid,
                            year=request.input.year

                        }, commandType: CommandType.StoredProcedure).ToList();


                    _result.data.bygender = conn.Query<TypesbyGender>("SP_Dashboard_Stats_Extended",
                       new
                       {
                           universityid = request.input.universityid,
                           uuid = request.input.uuid,
                           usertype = request.input.usertype,
                           stattype = 7, //7
                           typeid = request.input.typeid,
                           year = request.input.year
                       }, commandType: CommandType.StoredProcedure).ToList();

                    _result.data.bylocation = conn.Query<TypesByLocation>("SP_Dashboard_Stats_Extended",
                       new
                       {
                           universityid = request.input.universityid,
                           uuid = request.input.uuid,
                           usertype = request.input.usertype,
                           stattype = 8, //8
                           typeid = request.input.typeid,
                           year = request.input.year
                       }, commandType: CommandType.StoredProcedure).ToList();

                    _result.data.bymajor = conn.Query<TypesbyMajor>("SP_Dashboard_Stats_Extended",
                       new
                       {
                           universityid = request.input.universityid,
                           uuid = request.input.uuid,
                           usertype = request.input.usertype,
                           stattype = 9, //9
                           typeid = request.input.typeid,
                           year = request.input.year
                       }, commandType: CommandType.StoredProcedure).ToList();



                }


                if (_result.data != null)
                {
                    _result.status = APIStatus.success;
                    _result.message = "recordsfound";
                }

                return _result;

            }
        }
    }
}
