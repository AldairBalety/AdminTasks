# 🚀 AutoTrafic Facebook News Bot

**Sistema Inteligente de Automatización de Contenido para Facebook**

AutoTrafic es un bot avanzado que genera y publica automáticamente noticias de tecnología de alta calidad en páginas de Facebook, optimizado para engagement y monetización.

## ✨ Características Principales

- 🧠 **IA Avanzada**: Usa OpenAI GPT-3.5-turbo para contenido inteligente
- 🎨 **Imágenes Personalizadas**: Genera imágenes únicas con DALL-E 2
- 📱 **Publicación Automática**: Integración completa con Facebook Graph API
- 📊 **Estrategia de Contenido**: Sistema inteligente de categorización y CTAs
- ⏰ **Horarios Óptimos**: Publicación en momentos de mayor engagement
- 💰 **Monetización**: Optimizado para maximizar clics y conversiones

## 🔧 Configuración Inicial

### 1. Configurar OpenAI

Agrega tu API Key de OpenAI en `appsettings.Development.json`:

```json
{
  "OpenAI": {
    "ApiKey": "sk-tu-api-key-de-openai"
  }
}
```

### 2. Configurar Facebook (Opcional - para publicación automática)

```json
{
  "Facebook": {
    "PageAccessToken": "tu-page-access-token",
    "PageId": "tu-page-id",
    "AppId": "tu-app-id",
    "AppSecret": "tu-app-secret"
  }
}
```

### 3. Configurar el Bot

```json
{
  "NewsBot": {
    "IntervalMinutes": 240,
    "AutoPublish": false
  }
}
```

## 🚀 Instalación y Ejecución

### Instalar dependencias:
```bash
dotnet restore
```

### Compilar:
```bash
dotnet build
```

### Ejecutar:
```bash
dotnet run
```

La aplicación estará disponible en:
- **API**: http://localhost:4000
- **Swagger**: http://localhost:4000/swagger
- **Configuración Facebook**: http://localhost:4000/auth/facebook-setup

## 📋 Endpoints Principales

### 🚀 Generar Noticia
```
POST /api/news/generate
```

Ejemplo de request:
```json
{
  "category": "AI",
  "customTopic": "Nuevos avances en inteligencia artificial",
  "cta": "LearnMore",
  "generateImage": true,
  "publishToFacebook": false
}
```

### 📊 Estadísticas
```
GET /api/news/stats
```

### 📋 Posts Pendientes
```
GET /api/news/pending
```

## 🔧 Configuración de Facebook

