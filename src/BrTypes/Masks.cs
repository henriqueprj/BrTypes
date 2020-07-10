using System;
using System.Text;

namespace BrTypes
{
    public static class Masks
    {
        public const string Cpf = "###.###.###-##";
        public const string Cnpj = "##.###.###/####-##";
        
        /// <summary>
        /// Apply an specified mask to an specified unmasked value.
        /// </summary>
        /// <remarks>
        /// Apply will replace each '#' in the mask by its respective character value.
        /// Any other character in mask will considered as a mask character.
        /// </remarks>
        /// <param name="mask">Mask to be applied to a value</param>
        /// <param name="value">Unmasked value</param>
        /// <returns>Value with a mask applied</returns>
        public static string Apply(string mask, string value)
        {
            if (string.IsNullOrEmpty(mask))
                throw new ArgumentNullException(nameof(mask));

            Span<char> maskedValue = stackalloc char[mask.Length];
            var valueIndex = 0;
            for (var i = 0; i < mask.Length; i++)
            {
                if (mask[i] == '#')
                    maskedValue[i] = value[valueIndex++];
                else
                    maskedValue[i] = mask[i];
            }
            return new string(maskedValue);
        }
    }
}