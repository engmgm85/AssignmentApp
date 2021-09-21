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

    public class StudentlistQuery : IRequest<Webresponse<PagedResult<Studentlist>>>
    {

        public StudentFilter input { get; set; }
        public class StudentlistHandler : IRequestHandler<StudentlistQuery, Webresponse<PagedResult<Studentlist>>>
        {
            private readonly IDatabaseConnectionFactory _db;

            public StudentlistHandler(IDatabaseConnectionFactory db)
            {
                _db = db;
            }
            public async Task<Webresponse<PagedResult<Studentlist>>> Handle(StudentlistQuery request, CancellationToken cancellationToken)
            {
                Webresponse<PagedResult<Studentlist>> _result = new Webresponse<PagedResult<Studentlist>>
                {
                    status = APIStatus.processing,
                    message = "Initiaing db request",
                    data = new PagedResult<Studentlist>()
                };

                SqlMapper.GridReader _dbreaders = null;

                using var conn = await _db.CreateConnectionAsync();
                _dbreaders = conn.QueryMultiple("SP_Active_StudentlistList_Extended",
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
                _result.data.records = _dbreaders.Read<Studentlist>().ToList();

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
                    var minstudentid = _result.data.records.Min(a => a.studentid);
                    _result.data.firstindex = minstudentid;

                    //to get maxrow id
                    var maxstudentid = _result.data.records.Max(a => a.studentid);
                    _result.data.lastindex = maxstudentid;
                }
                return _result;

            }
        }
    }
}
