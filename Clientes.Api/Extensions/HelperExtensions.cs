using System.Text.RegularExpressions;

namespace Clientes.Api.Extensions
{
    public static class HelperExtensions
    {
        public static string? RemoveCaracterEspecial(this string? text) => string.IsNullOrEmpty(text) ? 
            null : Regex.Replace(text, @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ]+?", string.Empty);
        

        public static bool EmailValido(this string? email) => !string.IsNullOrEmpty(email) 
            && Regex.IsMatch(email, "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");

        public static TEnum ConvertEnum<TEnum>(this string stringEnum) where TEnum : struct
            => Enum.TryParse(stringEnum, out TEnum convertedEnum) ? convertedEnum : default;

        public static string FormatarStringTelefone(this string telefone) => Regex.Replace(telefone, @"(\d{2})(\d{5})(\d{4})", "($1) $2-$3");

        public static string FormatDataddMMYY(this DateTime data) => data.ToString("dd/MM/yyyy");
    }
}
