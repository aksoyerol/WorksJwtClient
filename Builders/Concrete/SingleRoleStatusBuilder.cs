using WorksJwtClient.Builders.Abstract;
using WorksJwtClient.Models;

namespace WorksJwtClient.Builders.Concrete{
    public class SingleRoleStatusBuilder : StatusBuilder
    {
        public override Status GenerateStatus(AppUser activeUser, string roles)
        {
            Status status = new Status();
            if (activeUser.Roles.Contains(roles)) status.AccessStatus = true;
            return status;
        }
    }
}