using System.Threading.Tasks;
using WorksJwtClient.Models;

namespace WorksJwtClient.ApiServices.Interfaces{
    public interface IAuthService{
        Task<bool> Login(AppUserLogin appUserLogin);
        void LogOut();
    }
}