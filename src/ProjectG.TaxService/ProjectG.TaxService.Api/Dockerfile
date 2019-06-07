FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectG.TaxService/ProjectG.TaxService.Api/ProjectG.TaxService.Api.csproj", "ProjectG.TaxService/ProjectG.TaxService.Api/"]
RUN dotnet restore "ProjectG.TaxService/ProjectG.TaxService.Api/ProjectG.TaxService.Api.csproj"
COPY . .
WORKDIR "/src/ProjectG.TaxService/ProjectG.TaxService.Api"
RUN dotnet build "ProjectG.TaxService.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProjectG.TaxService.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjectG.TaxService.Api.dll"]