using System.Globalization;

namespace Vaquinha.App.Extensions
{
    public static class IntExtension
    {
        public static string ToBRLString(this int valor)
        {
            return valor.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
        }
    }
}