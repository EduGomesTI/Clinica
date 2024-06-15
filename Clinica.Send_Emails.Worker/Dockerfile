#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Clinica.Send_Emails.Worker/Clinica.Send_Emails.Worker.csproj", "Clinica.Send_Emails.Worker/"]
COPY ["Clinica.Base/Clinica.Base.csproj", "Clinica.Base/"]
COPY ["Clinica.Send_Emails.Infrastructure/Clinica.Send_Emails.Infrastructure.csproj", "Clinica.Send_Emails.Infrastructure/"]
RUN dotnet restore "./Clinica.Send_Emails.Worker/./Clinica.Send_Emails.Worker.csproj"
COPY . .
WORKDIR "/src/Clinica.Send_Emails.Worker"
RUN dotnet build "./Clinica.Send_Emails.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Clinica.Send_Emails.Worker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Clinica.Send_Emails.Worker.dll"]