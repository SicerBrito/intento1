namespace API.Helpers;
    public class Autorizacion {

        public enum Roles{
            Administrador,
            Gerente,
            Empleado,
            Cliente
        }
        public const Roles rol_predeterminado = Roles.Cliente;

    }
