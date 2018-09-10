﻿using System;
using BrTypes.Tests.Utils;
using FluentAssertions;
using Xunit;

namespace BrTypes.Tests
{
    public class CpfTests
    {
        [Fact]
        public void Deve_falhar_ao_instanciar_cpf_com_digito_errado()
        {
            var cpfInvalido = CpfUtils.GerarCpfComDigitosInvalidos();
            
            Action act = () => new Cpf(cpfInvalido);
            act.Should().ThrowExactly<CpfInvalidoException>();
        }
        
        [Theory]
        [InlineData("00000000000")]
        [InlineData("11111111111")]
        [InlineData("22222222222")]
        [InlineData("33333333333")]
        [InlineData("44444444444")]
        [InlineData("55555555555")]
        [InlineData("66666666666")]
        [InlineData("77777777777")]
        [InlineData("88888888888")]
        [InlineData("99999999999")]
        public void Cpf_digitos_iguais_deve_tratar_como_invalido(string numero)
        {
            Action act = () => new Cpf(numero);

            act.Should().ThrowExactly<CpfInvalidoException>();
        }

        [Theory]
        [InlineData("617.097.540-79")]
        [InlineData("61709754079")]
        public void Deve_ignorar_formatacao_padrao_para_cpf_valido(string numero)
        {
            Action act = () => new Cpf(numero);
            act.Should().NotThrow<CpfInvalidoException>();
        }

        [Theory]
        [InlineData("61709754079", "617097540")]
        [InlineData("27823643081", "278236430")]
        [InlineData("18171528074", "181715280")]
        public void Cpf_base_deve_ser_obtido_com_sucesso(string cpfCompleto, string cpfBase)
        {
            var cpf = new Cpf(cpfCompleto);
            cpf.Base.Should().Be(cpfBase);
        }

        [Theory]
        [InlineData("61709754079", "79")]
        [InlineData("27823643081", "81")]
        [InlineData("18171528074", "74")]
        public void Cpf_deve_retornar_digitos_verificadores_corretamente(string cpfCompleto, string dv)
        {
            var cpf = new Cpf(cpfCompleto);
            cpf.DigitosVerificadores.Should().Be(dv);
        }

        [Fact]
        public void Cpfs_com_mesmo_numero_devem_ser_considerados_iguais()
        {
            var numero = CpfUtils.GerarCpf();
            
            var cpf1 = new Cpf(numero);
            var cpf2 = new Cpf(numero);

            cpf1.Should().Be(cpf2);
            
            if (cpf1 != cpf2)
                Assert.True(false, "Cpfs são iguais");
        }

        [Fact]
        public void ToString_deve_retornar_cpf_sem_formatacao()
        {
            var cpfComMascara = CpfUtils.GerarCpf(comMascara: true);
            var cpfSemMascara = CpfUtils.RemoverMascara(cpfComMascara);

            var cpf = new Cpf(cpfComMascara);
            cpf.ToString().Should().Be(cpfSemMascara);
        }

        [Fact]
        public void Deve_formatar_cpf_com_estilo_Padrao()
        {
            var cpf = new Cpf("61709754079");
            cpf.ToString(EstiloFormatacaoCpf.Padrao).Should().Be("617.097.540-79");
        }
        
        [Fact]
        public void Deve_formatar_cpf_com_estilo_Nenhum()
        {
            var cpf = new Cpf("617.097.540-79");
            cpf.ToString(EstiloFormatacaoCpf.Nenhum).Should().Be("61709754079");
        }
        
    }
}