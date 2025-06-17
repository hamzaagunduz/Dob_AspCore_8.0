# 1. Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Solution dosyasını kopyala
COPY Dob_AspCore_8.0.sln ./

# Tüm projeleri kopyala
COPY . ./

# Restore
RUN dotnet restore Dob_AspCore_8.0.sln

# Publish sadece WebApi projesi
WORKDIR /src/Presentation/WebApi
RUN dotnet publish -c Release -o /app/publish

# 2. Runtime aşaması
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "WebApi.dll"]
