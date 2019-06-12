FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["ProjectG.OrderService/ProjectG.OrderService.WriteApi/ProjectG.OrderService.WriteApi.csproj", "ProjectG.OrderService/ProjectG.OrderService.WriteApi/"]
RUN dotnet restore "ProjectG.OrderService/ProjectG.OrderService.WriteApi/ProjectG.OrderService.WriteApi.csproj"
COPY . .
WORKDIR "/src/ProjectG.OrderService/ProjectG.OrderService.WriteApi"
RUN dotnet build "ProjectG.OrderService.WriteApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProjectG.OrderService.WriteApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProjectG.OrderService.WriteApi.dll"]