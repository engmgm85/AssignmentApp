using Dapper;
using MediatR;
using MyfutureData;
using MyfutureModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyfutureAPI.Queries
{

    public class LookUpQuery : IRequest<Webresponse<List<Lookup>>>
    {
        public Lookupfilter input { get; set; }
        public class LookUpHandler : IRequestHandler<LookUpQuery, Webresponse<List<Lookup>>>
        {
            private readonly IDatabaseConnectionFactory _db;

            public LookUpHandler(IDatabaseConnectionFactory db)
            {
                _db = db;
            }
            public async Task<Webresponse<List<Lookup>>> Handle(LookUpQuery request, CancellationToken cancellationToken)
            {
                Webresponse<List<Lookup>> _result = new Webresponse<List<Lookup>>
                {
                    status = APIStatus.processing,
                    message = "Initiaing db request",
                    data = null
                };

                using var conn = await _db.CreateConnectionAsync();
                _result.data = conn.Query<Lookup>("SP_STUD_Master",
                        new
                        {
                            lookuptype = request.input.lookuptype,
                            searchterm = request.input.searchterm.Trim().ToLower()                          

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
