using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Alura.ByteBank.Dominio.Interfaces.Repositorios;
using Alura.ByteBank.Infraestrutura.Testes.Servico;
using Alura.ByteBank.Infraestrutura.Testes.Servico.DTO;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class ContaCorrenteRepositorioTestes
    {
        private readonly IContaCorrenteRepositorio _repo;
        public ContaCorrenteRepositorioTestes()
        {
            //Injetando dependência no construtor
            //para não depender da classe concreta e sim da abstração! 
            var servico = new ServiceCollection();
            servico.AddTransient<IContaCorrenteRepositorio, ContaCorrenteRepositorio>();

            var provedor = servico.BuildServiceProvider();
            _repo = provedor.GetService<IContaCorrenteRepositorio>();
        }

        [Fact]
        public void TestaObterTodosAsContas()
        {
            //Arrange

            //Act
            List<ContaCorrente> contas = _repo.ObterTodos();

            //Assert
            Assert.NotNull(contas);
        }

        [Fact]
        public void TestaObterContasPorId()
        {
            //Arrange

            //Act
            var conta = _repo.ObterPorId(3);

            //Assert
            Assert.NotNull(conta);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TestaObterContasPorVariosIds(int id)
        {
            //Arrange

            //Act
            var conta = _repo.ObterPorId(id);

            //Assert
            Assert.NotNull(conta);
            Assert.Equal(id, conta.Id);
        }

        [Fact]
        public void TestaAtualizarContaCorrente()
        {
            //Arrange
            var conta = _repo.ObterPorId(1);
            var novoSaldo = 150;
            conta.Saldo = novoSaldo;

            //Act
            var atualizado = _repo.Atualizar(1, conta); //Retorna um boll

            //Assert
            Assert.True(atualizado);
        }

        [Fact]
        public void TestaAdicionarContaCorrente()
        {
            //Arrange
            var conta = new ContaCorrente()
            {
                Saldo = 10,
                Identificador = Guid.NewGuid(),
                Cliente = new Cliente()
                {
                    Nome = "Ranimy Leite",
                    CPF = "062.081.603-16",
                    Identificador = Guid.NewGuid(),
                    Profissao = "desenvolvedor",
                    Id = 1
                },
                Agencia = new Agencia()
                {
                    Nome = "Agencia Central",
                    Identificador = Guid.NewGuid(),
                    Endereco = "Rua Mario Montenegro",
                    Numero = 147
                }
            };

            //Act
            var resultado = _repo.Adicionar(conta);

            //Assert
            Assert.True(resultado);
        }

        [Fact]
        public void TestaObterContaCorrentePorGuid()
        {
            //Arrange
            var guid = new Guid("1001b6f8-4fdb-44dd-a63d-850e6bf5e1d3");

            //Act
            var conta = _repo.ObterPorGuid(guid);

            //Assert
            Assert.Equal(guid, conta.Identificador);
        }

        [Fact]
        public void TestaExcluirContaCorrente()
        {
            //Arrange
            var conta = new ContaCorrente()
            {
                Saldo = 10,
                Identificador = Guid.NewGuid(),
                Cliente = new Cliente()
                {
                    Nome = "Kassia Andrade",
                    CPF = "614.574.640-80",
                    Identificador = Guid.NewGuid(),
                    Profissao = "contadora",
                    Id = 1
                },
                Agencia = new Agencia()
                {
                    Nome = "Agencia ABC",
                    Identificador = Guid.NewGuid(),
                    Endereco = "Rua Mario Montenegro",
                    Numero = 165
                }
            };
            var resultado = _repo.Adicionar(conta);

            //Act
            var contaExcluida = _repo.Excluir(conta.Id);

            //Assert
            Assert.True(contaExcluida);
        }

        [Fact]
        //Utilizaremos o conceito de stub que diferencia um pouco de mock
        //No mock testamos um comportamento 
        //No Stub verificamos se o retorno de um método está igual ao esperado no test
        public void TestaConsultaTodosPix()
        {
            //Arrange
            var guid = new Guid("a0b80d53-c0dd-4897-ab90-c0615ad80d5a"); //guid que será consultado
            var pix = new PixDTO() { Chave = guid, Saldo = 10 }; //Cria um novo pix

            var pixRepositorioMock = new Mock<IPixRepositorio>(); //Cria o Mock

            //Criando consulta, passando um guid e retorna um pix
            pixRepositorioMock.Setup(x => x.consultaPix(It.IsAny<Guid>())).Returns(pix);

            var mock = pixRepositorioMock.Object;//Cria instancia do mock 

            //Act
            var saldo = mock.consultaPix(guid).Saldo;//Faz a consulta do saldo do pix criado acima

            //Assert
            Assert.Equal(10, saldo);//Verifica o retorno e compara com o esperado
        }
    }
}
