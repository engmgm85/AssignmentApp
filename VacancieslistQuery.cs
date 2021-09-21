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

    public class VacancieslistQuery : IRequest<Webresponse<PagedResult<Vacancylist>>>
    {

        public VacancyFilter input { get; set; }
        public class VacancieslistHandler : IRequestHandler<VacancieslistQuery, Webresponse<PagedResult<Vacancylist>>>
        {
            private readonly IDatabaseConnectionFactory _db;

            public VacancieslistHandler(IDatabaseConnectionFactory db)
            {
                _db = db;
            }
            public async Task<Webresponse<PagedResult<Vacancylist>>> Handle(VacancieslistQuery request, CancellationToken cancellationToken)
            {
                Webresponse<PagedResult<Vacancylist>> _result = new Webresponse<PagedResult<Vacancylist>>
                {
                    status = APIStatus.processing,
                    message = "Initiaing db request",
                    data = new PagedResult<Vacancylist>()
                };

                SqlMapper.GridReader _dbreaders = null;

                using var conn = await _db.CreateConnectionAsync();
                _dbreaders = conn.QueryMultiple("SP_Available_OpportunityList_Extended",
                        new
                        {
                            universityid = request.input.universityid,                            
                            uuid = request.input.uuid,
                            usertype = request.input.usertype,
                            pagenumber = request.input.pagenumber,
                            pagesize = request.input.pagesize,
                            minindex = request.input.minindex,
                            maxindex = request.input.maxindex
                        }, commandType: CommandType.StoredProcedure);


                //Read first data set
                _result.data.totalrecords = _dbreaders.Read<int>().FirstOrDefault();

                //Read second data set with paged data set
                _result.data.records = _dbreaders.Read<Vacancylist>().ToList();

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
                    var minjobid = _result.data.records.Min(a => a.jobid);
                    _result.data.firstindex = minjobid;

                    //to get maxrow id
                    var maxjobid = _result.data.records.Max(a => a.jobid);
                    _result.data.lastindex = maxjobid;
                }
                return _result;

            }
        }
    }
}
