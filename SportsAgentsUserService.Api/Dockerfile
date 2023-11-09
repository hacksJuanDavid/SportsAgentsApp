FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SportsAgentsUserService.Api/SportsAgentsUserService.Api.csproj", "SportsAgentsUserService.Api/"]
RUN dotnet restore "SportsAgentsUserService.Api/SportsAgentsUserService.Api.csproj"
COPY . .
WORKDIR "/src/SportsAgentsUserService.Api"
RUN dotnet build "SportsAgentsUserService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SportsAgentsUserService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SportsAgentsUserService.Api.dll"]
