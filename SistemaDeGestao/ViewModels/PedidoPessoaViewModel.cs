using SistemaDeGestao.Models;
using SistemaDeGestao.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace SistemaDeGestao.ViewModels
{
    internal class PedidoPessoaViewModel : ViewModelBase
    {
        private readonly PedidoService _pedidoService = new PedidoService();
        private readonly PessoaService _pessoaService = new PessoaService();

        // Propriedades de filtro
        private bool _showOnlyEntregues;
        private bool _showOnlyPagos;
        private bool _showOnlyPendentes;

        public bool ShowOnlyEntregues
        {
            get => _showOnlyEntregues;
            set { _showOnlyEntregues = value; OnPropertyChanged(); FiltrarPedidos(); }
        }

        public bool ShowOnlyPagos
        {
            get => _showOnlyPagos;
            set { _showOnlyPagos = value; OnPropertyChanged(); FiltrarPedidos(); }
        }

        public bool ShowOnlyPendentes
        {
            get => _showOnlyPendentes;
            set { _showOnlyPendentes = value; OnPropertyChanged(); FiltrarPedidos(); }
        }

        // Lista de todas as pessoas para a ComboBox
        private ObservableCollection<Pessoa> _pessoas;
        public ObservableCollection<Pessoa> Pessoas
        {
            get => _pessoas;
            set { _pessoas = value; OnPropertyChanged(); }
        }

        // A pessoa selecionada na ComboBox
        private Pessoa _pessoaSelecionada;
        public Pessoa PessoaSelecionada
        {
            get => _pessoaSelecionada;
            set
            {
                _pessoaSelecionada = value;
                OnPropertyChanged();
                FiltrarPedidos();
            }
        }

        // A lista de pedidos para a DataGrid
        private ObservableCollection<Pedido> _pedidos;
        public ObservableCollection<Pedido> Pedidos
        {
            get => _pedidos;
            set { _pedidos = value; OnPropertyChanged(); }
        }

        // Novos comandos para as ações por linha
        public ICommand MarcarComoPagoCommand { get; }
        public ICommand MarcarComoEnviadoCommand { get; }
        public ICommand MarcarComoEntregueCommand { get; }

        public PedidoPessoaViewModel()
        {
            Pessoas = new ObservableCollection<Pessoa>(_pessoaService.GetAll());
            Pedidos = new ObservableCollection<Pedido>(_pedidoService.GetAll());

            // Inicializa os novos comandos
            MarcarComoPagoCommand = new RelayCommand(MarcarComoPagoExecute);
            MarcarComoEnviadoCommand = new RelayCommand(MarcarComoEnviadoExecute);
            MarcarComoEntregueCommand = new RelayCommand(MarcarComoEntregueExecute);
        }

        private void FiltrarPedidos()
        {
            var todosPedidos = _pedidoService.GetAll();
            var query = todosPedidos.AsEnumerable();

            if (_pessoaSelecionada != null)
            {
                query = query.Where(p => p.Pessoa.Id == _pessoaSelecionada.Id);
            }

            if (ShowOnlyEntregues)
            {
                query = query.Where(p => p.Status == "Entregue");
            }
            if (ShowOnlyPagos)
            {
                query = query.Where(p => p.Status == "Pago");
            }
            if (ShowOnlyPendentes)
            {
                query = query.Where(p => p.Status == "Pendente de Pagamento");
            }

            Pedidos = new ObservableCollection<Pedido>(query.ToList());
        }

        // Lógica para marcar como pago
        private void MarcarComoPagoExecute(object parameter)
        {
            var pedido = (Pedido)parameter;
            if (pedido != null)
            {
                pedido.Status = "Pago";
                OnPropertyChanged(nameof(Pedidos));
            }
        }

        // Lógica para marcar como enviado
        private void MarcarComoEnviadoExecute(object parameter)
        {
            var pedido = (Pedido)parameter;
            if (pedido != null)
            {
                pedido.Status = "Enviado";
                OnPropertyChanged(nameof(Pedidos));
            }
        }

        // Lógica para marcar como entregue
        private void MarcarComoEntregueExecute(object parameter)
        {
            var pedido = (Pedido)parameter;
            if (pedido != null)
            {
                pedido.Status = "Entregue";
                OnPropertyChanged(nameof(Pedidos));
            }
        }
    }
}