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

    public class DashboardYearWiseForecastQuery : IRequest<Webresponse<List<DashboardYearwise>>>
    {

        public DashboardInput input { get; set; }
        public class DashboardYearWiseForecastQueryHandler : IRequestHandler<DashboardYearWiseForecastQuery, Webresponse<List<DashboardYearwise>>>
        {
            private readonly IDatabaseConnectionFactory _db;

            public DashboardYearWiseForecastQueryHandler(IDatabaseConnectionFactory db)
            {
                _db = db;
            }
            public async Task<Webresponse<List<DashboardYearwise>>> Handle(DashboardYearWiseForecastQuery request, CancellationToken cancellationToken)
            {
                Webresponse<List<DashboardYearwise>> _result = new Webresponse<List<DashboardYearwise>>
                {
                    status = APIStatus.processing,
                    message = "Initiaing db request",
                    data = null
                };

                using var conn = await _db.CreateConnectionAsync();
                _result.data = conn.Query<DashboardYearwise>("SP_Dashboard_Stats_Extended",
                        new
                        {
                            universityid = request.input.universityid,
                            uuid = request.input.uuid,
                            usertype = request.input.usertype,
                            stattype = request.input.stattype //3

                        }, commandType: CommandType.StoredProcedure).ToList();


                if (_result.data != null)
                {
                    _result.status = APIStatus.success;
                    _result.message = "recordsfound";

                    int totalforecast = _result.data.Sum(a => a.total);
                    foreach (var item in _result.data)
                    {
                        decimal share = (Convert.ToDecimal(item.total) / Convert.ToDecimal(totalforecast)) * 100m;
                        item.percentage = Convert.ToInt32(share);

                    }
                }

                return _result;

            }
        }
    }
}
