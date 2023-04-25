using LightMessanger.BLL.Interfaces;
using LightMessanger.BLL.Services;
using LightMessanger.DAL.Interfaces;
using LightMessanger.DAL.Repositories;

namespace LightMessanger.WEB
{
    public static class DI
    {
        public static void AddDependency(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            //services.AddScoped<IChatRepository, ChatRepository>();
            //services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IGroupMessageRepository, GroupMessageRepository>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IGroupsService, GroupsService>();
            services.AddScoped<IGroupMessagesService, GroupMessagesService>();
        }
    }
}
