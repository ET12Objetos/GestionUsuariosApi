# Gestión de Usuarios y Roles

## Conexión de WebAPI a MySql mediante Entity Framework Core

### Instalar Entity Framework Core CLI

Para poder ejecutar los comandos de **Entity Framework Core** es necesario instalar su CLI (**Command Line Interface**), lo cual se realiza una única vez con el siguiente comando:

```
dotnet tool install --global dotnet-ef
```

Si ya se instaló una vez se debe actualizarlo con el siguiente comando:

```
dotnet tool update --global dotnet-ef
```

### Instalar Paquetes Nuget

- Instalar los siguientes paquetes nuget en la versión mas reciente:
    - Microsoft.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore.Design
    - Microsoft.EntityFrameworkCore.Tools
    - Pomelo.EntityFrameworkCore.MySql

Ejercutar los siguientes comandos (desde el directorio de los proyectos):
```
dotnet add Api package Microsoft.EntityFrameworkCore
```

```
dotnet add Api package Microsoft.EntityFrameworkCore.Design
```

```
dotnet add Api package Microsoft.EntityFrameworkCore.Tools
```

```
dotnet add Api package Pomelo.EntityFrameworkCore.MySql
```

### Creación de connection string

La **cadena de conexión** o **connection string** son las credenciales necesarias para conectarse a cualquier tipo de repositorio de datos, por ejemplo una base de datos relacional como MySQL o Postgres

Para configurar el **ConnectionString** se debe editar el archivo **appsettings.json** y agregar el siguiente apartado:

```json
"ConnectionStrings": {
    "ITEM_DB" : "server=IP_SERVIDOR_MYSQL;database=NOMBRE_BD;user=USUARIO;password=CONTRASEÑA"
  }
```

La configuración se debe completar con los datos correspondientes de la computadora en donde se encuentre la base de datos:

```json
"ConnectionStrings": {
    "aplicacion_db" : "Server=localhost;Database=proyectodb;User=root;Password=pass123"
  }
```

El archivo **appsettings.json** queda:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "aplicacion_db" : "Server=localhost;Database=proyectodb;User=root;Password=pass123"
  }
}
```

### Creación clase DbContext

**DbContext** es la clase responsable de mapear cada **DbSet** (lista) en una tabla en la base de datos. En tiempo de ejecución de la aplicación se crea un objeto de la clase **DbContext** y mediante ese objeto se va a poder acceder a cada lista (tabla de la base de datos)

Para ello debemos:
- Crear un directorio con el nombre **Persistencia** dentro del proyecto **Api**
- Dentro del directorio **Persistencia** crear una clase con el nombre **GestionUsuariosDbContext**. Por convención el nombre de la clase debe terminar el *DbContext* y la palabra que lo antecede debe ser el nombre de la aplicación o algun nombre significativo

El contenido de la clase **GestionUsuariosDbContext** :

```csharp
public class GestionUsuariosDbContext : DbContext
{
    public GestionUsuariosDbContext(DbContextOptions<GestionUsuariosDbContext> opciones)
        : base(opciones)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Rol> Roles { get; set; }
}
```

No olvidar agregar los **usings** necesarios para hacer referencias a las entidades **Sing** y **Playlist**, como tambien a la biblioteca de **Entity Framework Core** 

### Agregado de Data Annotations a cada clase del Dominio

Para cada una de la entidades del dominio **relevantes** agregar las anotaciones necesarias para explicitar el comportamiento del atributo en la base de datos

Las anotaciones son caracteristicas que queremos que tengan nuestros atributos en C# a nivel de base de datos y se deben colocar a nivel clase o atributo, en la linea anterior de su definición

Aqui es donde se explita que atributo sera que un atributo **id** será una **[Key]**, **[ForeignKey]**, su tipo de dato especifico, nombre de la tabla a nivel clase, **[Table("nombre")]**, nombre de la columna (atributo), si es NULL , **[Required]**, etc.

Por ejemplo:
```csharp
[Table("NombreDeTabla")]
public abstract class Entidad
{
    [Key]
    [Required]
    public Guid Id { get; protected set; } = Guid.NewGuid();

    [Required]
    [StringLenght(50)]
    public required string Nombre { get; protected set; }

    [Required]
    public bool Habilitado { get; protected set; } = false;
}
``` 

### Configuración Entity Framework Core en la clase Program

En el archivo **Program.cs** se debe indicar que nuestro contexto se debe asociar con la base de datos MySql para ello se debe colocar el siguiente código:

```csharp
var connectionString = builder.Configuration.GetConnectionString("aplicacion_db");

