using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Grains.User;

public class UserGrains : Grain, IUserGrains
{
    public async Task SayHalo()
    {
        await Console.Out.WriteLineAsync("调用orlean成功！！");
    }
}

