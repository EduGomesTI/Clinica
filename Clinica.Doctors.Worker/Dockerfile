#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Clinica.Doctors.Worker/Clinica.Doctors.Worker.csproj", "Clinica.Doctors.Worker/"]
COPY ["Clinica.Base/Clinica.Base.csproj", "Clinica.Base/"]
COPY ["Clinica.Doctors.Application/Clinica.Doctors.Application.csproj", "Clinica.Doctors.Application/"]
COPY ["Clinica.Doctors.Domain/Clinica.Doctors.Domain.csproj", "Clinica.Doctors.Domain/"]
COPY ["Clinica.Doctors.Infrastructure/Clinica.Doctors.Infrastructure.csproj", "Clinica.Doctors.Infrastructure/"]
RUN dotnet restore "./Clinica.Doctors.Worker/./Clinica.Doctors.Worker.csproj"
COPY . .
WORKDIR "/src/Clinica.Doctors.Worker"
RUN dotnet build "./Clinica.Doctors.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Clinica.Doctors.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clinica.Doctors.Worker.dll"]