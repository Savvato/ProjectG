FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectG.DocumentService/ProjectG.DocumentService.Api/ProjectG.DocumentService.Api.csproj", "ProjectG.DocumentService/ProjectG.DocumentService.Api/"]
RUN dotnet restore "ProjectG.DocumentService/ProjectG.DocumentService.Api/ProjectG.DocumentService.Api.csproj"
COPY . .
WORKDIR "/src/ProjectG.DocumentService/ProjectG.DocumentService.Api"
RUN dotnet build "ProjectG.DocumentService.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProjectG.DocumentService.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjectG.DocumentService.Api.dll"]