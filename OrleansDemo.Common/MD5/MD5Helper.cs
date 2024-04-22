using System.Text;
namespace OrleansDemo.Common.MD5;
public static class MD5Helper
{
    public static string MD5Hash(string input)
    {
        using (var md5 = System.Security.Cryptography.MD5.Create())
        {
            var result = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
            var strResult = BitConverter.ToString(result);
            return strResult.Replace("-", "");
        }

    }

}
