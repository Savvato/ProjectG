FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectG.ProductService/ProjectG.ProductService.ReadApi/ProjectG.ProductService.ReadApi.csproj", "ProjectG.ProductService/ProjectG.ProductService.ReadApi/"]
COPY ["ProjectG.ProductService/ProjectG.ProductService.Infrastructure/ProjectG.ProductService.Infrastructure.csproj", "ProjectG.ProductService/ProjectG.ProductService.Infrastructure/"]
COPY ["ProjectG.ProductService/ProjectG.ProductService.Infrastructure.Cache/ProjectG.ProductService.Infrastructure.Cache.csproj", "ProjectG.ProductService/ProjectG.ProductService.Infrastructure.Cache/"]
COPY ["ProjectG.ProductService/ProjectG.ProductService.Core/ProjectG.ProductService.Core.csproj", "ProjectG.ProductService/ProjectG.ProductService.Core/"]
COPY ["ProjectG.ProductService/ProjectG.ProductService.Infrastructure.OrderApi/ProjectG.ProductService.Infrastructure.OrderApi.csproj", "ProjectG.ProductService/ProjectG.ProductService.Infrastructure.OrderApi/"]
COPY ["ProjectG.ProductService/ProjectG.ProductService.Infrastructure.Db/ProjectG.ProductService.Infrastructure.Db.csproj", "ProjectG.ProductService/ProjectG.ProductService.Infrastructure.Db/"]
COPY . .
WORKDIR "/src/ProjectG.ProductService/ProjectG.ProductService.ReadApi"
RUN dotnet publish "ProjectG.ProductService.ReadApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ProjectG.ProductService.ReadApi.dll"]