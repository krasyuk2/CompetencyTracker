FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CompetencyTracker/CompetencyTracker.csproj", "CompetencyTracker/"]
COPY ["CompetencyTracker.Application/CompetencyTracker.Application.csproj", "CompetencyTracker.Application/"]
COPY ["CompetencyTracker.Core/CompetencyTracker.Core.csproj", "CompetencyTracker.Core/"]
COPY ["CompetencyTracker.DataAccess/CompetencyTracker.DataAccess.csproj", "CompetencyTracker.DataAccess/"]
RUN dotnet restore "CompetencyTracker/CompetencyTracker.csproj"
COPY . .
WORKDIR "/src/CompetencyTracker"
RUN dotnet build "CompetencyTracker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CompetencyTracker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CompetencyTracker.dll"]
