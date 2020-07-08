using System.Threading.Tasks;
using SRC.Auth.API.Models;

namespace SRC.Auth.API.Interface
{
    public interface IAuthService
    {
        Task<string> RegisterUser(RegisterModel model);
    }
}