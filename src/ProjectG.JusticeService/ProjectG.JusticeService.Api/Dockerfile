FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectG.JusticeService/ProjectG.JusticeService.Api/ProjectG.JusticeService.Api.csproj", "ProjectG.JusticeService/ProjectG.JusticeService.Api/"]
RUN dotnet restore "ProjectG.JusticeService/ProjectG.JusticeService.Api/ProjectG.JusticeService.Api.csproj"
COPY . .
WORKDIR "/src/ProjectG.JusticeService/ProjectG.JusticeService.Api"
RUN dotnet build "ProjectG.JusticeService.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProjectG.JusticeService.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjectG.JusticeService.Api.dll"]