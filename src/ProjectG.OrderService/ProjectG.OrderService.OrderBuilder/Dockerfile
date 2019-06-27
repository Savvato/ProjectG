FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectG.OrderService/ProjectG.OrderService.OrderBuilder/ProjectG.OrderService.OrderBuilder.csproj", "ProjectG.OrderService/ProjectG.OrderService.OrderBuilder/"]
COPY ["ProjectG.OrderService/ProjectG.OrderService.Infrastructure.BasketApi/ProjectG.OrderService.Infrastructure.BasketApi.csproj", "ProjectG.OrderService/ProjectG.OrderService.Infrastructure.BasketApi/"]
COPY ["ProjectG.OrderService/ProjectG.OrderService.Core/ProjectG.OrderService.Core.csproj", "ProjectG.OrderService/ProjectG.OrderService.Core/"]
COPY ["ProjectG.OrderService/ProjectG.OrderService.Infrastructure.CustomerApi/ProjectG.OrderService.Infrastructure.CustomerApi.csproj", "ProjectG.OrderService/ProjectG.OrderService.Infrastructure.CustomerApi/"]
COPY ["ProjectG.Core/ProjectG.Core.csproj", "ProjectG.Core/"]
COPY ["ProjectG.OrderService/ProjectG.OrderService.Infrastructure.Db/ProjectG.OrderService.Infrastructure.Db.csproj", "ProjectG.OrderService/ProjectG.OrderService.Infrastructure.Db/"]
COPY ["ProjectG.OrderService/ProjectG.OrderService.Infrastructure/ProjectG.OrderService.Infrastructure.csproj", "ProjectG.OrderService/ProjectG.OrderService.Infrastructure/"]
COPY . .
WORKDIR "/src/ProjectG.OrderService/ProjectG.OrderService.OrderBuilder"
RUN dotnet publish "ProjectG.OrderService.OrderBuilder.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ProjectG.OrderService.OrderBuilder.dll"]