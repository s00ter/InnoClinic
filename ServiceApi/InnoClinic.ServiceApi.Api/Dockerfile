FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ServiceApi/InnoClinic.ServiceApi.Api/InnoClinic.ServiceApi.Api.csproj", "ServiceApi/InnoClinic.ServiceApi.Api/"]
COPY ["ServiceApi/InnoClinic.ServiceApi.DAL/InnoClinic.ServiceApi.DAL.csproj", "ServiceApi/InnoClinic.ServiceApi.DAL/"]
COPY ["ServiceApi/InnoClinic.ServiceApi.BL/InnoClinic.ServiceApi.BL.csproj", "ServiceApi/InnoClinic.ServiceApi.BL/"]
RUN dotnet restore "ServiceApi/InnoClinic.ServiceApi.Api/InnoClinic.ServiceApi.Api.csproj"
COPY . .
WORKDIR "/src/ServiceApi/InnoClinic.ServiceApi.Api"
RUN dotnet build "InnoClinic.ServiceApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InnoClinic.ServiceApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InnoClinic.ServiceApi.Api.dll"]
