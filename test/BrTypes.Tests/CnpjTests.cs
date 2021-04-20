using System;
using BrTypes.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace BrTypes.Tests
{
    public class CnpjTests
    {
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
            
            var eValido = Cnpj.TryParse3(cnpjInvalido, out var cnpj);
            eValido.Should().BeFalse();
            
        }
        
        [Theory]
        [InlineData("07.245.465/0001-52")]
        public void Parse_deve_ignorar_formatacao_padrao_para_cnpj_valido(string numero)
        {
            Action act = () => Cnpj.Parse(numero);
            act.Should().NotThrow<CnpjInvalidoException>();
        }
        
        [Fact]
        public void Parse_deve_falhar_quando_houver_mais_de_14_digitos()
        {
            var cnpjInvalido = "07.245.465/0001-511";
            
            Action act = () => Cnpj.Parse(cnpjInvalido);
            act.Should().ThrowExactly<CnpjInvalidoException>()
                .And.Value.Should().Be(cnpjInvalido);
        }
    }
}   