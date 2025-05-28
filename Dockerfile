FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Создаем папку для сертификата и открываем порты
RUN mkdir -p /app/https
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["App/App.csproj", "App/"]
RUN dotnet restore "App/App.csproj"
COPY . .
WORKDIR "/src/App"
RUN dotnet build "App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "App.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app

# Копируем опубликованное приложение
COPY --from=publish /app/publish .

# Копируем сертификат (должен лежать в ./https/certificate.pfx относительно Dockerfile)
COPY ./App/https/certificate.pfx /app/https/certificate.pfx

# Устанавливаем права (если требуется)
RUN chmod 644 /app/https/certificate.pfx

ENTRYPOINT ["dotnet", "App.dll"]