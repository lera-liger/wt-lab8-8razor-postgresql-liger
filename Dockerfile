# ===== Этап 1: Сборка =====
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем только csproj и восстанавливаем зависимости (для кэширования)
COPY *.csproj ./
RUN dotnet restore

# Копируем остальные файлы и публикуем
COPY . ./
# Указываем конкретный проект (а не .sln), чтобы избежать предупреждения NETSDK1194
RUN dotnet publish wt-lab8-8razor-postgresql-liger.csproj -c Release -o /app/publish /p:UseAppHost=false

# ===== Этап 2: Запуск =====
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "wt-lab8-8razor-postgresql-liger.dll"]