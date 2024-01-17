using System.Text.RegularExpressions;

namespace Clientes.Api.Extensions
{
    public static class HelperExtensions
    {
        public static string? RemoveCaracterEspecial(this string? text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            return Regex.Replace(text, @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ]+?", string.Empty);
        }
    }
}
