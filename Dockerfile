﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Install Node.js
RUN curl -fsSL https://deb.nodesource.com/setup_14.x | bash - && apt-get install -y nodejs && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["facilityhub/facilityhub.csproj", "facilityhub/"]
RUN dotnet restore "facilityhub/facilityhub.csproj"
COPY . .
WORKDIR "/src/facilityhub"
RUN dotnet build "facilityhub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "facilityhub.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "facilityhub.dll"]
