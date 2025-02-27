- [(Produccion)](https://admintasks.onrender.com/)
#  1. Verifica los Requisitos Previos   
Asegúrate de que la nueva PC tenga instalados:  
## Backend:  
  - .NET SDK 6.0+  
  - MySQL Server (si usas MySQL)  
## Frontend:  
  - Node.js (que incluye npm)  

#  2. Descarga el Código   
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