FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectG.OrderService/ProjectG.OrderService.WriteApi/ProjectG.OrderService.WriteApi.csproj", "ProjectG.OrderService/ProjectG.OrderService.WriteApi/"]
COPY ["ProjectG.OrderService/ProjectG.OrderService.Infrastructure.Db/ProjectG.OrderService.Infrastructure.Db.csproj", "ProjectG.OrderService/ProjectG.OrderService.Infrastructure.Db/"]
COPY ["ProjectG.OrderService/ProjectG.OrderService.Core/ProjectG.OrderService.Core.csproj", "ProjectG.OrderService/ProjectG.OrderService.Core/"]
COPY . .
WORKDIR "/src/ProjectG.OrderService/ProjectG.OrderService.WriteApi"

FROM build AS publish
RUN dotnet publish "ProjectG.OrderService.WriteApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjectG.OrderService.WriteApi.dll"]