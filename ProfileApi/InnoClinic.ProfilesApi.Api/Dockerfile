FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProfilesApi/InnoClinic.ProfilesApi.Api/InnoClinic.ProfilesApi.Api.csproj", "ProfilesApi/InnoClinic.ProfilesApi.Api/"]
COPY ["ProfilesApi/InnoClinic.ProfilesApi.BL/InnoClinic.ProfilesApi.BL.csproj", "ProfilesApi/InnoClinic.ProfilesApi.BL/"]
COPY ["ProfilesApi/InnoClinic.ProfilesApi.DAL/InnoClinic.ProfilesApi.DAL.csproj", "ProfilesApi/InnoClinic.ProfilesApi.DAL/"]
RUN dotnet restore "ProfilesApi/InnoClinic.ProfilesApi.Api/InnoClinic.ProfilesApi.Api.csproj"
COPY . .
WORKDIR "/src/ProfilesApi/InnoClinic.ProfilesApi.Api"
RUN dotnet build "InnoClinic.ProfilesApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InnoClinic.ProfilesApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InnoClinic.ProfilesApi.Api.dll"]
