# Imagen base para .NET
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 10000  
RUN find .
# Exponer el puerto 10000

# Imagen para compilar
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
RUN find .

# Copiar Backend.csproj y los archivos del proyecto
COPY Backend/Backend.csproj Backend/
COPY Backend/ Backend/

# Ejecutar dotnet restore
RUN dotnet restore "Backend/Backend.csproj"
RUN find .

# Publicar el proyecto
WORKDIR "/src/Backend"
RUN dotnet publish -c Release -o /app/publish
RUN find .

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Asegurar que el contenedor escuche en el puerto correcto usando el valor de la variable de entorno
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
ENTRYPOINT ["dotnet", "Backend.dll"]
