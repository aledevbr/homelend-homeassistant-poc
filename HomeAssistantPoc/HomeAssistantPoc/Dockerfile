#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HomeAssistantPoc/HomeAssistantPoc/HomeAssistantPoc.csproj", "HomeAssistantPoc/HomeAssistantPoc/"]
COPY ["HomeAssistantPoc/HomeAssistantPoc.Client/HomeAssistantPoc.Client.csproj", "HomeAssistantPoc/HomeAssistantPoc.Client/"]
RUN dotnet restore "./HomeAssistantPoc/HomeAssistantPoc/./HomeAssistantPoc.csproj"
COPY . .
WORKDIR "/src/HomeAssistantPoc/HomeAssistantPoc"
RUN dotnet build "./HomeAssistantPoc.csproj" -c $BUILD_CONFIGURATION -o /app/build -r linux-x64

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HomeAssistantPoc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeAssistantPoc.dll"]