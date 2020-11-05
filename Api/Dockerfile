#FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
#WORKDIR /Api

# Copy csproj and restore as distinct layers
#COPY *.csproj ./
#RUN dotnet restore

# Copy everything else and build
#COPY . ./
#RUN dotnet publish -c Release -o out

# Build runtime image
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
#WORKDIR /Api
#COPY --from=build-env /Api/out .
#ENTRYPOINT ["dotnet", "GamesAndFriends.Api.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Api/GamesAndFriends.Api.csproj", "Api/"]
COPY ["Applications/GamesAndFriends.Application.csproj", "Applications/"]
COPY ["Data/GamesAndFriends.Data.csproj", "Data/"]
COPY ["Domain/GamesAndFriends.Domain.csproj", "Domain/"]
COPY ["IoC/GamesAndFriends.IoC.csproj", "IoC/"]
RUN dotnet restore "Api/GamesAndFriends.Api.csproj"
COPY . .
WORKDIR "/src/Api"
RUN dotnet build "GamesAndFriends.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GamesAndFriends.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GamesAndFriends.Api.dll"]