### Paso 1: Crear App en Facebook Developers
1. Ve a [Facebook Developers](https://developers.facebook.com/apps/)
2. Crea una nueva app tipo **Business**
3. Agrega el producto **Facebook Login**
4. Configura los permisos: `pages_show_list`, `pages_read_engagement`, `pages_manage_posts`

### Paso 2: Obtener Tokens
1. Ve a **Graph API Explorer** en Facebook Developers
2. Genera un **User Access Token** con permisos de páginas
3. Usa el endpoint: `/me/accounts` para obtener el **Page Access Token**
4. Guarda tu **Page ID** y **Page Access Token**

### Paso 3: App Review (Para publicación automática)
Para publicación automática en Facebook, necesitas completar el **App Review** de Facebook:

1. Sube tu **Política de Privacidad**: http://localhost:4000/privacy-policy
2. Configura **Eliminación de Datos**: http://localhost:4000/data-deletion
3. Solicita **App Review** para `pages_manage_posts`
4. Proporciona video demostrativo de la funcionalidad

### URLs de Ayuda
- 🔧 [Guía de Configuración](http://localhost:4000/auth/facebook-setup)
- 🔍 [Validar Token](http://localhost:4000/auth/facebook-tokens?token=TU_USER_TOKEN)
- ✅ [Validar Configuración](http://localhost:4000/auth/validate-facebook-token)
- 🔐 [Verificar Permisos](http://localhost:4000/auth/check-page-permissions)

## 📄 Políticas (Requeridas para Facebook)

- **Política de Privacidad**: http://localhost:4000/privacy-policy
- **Términos de Servicio**: http://localhost:4000/terms-of-service
- **Eliminación de Datos**: http://localhost:4000/data-deletion

## 🎯 Categorías de Noticias

- `Technology` - Tecnología general
- `AI` - Inteligencia Artificial
- `Programming` - Programación y desarrollo
- `WebDevelopment` - Desarrollo web
- `MobileDevelopment` - Desarrollo móvil
- `DataScience` - Ciencia de datos
- `Cybersecurity` - Ciberseguridad
- `StartupNews` - Noticias de startups
- `TechTrends` - Tendencias tecnológicas

## 💡 Call-to-Actions

- `ReadMore` - Leer más
- `LearnMore` - Aprender más
- `DiscoverMore` - Descubrir más
- `GetStarted` - Empezar
- `Subscribe` - Suscribirse
- `Download` - Descargar
- `TryNow` - Probar ahora
- `BookDemo` - Agendar demo
- `ContactUs` - Contactar

## 🔄 Sistema Automático

El bot funciona automáticamente:

1. **Horarios Inteligentes**: Publica en horarios de mayor engagement (9 AM, 12 PM, 3 PM, 6 PM, 9 PM)
2. **Selección de Contenido**: Algoritmo inteligente para seleccionar categorías según día/hora
3. **Generación de Imágenes**: Crea imágenes únicas para cada noticia
4. **Manejo de Errores**: Gestión robusta de errores de Facebook y OpenAI

## ⚠️ Notas Importantes

### Sin App Review
Si no has completado el App Review de Facebook:
- ✅ El sistema genera contenido perfectamente
- ✅ Las imágenes se crean correctamente
- ✅ El contenido queda listo para publicación manual
- ❌ La publicación automática está limitada

### Con App Review Aprobado
Una vez aprobado el App Review:
- ✅ Publicación completamente automática
- ✅ Sin intervención manual necesaria
- ✅ Sistema funciona 24/7

## 🛠️ Desarrollo

### Estructura del Proyecto
```
Backend/
├── Models/
│   └── NewsModels.cs          # Modelos de datos
├── Services/
│   └── NewsService.cs         # Lógica principal del bot
├── HostedServices/
│   └── NewsProcessRelease.cs  # Servicio automático
├── Program.cs                 # Configuración y endpoints
└── appsettings.json          # Configuración
```

### Dependencias Principales
- `OpenAI-DotNet` - Cliente de OpenAI
- `Swashbuckle.AspNetCore` - Documentación Swagger
- `Microsoft.AspNetCore` - Framework web

## 📞 Soporte

- **Email**: support@autotrafic.com
- **Documentación**: http://localhost:4000/swagger
- **Configuración**: http://localhost:4000/auth/facebook-setup

---

© 2025 AutoTrafic. Sistema Inteligente de Automatización de Contenido.   
Si el código está en un repositorio de GitHub:  
` git clone <URL_DEL_REPOSITORIO>  `
` cd <NOMBRE_DEL_PROYECTO>  `
Si no, copia los archivos manualmente en la nueva PC.  

#  3. Restaura las Dependencias 
Ejecuta estos comandos:  

## Backend:  
` cd Backend  `
` dotnet restore  `

## Frontend:  
` cd frontend  `
` npm install  `

#  4. Configura la Base de Datos   
la base de datos esta dada de alta en un servidor, 
por lo que no es necesario instancia una en su equipo

#  5. Ejecuta el Proyecto   
dentro de .vscode hay 3 archivos para correr 2 maneras distintas la app, 
una es para debuggear el back y otra para ejecuta el fron y el back,

## Opcion 1
ejecute `control` + `shif` + `p`
escriba `Run Task`
luego en `iniciar todo`

## Opcion 2
es mas recomendable
ejecutar 2 consolas y ejecutar el backend y el frontend por separado
![ejemplo de cli con 2 ejecucions](/assets/Captura%20de%20pantalla%202025-02-26%20a%20la(s)%207.06.26 p.m..png)

### Ejecutar Backend:  
` cd Backend  `
` dotnet run  `

### Ejecutar Frontend:  
` cd frontend  `
` npm start  `

# 6 interaccion con base de datos
`SELECT * FROM bmpptmqkwzowojwtv6ha.Users;`

`SELECT * FROM bmpptmqkwzowojwtv6ha.Task;`

`call LoginUser("maria@example.com", "password456");`

`call AddTask("don quijote de la mancha", "libro", 1);`

`call AddUser("Aldair", "Gonzalez Conde", "aldebranbarsa@gmail.com", "password123");`

`call DeleteTask(1);`

`call GetTasksByUserId(1);`

`call GetUserById(1);`

`call UpdateTask(1, "don quijote de la mancha", "libro textual", 1);`

`call UpdateUser(1, "Aldair", "Gonzalez Conde", "aldebranbarsa@gmail.com", "password1234");`


# 7 creacion e implementacion del app

## creacion de backend minimal api
`dotnet new web -n Backend`

`cd Backend`

`dotnet add package Swashbuckle.AspNetCore --version 6.0.0`

`dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer`

`dotnet add package MySql.Data`

`dotnet add package System.IdentityModel.Tokens.Jwt`

`dotnet add package Microsoft.IdentityModel.Tokens`

## creacion de frontend 
`npx create-react-app frontend --template typescript`

`cd frontend`

`npm install axios`

`npm install react-router-dom axios`

`npm install --save-dev @types/react`

`npm install styled-components`

`npm install bootstrap`

# 8 interaccion con la aplicacion 
## accesos

`admin@gmail.com`

`qwe123`

una vez logeado podra hacer uso del app,
se puede agregar tareas de forma sencilla 
y entendible para el usuario, 
pestaña de logeo

![pestaña de logeo](/assets/Captura%20de%20pantalla%202025-02-26%20a%20la(s)%207.21.27 p.m..png)

inmediatamente lo mandar a las tareas

![pestaña de tareas](/assets/Captura%20de%20pantalla%202025-02-26%20a%20la(s)%207.36.24 p.m..png)

ahi podra agregar, editar o eliminar cualquier tarea.
al dar click en el boton de editar los datos se pondran 
en los campos de agregar y cambiara el boton.

si una tarea no ah sido completada apareceera en amarillo.

si una tarea ya ah sido completada aparecera en verde.

![edicion de tareas](/assets/Captura%20de%20pantalla%202025-02-26%20a%20la(s)%207.36.45 p.m..png)

en la parte de del perfil podra actualizar sus datos como Nombre, Apellido y correo