
using Alura.ByteBank.Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace Alura.ByteBank.Infraestrutura.Testes.Servico
{
    public class ByteBankRepositorio : IByteBankRepositorio
    {
        private List<Cliente> cliente = new List<Cliente>()
        {
            new Cliente()
            {
                Nome = "Lucas Domingues",
                CPF = "860.966.330-21",
                Identificador = Guid.NewGuid(),
                Profissao = "Contador",
                Id = 1
            },
            new Cliente()
            {
                Nome = "Raquel Souza",
                CPF = "811.106.060-28",
                Identificador = Guid.NewGuid(),
                Profissao = "Auxiliar Administrativo",
                Id = 2
            },
            new Cliente()
            {
                Nome = "Maria Luiza",
                CPF = "795.013.970-68",
                Identificador = Guid.NewGuid(),
                Profissao = "Comissaria de bordo",
                Id = 3
            },
        };

        public List<Cliente> Clientes { get { return cliente; } }

        private List<Agencia> agencias = new List<Agencia>()
        {
            new Agencia()
            {
                Nome = "Agencia Central 1",
                Identificador = Guid.NewGuid(),
                Id = 1,
                Endereco= "Rua das Flores,25",
                Numero=147
            },
            new Agencia()
            {
                Nome = "Agencia Central 2",
                Identificador = Guid.NewGuid(),
                Id = 2,
                Endereco= "Rua Gomes de Freitas,418",
                Numero=852
            },
            new Agencia()
            {
                Nome = "Agencia Central 3",
                Identificador = Guid.NewGuid(),
                Id = 3,
                Endereco= "Av. Gumercindo Avides,13",
                Numero=349
            }
        };

        public List<Agencia> Agencias { get { return agencias; } }

        private List<ContaCorrente> contas = new List<ContaCorrente>()
        {
            new ContaCorrente()
            {
                Saldo = 10,
                Id = 1,
                Identificador = Guid.NewGuid(),
                Cliente = new Cliente(){
                      Nome = "Bruce Kent",
                      CPF = "486.074.980-45",
                      Identificador = Guid.NewGuid(),
                      Profissao = "Empresário",
                      Id = 1
                },
                Agencia = new Agencia(){
                     Nome = "Agencia Central 1",
                     Identificador = Guid.NewGuid(),
                     Id = 1,
                     Endereco= "Rua das Flores,25",
                     Numero=147
                }
            },
            new ContaCorrente()
            {
                Saldo = 10,
                Id = 2,
                Identificador = Guid.NewGuid(),
                Cliente = new Cliente(){
                      Nome = "Marta Silva",
                      CPF = "877.288.430-44",
                      Identificador = Guid.NewGuid(),
                      Profissao = "Agente de Viagens",
                      Id = 2
                },
                Agencia = new Agencia(){
                    Nome = "Agencia Central 2",
                    Identificador = Guid.NewGuid(),
                    Id = 2,
                    Endereco= "Rua Gomes de Freitas,418",
                    Numero=852
                }
            }
        };

        public List<ContaCorrente> Contas { get { return contas; } }
        public bool AdicionarConta(ContaCorrente conta)
        {
            try
            {
                this.Contas.Add(conta);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
        public bool AdicionarAgencia(Agencia agencia)
        {
            try
            {
                this.Agencias.Add(agencia);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool AdicionarCliente(Cliente cliente)
        {
            try
            {
                this.Clientes.Add(cliente);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public List<Agencia> BuscarAgencias()
        {
            return this.Agencias;
        }

        public List<Cliente> BuscarClientes()
        {
            return this.Clientes;
        }

        public List<ContaCorrente> BuscarContasCorrentes()
        {
            return this.Contas;
        }
    }
}
