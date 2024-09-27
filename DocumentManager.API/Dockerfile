# Stage 1: Build stage with SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
EXPOSE 80
EXPOSE 443

# Copy all project files and restore dependencies
COPY ["DocumentManager.API/DocumentManager.API.csproj", "DocumentManager.API/"]
COPY ["DocumentManager.Infrastructure/DocumentManager.Infrastructure.csproj", "DocumentManager.Infrastructure/"]
COPY ["DocumentManager.Presentation/DocumentManager.Presentation.csproj", "DocumentManager.Presentation/"]
RUN dotnet restore "DocumentManager.API/DocumentManager.API.csproj"

# Copy the remaining files and build the project
COPY . .
WORKDIR "/src/DocumentManager.API"
RUN dotnet build "./DocumentManager.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 2: Publish
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DocumentManager.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 3: Final runtime stage with ASP.NET Core runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Install telnet and bash
RUN apt-get update && apt-get install -y telnet bash && rm -rf /var/lib/apt/lists/*

# Copy the wait-for-it script and ensure it's executable
COPY DocumentManager.API/wait-for-it.sh /usr/local/bin/wait-for-it.sh
RUN chmod +x /usr/local/bin/wait-for-it.sh

# Entry point for the API
ENTRYPOINT ["bash", "/usr/local/bin/wait-for-it.sh", "sqlserver", "1433", "--", "dotnet", "DocumentManager.API.dll"]
