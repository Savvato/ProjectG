FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ProjectG.OrderService/ProjectG.OrderService.OrderBuilder/ProjectG.OrderService.OrderBuilder.csproj", "ProjectG.OrderService/ProjectG.OrderService.OrderBuilder/"]
COPY . .
WORKDIR "/src/ProjectG.OrderService/ProjectG.OrderService.OrderBuilder"
RUN dotnet publish "ProjectG.OrderService.OrderBuilder.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ProjectG.OrderService.OrderBuilder.dll"]