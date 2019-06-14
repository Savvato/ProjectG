FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectG.ProductService/ProjectG.ProductService.WriteApi/ProjectG.ProductService.WriteApi.csproj", "ProjectG.ProductService/ProjectG.ProductService.WriteApi/"]
COPY ["ProjectG.Core/ProjectG.Core.csproj", "ProjectG.Core/"]
COPY ["ProjectG.ProductService/ProjectG.ProductService.Infrastructure.Db/ProjectG.ProductService.Infrastructure.Db.csproj", "ProjectG.ProductService/ProjectG.ProductService.Infrastructure.Db/"]
COPY ["ProjectG.ProductService/ProjectG.ProductService.Core/ProjectG.ProductService.Core.csproj", "ProjectG.ProductService/ProjectG.ProductService.Core/"]
RUN dotnet restore "ProjectG.ProductService/ProjectG.ProductService.WriteApi/ProjectG.ProductService.WriteApi.csproj"
COPY . .
WORKDIR "/src/ProjectG.ProductService/ProjectG.ProductService.WriteApi"
RUN dotnet build "ProjectG.ProductService.WriteApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProjectG.ProductService.WriteApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjectG.ProductService.WriteApi.dll"]