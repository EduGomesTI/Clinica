#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt-get update && apt-get install -y curl
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Clinica.Main.WebApi/Clinica.Main.WebApi.csproj", "Clinica.Main.WebApi/"]
COPY ["Clinica.Base/Clinica.Base.csproj", "Clinica.Base/"]
COPY ["Clinica.Main.Application/Clinica.Main.Application.csproj", "Clinica.Main.Application/"]
COPY ["Clinica.Main.Domain/Clinica.Main.Domain.csproj", "Clinica.Main.Domain/"]
COPY ["Clinica.Main.Infrastrucure/Clinica.Main.Infrastructure.csproj", "Clinica.Main.Infrastrucure/"]
COPY ["Clinica.Main.Presentation/Clinica.Main.Presentation.csproj", "Clinica.Main.Presentation/"]
RUN dotnet restore "./Clinica.Main.WebApi/./Clinica.Main.WebApi.csproj"
COPY . .
WORKDIR "/src/Clinica.Main.WebApi"
RUN dotnet build "./Clinica.Main.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Clinica.Main.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clinica.Main.WebApi.dll"]