FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AppointmentApi/InnoClinic.AppointmentApi.Api/InnoClinic.AppointmentApi.Api.csproj", "AppointmentApi/InnoClinic.AppointmentApi.Api/"]
COPY ["AppointmentApi/InnoClinic.AppointmentApi.BL/InnoClinic.AppointmentApi.BL.csproj", "AppointmentApi/InnoClinic.AppointmentApi.BL/"]
COPY ["AppointmentApi/InnoClinic.AppointmentApi.DAL/InnoClinic.AppointmentApi.DAL.csproj", "AppointmentApi/InnoClinic.AppointmentApi.DAL/"]
RUN dotnet restore "AppointmentApi/InnoClinic.AppointmentApi.Api/InnoClinic.AppointmentApi.Api.csproj"
COPY . .
WORKDIR "/src/AppointmentApi/InnoClinic.AppointmentApi.Api"
RUN dotnet build "InnoClinic.AppointmentApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "InnoClinic.AppointmentApi.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InnoClinic.AppointmentApi.Api.dll"]
