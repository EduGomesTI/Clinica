#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Clinica.Patients.Worker/Clinica.Patients.Worker.csproj", "Clinica.Patients.Worker/"]
COPY ["Clinica.Base/Clinica.Base.csproj", "Clinica.Base/"]
COPY ["Clinica.Patients.Application/Clinica.Patients.Application.csproj", "Clinica.Patients.Application/"]
COPY ["Clinica.Patients.Domain/Clinica.Patients.Domain.csproj", "Clinica.Patients.Domain/"]
COPY ["Clinica.Patients.Infrastructure/Clinica.Patients.Infrastructure.csproj", "Clinica.Patients.Infrastructure/"]
RUN dotnet restore "./Clinica.Patients.Worker/./Clinica.Patients.Worker.csproj"
COPY . .
WORKDIR "/src/Clinica.Patients.Worker"
RUN dotnet build "./Clinica.Patients.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Clinica.Patients.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clinica.Patients.Worker.dll"]