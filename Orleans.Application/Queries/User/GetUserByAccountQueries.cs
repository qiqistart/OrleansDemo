using MediatR;
using Orleans.Domain.Entity.UserAggregate;
using Orleans.Grains.User;

namespace Orleans.Application.Queries.User;

public class GetUserByAccountQueries : IRequest<SysUser>
{
    /// <summary>
    ///                      
    /// </summary>
    /// <param name="Account"></param>
    public GetUserByAccountQueries(string Account)
    {
        this.Account = Account;
    }
    public string Account { get; set; }
}
public class GetUserByAccountQueriesHandler : IRequestHandler<GetUserByAccountQueries, SysUser>
{

    private readonly IClusterClient clusterClient;
    /// <summary>
    /// 构造函数注入
    /// </summary>
    /// <param name="clusterClient"></param>
    public GetUserByAccountQueriesHandler(IClusterClient clusterClient)
    {
        this.clusterClient = clusterClient;
    }
    /// <summary>
    ///     
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<SysUser> Handle(GetUserByAccountQueries request, CancellationToken cancellationToken)
    {
        var userData = await clusterClient.GetGrain<IUserGrains>(request.Account).GetUserByAccount(request.Account);
        if (userData!= null)
        {
          await clusterClient.GetGrain<IUserGrains>(userData.Id).GetUserByUserId(userData.Id);
        }
        return userData;
    }
}