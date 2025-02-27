# Imagen base para .NET
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Imagen para compilar
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Ajusta la ruta si es necesario
COPY ["AdminTasks/Backend/Backend.csproj", "Backend/"]
RUN dotnet restore "Backend/Backend.csproj"

# Copiar el resto del código
COPY AdminTasks/Backend/ Backend/
WORKDIR "/src/Backend"
RUN dotnet publish -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Backend.dll"]
