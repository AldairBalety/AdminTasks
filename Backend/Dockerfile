# Imagen base de .NET para la ejecución
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Imagen para compilar la API
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Backend/Backend.csproj", "Backend/"]
RUN dotnet restore "Backend/Backend.csproj"

# Copiar el resto del código y compilarlo
COPY Backend/ Backend/
WORKDIR "/src/Backend"
RUN dotnet publish -c Release -o /app/publish

# Imagen final con la aplicación lista
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Backend.dll"]
