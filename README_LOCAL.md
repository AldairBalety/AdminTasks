# AutoTrafic - Bot de Facebook para Noticias de Tecnología

## 🚀 Configuración Local

Para ejecutar el proyecto localmente, necesitas configurar las claves API:

### 1. Crear archivo de configuración local

Crea el archivo `Backend/appsettings.Local.json` con el siguiente contenido:

```json
{
  "OpenAI": {
    "ApiKey": "tu-clave-api-de-openai-aqui"
  },
  "Facebook": {
    "PageAccessToken": "tu-token-de-acceso-de-facebook",
    "PageId": "tu-id-de-pagina-de-facebook"
  }
}
```

### 2. Obtener las claves necesarias

#### OpenAI API Key
1. Ve a [OpenAI API Keys](https://platform.openai.com/api-keys)
2. Crea una nueva clave API
3. Cópiala al archivo `appsettings.Local.json`

#### Facebook Page Access Token
1. Ve a [Facebook Developers](https://developers.facebook.com/)
2. Crea una aplicación de Facebook
3. Configura la página de Facebook
4. Obtén el Page Access Token y Page ID

### 3. Ejecutar el proyecto

```bash
# Backend
dotnet run --project Backend

# Frontend  
npm start --prefix frontend
```

## 📋 Endpoints de la API

- `POST /api/news/generate` - Generar nueva noticia
- `GET /swagger` - Documentación de la API

## 🔒 Seguridad

- Los archivos `appsettings.Local.json` están en `.gitignore`
- Nunca subas claves API al repositorio
- Usa variables de entorno en producción
