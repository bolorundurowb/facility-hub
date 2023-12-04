FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["facility-hub/facility-hub.csproj", "facility-hub/"]
RUN dotnet restore "facility-hub/facility-hub.csproj"
COPY . .
WORKDIR "/src/facility-hub"
RUN dotnet build "facility-hub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "facility-hub.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "facility-hub.dll"]
