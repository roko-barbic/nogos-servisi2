# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
# Copy the project file and restore dependencies
COPY ["NogosServisi.csproj", "./"]
RUN dotnet restore "NogosServisi.csproj"
# Copy the remaining source code and build
COPY . .
RUN dotnet build "NogosServisi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NogosServisi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image (using the SDK image so you can run EF Core commands manually if needed)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final
WORKDIR /app
# (Optional) Install EF Core tools for manual migration commands
RUN dotnet tool install --global dotnet-ef --version 8.0.0
ENV PATH="$PATH:/root/.dotnet/tools"
# Copy published app to run normally
COPY --from=publish /app/publish .
# Copy entire project source (including Migrations) so you can run EF Core commands manually
COPY --from=build /src /app/src
EXPOSE 8080
# Run the published application (no automatic migration)
ENTRYPOINT ["dotnet", "NogosServisi.dll"]
