# Documentación

## Migraciones

Crear

    dotnet ef migrations add InitialCreate --project ./Persistencia/ --startup-project ./API/ --output-dir ./Data/Migrations/

Actualizar

    dotnet ef database update --project ./Persistencia/ --startup-project ./API/

## Consultas

Consultas basicas URL

- GET
```
   http://localhost:9000/api/sicer/candidato
```
- GET
```
http://localhost:9000/api/sicer/candidato/{id}
```

- POST
```
http://localhost:9000/api/sicer/candidato
```
Body
```
    {
    "id": 0,
    "nombreCompleto": "string",
    "fechaNacimiento": "2024-05-01T23:00:05.790Z",
    "genero": "string",
    "telefono": "string",
    "ciudadRecidencia": "string",
    "email": "string",
    "descripcionHabilidades": "string",
    "experiencia": "string",
    "urlLinkedIn": "string"
    }
```


- PUT
```
http://localhost:9000/api/sicer/candidato/{id}
```

Query 

    id

Body
```
    {
    "id": 0,
    "nombreCompleto": "string",
    "fechaNacimiento": "2024-05-01T23:00:05.790Z",
    "genero": "string",
    "telefono": "string",
    "ciudadRecidencia": "string",
    "email": "string",
    "descripcionHabilidades": "string",
    "experiencia": "string",
    "urlLinkedIn": "string"
    }
```

- DELETE
```
http://localhost:9000/api/sicer/candidato/{id}
```

Consultas basicas URL

```
SELECT * FROM Candidato;
```
```
SELECT * FROM Candidato WHERE NombreCompleto LIKE '%Nombre%';
```
```
SELECT * FROM Candidato WHERE IdCandidato = id;
```
```
UPDATE Candidato SET NombreCompleto = 'NuevoNombre' WHERE IdCandidato = id;
```
```
DELETE FROM Candidato WHERE IdCandidato = id;
```
```
SELECT * FROM Candidato WHERE Genero = 'Genero';
```
```
SELECT * FROM Candidato WHERE CiudadRecidencia = 'ciudad';
```
```
SELECT COUNT(*) FROM Candidato;
```

ENPOINTS
 - Consulta #1

     - URL

        ```
        http://localhost:9000/api/sicer/candidato/por-ciudad?ciudad=NombreCiudad
        ```
    
     - Sql

        ```
        SELECT * FROM Candidato WHERE CiudadRecidencia = 'NombreCiudad';
        ```

---

 - Consulta #2

     - URL

        ```
        http://localhost:9000/api/sicer/candidato/buscar?nombre=Nombre
        ```

     - Sql

        ```
        SELECT * FROM Candidato WHERE NombreCompleto LIKE '%Nombre%';
        ```


---

 - Consulta #3

     - URL

        ```
        http://localhost:9000/api/sicer/candidato/por-genero?genero=genero
        ```

     - Sql

        ```
        SELECT * FROM Candidato WHERE Genero = 'Genero';
        ```


---

 - Consulta #4

     - URL

        ```
        http://localhost:9000/api/sicer/candidato/por-experiencia?experienciaMinima=10
        ```


---


 - Consulta #5

     - URL

        ```
        http://localhost:9000/api/sicer/candidato/cantidad
        ```

     - Sql

        ```
        SELECT COUNT(*) FROM Candidato;
        ```


---

 - Consulta #6

     - URL

        ```
        http://localhost:9000/api/sicer/candidato/filtro-por-edad?edadMinima=40&edadMaxima=50
        ```


---
