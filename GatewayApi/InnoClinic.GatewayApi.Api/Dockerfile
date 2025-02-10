FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GatewayApi/InnoClinic.GatewayApi.Api/InnoClinic.GatewayApi.Api.csproj", "GatewayApi/InnoClinic.GatewayApi.Api/"]
COPY ["AuthorizationApi/InnoClinic.AuthorizationApi.Core/InnoClinic.AuthorizationApi.Core.csproj", "AuthorizationApi/InnoClinic.AuthorizationApi.Core/"]
RUN dotnet restore "GatewayApi/InnoClinic.GatewayApi.Api/InnoClinic.GatewayApi.Api.csproj"
COPY . .
WORKDIR "/src/GatewayApi/InnoClinic.GatewayApi.Api"
RUN dotnet build "InnoClinic.GatewayApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InnoClinic.GatewayApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InnoClinic.GatewayApi.Api.dll"]
