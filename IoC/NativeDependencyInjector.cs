using System;
using System.Collections.Generic;
using AutoMapper;
using GamesAndFriends.Application.Mapper;
using GamesAndFriends.Application.Services.Friends;
using GamesAndFriends.Application.Services.Games;
using GamesAndFriends.Application.Services.Interfaces;
using GamesAndFriends.Application.Services.Users;
using GamesAndFriends.Data.Context;
using GamesAndFriends.Data.Repository;
using GamesAndFriends.Domain.Entities;
using GamesAndFriends.Domain.Repository;
using GamesAndFriends.Domain.Services.Friends.Command;
using GamesAndFriends.Domain.Services.Friends.Query;
using GamesAndFriends.Domain.Services.Games.Command;
using GamesAndFriends.Domain.Services.Games.Query;
using GamesAndFriends.Domain.Services.Users.Query;
using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace GamesAndFriends.IoC
{
    public static class NativeDependencyInjector
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IFriendApplication, FriendsApplication>();
            services.AddScoped<IGameApplication, GamesApplication>();
            services.AddScoped<IUserApplication, UserApplication>();

            services.AddAutoMapper(typeof(DtoAndEntityMappingProfile));

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());


            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IFriendRepository, FriendRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var connection = new SqliteConnection(connectionString);

            services.AddDbContext<FriendDbContext>(options =>
                options.UseSqlite(connection)
            );
            services.AddDbContext<GameDbContext>(options =>
                options.UseSqlite(connection)
            );
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlite(connection)
            );


            services.AddScoped<IRequestHandler<GetAllFriendsQuery, IList<Friend>>, FriendQueryHandle>();
            services.AddScoped<IRequestHandler<GetFriendQuery, Friend>, FriendQueryHandle>();
            services.AddScoped<IRequestHandler<AddFriendCommand, Friend>, FriendCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteFriendCommand>, FriendCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateFriendCommand, Friend>, FriendCommandHandler>();

            services.AddScoped<IRequestHandler<GetAllGamesQuery, IList<Game>>, GameQueryHandler>();
            services.AddScoped<IRequestHandler<GetGameQuery, Game>, GameQueryHandler>();
            services.AddScoped<IRequestHandler<AddGameCommand, Game>, GameCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteGameCommand>, GameCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateGameCommand, Game>, GameCommandHandler>();

            services.AddScoped<IRequestHandler<GetAllUsersQuery, IList<User>>, UserQueryHandle>();
            services.AddScoped<IRequestHandler<GetUserQuery, User>, UserQueryHandle>();
        }

        public static void ConfigureDbContext(this IServiceScopeFactory serviceScopeFactory) {
            using (var serviceScope = serviceScopeFactory.CreateScope())
            {
                var gameDbContext = serviceScope.ServiceProvider.GetService<GameDbContext>();
                (gameDbContext.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).CreateTables();

                var userDbContext = serviceScope.ServiceProvider.GetService<UserDbContext>();
                (userDbContext.Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).CreateTables();
            }
        }
    }
}
