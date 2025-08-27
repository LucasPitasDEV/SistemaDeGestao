using SistemaDeGestao.Models;
using SistemaDeGestao.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SistemaDeGestao.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly PessoaViewModel _pessoaViewModel;
        private readonly ProdutoViewModel _produtoViewModel;
        private readonly PedidoViewModel _pedidoViewModel;
        private readonly PedidoPessoaViewModel _pedidoPessoaViewModel;
        private readonly HomeViewModel _homeViewModel;

        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateToInicioCommand { get; }
        public ICommand NavigateToPessoasCommand { get; }
        public ICommand NavigateToProdutosCommand { get; }
        public ICommand NavigateToPedidosCommand { get; }
        public ICommand NavigateToPedidosPessoaCommand { get; }

        public MainViewModel()
        {
            _pessoaViewModel = new PessoaViewModel();
            _produtoViewModel = new ProdutoViewModel();
            

            

            _homeViewModel = new HomeViewModel();
            _pessoaViewModel = new PessoaViewModel();
            _produtoViewModel = new ProdutoViewModel();
            _pedidoViewModel = new PedidoViewModel();
            _pedidoPessoaViewModel = new PedidoPessoaViewModel();
            _pessoaViewModel.NavigateToPedidos += OnNavigateToPedidos;
            NavigateToInicioCommand = new RelayCommand(p => CurrentViewModel = _homeViewModel);
            NavigateToPessoasCommand = new RelayCommand(p =>
            {
                _pessoaViewModel.LoadPessoas();
                CurrentViewModel = _pessoaViewModel;
            });
            NavigateToProdutosCommand = new RelayCommand(p =>
            {
                _produtoViewModel.LoadProdutos();
                CurrentViewModel = _produtoViewModel;
            });
            NavigateToPedidosCommand = new RelayCommand(p =>
            {
                _pedidoViewModel.LoadPedido();
                CurrentViewModel = _pedidoViewModel;
            });
            NavigateToPedidosPessoaCommand = new RelayCommand(p =>
            {
                _pedidoPessoaViewModel.LoadPedidoPessoa();
                CurrentViewModel = _pedidoPessoaViewModel;
            });
            CurrentViewModel = _homeViewModel;
        }

        private void OnNavigateToPedidos(Pessoa pessoa)
        {
            CurrentViewModel = new PedidoViewModel(pessoa);
        }
    }
}