FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["POI.Api/POI.Api.csproj", "POI.Api/"]
RUN dotnet restore "POI.Api/POI.Api.csproj"
COPY . .
WORKDIR "/src/POI.Api"
RUN dotnet build "POI.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "POI.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "POI.Api.dll"]
