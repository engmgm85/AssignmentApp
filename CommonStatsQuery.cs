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
using Serilog;

namespace MyfutureAPI.Queries
{

    public class CommonStatsQuery : IRequest<Webresponse<HomePageInfo>>
    {
        public class CommonStatsQueryHandler : IRequestHandler<CommonStatsQuery, Webresponse<HomePageInfo>>
        {
            private readonly IDatabaseConnectionFactory _db;

            public CommonStatsQueryHandler(IDatabaseConnectionFactory db)
            {
                _db = db;
            }
            public async Task<Webresponse<HomePageInfo>> Handle(CommonStatsQuery request, CancellationToken cancellationToken)
            {
                Webresponse<HomePageInfo> _result = new Webresponse<HomePageInfo>
                {
                    status = APIStatus.processing,
                    message = "Initiaing db request",
                    data = new HomePageInfo()
                };

                SqlMapper.GridReader _dbreaders = null;
                using var _dbConnection = await _db.CreateConnectionAsync();
                _dbreaders = _dbConnection.QueryMultiple("Getcommonpagestats", commandType: CommandType.StoredProcedure);

                IList<StudentStat> studentStats = _dbreaders.Read<StudentStat>().ToList();
                IList<StudentClassification> studentClassifications = _dbreaders.Read<StudentClassification>().ToList();
                _result.data.StudentInfo = GetStudentStats(studentStats, studentClassifications);

                IList<CouncelorStat> councelorStat = _dbreaders.Read<CouncelorStat>().ToList();
                IList<CouncelorClassification> councelorClassification = _dbreaders.Read<CouncelorClassification>().ToList();
                _result.data.CounselorInfo = GetCounselorStats(councelorStat, councelorClassification);

                IList<EmployeerClassification> employeerClassification = _dbreaders.Read<EmployeerClassification>().ToList();
                IList<ForcastClassification> forcastClassification = _dbreaders.Read<ForcastClassification>().ToList();
                _result.data.EmployerInfo = GetEmployerStats(employeerClassification, forcastClassification);

                _result.message = "record found";
                _result.status = APIStatus.success;

                return _result;


            }

            public StudentStats GetStudentStats(IList<StudentStat> liststudentStat, IList<StudentClassification> liststudentClassification)
            {
                StudentStats StudentStats = new StudentStats();
                try
                {
                    foreach (StudentStat studentStat in liststudentStat)
                    {
                        StudentStats.listEnrolled.Add(new StudentsGenderStat { Order = studentStat.Order, ArTitle = studentStat.GenderAR, EnTitle = studentStat.GenderEN, Total = studentStat.Count });
                        StudentStats.TotalGender += studentStat.Count;
                    }

                    foreach (StudentClassification studentClassification in liststudentClassification)
                    {
                        StudentStats.listClasses.Add(new StudentsClassStat { Order = studentClassification.Order, ArTitle = studentClassification.ClassificationAR, EnTitle = studentClassification.ClassificationEN, Total = studentClassification.Count });
                        StudentStats.TotalClassifications += studentClassification.Count;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"EducationStatsRepository: StudentsStatistics\n {ex.Message}");
                }
                return StudentStats;
            }

            public CounselorStats GetCounselorStats(IList<CouncelorStat> listcouncelorStat, IList<CouncelorClassification> listcouncelorClassification)
            {
                CounselorStats counselorStats = new CounselorStats();
                try
                {
                    foreach (CouncelorStat councelorStat in listcouncelorStat)
                    {
                        counselorStats.listClasses.Add(new CounselorsClassStat { Order = councelorStat.Order, ArTitle = councelorStat.ClassAR, EnTitle = councelorStat.ClassEN, Total = councelorStat.Count });
                        counselorStats.TotalClassifications += councelorStat.Count;
                    }

                    foreach (CouncelorClassification councelorClassification in listcouncelorClassification)
                    {
                        counselorStats.listUniversityClassStat.Add(new UniversityClassStat { Order = councelorClassification.Order, ArTitle = councelorClassification.ClassificationAR, EnTitle = councelorClassification.ClassificationEN, Total = councelorClassification.Count });
                        counselorStats.TotalUniversities += councelorClassification.Count;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"EducationStatsRepository: CounselorStatistics\n {ex.Message}");
                }

                return counselorStats;
            }

            public EmployerStats GetEmployerStats(IList<EmployeerClassification> listemployeerClassification, IList<ForcastClassification> listforcastClassification)
            {
                EmployerStats employerStats = new EmployerStats();
                try
                {
                    foreach (EmployeerClassification employeerClassification in listemployeerClassification)
                    {
                        employerStats.listRegisteredEmployeesStat.Add(new RegisteredEmployeesStat { Order = employeerClassification.Order, ArTitle = employeerClassification.ClassificationAR, EnTitle = employeerClassification.ClassificationEN, Total = employeerClassification.Count });
                        employerStats.TotalRegisteredEmployees += employeerClassification.Count;
                    }
                    foreach (ForcastClassification forcastClassification in listforcastClassification)
                    {
                        employerStats.listForecastJobsStat.Add(new ForecastJobsStat { Order = forcastClassification.Order, ArTitle = forcastClassification.ClassificationAR, EnTitle = forcastClassification.ClassificationEN, Total = forcastClassification.Count });
                        employerStats.TotalForecastJobs += forcastClassification.Count;
                    }

                }
                catch (Exception ex)
                {
                    Log.Error($"EducationStatsRepository: EmployerStatistics\n {ex.Message}");
                }

                return employerStats;
            }

        }
    }

}
