FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectG.ClientService/ProjectG.ClientService.Web/ProjectG.ClientService.Web.csproj", "ProjectG.ClientService/ProjectG.ClientService.Web/"]
RUN dotnet restore "ProjectG.ClientService/ProjectG.ClientService.Web/ProjectG.ClientService.Web.csproj"
COPY . .
WORKDIR "/src/ProjectG.ClientService/ProjectG.ClientService.Web"
RUN dotnet build "ProjectG.ClientService.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProjectG.ClientService.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjectG.ClientService.Web.dll"]