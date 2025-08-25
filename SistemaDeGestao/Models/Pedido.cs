using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestao.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        public Pessoa Pessoa { get; set; }

        public List<ItemPedido> Itens { get; set; }

        public decimal ValorTotal { get; set; }

        public DateTime DataVenda { get; set; }

        public string FormaPagamento { get; set; }

        public string Status { get; set; }
    }
}
