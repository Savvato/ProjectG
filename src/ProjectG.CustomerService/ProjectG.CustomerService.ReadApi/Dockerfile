FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectG.CustomerService/ProjectG.CustomerService.ReadApi/ProjectG.CustomerService.ReadApi.csproj", "ProjectG.CustomerService/ProjectG.CustomerService.ReadApi/"]
COPY ["ProjectG.CustomerService/ProjectG.CustomerService.Infrastructure.Db/ProjectG.CustomerService.Infrastructure.Db.csproj", "ProjectG.CustomerService/ProjectG.CustomerService.Infrastructure.Db/"]
COPY ["ProjectG.CustomerService/ProjectG.CustomerService.Core/ProjectG.CustomerService.Core.csproj", "ProjectG.CustomerService/ProjectG.CustomerService.Core/"]
COPY . .
WORKDIR "/src/ProjectG.CustomerService/ProjectG.CustomerService.ReadApi"
RUN dotnet publish "ProjectG.CustomerService.ReadApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ProjectG.CustomerService.ReadApi.dll"]