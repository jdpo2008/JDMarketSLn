#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["JDMarketSLn/JDMarketSLn.WebApi/JDMarketSLn.WebApi.csproj", "JDMarketSLn/JDMarketSLn.WebApi/"]
COPY ["JDMarketSLn/JDMarketSLn.Application/JDMarketSLn.Application.csproj", "JDMarketSLn/JDMarketSLn.Application/"]
COPY ["JDMarketSLn/JDMarketSLn.Domain/JDMarketSLn.Domain.csproj", "JDMarketSLn/JDMarketSLn.Domain/"]
COPY ["JDMarketSLn/JDMarketSLn.Infraestructure.Identity/JDMarketSLn.Infraestructure.Identity.csproj", "JDMarketSLn/JDMarketSLn.Infraestructure.Identity/"]
COPY ["JDMarketSLn/JDMarketSLn.Infraestructure.Persistence/JDMarketSLn.Infraestructure.Persistence.csproj", "JDMarketSLn/JDMarketSLn.Infraestructure.Persistence/"]
COPY ["JDMarketSLn/JDMarketSLn.Infraestructure.Shared/JDMarketSLn.Infraestructure.Shared.csproj", "JDMarketSLn/JDMarketSLn.Infraestructure.Shared/"]
RUN dotnet restore "JDMarketSLn/JDMarketSLn.WebApi/JDMarketSLn.WebApi.csproj"
COPY . .
WORKDIR "/src/JDMarketSLn/JDMarketSLn.WebApi"
RUN dotnet build "JDMarketSLn.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JDMarketSLn.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JDMarketSLn.WebApi.dll"]