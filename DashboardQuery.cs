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

    public class DashboardQuery<T> : IRequest<Webresponse<List<T>>> where T : class
    {

        public DashboardInput input { get; set; }
        public class DashboardQueryHandler : IRequestHandler<DashboardQuery<T>, Webresponse<List<T>>> 
        {
            private readonly IDatabaseConnectionFactory _db;

            public DashboardQueryHandler(IDatabaseConnectionFactory db)
            {
                _db = db;
            }
            public async Task<Webresponse<List<T>>> Handle(DashboardQuery<T> request, CancellationToken cancellationToken)
            {
                Webresponse<List<T>> _result = new Webresponse<List<T>>
                {
                    status = APIStatus.processing,
                    message = "Initiaing db request",
                    data = null
                };

                using var conn = await _db.CreateConnectionAsync();
                _result.data = conn.Query<T>("SP_Dashboard_Stats_Extended",
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
