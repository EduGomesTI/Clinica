#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Clinica.Schedulings.Worker/Clinica.Schedulings.Worker.csproj", "Clinica.Schedulings.Worker/"]
COPY ["Clinica.Schedulings.Application/Clinica.Schedulings.Application.csproj", "Clinica.Schedulings.Application/"]
COPY ["Clinica.Base/Clinica.Base.csproj", "Clinica.Base/"]
COPY ["Clinica.Schedulings.Domain/Clinica.Schedulings.Domain.csproj", "Clinica.Schedulings.Domain/"]
COPY ["Clinica.Schedulings.Infrastructure/Clinica.Schedulings.Infrastructure.csproj", "Clinica.Schedulings.Infrastructure/"]
RUN dotnet restore "./Clinica.Schedulings.Worker/./Clinica.Schedulings.Worker.csproj"
COPY . .
WORKDIR "/src/Clinica.Schedulings.Worker"
RUN dotnet build "./Clinica.Schedulings.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Clinica.Schedulings.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clinica.Schedulings.Worker.dll"]