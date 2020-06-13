using WorksJwtClient.Builders.Concrete;
using WorksJwtClient.Models;

namespace WorksJwtClient.Builders.Abstract{
    public abstract class StatusBuilder{
        public abstract Status GenerateStatus(AppUser activeUser,string roles);
    }
}