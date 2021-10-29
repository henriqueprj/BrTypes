﻿using System;
using FluentAssertions;
using Xunit;

namespace BrTypes.Tests
{
    public class DigitsFillerTests
    {
        [Fact]
        public void TrueQuandoPreencheExatamenteOBuffer()
        {
            Span<int> digits = stackalloc int[5];
            
            var resultado = Digits.Default.TryParse("12345", digits);

            resultado.Should().BeTrue();

            var expectedDigits = new[] { 1, 2, 3, 4, 5 };

            digits.Length.Should().Be(expectedDigits.Length);
            
            for (var i = 0; i < digits.Length; i++)
            {
                Assert.Equal(expectedDigits[i], digits[i]);
            }
        }
        
        [Fact]
        public void TrueQuandoPreencheExatamenteOBufferComCaracteresInvalidos()
        {
            Span<int> digits = stackalloc int[5];
            
            var resultado =  Digits.Default.TryParse("12a34.5", digits);

            resultado.Should().BeTrue();
            
            var expectedDigits = new[] { 1, 2, 3, 4, 5 };

            digits.Length.Should().Be(expectedDigits.Length);
            
            for (var i = 0; i < digits.Length; i++)
            {
                Assert.Equal(expectedDigits[i], digits[i]);
            }
        }
        
        [Fact]
        public void FalseQuandoNaoPreencheOMinimoComCaracteresInvalidos()
        {
            Span<int> digits = stackalloc int[5];
            
            
            var resultado =  Digits.Default.TryParse("1a2.3", digits);

            resultado.Should().BeFalse();
            
            var expectedDigits = new[] { 1, 2, 3, 0, 0 };

            digits.Length.Should().Be(expectedDigits.Length);
            
            for (var i = 0; i < digits.Length; i++)
            {
                Assert.Equal(expectedDigits[i], digits[i]);
            }
        }
        
        [Fact]
        public void FalseQuandoUltrapassaOMaximo()
        {
            Span<int> digits = stackalloc int[3];
            
            var resultado = Digits.Default.TryParse("1234", digits);

            resultado.Should().BeFalse();
            
            var expectedDigits = new[] { 1, 2, 3 };

            digits.Length.Should().Be(expectedDigits.Length);
            
            for (var i = 0; i < digits.Length; i++)
            {
                Assert.Equal(expectedDigits[i], digits[i]);
            }
        }
        
        [Fact]
        public void FalseQuandoUltrapassaOMaximoComCaracteresInvalidos()
        {
            Span<int> digits = stackalloc int[3];
            
            var resultado = Digits.Default.TryParse("1a23.4", digits);

            resultado.Should().BeFalse();
            
            var expectedDigits = new[] { 1, 2, 3 };

            digits.Length.Should().Be(expectedDigits.Length);
            
            for (var i = 0; i < digits.Length; i++)
            {
                Assert.Equal(expectedDigits[i], digits[i]);
            }
        }
    }
}