using SistemaDeGestao.Models;
using SistemaDeGestao.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SistemaDeGestao.ViewModels
{
    public class PedidoViewModel : ViewModelBase
    {
        // Serviços para buscar e salvar dados
        private readonly PedidoService _pedidoService = new PedidoService();
        private readonly PessoaService _pessoaService = new PessoaService();
        private readonly ProdutoService _produtoService = new ProdutoService();

        // Propriedades para Binding com a View
        private Pessoa _pessoaSelecionada;
        private Produto _produtoParaAdicionar;
        private int _quantidadeParaAdicionar;
        private ObservableCollection<ItemPedido> _itensDoPedido;
        private string _formaPagamentoSelecionada;
        private decimal _valorTotal;

        // Listas para ComboBoxes e DataGrid
        public ObservableCollection<Pessoa> Pessoas { get; set; }
        public ObservableCollection<Produto> Produtos { get; set; }
        public ObservableCollection<string> FormasPagamento { get; set; }

        // Propriedades Públicas (Binding)
        public Pessoa PessoaSelecionada
        {
            get => _pessoaSelecionada;
            set
            {
                _pessoaSelecionada = value;
                OnPropertyChanged();
                // Lógica para carregar pedidos da pessoa se necessário
            }
        }

        public Produto ProdutoParaAdicionar
        {
            get => _produtoParaAdicionar;
            set
            {
                _produtoParaAdicionar = value;
                OnPropertyChanged();
            }
        }

        public int QuantidadeParaAdicionar
        {
            get => _quantidadeParaAdicionar;
            set
            {
                _quantidadeParaAdicionar = value;
                OnPropertyChanged();
                // Lógica para validação (ex: quantidade > 0)
            }
        }

        public ObservableCollection<ItemPedido> ItensDoPedido
        {
            get => _itensDoPedido;
            set
            {
                _itensDoPedido = value;
                OnPropertyChanged();
            }
        }

        public string FormaPagamentoSelecionada
        {
            get => _formaPagamentoSelecionada;
            set
            {
                _formaPagamentoSelecionada = value;
                OnPropertyChanged();
            }
        }

        public decimal ValorTotal
        {
            get => _valorTotal;
            private set
            {
                _valorTotal = value;
                OnPropertyChanged();
            }
        }

        // Comandos para as ações da View
        public ICommand AdicionarItemCommand { get; }
        public ICommand FinalizarPedidoCommand { get; }

        public PedidoViewModel()
        {
            // Inicializa as listas de dados
            Pessoas = new ObservableCollection<Pessoa>(_pessoaService.GetAll());
            Produtos = new ObservableCollection<Produto>(_produtoService.GetAll());
            FormasPagamento = new ObservableCollection<string> { "Dinheiro", "Cartão", "Boleto" };

            ItensDoPedido = new ObservableCollection<ItemPedido>();

            // Define os comandos
            AdicionarItemCommand = new RelayCommand(AdicionarItemExecute, AdicionarItemCanExecute);
            FinalizarPedidoCommand = new RelayCommand(FinalizarPedidoExecute, FinalizarPedidoCanExecute);

            // Associa um evento para recalcular o total quando a lista de itens muda
            ItensDoPedido.CollectionChanged += (sender, e) => CalcularValorTotal();

        }

        public PedidoViewModel(Pessoa pessoa)
        {
            // Inicializa as listas de dados
            Pessoas = new ObservableCollection<Pessoa>(_pessoaService.GetAll());
            Produtos = new ObservableCollection<Produto>(_produtoService.GetAll());
            FormasPagamento = new ObservableCollection<string> { "Dinheiro", "Cartão", "Boleto" };

            ItensDoPedido = new ObservableCollection<ItemPedido>();

            // Define os comandos
            AdicionarItemCommand = new RelayCommand(AdicionarItemExecute, AdicionarItemCanExecute);
            FinalizarPedidoCommand = new RelayCommand(FinalizarPedidoExecute, FinalizarPedidoCanExecute);

            // Associa um evento para recalcular o total quando a lista de itens muda
            ItensDoPedido.CollectionChanged += (sender, e) => CalcularValorTotal();

        }

        // Métodos para os Comandos
        private void AdicionarItemExecute(object parameter)
        {
            if (ProdutoParaAdicionar == null || QuantidadeParaAdicionar <= 0) return;

            var itemExistente = ItensDoPedido.FirstOrDefault(i => i.Produto.Id == ProdutoParaAdicionar.Id);
            if (itemExistente != null)
            {
                itemExistente.Quantidade += QuantidadeParaAdicionar;
            }
            else
            {
                ItensDoPedido.Add(new ItemPedido
                {
                    Produto = ProdutoParaAdicionar,
                    Quantidade = QuantidadeParaAdicionar,
                    ValorUnitario = ProdutoParaAdicionar.Valor
                });
            }

            // Limpa os campos para o próximo item
            ProdutoParaAdicionar = null;
            QuantidadeParaAdicionar = 0;

            CalcularValorTotal();

        }

        private bool AdicionarItemCanExecute(object parameter)
        {
            return ProdutoParaAdicionar != null && QuantidadeParaAdicionar > 0;
        }

        private void FinalizarPedidoExecute(object parameter)
        {
            if (!FinalizarPedidoCanExecute(null)) return;

            var novoPedido = new Pedido
            {
                Pessoa = PessoaSelecionada,
                Produtos = ItensDoPedido.ToList(),
                FormaPagamento = FormaPagamentoSelecionada,
                ValorTotal = ValorTotal,
                DataVenda = DateTime.Now,
                Status = "Pendente"
            };

            _pedidoService.Add(novoPedido);

            // Opcional: Limpar a tela para um novo pedido
            LimparFormulario();
            // Opcional: Exibir uma mensagem de sucesso
        }

        private bool FinalizarPedidoCanExecute(object parameter)
        {
            return PessoaSelecionada != null &&
                   ItensDoPedido.Any() &&
                   !string.IsNullOrWhiteSpace(FormaPagamentoSelecionada);
        }

        // Métodos Auxiliares
        private void CalcularValorTotal()
        {
            ValorTotal = ItensDoPedido.Sum(item => item.Produto.Valor * item.Quantidade);
        }

        private void LimparFormulario()
        {
            PessoaSelecionada = null;
            ItensDoPedido.Clear();
            FormaPagamentoSelecionada = null;
            ValorTotal = 0;
        }
    }
}
