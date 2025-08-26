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
        private bool _showOnlyEnviados;

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
        public bool ShowOnlyEnviados
        {
            get => _showOnlyEnviados;
            set { _showOnlyEnviados = value; OnPropertyChanged(); FiltrarPedidos(); }
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

            var statusSelecionados = new List<string>();
            if (ShowOnlyEntregues)
            {
                statusSelecionados.Add("Entregue");
            }
            if (ShowOnlyPagos)
            {
                statusSelecionados.Add("Pago");
            }
            if (ShowOnlyPendentes)
            {
                statusSelecionados.Add("Pendente");
            }
            if (ShowOnlyEnviados)
            {
                statusSelecionados.Add("Enviado");
            }

            if (statusSelecionados.Any())
            {
                query = query.Where(p => statusSelecionados.Contains(p.Status));
            }

            Pedidos = new ObservableCollection<Pedido>(query.ToList());
        }

        private void MarcarComoPagoExecute(object parameter)
        {
            var pedido = (Pedido)parameter;
            if (pedido != null)
            {
                
                _pedidoService.UpdateStatus(pedido.Id, "Pago");
                FiltrarPedidos();
            }
        }

        private void MarcarComoEnviadoExecute(object parameter)
        {
            var pedido = (Pedido)parameter;
            if (pedido != null)
            {
                
                _pedidoService.UpdateStatus(pedido.Id, "Enviado");
                FiltrarPedidos();
            }
        }

        private void MarcarComoEntregueExecute(object parameter)
        {
            var pedido = (Pedido)parameter;
            if (pedido != null)
            {
                
                _pedidoService.UpdateStatus(pedido.Id, "Entregue");
                FiltrarPedidos();
            }
        }
    }
}