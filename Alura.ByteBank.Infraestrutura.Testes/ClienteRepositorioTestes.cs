using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ClienteRepositorioTestes
    {
        private readonly IClienteRepositorio _repo;
        public ClienteRepositorioTestes()
        {
            //Injetando dependência no construtor
            //para não depender da classe concreta e sim da abstração! 
            var servico = new ServiceCollection();
            servico.AddTransient<IClienteRepositorio, ClienteRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _repo = provedor.GetService<IClienteRepositorio>();
        }

        [Fact]
        public void TestaObterTodosOsClientes()
        {
            //Arrange
            //Como o repositório está sendo instanciado no construtor não precisamos cria-lo aqui!

            //Act
            List<Cliente> lista = _repo.ObterTodos();

            //Assert
            Assert.NotNull(lista);
        }

        [Fact]
        public void TestaObterClientePorId()
        {
            //Arrange

            //Act
             var cliente = _repo.ObterPorId(2);

            //Assert
            Assert.NotNull(cliente);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TestaObterClientePorVariosIds(int id)
        {
            //Arrange

            //Act
            var cliente = _repo.ObterPorId(id);

            //Assert
            Assert.Equal(id, cliente.Id);
        }
    }
}
