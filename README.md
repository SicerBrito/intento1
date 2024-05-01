# Documentación

## Migraciones

Crear

    dotnet ef migrations add InitialCreate --project ./Persistencia/ --startup-project ./API/ --output-dir ./Data/Migrations/

Actualizar

    dotnet ef database update --project ./Persistencia/ --startup-project ./API/

## Consultas

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
        ```

     - Sql

        ```
        ```


---

 - Consulta #2

     - URL

        ```
        ```

     - Sql

        ```
        ```


---

 - Consulta #2

     - URL

        ```
        ```

     - Sql

        ```
        ```


---
