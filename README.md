
## Requisitos
- Docker
- .NET 8 SDK
- SQL Server

## Pasos para correr la API
1. Levanta OpenLDAP con Docker: docker run --name openldap-container -d   -p 389:389 -p 636:636   -e LDAP_ORG="Example Organization"   -e LDAP_DOMAIN="example.com"   -e LDAP_ADMIN_PASSWORD="admin123"   -e LDAP_CONFIG_PASSWORD="config123"   osixia/openldap:1.5.0
2. Corre la API: dotnet run o ejecución desde Visual Studio
3. Probar los endpoints expuestos `/api/authenticate`

## Ejemplo de peticiones
**POST Authenticate/authenticate**
**POST **POST Authenticate/authenticate****

Adjuntos en colección de PostMan
