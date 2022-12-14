using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class AgenciaRepositorioTestes
    {
        private readonly IAgenciaRepositorio _repo;
        public AgenciaRepositorioTestes()
        {
            //Injetando dependência no construtor
            //para não depender da classe concreta e sim da abstração! 
            var servico = new ServiceCollection();
            servico.AddTransient<IAgenciaRepositorio, AgenciaRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _repo = provedor.GetService<IAgenciaRepositorio>();
        }

        [Fact]
        public void TestaObterTodasAsAgencias()
        {
            //Arrange

            //Act
            List<Agencia> agencia = _repo.ObterTodos();

            //Assert
            Assert.NotNull(agencia);
        }

        [Fact]
        public void TestaObterAgenciaPorId()
        {
            //Arrange

            //Act
            var agencia = _repo.ObterPorId(1);

            //Assert
            Assert.NotNull(agencia);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterAgenciaPorVariosIds(int id)
        {
            //Arrange

            //Act
            var agencia = _repo.ObterPorId(id);

            //Assert
            Assert.NotNull(agencia);
            Assert.Equal(id, agencia.Id);
        }

        [Fact]
        public void TestaObterAgenciaPorGuid()
        {
            //Arrange
            var guid = new Guid("d2fdf64f-ff97-4a9a-9367-2b7055cbd449");

            //Act
            var agencia = _repo.ObterPorGuid(guid);

            //Assert
            Assert.Equal(guid, agencia.Identificador);
        }

        [Fact]
        public void TestaAtualizarAgencia()
        {
            //Arrange
            var agencia = _repo.ObterPorId(1);
            var numeroNovo = 198;
            agencia.Numero = numeroNovo;

            //Act
            var atualizado = _repo.Atualizar(1, agencia);

            //Assert
            Assert.True(atualizado);
        }

        [Fact]
        public void TestaAdicionarAgencia()
        {
            //Arrange
            var agencia = new Agencia()
            {
                Id = 58,
                Numero= 1003,
                Nome = "New Agency",
                Identificador = Guid.NewGuid(),
                Endereco = "Seridião Montenegro"
            };

            //Act
            var novaAgencia = _repo.Adicionar(agencia);

            //Assert
            Assert.True(novaAgencia);
        }

        [Fact]
        public void TestaExcluirAgencia()
        {
            //Arrange
            var agencia = new Agencia()
            {
                Id = 58,
                Numero = 1003,
                Nome = "New Agency",
                Identificador = Guid.NewGuid(),
                Endereco = "Seridião Montenegro"
            };
            var novaAgencia = _repo.Adicionar(agencia);

            //Act
            var resultado = _repo.Excluir(agencia.Id);

            //Assert
            Assert.True(resultado);
        }
    }
}