builder.Services.AddDbContext<GestionUsuariosDbContext>(opcion => opcion.UseMySql(connectionString, new MySqlServerVersion("8.0.39"));

builder.Services.AddDbContext<GestionUsuariosDbContext>();
```

Tambien debemos crear la base de datos en MySql de forma explicita para no tener que ejecutar el comando `CREATE DATABASE aplicacion_db` manualmente. Las siguientes lineas realizar la creación de la base de datos de forma automática al momento de ejercutar la aplicación

```csharp
var opciones = new DbContextOptionsBuilder<GestionUsuariosDbContext>();

opciones.UseMySql(connectionString, new MySqlServerVersion("8.0.39"));

var contexto = new GestionUsuariosDbContext(opciones.Options);

contexto.Database.EnsureCreated();
```

### Creación de Migrationes

Para crear la base de datos en MySql con las tablas correspondientes y su debida configuración se debe traducir desde nuestro contexto (C#) a un formato que entienda Entity Framework Core y este en tiempo de ejecución lo traduce dinámicamente a SQL

**Entity Framework Core** (EFC) es un **ORM** (Object Relational Mapping), el objetivo el **EFC** es traducir nuestro objetos en memoria RAM en filas que se encuentran en tablas en la base de datos. Una lista (C#) representa una tabla (SQL), un objeto (C#) representa una fila (SQL)

La ventaja que nos dan las migraciones son de mantener versionada la base de datos, así como **git** mantiene versionado el código fuente, las migraciones permiten mantener versiones de la base de datos permitiendo en caso de ser necesario volver a una versión de la base de datos anterior

Para crear una migración se debe ejecutar el siguiente comando:

```sh
dotnet ef migrations add NOMBRE_MIGRACION --context NOMBRE_CONTEXTO --output-dir DIRECTORIO_MIGRACIONES --project NOMBRE_PROYECTO --startup-project NOMBRE_PROYECTO_EJECUTABLE
```

Ejemplo:

```sh
dotnet ef migrations add MigracionInicial --context GestionUsuariosDbContext --output-dir Persistencia/Migraciones --project Api --startup-project Api
```

- Cada vez que ingresemos un nuevo cambio en el contexto. Por ejemplo agregar un nuevo atributo a una entidad. Se debe realizar una nueva migracion con un nombre distinto a los existentes.

```sh
dotnet ef migrations add UnNuevoCambio --context GestionUsuariosDbContext --output-dir Persistencia/Migraciones --project Api --startup-project Api
```

## Docker

Para crear una imagen a partir del codigo fuente se debe crear un archivo con nombre **Dockerfile** y su contenido depende de la tecnología del código fuente. 

En el caso de C# el archivo **Dockerfile** debe estar en el mismo directorio en donde se encuentre el archivo **.sln** o las carpetas de los proyectos de Api, Biblioteca, Tests, etc.

Para ver el ejemplo del contenido del archivo **Dockerfile** revisar el que se encuentre en el repositorio, se encuentra explicado linea por linea. 

### Generación Imagen de Docker

Una vez que el archivo ya se encuentre terminado se debe generar la imagen de docker con el siguiente comando cuyo formato genérico es:

```sh
docker build -t NombreUsuarioDockerHub/NombreImagen:Version .
```
Por ejemplo para si mi el usuario de dockerhub es **jonathanvgms**, y el nombre de la imagen es **gestionusuarios** y la versión es **1.0**

```sh
docker build -t jonathanvgms/gestionusuarios:1.0 .
```

### Ejecución de imagen docker (Container)

Para ejecutar la imagen (crear el container) se debe ejecutar el siguiente comando:

```sh
docker run -p 5000:5000 -d jonathanvgms/gestionusuarios:1.0
```

El parametro **-p** indica el puerto de acceso externo al container y el puerto de interno expuesto en el container. Por ejemplo si **-p 1234:6789** significa que mediante el puerto **1234** se puede acceder al container y que el trafico de red que llegue al puerto **1234** se va a redireccionar al puerto **6789**

### Dockerhub

Para subir la imagen de docker a dockerhub es necesario, previamente loguearse con el usuario creado en la página **https://hub.docker.com/** con el siguiente comando:

```sh
docker login
```

Para **subir** una imagen al registry de Dockerhub desde nuestra PC se usa el comando:
```sh
docker push jonathanvmgs/gestionusuarios:1.0
```

Para **bajar** una imagen desde registry de Dockerhub a nuestra PC se usa el comando:
```sh
docker pull jonathanvmgs/gestionusuarios:1.0
```

### Docker Compose

A veces es necesario ejecutar mas de una imagen en simulaneo y realizarlo mediante el CLI es muy complejo. Para simplicar ese proceso se suele usar los archivos **docker-compose**.

El arhivo **docker-compose** usa el formato YAML por lo cual es importante respetar la identación de cada linea.

```yml
services:
  db: # nombre de la imagen/container dentro del archivo
    image: postgres:17.0 # nombre la imagen a usar
    restart: always # indica que el container se ejecuta automaticamente al arrancar el sistema operativo
    environment: # se especifican las variables de entorno
      - POSTGRES_PASSWORD=pass123
    ports: # se especifican los puertos externos e internos al container
      - 5001:5432
      
  webapi:
    image: jonathanvmgms/apipostgres6to7ma
    restart: always
    environment:
      - ConnectionStrings__gestionusuarios_db=Server=db;Database=gestionusuarios;Username=postgres;Password=pass123
    ports:
      - 5000:5000
    depends_on: # da un orden de jerarquia al momento de crear los containers, en este caso se indica que primero se debe crear el container db
      - db
```

Para ejecutar el archivo **docker-compose** se debe ejecutar el comando:

```sh
docker compose up -d
```