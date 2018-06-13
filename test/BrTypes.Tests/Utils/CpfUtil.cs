using System;
using System.Configuration;
using System.Linq;

namespace BrTypes.Tests.Utils
{
    internal static class CpfUtils
    {
        private static readonly int[] Multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] Multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        
        private static readonly Random Rnd = new Random();

        public static string GerarCpfComDigitosInvalidos()
        {
            var cpfValido = GerarCpf();

            var digito = int.Parse(cpfValido.Substring(9, 2));

            int digitoInvalido;
            
            do { digitoInvalido = Rnd.Next(0, 99); } 
            while (digitoInvalido == digito);

            return string.Concat(cpfValido.Substring(0, 9), digitoInvalido.ToString("00"));
        }
        
        public static string GerarCpf(bool comMascara = false)
        {
            string cpf;
            
            do
            {
                cpf = GerarCpfInternal();
            } 
            while (cpf == "00000000000" || cpf == "11111111111" || cpf == "22222222222" || cpf == "33333333333" ||
                   cpf == "44444444444" || cpf == "55555555555" || cpf == "66666666666" || cpf == "77777777777" ||
                   cpf == "88888888888" || cpf == "99999999999");

            return comMascara 
                ? AplicarMascara(cpf) 
                : cpf;
        }

        public static string RemoverMascara(string cpfComMascara)
            => new string(cpfComMascara.Where(char.IsDigit).ToArray());

        public static string AplicarMascara(string cpf)
            => $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
        
        private static string GerarCpfInternal()
        {
            int soma1 = 0, soma2 = 0;

            var semente = Rnd.Next(1, 999999999).ToString("000000000");

            for (var i = 0; i < 9; i++)
            {
                var digito = semente[i] - '0';
                soma1 += digito * Multiplicador1[i];
                soma2 += digito * Multiplicador2[i];
            }

            var dv1 = soma1 % 11;
            if (dv1 < 2)
                dv1 = 0;
            else
                dv1 = 11 - dv1;

            soma2 += dv1 * 2;

            var dv2 = soma2 % 11;
            if (dv2 < 2)
                dv2 = 0;
            else
                dv2 = 11 - dv2;

            return string.Concat(semente, dv1, dv2);
        }
    }
}