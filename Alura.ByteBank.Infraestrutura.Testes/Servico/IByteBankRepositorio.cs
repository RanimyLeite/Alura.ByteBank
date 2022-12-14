using Alura.ByteBank.Dominio.Entidades;
using System.Collections.Generic;

namespace Alura.ByteBank.Infraestrutura.Testes.Servico
{
    public interface IByteBankRepositorio
    {
        public List<Cliente> BuscarClientes();
        public List<Agencia> BuscarAgencias();
        public List<ContaCorrente> BuscarContasCorrentes();
    }
}
