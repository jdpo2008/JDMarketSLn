#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Web/JDMarketSLn.Web.UI/JDMarketSLn.Web.UI.csproj", "src/Web/JDMarketSLn.Web.UI/"]
RUN dotnet restore "src/Web/JDMarketSLn.Web.UI/JDMarketSLn.Web.UI.csproj"
COPY . .
WORKDIR "/src/src/Web/JDMarketSLn.Web.UI"
RUN dotnet build "JDMarketSLn.Web.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JDMarketSLn.Web.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JDMarketSLn.Web.UI.dll"]
