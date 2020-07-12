using System;
using FluentAssertions;
using Xunit;

namespace BrTypes.Tests
{
    public class MaskApplierTests
    {
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void MascaraNaoPodeSerNullOuVazio(string mask)
        {
            Action act = () => Masks.Apply(mask, "012345678909");
            act.Should().ThrowExactly<ArgumentNullException>()
                .And.ParamName.Should().Be("mask");
        }

        [Fact]
        public void RetornaNullSeValorForNull()
        {
            var resultado = Masks.Apply("###", null);
            resultado.Should().BeNull();
        }
        
        [Theory]
        [InlineData("###.###.###-##", "12345678901", "123.456.789-01")]
        [InlineData("#####-###", "12345678", "12345-678")]
        [InlineData("##.###.###/####-##", "01234567000189", "01.234.567/0001-89")]
        [InlineData("########", "01234567", "01234567")]
        [InlineData(" ### ", "01234", " 012 ")]
        [InlineData("?###90", "01234", "?01290")]
        [InlineData("12345", "98765", "12345")]
        public void MascarasValidas(string mask, string value, string expected)
        {
            var result = Masks.Apply(mask, value);
            result.Should().Be(expected);
        }

        [Theory]
        [InlineData("###.###.###-##", "123", "123")]
        [InlineData("###.###.###-##", "12345", "123.45")]
        public void ValorMenorQueMascara(string mask, string value, string expected)
        {
            var result = Masks.Apply(mask, value);
            result.Should().Be(expected);
        }
    }
}