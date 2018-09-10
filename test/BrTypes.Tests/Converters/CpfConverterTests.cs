using System.Linq;
using BrTypes.Converters;
using BrTypes.Tests.Utils;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace BrTypes.Tests.Converters
{
    public class CpfConverterTests
    {
        [Fact]
        public void Deve_serializar_para_string()
        {
            var cpfString = CpfUtils.GerarCpf();
            var cpf = new Cpf(cpfString);

            var json = JsonConvert.SerializeObject(cpf);

            json.Should().Be("\"" + cpfString + "\"");
        }
    }
}