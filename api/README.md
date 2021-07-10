# API Exámen técnico FlockIT
Autor: Pablo Insaurralde

# FlockIT API Rest (.NET Core 3.1)

# Uso

La API cuenta con una serie de recursos:

Login de la API con manejo de tokens JWT.

´[HttpPost]´
´[AllowAnonymous]´
´/Usuarios/authenticate´
	
	# Request
	2 parametros en el body del request (nombreUsuario y password)

	Ejemplos:

	Usuario con rol de "Admin":

	´{
	  "nombreUsuario": "admin",
	  "password": "admin"
	}´

	Usuario con rol de "User":

	´{
	  "nombreUsuario": "prueba",
	  "password": "1234"
	}´

	# Response
	´json´

	Ejemplo:
	´{
	  "id": 1,
	  "nombre": "Admin",
	  "apellido": "User",
	  "nombreUsuario": "admin",
	  "password": null,
	  "rol": "Admin",
	  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE2MjU5NDMxNTIsImV4cCI6MTYyNjU0Nzk1MiwiaWF0IjoxNjI1OTQzMTUyfQ.WSkqQYJ4Mhk9rRmEJJuoiwfE9VTjRkO_9FrHo9CICS4"
	}´

	El token recibido debe usarse para llamar a los recursos "protegidos".

´[HttpGet]´
´[Authorize]´
´/Provincias/{nombreProvincia}´

	Buscar latitud y longitud de una provincia de Argentina dado el nombre de la misma.
	
	# Request
	Nombre de la provincia.
	Este recurso es protegido. El token debe pasarse en la cabecera del request como "Bearer {token}"

	Ejemplos:

	´/Provincias/Cordoba´
	´/Provincias/cba´
	´/Provincias/Buenos Aires´
	´/Provincias/Bs As´

	# Response
	´json´

	Ejemplo:

	´{
	  "centroide": {
		"lat": -32.142932663607,
		"lon": -63.8017532741662
	  },
	  "id": "14",
	  "nombre": "Córdoba"
	}´

	Obteniendo la latitud y longitud de la ciudad buscada como el requerimiento indica.



	EXTRAS:
	
	´[Authorize]´
	´[HttpGet]´
	´/Provincias´
		
		Lista todas las provincias de Argentina. Método protegido solo accesible para usuarios dentro del rol "User" y "Admin"

	´[Authorize(Roles = Role.Admin)]´
	´[HttpGet]´
	´/Usuarios´

		Lista todos los usuarios. Método protegido, solo accesible para usuarios dentro del rol Admin.

	´[Authorize(Roles = Role.Admin)]´
	´[HttpGet]´
	´/Usuarios/{id}´

		Muestra información del usuario requerido. Método protegido, solo accesible para usuarios dentro del rol "Admin".

