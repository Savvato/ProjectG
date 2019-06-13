FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ProjectG.ProductService/ProjectG.ProductService.ReadApi/ProjectG.ProductService.ReadApi.csproj", "ProjectG.ProductService/ProjectG.ProductService.ReadApi/"]
COPY ["ProjectG.ProductService/ProjectG.ProductService.Infrastructure.Db/ProjectG.ProductService.Infrastructure.Db.csproj", "ProjectG.ProductService/ProjectG.ProductService.Infrastructure.Db/"]
COPY ["ProjectG.ProductService/ProjectG.ProductService.Core/ProjectG.ProductService.Core.csproj", "ProjectG.ProductService/ProjectG.ProductService.Core/"]
RUN dotnet restore "ProjectG.ProductService/ProjectG.ProductService.ReadApi/ProjectG.ProductService.ReadApi.csproj"
COPY . .
WORKDIR "/src/ProjectG.ProductService/ProjectG.ProductService.ReadApi"
RUN dotnet build "ProjectG.ProductService.ReadApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProjectG.ProductService.ReadApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjectG.ProductService.ReadApi.dll"]