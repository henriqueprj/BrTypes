using System;
using System.Diagnostics.CodeAnalysis;

namespace BrTypes
{
    public interface IDigitsFiller
    {
        bool TryParse([NotNullWhen(true)]string? s, ref Span<int> digits);
    }
}