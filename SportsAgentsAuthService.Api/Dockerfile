FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SportsAgentsAuthService.Api/SportsAgentsAuthService.Api.csproj", "SportsAgentsAuthService.Api/"]
RUN dotnet restore "SportsAgentsAuthService.Api/SportsAgentsAuthService.Api.csproj"
COPY . .
WORKDIR "/src/SportsAgentsAuthService.Api"
RUN dotnet build "SportsAgentsAuthService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SportsAgentsAuthService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportsAgentsAuthService.Api.dll"]
