using SistemaDeGestao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestao.Services
{
    public class PedidoService
    {
        private readonly DataService<Pedido> _dataService = new DataService<Pedido>("pedidos.json");
        private List<Pedido> _pedidos;

        public PedidoService()
        {
            _pedidos = _dataService.LoadData();
        }

        public List<Pedido> GetAll()
        {
            return _pedidos;
        }

        public void Add(Pedido pedido)
        {
            pedido.Id = _pedidos.Any() ? _pedidos.Max(p => p.Id) + 1 : 1;
            _pedidos.Add(pedido);
            _dataService.SaveData(_pedidos);
        }

        public void UpdateStatus(int pedidoId, string status)
        {
            var pedido = _pedidos.FirstOrDefault(p => p.Id == pedidoId);
            if (pedido != null)
            {
                pedido.Status = status;
                _dataService.SaveData(_pedidos);
            }
        }

        public List<Pedido> GetByPessoaId(int pessoaId)
        {
            return _pedidos.Where(p => p.Pessoa.Id == pessoaId).ToList();
        }
    }
}
