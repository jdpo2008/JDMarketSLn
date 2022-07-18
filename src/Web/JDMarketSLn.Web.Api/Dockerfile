#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Web/JDMarketSLn.Web.Api/JDMarketSLn.Web.Api.csproj", "src/Web/JDMarketSLn.Web.Api/"]
COPY ["src/Infrastructure/JdMarketSln.Infrastructure.Shared/JdMarketSln.Infrastructure.Shared.csproj", "src/Infrastructure/JdMarketSln.Infrastructure.Shared/"]
COPY ["src/Application/JdMarketSln.Application/JdMarketSln.Application.csproj", "src/Application/JdMarketSln.Application/"]
COPY ["src/Domain/JdMarketSln.Domain/JdMarketSln.Domain.csproj", "src/Domain/JdMarketSln.Domain/"]
COPY ["src/Infrastructure/JdMarketSln.Infrastructure.Identity/JdMarketSln.Infrastructure.Identity.csproj", "src/Infrastructure/JdMarketSln.Infrastructure.Identity/"]
COPY ["src/infrastructure/JdMarketSln.Infrastructure.Persistence/JdMarketSln.Infrastructure.Persistence.csproj", "src/infrastructure/JdMarketSln.Infrastructure.Persistence/"]
RUN dotnet restore "src/Web/JDMarketSLn.Web.Api/JDMarketSLn.Web.Api.csproj"
COPY . .
WORKDIR "/src/src/Web/JDMarketSLn.Web.Api"
RUN dotnet build "JDMarketSLn.Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JDMarketSLn.Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JDMarketSLn.Web.Api.dll"]
