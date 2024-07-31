#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/QuokkaDev.Templates.Api/QuokkaDev.Templates.Api.csproj", "src/QuokkaDev.Templates.Api/"]
COPY ["src/QuokkaDev.Templates.Application/QuokkaDev.Templates.Application.csproj", "src/QuokkaDev.Templates.Application/"]
COPY ["src/QuokkaDev.Templates.Domain/QuokkaDev.Templates.Domain.csproj", "src/QuokkaDev.Templates.Domain/"]
COPY ["src/QuokkaDev.Templates.Persistence.Ef/QuokkaDev.Templates.Persistence.Ef.csproj", "src/QuokkaDev.Templates.Persistence.Ef/"]
COPY ["src/QuokkaDev.Templates.Query.Dapper/QuokkaDev.Templates.Query.Dapper.csproj", "src/QuokkaDev.Templates.Query.Dapper/"]
RUN dotnet restore "./src/QuokkaDev.Templates.Api/QuokkaDev.Templates.Api.csproj"
COPY . .
WORKDIR "/src/src/QuokkaDev.Templates.Api"
RUN dotnet build "./QuokkaDev.Templates.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./QuokkaDev.Templates.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuokkaDev.Templates.Api.dll"]