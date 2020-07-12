using System;
using System.Text;

namespace BrTypes
{
    public static class Masks
    {
        private const char PlaceHolder = '#';
        
        /// <summary>
        /// Apply an specified mask to an specified unmasked value.
        /// </summary>
        /// <remarks>
        /// Apply will replace each '#' in the mask by its respective character value.
        /// Any other character in mask will be considered as a mask character.
        /// </remarks>
        /// <param name="mask">Mask to be applied to a value</param>
        /// <param name="value">Unmasked value</param>
        /// <returns>Value with a mask applied to it</returns>
        public static string Apply(string mask, string value)
        {
            if (string.IsNullOrEmpty(mask))
                throw new ArgumentNullException(nameof(mask), "Parameter 'mask' should not be null");
            
            if (value == null)
                return null;

            Span<char> maskedValue = stackalloc char[mask.Length];
            int valueIndex = 0;
            int i;
            for (i = 0; i < mask.Length && valueIndex < value.Length; i++)
                maskedValue[i] = mask[i] == PlaceHolder ? value[valueIndex++] : mask[i];
            return new string(maskedValue[..i]);
        }
    }
}