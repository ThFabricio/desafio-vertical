FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# Remova ou defina APP_UID se necessário:
# USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["desafio-api/desafio-api.csproj", "desafio-api/"]
RUN dotnet restore "desafio-api/desafio-api.csproj"
COPY . .
WORKDIR "/src/desafio-api"
RUN dotnet build "desafio-api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "desafio-api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "desafio-api.dll"]