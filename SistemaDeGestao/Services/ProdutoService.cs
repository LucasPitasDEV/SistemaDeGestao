using SistemaDeGestao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestao.Services
{
    public class ProdutoService
    {
        private readonly DataService<Produto> _dataService = new DataService<Produto>("produtos.json");
        private List<Produto> _produtos;

        public ProdutoService()
        {
            _produtos = _dataService.LoadData();
        }

        public List<Produto> GetAll()
        {
            return _produtos;
        }

        public void AddOrUpdate(Produto produto)
        {
            if (produto.Id == 0)
            {
                produto.Id = _produtos.Any() ? _produtos.Max(p => p.Id) + 1 : 1;
                _produtos.Add(produto);
            }
            else
            {
                var existingProduto = _produtos.FirstOrDefault(p => p.Id == produto.Id);
                if (existingProduto != null)
                {
                    existingProduto.Nome = produto.Nome;
                    existingProduto.Codigo = produto.Codigo;
                    existingProduto.Valor = produto.Valor;
                }
            }
            _dataService.SaveData(_produtos);
        }

        public void Delete(int id)
        {
            _produtos.RemoveAll(p => p.Id == id);
            _dataService.SaveData(_produtos);
        }

        public List<Produto> Search(string nome, string codigo, decimal? valorMin, decimal? valorMax)
        {
            var query = _produtos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                query = query.Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(codigo))
            {
                query = query.Where(p => p.Codigo.Contains(codigo, StringComparison.OrdinalIgnoreCase));
            }

            if (valorMin.HasValue)
            {
                query = query.Where(p => p.Valor >= valorMin.Value);
            }

            if (valorMax.HasValue)
            {
                query = query.Where(p => p.Valor <= valorMax.Value);
            }

            return query.ToList();
        }
    }
}
