FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectG.ClientService/ProjectG.ClientService.Web/ProjectG.ClientService.Web.csproj", "ProjectG.ClientService/ProjectG.ClientService.Web/"]
COPY ["ProjectG.ClientService/ProjectG.ClientService.Infrastructure.CustomerApi/ProjectG.ClientService.Infrastructure.CustomerApi.csproj", "ProjectG.ClientService/ProjectG.ClientService.Infrastructure.CustomerApi/"]
COPY ["ProjectG.ClientService/ProjectG.ClientService.Infrastructure.BasketApi/ProjectG.ClientService.Infrastructure.BasketApi.csproj", "ProjectG.ClientService/ProjectG.ClientService.Infrastructure.BasketApi/"]
COPY ["ProjectG.ClientService/ProjectG.ClientService.Infrastructure/ProjectG.ClientService.Infrastructure.csproj", "ProjectG.ClientService/ProjectG.ClientService.Infrastructure/"]
COPY ["ProjectG.ClientService/ProjectG.ClientService.Infrastructure.ProductApi/ProjectG.ClientService.Infrastructure.ProductApi.csproj", "ProjectG.ClientService/ProjectG.ClientService.Infrastructure.ProductApi/"]
COPY . .
WORKDIR "/src/ProjectG.ClientService/ProjectG.ClientService.Web"
RUN dotnet publish "ProjectG.ClientService.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ProjectG.ClientService.Web.dll"]