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

    public class DashboardJobPostingQuery : IRequest<Webresponse<List<DashboardJobpost>>>
    {

        public DashboardInput input { get; set; }
        public class DashboardJobpostHandler : IRequestHandler<DashboardJobPostingQuery, Webresponse<List<DashboardJobpost>>>
        {
            private readonly IDatabaseConnectionFactory _db;

            public DashboardJobpostHandler(IDatabaseConnectionFactory db)
            {
                _db = db;
            }
            public async Task<Webresponse<List<DashboardJobpost>>> Handle(DashboardJobPostingQuery request, CancellationToken cancellationToken)
            {
                Webresponse<List<DashboardJobpost>> _result = new Webresponse<List<DashboardJobpost>>
                {
                    status = APIStatus.processing,
                    message = "Initiaing db request",
                    data = null
                };

                using var conn = await _db.CreateConnectionAsync();
                _result.data = conn.Query<DashboardJobpost>("SP_Dashboard_Stats_Extended",
                        new
                        {
                            universityid = request.input.universityid,
                            uuid = request.input.uuid,
                            usertype = request.input.usertype,
                            stattype = request.input.stattype

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
