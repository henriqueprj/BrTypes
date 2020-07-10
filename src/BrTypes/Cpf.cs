using System;
using System.ComponentModel;
using BrTypes.Converters;

namespace BrTypes
{
    /// <summary>
    /// Representa um CPF.
    /// </summary>
    [TypeConverter(typeof(CpfConverter))]
    public readonly struct Cpf : IEquatable<Cpf>
    {
        private readonly string _numero;

        public static readonly Cpf Empty = new Cpf();

        private Cpf(string cpfValido)
        {
            _numero = cpfValido;
        }

        /// <summary>
        /// Retorna o valor base do Cpf (primeiros 9 digitos).
        /// </summary>
        public string Base => _numero[..9];

        /// <summary>
        /// Retorna os digitos verificadores do Cpf.
        /// </summary>
        public string DV => _numero[9..];

        public static Cpf Parse(string s)
        {
            if (!TryParse(s, out var cpfValido))
                throw new CpfInvalidoException(s);

            return cpfValido;
        }

        /// <summary>
        /// Tenta converter uma string em <see cref="Cpf"/>. Retorna true caso tenha sucesso.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool TryParse(string s, out Cpf cpf)
        {
            if (!Validar(s, true, out var cpfValido))
            {
                cpf = default;
                return false;
            }

            cpf = new Cpf(cpfValido);
            return true;
        }

        /// <summary>
        /// Determina se um determinada string é ou não um Cpf válido.
        /// </summary>
        /// <param name="numero">Valor representando um Cpf.</param>
        /// <returns></returns>
        public static bool IsValid(string numero) => Validar(numero, false, out _);

        public static bool operator ==(Cpf left, Cpf right)
        {
            return string.Equals(left._numero, right._numero, StringComparison.Ordinal);
        }

        public static bool operator !=(Cpf left, Cpf right)
        {
            return !(left == right);
        }

        public static implicit operator Cpf(string numero)
            => new Cpf(numero);

        private static bool Validar(string numero, bool returnValue, out string cpf)
        {
            var pos = 0;
            var ultimoChar = '0';
            var todosCharsIdenticos = true;
            var totalDigito1 = 0;
            var totalDigito2 = 0;
            var dv1Calculado = 0;

            Span<char> digitos = stackalloc char[11];
            cpf = default;

            foreach (var c in numero)
            {
                if (c == '.' || c == '-')
                    continue;

                var digito = c - '0';
                if (digito < 0 || digito > 9)
                    return false;

                if (pos != 0 && ultimoChar != c)
                    todosCharsIdenticos = false;

                ultimoChar = c;

                if (pos < 9)
                {
                    totalDigito1 += digito * (10 - pos);
                    totalDigito2 += digito * (11 - pos);
                }
                else if (pos == 9)
                {
                    dv1Calculado = totalDigito1 % 11;
                    dv1Calculado = dv1Calculado < 2 ? 0 : 11 - dv1Calculado;

                    if (digito != dv1Calculado)
                        return false;
                }
                else if (pos == 10)
                {
                    totalDigito2 += dv1Calculado * 2;
                    var dv2Calculado = totalDigito2 % 11;
                    dv2Calculado = dv2Calculado < 2 ? 0 : 11 - dv2Calculado;

                    if (digito != dv2Calculado)
                        return false;
                }

                digitos[pos] = c;
                pos++;
            }

            if (todosCharsIdenticos)
                return false;

            if (returnValue)
            {
                cpf = numero.Length == 11 ? numero : new string(digitos);
            }

            return true;
        }

        public bool Equals(Cpf other)
        {
            return string.Equals(_numero, other._numero);
        }

        public override bool Equals(object obj)
        {
            return obj is Cpf other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _numero != null ? _numero.GetHashCode() : 0;
        }

        public override string ToString() => _numero ?? string.Empty;

        /// <summary>
        /// Converte um <see cref="Cpf"/> em <see cref="System.String"/> de acordo com o estilo de formatação informado.
        /// </summary>
        /// <param name="estilo">Estilo de formatação</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Caso o estilo de formatação informado não seja válido.</exception>
        public string ToString(EstiloFormatacaoCpf estilo) =>
            estilo switch
            {
                EstiloFormatacaoCpf.Nenhum => _numero ?? string.Empty,
                EstiloFormatacaoCpf.Padrao => Masks.Apply(Masks.Cpf, _numero),
                _ => throw new ArgumentException("O estilo informado não é válido")
            };
    }
}