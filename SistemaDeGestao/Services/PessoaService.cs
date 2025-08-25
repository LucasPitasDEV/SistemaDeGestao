using SistemaDeGestao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestao.Services
{
    public class PessoaService
    {
        private readonly DataService<Pessoa> _dataService = new DataService<Pessoa>("pessoas.json");
        private List<Pessoa> _pessoas;

        public PessoaService()
        {
            _pessoas = _dataService.LoadData();
        }
        public List<Pessoa> GetAll()
        {
            return _pessoas;
        }

        public void AddOrUpdate(Pessoa pessoa)
        {
            if (pessoa.Id == 0)
            {
                // Lógica para gerar um novo ID.
                pessoa.Id = _pessoas.Any() ? _pessoas.Max(p => p.Id) + 1 : 1;
                _pessoas.Add(pessoa);
            }
            else
            {
                var existingPessoa = _pessoas.FirstOrDefault(p => p.Id == pessoa.Id);
                if (existingPessoa != null)
                {
                    existingPessoa.Nome = pessoa.Nome;
                    existingPessoa.Cpf = pessoa.Cpf;
                    existingPessoa.Endereco = pessoa.Endereco;
                }
            }
            _dataService.SaveData(_pessoas);
        }

        public void Delete(int id)
        {
            _pessoas.RemoveAll(p => p.Id == id);
            _dataService.SaveData(_pessoas);
        }

        // Implemente a lógica de busca com LINQ
        public List<Pessoa> Search(string nome, string cpf)
        {
            var query = _pessoas.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                query = query.Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(cpf))
            {
                query = query.Where(p => p.Cpf.Contains(cpf));
            }

            return query.ToList();
        }
    }
}
