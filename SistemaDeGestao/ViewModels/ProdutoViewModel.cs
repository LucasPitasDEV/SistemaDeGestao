using SistemaDeGestao.Models;
using SistemaDeGestao.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace SistemaDeGestao.ViewModels
{
    public class ProdutoViewModel : ViewModelBase
    {
        private readonly ProdutoService _produtoService = new ProdutoService();

        // Propriedades para os filtros de busca
        private string _nomeFiltro;
        private string _codigoFiltro;
        private decimal? _valorMinFiltro;
        private decimal? _valorMaxFiltro;

        // Propriedades para o DataGrid
        private ObservableCollection<Produto> _produtos;
        private Produto _produtoSelecionado;

        // Propriedades para a edição (podem ser um único objeto ou campos separados)
        private int _id;
        private string _nome;
        private string _codigo;
        private decimal _valor;

        // Listas para a UI
        public ObservableCollection<Produto> Produtos
        {
            get => _produtos;
            set
            {
                _produtos = value;
                OnPropertyChanged();
            }
        }

        public Produto ProdutoSelecionado
        {
            get => _produtoSelecionado;
            set
            {
                _produtoSelecionado = value;
                OnPropertyChanged();
                // Ao selecionar, preenche os campos para edição
                if (value != null)
                {
                    Id = value.Id;
                    Nome = value.Nome;
                    Codigo = value.Codigo;
                    Valor = value.Valor;
                }
            }
        }

        // Propriedades para os campos de edição
        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string Nome
        {
            get => _nome;
            set { _nome = value; OnPropertyChanged(); }
        }

        public string Codigo
        {
            get => _codigo;
            set { _codigo = value; OnPropertyChanged(); }
        }

        public decimal Valor
        {
            get => _valor;
            set { _valor = value; OnPropertyChanged(); }
        }

        // Propriedades para os campos de filtro
        public string NomeFiltro
        {
            get => _nomeFiltro;
            set { _nomeFiltro = value; OnPropertyChanged(); }
        }

        public string CodigoFiltro
        {
            get => _codigoFiltro;
            set { _codigoFiltro = value; OnPropertyChanged(); }
        }

        public decimal? ValorMinFiltro
        {
            get => _valorMinFiltro;
            set { _valorMinFiltro = value; OnPropertyChanged(); }
        }

        public decimal? ValorMaxFiltro
        {
            get => _valorMaxFiltro;
            set { _valorMaxFiltro = value; OnPropertyChanged(); }
        }

        // Comandos
        public ICommand BuscarProdutosCommand { get; }
        public ICommand SalvarProdutoCommand { get; }
        public ICommand ExcluirProdutoCommand { get; }
        public ICommand NovoProdutoCommand { get; }

        public ProdutoViewModel()
        {
            // Carrega todos os produtos ao iniciar a tela
            Produtos = new ObservableCollection<Produto>(_produtoService.GetAll());

            // Define os comandos
            BuscarProdutosCommand = new RelayCommand(BuscarProdutosExecute);
            SalvarProdutoCommand = new RelayCommand(SalvarProdutoExecute, CanSalvarProduto);
            ExcluirProdutoCommand = new RelayCommand(ExcluirProdutoExecute, CanExcluirProduto);
            NovoProdutoCommand = new RelayCommand(NovoProdutoExecute);
        }

        // Métodos de execução dos Comandos
        private void BuscarProdutosExecute(object parameter)
        {
            var produtosFiltrados = _produtoService.Search(NomeFiltro, CodigoFiltro, ValorMinFiltro, ValorMaxFiltro);
            Produtos = new ObservableCollection<Produto>(produtosFiltrados);
        }

        private void SalvarProdutoExecute(object parameter)
        {
            var produto = new Produto
            {
                Id = Id,
                Nome = Nome,
                Codigo = Codigo,
                Valor = Valor
            };

            _produtoService.AddOrUpdate(produto);

            // Atualiza a lista após salvar
            Produtos = new ObservableCollection<Produto>(_produtoService.GetAll());
            NovoProdutoExecute(null); // Limpa o formulário
        }

        private bool CanSalvarProduto(object parameter)
        {
            // Regras de validação
            return !string.IsNullOrWhiteSpace(Nome) &&
                   !string.IsNullOrWhiteSpace(Codigo) &&
                   Valor >= 0;
        }

        private void ExcluirProdutoExecute(object parameter)
        {
            if (ProdutoSelecionado != null)
            {
                _produtoService.Delete(ProdutoSelecionado.Id);
                Produtos.Remove(ProdutoSelecionado);
                NovoProdutoExecute(null); // Limpa o formulário
            }
        }

        private bool CanExcluirProduto(object parameter)
        {
            return ProdutoSelecionado != null;
        }

        private void NovoProdutoExecute(object parameter)
        {
            // Reseta os campos para um novo cadastro
            Id = 0;
            Nome = string.Empty;
            Codigo = string.Empty;
            Valor = 0;
            ProdutoSelecionado = null; // Desseleciona no DataGrid
        }
    }
}
