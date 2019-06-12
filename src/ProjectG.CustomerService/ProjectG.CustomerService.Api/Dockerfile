FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ProjectG.CustomerService/ProjectG.CustomerService.Api/ProjectG.CustomerService.Api.csproj", "ProjectG.CustomerService/ProjectG.CustomerService.Api/"]
RUN dotnet restore "ProjectG.CustomerService/ProjectG.CustomerService.Api/ProjectG.CustomerService.Api.csproj"
COPY . .
WORKDIR "/src/ProjectG.CustomerService/ProjectG.CustomerService.Api"
RUN dotnet build "ProjectG.CustomerService.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProjectG.CustomerService.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjectG.CustomerService.Api.dll"]