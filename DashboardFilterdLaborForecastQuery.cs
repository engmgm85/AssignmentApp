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

    public class DashboardFilterdLaborForecastQuery : IRequest<Webresponse<List<DashboardFilterLaborMarket>>>
    {

        public DashboardInput input { get; set; }
        public class DashboardFilterdLaborForecastQueryHandler : IRequestHandler<DashboardFilterdLaborForecastQuery, Webresponse<List<DashboardFilterLaborMarket>>>
        {
            private readonly IDatabaseConnectionFactory _db;

            public DashboardFilterdLaborForecastQueryHandler(IDatabaseConnectionFactory db)
            {
                _db = db;
            }
            public async Task<Webresponse<List<DashboardFilterLaborMarket>>> Handle(DashboardFilterdLaborForecastQuery request, CancellationToken cancellationToken)
            {
                Webresponse<List<DashboardFilterLaborMarket>> _result = new Webresponse<List<DashboardFilterLaborMarket>>
                {
                    status = APIStatus.processing,
                    message = "Initiaing db request",
                    data = null
                };

                using var conn = await _db.CreateConnectionAsync();
                _result.data = conn.Query<DashboardFilterLaborMarket>("SP_Dashboard_Stats_Extended",
                        new
                        {
                            universityid = request.input.universityid,
                            uuid = request.input.uuid,
                            usertype = request.input.usertype,
                            stattype = request.input.stattype //4   
                        }, commandType: CommandType.StoredProcedure).ToList();


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
