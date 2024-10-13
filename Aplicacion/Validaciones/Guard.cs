namespace Aplicacion.Validaciones;

public static class Guard
{
    public static void ValidarCadena(string cadena, string mensajeError)
    {
        if (string.IsNullOrEmpty(cadena))
            throw new FormatException(mensajeError);
    }
}