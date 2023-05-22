FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NightSky.API/NightSky.API.csproj", "NightSky.API/"]
RUN dotnet restore "NightSky.API/NightSky.API.csproj"
COPY . .
WORKDIR "/src/NightSky.API"
RUN dotnet build "NightSky.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NightSky.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NightSky.API.dll"]
