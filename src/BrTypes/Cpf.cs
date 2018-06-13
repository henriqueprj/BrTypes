using System;
using System.ComponentModel;
using BrTypes.Converters;

namespace BrTypes
{
    /// <summary>
    /// Representa um CPF.
    /// </summary>
    [TypeConverter(typeof(CpfConverter))]
    public sealed class Cpf : IComparable, IComparable<Cpf>, IEquatable<Cpf>
    {
        private readonly string _numero;

        /// <summary>
        /// Inicializa uma nova instância de <see cref="Cpf"/>.
        /// </summary>
        /// <param name="numero">número do CPF</param>
        /// <exception cref="ArgumentNullException">Caso <paramref name="numero"/> seja vazio ou null.</exception>
        /// <exception cref="CpfInvalidoException">Caso o <paramref name="numero"/> seja válido.</exception>
        public Cpf(string numero)
        {
            if (numero == null) throw new ArgumentNullException(nameof(numero));

            if (!Validar(numero, true, out var chars))
                throw new CpfInvalidoException(numero);
            
            _numero = new string(chars);
        }

        private Cpf(char[] cpf)
            => _numero = new string(cpf);
        
        /// <summary>
        /// Retorna o valor base do Cpf (primeiros 9 digitos).
        /// </summary>
        public string Base
            => _numero.Substring(0, 9);

        /// <summary>
        /// Retorna os digitos verificadores do Cpf.
        /// </summary>
        public string DigitosVerificadores
            => _numero.Substring(9, 2);

        public int CompareTo(Cpf other) 
            => string.Compare(_numero, other?._numero, StringComparison.Ordinal);

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            if (!(obj is Cpf other))
                throw new ArgumentException($"Object is not a {typeof(Cpf).Name}");

            return CompareTo(other);
        }

        public bool Equals(Cpf other) 
            => _numero.Equals(other?._numero, StringComparison.Ordinal);

        public override bool Equals(object obj) 
            => CompareTo(obj) == 0;

        public override int GetHashCode() 
            => _numero.GetHashCode();

        public override string ToString()
            => ToString(EstiloFormatacaoCpf.Nenhum);

        /// <summary>
        /// Converte um <see cref="Cpf"/> em <see cref="System.String"/> de acordo com o estilo de formatação informado.
        /// </summary>
        /// <param name="estilo">Estilo de formatação</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Caso o estilo de formatação informado não seja válido.</exception>
        public string ToString(EstiloFormatacaoCpf estilo)
        {
            switch (estilo)
            {
                case EstiloFormatacaoCpf.Nenhum:
                    return _numero;
                case EstiloFormatacaoCpf.Padrao:
                    return $"{_numero.Substring(0, 3)}.{_numero.Substring(3, 3)}.{_numero.Substring(6, 3)}-{_numero.Substring(9, 2)}";
                default:
                    throw new ArgumentException("O estilo informado não é válido");
            }
        }

        /// <summary>
        /// Tenta converter uma string em <see cref="Cpf"/>. Retorna true caso tenha sucesso.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool TryParse(string s, out Cpf cpf)
        {
            if (!Validar(s, true, out var chars))
            {
                cpf = default;
                return false;
            }

            cpf = new Cpf(chars);
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
            if (ReferenceEquals(left, right))
                return true;

            if (ReferenceEquals(left, null))
                return false;

            if (ReferenceEquals(right, null))
                return false;
            
            return left._numero == right._numero;
        }
        
        public static bool operator !=(Cpf left, Cpf right)
        {
            return !(left == right);
        }
        
        public static implicit operator Cpf(string numero)
            => new Cpf(numero);

        private static bool Validar(string numero, bool returnValue, out char[] cpf)
        {
            var pos = 0;
            var ultimoChar = '0';
            var todosCharsIdenticos = true;
            var totalDigito1 = 0;
            var totalDigito2 = 0;
            var dv1Calculado = 0;

            char[] digitos = null;
            if (returnValue)
                digitos = new char[11];

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

                if (returnValue)
                    digitos[pos] = c;
                
                pos++;
            }

            if (todosCharsIdenticos)
                return false;
            
            if (returnValue)
                cpf = digitos;
            
            return true;
        }
    }
}