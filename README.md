
## Requisitos
- Docker
- .NET 8 SDK
- SQL Server

## Pasos para correr la API
1. Levanta OpenLDAP con Docker: docker run --name openldap-container -d   -p 389:389 -p 636:636   -e LDAP_ORG="Example Organization"   -e LDAP_DOMAIN="example.com"   -e LDAP_ADMIN_PASSWORD="admin123"   -e LDAP_CONFIG_PASSWORD="config123"   osixia/openldap:1.5.0
2. En su base de datos ejecutar el script ScritpDB_LDAP.sql
3. Corre la API: 
	* En la solución identificar el archivo appsettings.json y modificar la conexión a la base de datos 
	* Ejecutar el comando dotnet run o ejecución desde Visual Studio
4. Probar los endpoints expuestos

## Ejemplo de peticiones
**POST Authenticate/authenticate**
**POST Authenticate/GenerateToken**

Adjuntos en colección de PostMan: STTecnology LDAP.postman_collection
Debe cambiar la parte de la URL https://localhost:7083 por el puerto establecido en la maquina de prueba