# Base runtime image (Linux)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage (Linux SDK)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project file and restore
COPY ["Catalog.Api/Catalog.Api.csproj", "Catalog.Api/"]
RUN dotnet restore "./Catalog.Api/Catalog.Api.csproj"

# Copy everything else
COPY . .
WORKDIR "/src/Catalog.Api"

# Build
RUN dotnet build "Catalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Catalog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Catalog.Api.dll"]