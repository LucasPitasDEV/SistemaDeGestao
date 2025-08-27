using Newtonsoft.Json.Linq;
using SistemaDeGestao.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeGestao.Models
{
    public class ItemPedido : ViewModelBase
    {
        private int _quantidade;
        public Produto Produto { get; set; }

        public int Quantidade
        {
            get => _quantidade;
            set
            {
                if (_quantidade == value) return;

                _quantidade = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalOrcamento));
            }
        }

        public decimal ValorUnitario { get; set; }

        [NotMapped]
        public decimal TotalOrcamento {
            get { return Quantidade * ValorUnitario; } }
    }
}
