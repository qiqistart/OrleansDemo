using MediatR;
using Orleans.Grains.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Orleans.Application.Command.User;
public class AddUserCommand : IRequest<bool>
{
    /// <summary>
    ///     
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    ///     
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string PassWord { get; set; }
}

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
{
    /// <summary>
    /// 
    /// </summary>
    private readonly IClusterClient clusterClient;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="clusterClient"></param>
    public AddUserCommandHandler(IClusterClient clusterClient)
    {
        this.clusterClient = clusterClient;
    }
    public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var userData = await clusterClient.GetGrain<IUserGrains>(request.UserName).AddUser(new Domain.Entity.UserAggregate.SysUser(request.UserName, request.Account, request.Avatar, request.PassWord));
            bool addUserConfigIsOk = await clusterClient.GetGrain<IUserConfigGrains>(request.UserName).AddUserConfig(new Domain.Entity.UserAggregate.SysUserConfig(userData.Id, 0, false, 0));

        return true;
    }
}
