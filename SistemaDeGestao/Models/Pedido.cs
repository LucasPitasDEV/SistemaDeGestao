using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SistemaDeGestao.Models
{
    public class Pedido : INotifyPropertyChanged
    {
        private string _status;

        public int Id { get; set; }
        public Pessoa Pessoa { get; set; }
        public List<ItemPedido> Itens { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataVenda { get; set; }
        public string FormaPagamento { get; set; }
        public List<ItemPedido> Produtos { get; internal set; }
        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PodeMarcarComoPago));
                    OnPropertyChanged(nameof(PodeMarcarComoEnviado));
                    OnPropertyChanged(nameof(PodeMarcarComoEntregue));
                }
            }
        }

        public bool PodeMarcarComoPago => Status == "Pendente";
        public bool PodeMarcarComoEnviado => Status == "Pago";
        public bool PodeMarcarComoEntregue => Status == "Enviado";

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}