using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Infraestrutura.Testes.Servico;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class AgenciaRepositorioTestes
    {
        private readonly IAgenciaRepositorio _repo;
        public ITestOutputHelper SaidaConsoleTeste { get; }
        public AgenciaRepositorioTestes(ITestOutputHelper saidaConsoleTeste)
        {
            SaidaConsoleTeste = saidaConsoleTeste;
            SaidaConsoleTeste.WriteLine("Construtor executado com sucesso!");
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
                Id = 51,
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
                Id = 56,
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
        [Fact]
        public void TestaExcecaoObterAgenciaPorId()
        {
            //Arrange

            //Assert
            Assert.Throws<Exception>(
                //Act
                () => _repo.ObterPorId(98)
            );
        }

        [Fact]
        public void TestaAdicionarAgenciaMock()
        {
            //Arrange
            var egencia = new Agencia()
            {
                Nome = "Agencia Amaral",
                Identificador = Guid.NewGuid(),
                Id = 5,
                Endereco = "Av. Osorio de paiva,13",
                Numero = 1478
            };

            var repoMock = new ByteBankRepositorio();

            //Act
            var novaAgencia = repoMock.AdicionarAgencia(egencia);

            //Assert
            Assert.True(novaAgencia);
        }

        [Fact]
        public void TestaObterAgenciaMock()
        {
            //Arrange
            //Cria uma intancia de um mock passando como param a interface com comportamento para testar
            var bytebankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = bytebankRepositorioMock.Object; //Criação do objeto mock

            //Act
            var lista = mock.BuscarAgencias();// chama o método a ser testado que existe na interface IByteBankRepositorio

            //Assert
            //Verifica se o comportamento foi invocado normalmente
            bytebankRepositorioMock.Verify(b => b.BuscarAgencias());
        }
    }
}
