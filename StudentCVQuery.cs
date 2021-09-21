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
using MongoDB.Driver;

namespace MyfutureAPI.Queries
{

    public class StudentCVQuery : IRequest<Webresponse<StudentResume>>
    {
        public Studentdetailfilter filter { get; set; }

        public class StudentCVQueryHandler : IRequestHandler<StudentCVQuery, Webresponse<StudentResume>>
        {
            private readonly IContentDatabaseConnectionFactory _contentdb;

            public StudentCVQueryHandler(IContentDatabaseConnectionFactory contentdb)
            {
                _contentdb = contentdb;
            }
            public async Task<Webresponse<StudentResume>> Handle(StudentCVQuery request, CancellationToken cancellationToken)
            {
                Webresponse<StudentResume> _result = new Webresponse<StudentResume>
                {
                    status = APIStatus.processing,
                    message = "Initiaing db request",
                    data = null
                };

                var conn = await _contentdb.CreateConnectionAsync();
                var collection = conn.GetCollection<StudentResume>("student_resume");
                var sort = Builders<StudentResume>.Sort.Descending("createddate");
                var firstrecord = collection.Find(a => a.studentid == request.filter.studentid)
                       .Sort(sort)
                       .FirstOrDefault();

                if (firstrecord != null)
                {
                    _result.data = firstrecord;
                    _result.status = APIStatus.success;
                    _result.message = "recordfound";
                }
                else
                {
                    _result.status = APIStatus.error;
                    _result.message = "recordnotfound";
                }

                return _result;

            }
        }

    }
}
