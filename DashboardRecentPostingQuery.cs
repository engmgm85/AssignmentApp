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

    public class DashboardRecentPostingQuery : IRequest<Webresponse<List<DashboardRecentPostings>>>
    {

        public DashboardInput input { get; set; }
        public class DashboardRecentPostingQueryHandler : IRequestHandler<DashboardRecentPostingQuery, Webresponse<List<DashboardRecentPostings>>>
        {
            private readonly IDatabaseConnectionFactory _db;

            public DashboardRecentPostingQueryHandler(IDatabaseConnectionFactory db)
            {
                _db = db;
            }
            public async Task<Webresponse<List<DashboardRecentPostings>>> Handle(DashboardRecentPostingQuery request, CancellationToken cancellationToken)
            {
                Webresponse<List<DashboardRecentPostings>> _result = new Webresponse<List<DashboardRecentPostings>>
                {
                    status = APIStatus.processing,
                    message = "Initiaing db request",
                    data = null
                };

                using var conn = await _db.CreateConnectionAsync();
                _result.data = conn.Query<DashboardRecentPostings>("SP_Dashboard_Stats_Extended",
                        new
                        {
                            universityid = request.input.universityid,
                            uuid = request.input.uuid,
                            usertype = request.input.usertype,
                            stattype = request.input.stattype //2

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
