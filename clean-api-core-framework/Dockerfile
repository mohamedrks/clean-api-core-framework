FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all project files
COPY ./clean-api-core-framework/clean-api-core-framework.csproj ./clean-api-core-framework/
COPY ./Application/Application.csproj ./Application/
COPY ./Infrastructure/Infrastructure.csproj ./Infrastructure/
COPY ./Persistence/Persistence.csproj ./Persistence/

# Restore
RUN dotnet restore "./clean-api-core-framework/clean-api-core-framework.csproj"

# Copy everything
COPY . .

# Build
WORKDIR /src/clean-api-core-framework
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./clean-api-core-framework.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "clean-api-core-framework.dll"]