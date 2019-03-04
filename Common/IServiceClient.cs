using System.Threading.Tasks;

namespace Common
{
    public interface IServiceClient
    {
        Task<string> SayHello();
    }
}
