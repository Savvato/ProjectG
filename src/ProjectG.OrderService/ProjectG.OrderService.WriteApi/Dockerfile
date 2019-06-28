FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectG.OrderService/ProjectG.OrderService.WriteApi/ProjectG.OrderService.WriteApi.csproj", "ProjectG.OrderService/ProjectG.OrderService.WriteApi/"]
COPY ["ProjectG.Core/ProjectG.Core.csproj", "ProjectG.Core/"]
COPY ["ProjectG.OrderService/ProjectG.OrderService.Infrastructure.Db/ProjectG.OrderService.Infrastructure.Db.csproj", "ProjectG.OrderService/ProjectG.OrderService.Infrastructure.Db/"]
COPY ["ProjectG.OrderService/ProjectG.OrderService.Core/ProjectG.OrderService.Core.csproj", "ProjectG.OrderService/ProjectG.OrderService.Core/"]
COPY ["ProjectG.OrderService/ProjectG.OrderService.Infrastructure/ProjectG.OrderService.Infrastructure.csproj", "ProjectG.OrderService/ProjectG.OrderService.Infrastructure/"]
COPY ["ProjectG.OrderService/ProjectG.OrderService.Infrastructure.BasketApi/ProjectG.OrderService.Infrastructure.BasketApi.csproj", "ProjectG.OrderService/ProjectG.OrderService.Infrastructure.BasketApi/"]
COPY ["ProjectG.OrderService/ProjectG.OrderService.Infrastructure.CustomerApi/ProjectG.OrderService.Infrastructure.CustomerApi.csproj", "ProjectG.OrderService/ProjectG.OrderService.Infrastructure.CustomerApi/"]
COPY . .
WORKDIR "/src/ProjectG.OrderService/ProjectG.OrderService.WriteApi"
RUN dotnet publish "ProjectG.OrderService.WriteApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ProjectG.OrderService.WriteApi.dll"]