FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app

# Instala netcat para usar con el script
RUN apt-get update && apt-get install -y netcat-openbsd

# Copia app y script
COPY --from=build /app/publish .
COPY wait-for-it.sh .
RUN chmod +x wait-for-it.sh

# Usa wait-for-it para esperar a SQL Server
ENTRYPOINT ["dotnet", "TodoApi.dll"]
