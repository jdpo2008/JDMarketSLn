#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NetCoreApiTemplate/NetCoreApiTemplate.WebApi/NetCoreApiTemplate.WebApi.csproj", "NetCoreApiTemplate/NetCoreApiTemplate.WebApi/"]
COPY ["NetCoreApiTemplate/NetCoreApiTemplate.Application/NetCoreApiTemplate.Application.csproj", "NetCoreApiTemplate/NetCoreApiTemplate.Application/"]
COPY ["NetCoreApiTemplate/NetCoreApiTemplate.Domain/NetCoreApiTemplate.Domain.csproj", "NetCoreApiTemplate/NetCoreApiTemplate.Domain/"]
COPY ["NetCoreApiTemplate/NetCoreApiTemplate.Infraestructure.Identity/NetCoreApiTemplate.Infraestructure.Identity.csproj", "NetCoreApiTemplate/NetCoreApiTemplate.Infraestructure.Identity/"]
COPY ["NetCoreApiTemplate/NetCoreApiTemplate.Infraestructure.Persistence/NetCoreApiTemplate.Infraestructure.Persistence.csproj", "NetCoreApiTemplate/NetCoreApiTemplate.Infraestructure.Persistence/"]
COPY ["NetCoreApiTemplate/NetCoreApiTemplate.Infraestructure.Shared/NetCoreApiTemplate.Infraestructure.Shared.csproj", "NetCoreApiTemplate/NetCoreApiTemplate.Infraestructure.Shared/"]
RUN dotnet restore "NetCoreApiTemplate/NetCoreApiTemplate.WebApi/NetCoreApiTemplate.WebApi.csproj"
COPY . .
WORKDIR "/src/NetCoreApiTemplate/NetCoreApiTemplate.WebApi"
RUN dotnet build "NetCoreApiTemplate.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCoreApiTemplate.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreApiTemplate.WebApi.dll"]