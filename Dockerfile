# Imagen base para .NET
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 10000  
# Exponer el puerto 10000

# Imagen para compilar
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar Backend.csproj y los archivos del proyecto
COPY Backend/Backend.csproj Backend/
COPY Backend/ Backend/

# Ejecutar dotnet restore
RUN dotnet restore "Backend/Backend.csproj"

# Publicar el proyecto
WORKDIR "/src/Backend"
RUN dotnet publish -c Release -o /app/publish

# Imagen final
FROM base AS final
WORKDIR /app

# Asegurar que el directorio Backend exista y copiar los archivos de configuración
RUN mkdir -p /app/Backend

# Copiar los archivos de configuración (appsettings)
COPY Backend/appsettings.json /app/Backend/appsettings.json
COPY Backend/appsettings.Development.json /app/Backend/appsettings.Development.json

# Copiar los archivos del build
COPY --from=build /app/publish .

# Asegurar que el contenedor escuche en el puerto correcto usando el valor de la variable de entorno
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
ENTRYPOINT ["dotnet", "Backend.dll"]
