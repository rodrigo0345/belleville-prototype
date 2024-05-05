FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BelleVillePrototype/BelleVillePrototype.ApiService/BelleVillePrototype.ApiService.csproj", "BelleVillePrototype/BelleVillePrototype.ApiService/"]
COPY ["BelleVillePrototype/BelleVillePrototype.ServiceDefaults/BelleVillePrototype.ServiceDefaults.csproj", "BelleVillePrototype/BelleVillePrototype.ServiceDefaults/"]
RUN dotnet restore "BelleVillePrototype/BelleVillePrototype.ApiService/BelleVillePrototype.ApiService.csproj"
COPY . .
WORKDIR "/src/BelleVillePrototype/BelleVillePrototype.ApiService"
RUN dotnet build "BelleVillePrototype.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BelleVillePrototype.ApiService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BelleVillePrototype.ApiService.dll"]
