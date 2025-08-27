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
        private readonly PedidoService _pedidoService = new PedidoService();
        private readonly PessoaService _pessoaService = new PessoaService();
        private readonly ProdutoService _produtoService = new ProdutoService();

        private Pessoa _pessoaSelecionada;
        private Produto _produtoParaAdicionar;
        private int _quantidadeParaAdicionar;
        private ObservableCollection<ItemPedido> _itensDoPedido;
        private string _formaPagamentoSelecionada;
        private decimal _valorTotal;

        public ObservableCollection<Pessoa> Pessoas { get; set; }
        public ObservableCollection<Produto> Produtos { get; set; }
        public ObservableCollection<string> FormasPagamento { get; set; }

        public Pessoa PessoaSelecionada
        {
            get => _pessoaSelecionada;
            set
            {
                _pessoaSelecionada = value;
                OnPropertyChanged();
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

        public ICommand AdicionarItemCommand { get; }
        public ICommand FinalizarPedidoCommand { get; }

        public PedidoViewModel()
        {
            Pessoas = new ObservableCollection<Pessoa>(_pessoaService.GetAll());
            Produtos = new ObservableCollection<Produto>(_produtoService.GetAll());
            FormasPagamento = new ObservableCollection<string> { "Dinheiro", "Cartão", "Boleto" };

            ItensDoPedido = new ObservableCollection<ItemPedido>();

            AdicionarItemCommand = new RelayCommand(AdicionarItemExecute, AdicionarItemCanExecute);
            FinalizarPedidoCommand = new RelayCommand(FinalizarPedidoExecute, FinalizarPedidoCanExecute);

            ItensDoPedido.CollectionChanged += (sender, e) => CalcularValorTotal();

        }

        public PedidoViewModel(Pessoa pessoa)
        {
            Pessoas = new ObservableCollection<Pessoa>(_pessoaService.GetAll());
            Produtos = new ObservableCollection<Produto>(_produtoService.GetAll());
            FormasPagamento = new ObservableCollection<string> { "Dinheiro", "Cartão", "Boleto" };

            ItensDoPedido = new ObservableCollection<ItemPedido>();

            AdicionarItemCommand = new RelayCommand(AdicionarItemExecute, AdicionarItemCanExecute);
            FinalizarPedidoCommand = new RelayCommand(FinalizarPedidoExecute, FinalizarPedidoCanExecute);

            ItensDoPedido.CollectionChanged += (sender, e) => CalcularValorTotal();

        }

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

            LimparFormulario();
        }

        private bool FinalizarPedidoCanExecute(object parameter)
        {
            return PessoaSelecionada != null &&
                   ItensDoPedido.Any() &&
                   !string.IsNullOrWhiteSpace(FormaPagamentoSelecionada);
        }

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

        public void LoadPedido()
        {
            Pessoas = new ObservableCollection<Pessoa>(_pessoaService.GetAll());
            Produtos = new ObservableCollection<Produto>(_produtoService.GetAll());
            ItensDoPedido.Clear();
            PessoaSelecionada = null;
            FormaPagamentoSelecionada = null;
            ValorTotal = 0;
        }
    }
}
