using Microsoft.AspNetCore.Authorization;

namespace API.Helpers;
    public class GlobalVerbRoleRequirement : IAuthorizationRequirement {

        public bool IsAllowed(string role, string verb)
        {
            //TODO: Los Verbs se pasan como parámetros a un método llamado IsAllowed => Métodos o Parametros HTTP (GET, POST, PUT y DELETE)

            // Permitir todos los Verbs si el usuario es "Admin"
            if(string.Equals("Administrador", role, StringComparison.OrdinalIgnoreCase)) return true;
            //if(string.Equals("Gerente", role, StringComparison.OrdinalIgnoreCase)) return true;


            // Permitir el Verb "GET" si el usuario es "Support"
            if(string.Equals("Cliente", role, StringComparison.OrdinalIgnoreCase) && string.Equals("GET",verb, StringComparison.OrdinalIgnoreCase)){
                return true;
            };
            //if(string.Equals("Empresa", role, StringComparison.OrdinalIgnoreCase) && string.Equals("GET",verb, StringComparison.OrdinalIgnoreCase)){
            //    return true;
            //};
            //if(string.Equals("Empleado", role, StringComparison.OrdinalIgnoreCase) && string.Equals("GET",verb, StringComparison.OrdinalIgnoreCase)){
            //    return true;
            //};
            //if(string.Equals("Camper", role, StringComparison.OrdinalIgnoreCase) && string.Equals("GET",verb, StringComparison.OrdinalIgnoreCase)){
            //    return true;
            //};


            // ... añadir otras normas si se desea
            return false;
        }

    }
