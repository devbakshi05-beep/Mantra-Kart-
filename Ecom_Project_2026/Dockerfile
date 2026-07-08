# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Ecom_Project_2026/Ecom_Project_2026.csproj", "Ecom_Project_2026/"]
COPY ["Ecom_Project_2026.DataAccess/Ecom_Project_2026.DataAccess.csproj", "Ecom_Project_2026.DataAccess/"]
COPY ["Ecom_Project_2026.Models/Ecom_Project_2026.Models.csproj", "Ecom_Project_2026.Models/"]
COPY ["Ecom_Project_2026.Utility/Ecom_Project_2026.Utility.csproj", "Ecom_Project_2026.Utility/"]
RUN dotnet restore "./Ecom_Project_2026/Ecom_Project_2026.csproj"
COPY . .
WORKDIR "/src/Ecom_Project_2026"
RUN dotnet build "./Ecom_Project_2026.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Ecom_Project_2026.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecom_Project_2026.dll"]