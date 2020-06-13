using WorksJwtClient.Builders.Abstract;
using WorksJwtClient.Models;

namespace WorksJwtClient.Builders.Concrete
{
    public class StatusBuilderDirector
    {
        private StatusBuilder builder;
        public StatusBuilderDirector(StatusBuilder builder)
        {
            this.builder = builder;
        }

        public Status GenerateStatus(AppUser activeUser,string roles){
            return builder.GenerateStatus(activeUser,roles);
        }
    }
}