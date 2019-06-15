FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectG.BasketService/ProjectG.BasketService.Api/ProjectG.BasketService.Api.csproj", "ProjectG.BasketService/ProjectG.BasketService.Api/"]
COPY ["ProjectG.BasketService/ProjectG.BasketService.Infrastructure.Db/ProjectG.BasketService.Infrastructure.Db.csproj", "ProjectG.BasketService/ProjectG.BasketService.Infrastructure.Db/"]
COPY ["ProjectG.BasketService/ProjectG.BasketService.Core/ProjectG.BasketService.Core.csproj", "ProjectG.BasketService/ProjectG.BasketService.Core/"]
COPY ["ProjectG.BasketService/ProjectG.BasketService.Infrastructure/ProjectG.BasketService.Infrastructure.csproj", "ProjectG.BasketService/ProjectG.BasketService.Infrastructure/"]
COPY . .
WORKDIR "/src/ProjectG.BasketService/ProjectG.BasketService.Api"

FROM build AS publish
RUN dotnet publish "ProjectG.BasketService.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjectG.BasketService.Api.dll"]