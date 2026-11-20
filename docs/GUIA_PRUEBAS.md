# Pruebas de Lab10-AntonyHuayhua

1. Preparar PostgreSQL seg?n README.md y ejecutar la API.
2. En Swagger, POST /api/auth/register con:
   `{"username":"lukas","password":"Password123!","email":"lukas@example.com"}`.
3. POST /api/auth/login con el mismo username/password. Copiar token.
4. En Authorize ingresar el token JWT (el esquema HTTP agrega Bearer).
5. POST /api/tickets con `{"title":"Prueba Lab 10","description":"Acceso al sistema"}`.
6. GET /api/tickets y GET /api/tickets/{id}: comprobar el ticket creado.
7. POST /api/responses con el ticketId y `"message":"Revisando el incidente"`.
8. GET /api/responses/ticket/{ticketId}: comprobar el historial.
9. PATCH /api/tickets/{id}/status con `{"status":"cerrado"}`.
10. Sin token, GET /api/tickets debe responder 401.

La gesti?n de roles y asignaciones requiere un usuario con rol admin, asignado
previamente en PostgreSQL. El registro p?blico no concede privilegios de administrador.

La compilaci?n y las verificaciones de arranque/Swagger/JWT no sustituyen las
pruebas de persistencia: estas requieren PostgreSQL configurado y accesible.

Verificacion real completada contra support_tickets_db. Resultados en EVIDENCIA_API.json.
