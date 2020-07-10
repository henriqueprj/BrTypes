using FluentAssertions;
using Xunit;

namespace BrTypes.Tests
{
    public class MaskApplierTests
    {
        [Theory]
        [InlineData("61709754079", "617.097.540-79")]
        [InlineData("27823643081", "278.236.430-81")]
        [InlineData("18171528074", "181.715.280-74")]
        public void MascarasAceitas(string s, string expected)
        {
            var result = Masks.Apply(Masks.Cpf, s);
            result.Should().Be(expected);
        }
    }
}