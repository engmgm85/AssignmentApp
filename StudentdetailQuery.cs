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


namespace MyfutureAPI.Queries
{

    public class StudentdetailQuery : IRequest<Webresponse<Studentdetail>>
    {
        public Studentdetailfilter filter { get; set; }

        public class StudentdetailQueryHandler : IRequestHandler<StudentdetailQuery, Webresponse<Studentdetail>>
        {
            private readonly IDatabaseConnectionFactory _db;

            public StudentdetailQueryHandler(IDatabaseConnectionFactory db)
            {
                _db = db;
            }
            public async Task<Webresponse<Studentdetail>> Handle(StudentdetailQuery request, CancellationToken cancellationToken)
            {
                Webresponse<Studentdetail> _result = new Webresponse<Studentdetail>
                {
                    status = APIStatus.processing,
                    message = "Initiaing db request",
                    data = null
                };

                SqlMapper.GridReader _dbreaders = null;
                using var conn = await _db.CreateConnectionAsync();
                _dbreaders = conn.QueryMultiple("SP_GetMain_ProfileDetail_Extended",
                        new
                        {
                            universityid = request.filter.universityid,
                            studentid = request.filter.studentid,
                            uuid = request.filter.uuid,
                            usertype = request.filter.usertype
                        }, commandType: CommandType.StoredProcedure);

                if (_dbreaders != null)
                {
                    _result.data = _dbreaders.Read<Studentdetail>().FirstOrDefault();

                    if (_result.data != null)
                    {
                        _result.status = APIStatus.success;
                        _result.message = "recrodfound";

                        _result.data.educations = _dbreaders.Read<CurrentEducation>().ToList();
                        _result.data.skills = _dbreaders.Read<StudentSkill>().ToList();
                    }

                }



                return _result;

            }
        }

    }
}
