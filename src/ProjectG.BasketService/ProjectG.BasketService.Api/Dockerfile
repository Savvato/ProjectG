FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ProjectG.BasketService/ProjectG.BasketService.Api/ProjectG.BasketService.Api.csproj", "ProjectG.BasketService/ProjectG.BasketService.Api/"]
RUN dotnet restore "ProjectG.BasketService/ProjectG.BasketService.Api/ProjectG.BasketService.Api.csproj"
COPY . .
WORKDIR "/src/ProjectG.BasketService/ProjectG.BasketService.Api"
RUN dotnet build "ProjectG.BasketService.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProjectG.BasketService.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjectG.BasketService.Api.dll"]