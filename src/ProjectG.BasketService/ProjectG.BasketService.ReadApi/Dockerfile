FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectG.BasketService/ProjectG.BasketService.ReadApi/ProjectG.BasketService.ReadApi.csproj", "ProjectG.BasketService/ProjectG.BasketService.ReadApi/"]
COPY ["ProjectG.BasketService/ProjectG.BasketService.Infrastructure.Cache/ProjectG.BasketService.Infrastructure.Cache.csproj", "ProjectG.BasketService/ProjectG.BasketService.Infrastructure.Cache/"]
COPY ["ProjectG.BasketService/ProjectG.BasketService.Core/ProjectG.BasketService.Core.csproj", "ProjectG.BasketService/ProjectG.BasketService.Core/"]
COPY ["ProjectG.BasketService/ProjectG.BasketService.Infrastructure/ProjectG.BasketService.Infrastructure.csproj", "ProjectG.BasketService/ProjectG.BasketService.Infrastructure/"]
COPY ["ProjectG.BasketService/ProjectG.BasketService.Infrastructure.Db/ProjectG.BasketService.Infrastructure.Db.csproj", "ProjectG.BasketService/ProjectG.BasketService.Infrastructure.Db/"]
COPY ["ProjectG.BasketService/ProjectG.BasketService.Infrastructure.ProductApi/ProjectG.BasketService.Infrastructure.ProductApi.csproj", "ProjectG.BasketService/ProjectG.BasketService.Infrastructure.ProductApi/"]
COPY . .
WORKDIR "/src/ProjectG.BasketService/ProjectG.BasketService.ReadApi"
RUN dotnet publish "ProjectG.BasketService.ReadApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ProjectG.BasketService.ReadApi.dll"]