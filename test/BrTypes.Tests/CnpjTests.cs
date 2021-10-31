using System;
using BrTypes.Tests.Utils;
using FluentAssertions;
using Iced.Intel;
using Xunit;

namespace BrTypes.Tests
{
    public class CnpjTests
    {
        [Fact]
        public void TryParse_falha_com_valor_nulo()
        {
            Cnpj.TryParse(null, out _).Should().BeFalse();
        }

        [Fact]
        public void Parse_falha_com_valor_nulo()
        {
            Action act = () => Cnpj.Parse(null!);
            act.Should().ThrowExactly<ArgumentNullException>();
        }
        
        [Fact]
        public void Parse_deve_falhar_com_digito_errado()
        {
            var cnpjInvalido = "07.245.465/0001-52";
            
            Action act = () => Cnpj.Parse(cnpjInvalido);
            act.Should().ThrowExactly<CnpjInvalidoException>();
        }
        
        [Fact]
        public void TryParse_deve_falhar_com_digito_errado()
        {
            var cnpjInvalido = "07.245.465/0001-52";
            
            var eValido = Cnpj.TryParse(cnpjInvalido, out var cnpj);
            eValido.Should().BeFalse();
        }
        
        [Theory]
        [InlineData("07.245.465/0001-51")]
        public void Parse_deve_ignorar_formatacao_padrao_para_cnpj_valido(string numero)
        {
            Action act = () => Cnpj.Parse(numero);
            act.Should().NotThrow();
        }
        
        [Fact]
        public void Parse_deve_falhar_quando_houver_mais_de_14_digitos()
        {
            var cnpjInvalido = "07.245.465/0001-511";
            
            Action act = () => Cnpj.Parse(cnpjInvalido);
            act.Should().ThrowExactly<CnpjInvalidoException>()
                .And.Value.Should().Be(cnpjInvalido);
        }
        
        [Fact]
        public void ToString_deve_falhar_quando_estilo_formatacao_invalido()
        {
            var cnpj = Cnpj.Parse("07.245.465/0001-51");
            Action act = () => cnpj.ToString((EstiloFormatacaoCnpj)8);
            act.Should().ThrowExactly<ArgumentException>();
        }

        [Theory]
        [InlineData("07.245.465/0001-51", "07245465000151")]
        [InlineData("02.557.906/0001-37", "02557906000137")]
        public void ToString_deve_converter_para_string(string value, string expected)
        {
            var cnpj = Cnpj.Parse(value);
            cnpj.ToString().Should().Be(expected);
        }
        
        [Theory]
        [InlineData("07245465000151", "07.245.465/0001-51")]
        [InlineData("02557906000137", "02.557.906/0001-37")]
        public void ToString_deve_converter_para_string_formatada(string value, string expected)
        {
            var cnpj = Cnpj.Parse(value);
            cnpj.ToString(EstiloFormatacaoCnpj.Padrao).Should().Be(expected);
        }
        
        [Theory]
        [InlineData("00000000000000")]
        [InlineData("11111111111111")]
        [InlineData("22222222222222")]
        [InlineData("33333333333333")]
        [InlineData("44444444444444")]
        [InlineData("55555555555555")]
        [InlineData("66666666666666")]
        [InlineData("77777777777777")]
        [InlineData("88888888888888")]
        [InlineData("99999999999999")]
        public void Cnpj_nao_pode_conter_valores_iguais(string value)
        {
            var isValid = Cnpj.TryParse(value, out _);
            isValid.Should().BeFalse();
        }

        [Theory]
        [InlineData("07245465000151", "07.245.465/0001-51")]
        [InlineData("02557906000137", "02.557.906/0001-37")]
        public void Equals_deve_comparar_pelo_valor(string value, string formattedValued)
        {
            var cnpj1 = Cnpj.Parse(value);
            var cnpj2 = Cnpj.Parse(formattedValued);

            cnpj1.Equals(cnpj2).Should().BeTrue();
        }
        
        [Theory]
        [InlineData("07245465000151", "07.245.465/0001-51")]
        [InlineData("02557906000137", "02.557.906/0001-37")]
        public void Equals_deve_comparar_pelo_valor_usando_operador_igualdade(string value, string formattedValued)
        {
            var cnpj1 = Cnpj.Parse(value);
            var cnpj2 = Cnpj.Parse(formattedValued);

            Assert.True(cnpj1 == cnpj2);
            Assert.False(cnpj1 != cnpj2);
        }
        
        [Theory]
        [InlineData("07245465000151", "07.245.465/0001-51")]
        [InlineData("02557906000137", "02.557.906/0001-37")]
        public void Equals_deve_comparar_pelo_valor_usando_EqualObject(string value, string formattedValued)
        {
            var cnpj1 = Cnpj.Parse(value);
            var cnpj2 = Cnpj.Parse(formattedValued);

            Assert.True(cnpj1.Equals((object)cnpj2));
        }

        [Fact]
        public void GetHashCodeTest()
        {
            const string value = "07245465000151";
            long internalValue = long.Parse(value);
            var cnpj = Cnpj.Parse(value);
            
            Assert.Equal(internalValue.GetHashCode(), cnpj.GetHashCode());
        }
        
        
    }
}   