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

    public class NominationslistQuery : IRequest<Webresponse<PagedResult<Nomination>>>
    {

        public NominationFilter input { get; set; }
        public class NominationslistHandler : IRequestHandler<NominationslistQuery, Webresponse<PagedResult<Nomination>>>
        {
            private readonly IDatabaseConnectionFactory _db;

            public NominationslistHandler(IDatabaseConnectionFactory db)
            {
                _db = db;
            }
            public async Task<Webresponse<PagedResult<Nomination>>> Handle(NominationslistQuery request, CancellationToken cancellationToken)
            {
                Webresponse<PagedResult<Nomination>> _result = new Webresponse<PagedResult<Nomination>>
                {
                    status = APIStatus.processing,
                    message = "Initiaing db request",
                    data = new PagedResult<Nomination>()
                };

                SqlMapper.GridReader _dbreaders = null;

                using var conn = await _db.CreateConnectionAsync();
                _dbreaders = conn.QueryMultiple("SP_Nominationlist_Extended",
                        new
                        {
                            universityid = request.input.universityid,
                            uuid = request.input.uuid,
                            jobuuid = request.input.jobid,
                            studentuuid = request.input.studentid,
                            usertype = request.input.usertype,
                            pagenumber = request.input.pagenumber,
                            pagesize = request.input.pagesize,
                            minindex = request.input.minindex,
                            maxindex = request.input.maxindex
                        }, commandType: CommandType.StoredProcedure);


                //Read first data set
                _result.data.totalrecords = _dbreaders.Read<int>().FirstOrDefault();

                //Read second data set with paged data set
                _result.data.records = _dbreaders.Read<Nomination>().ToList();

                if (_result.data != null && _result.data.records != null && _result.data.records.Count > 0)
                {
                    _result.status = APIStatus.success;
                    _result.message = "records found";

                    _result.data.totalpages = _result.data.totalrecords > 0 ? _result.data.totalrecords / request.input.pagesize : _result.data.totalrecords;
                    _result.data.nextpage = request.input.pagenumber >= 1 && (request.input.pagenumber <= _result.data.totalpages) ? request.input.pagenumber + 1 : 1;
                    _result.data.previouspage = _result.data.totalpages - request.input.pagenumber >= 0
                        ? (request.input.pagenumber - 1 == 0 ? 1 : request.input.pagenumber - 1)
                        : 1;

                    //to get minrow id
                    var minnominationid = _result.data.records.Min(a => a.id);
                    _result.data.firstindex = minnominationid;

                    //to get maxrow id
                    var maxnominationid = _result.data.records.Max(a => a.id);
                    _result.data.lastindex = maxnominationid;
                }
                return _result;

            }
        }
    }
}
