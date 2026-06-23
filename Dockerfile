FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "NebrasProjectAPI/NebrasProjectAPI.csproj"
RUN dotnet publish "NebrasProjectAPI/NebrasProjectAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT dotnet NebrasProjectAPI.dll --urls http://0.0.0.0:${PORT:-8080}