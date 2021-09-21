using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MyfutureModel;

namespace MyfutureAPI.Queries
{
    //Query definition below. Handler is definition in seperate class
    public class PingQuery : IRequest<LoginResult>
    {
        public string username { get; set; }
        public string password { get; set; }
    }


    //Both query and handler in single class
    public class PingHelloQuery : IRequest<string>
    {
        public class PingHelloQueryHandler : IRequestHandler<PingHelloQuery, string>
        {
            public async Task<string> Handle(PingHelloQuery query, CancellationToken cancellationToken)
            {
                //var companyapi = RestService.For<ICompanyAPI>("http://172.19.21.127/contactweb/api/Company");
                //var entity = await companyapi.GetCompanybyId(1);

                return await Task.FromResult("Myfuture API is UP!!!!");
            }
        }
    }

}
