FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AuthorizationApi/InnoClinic.AuthorizationApi.Api/InnoClinic.AuthorizationApi.Api.csproj", "AuthorizationApi/InnoClinic.AuthorizationApi.Api/"]
COPY ["AuthorizationApi/InnoClinic.AuthorizationApi.Application/InnoClinic.AuthorizationApi.Application.csproj", "AuthorizationApi/InnoClinic.AuthorizationApi.Application/"]
COPY ["AuthorizationApi/InnoClinic.AuthorizationApi.Core/InnoClinic.AuthorizationApi.Core.csproj", "AuthorizationApi/InnoClinic.AuthorizationApi.Core/"]
COPY ["AuthorizationApi/InnoClinic.AuthorizationApi.Infrastructure/InnoClinic.AuthorizationApi.Infrastructure.csproj", "AuthorizationApi/InnoClinic.AuthorizationApi.Infrastructure/"]
RUN dotnet restore "AuthorizationApi/InnoClinic.AuthorizationApi.Api/InnoClinic.AuthorizationApi.Api.csproj"
COPY . .
WORKDIR "/src/AuthorizationApi/InnoClinic.AuthorizationApi.Api"
RUN dotnet build "InnoClinic.AuthorizationApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InnoClinic.AuthorizationApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InnoClinic.AuthorizationApi.Api.dll"]
