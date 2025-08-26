using SistemaDeGestao.Models;
using System.Windows.Input;

namespace SistemaDeGestao.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly PessoaViewModel _pessoaViewModel;
        private readonly ProdutoViewModel _produtoViewModel;
        private readonly PedidoViewModel _pedidoViewModel;
        private readonly PedidoPessoaViewModel _pedidoPessoaViewModel;

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

        public ICommand NavigateToPessoasCommand { get; }
        public ICommand NavigateToProdutosCommand { get; }
        public ICommand NavigateToPedidosCommand { get; }
        public ICommand NavigateToPedidosPessoaCommand { get; }

        public MainViewModel()
        {
            _pessoaViewModel = new PessoaViewModel();
            _produtoViewModel = new ProdutoViewModel();
            _pedidoViewModel = new PedidoViewModel();
            _pedidoPessoaViewModel = new PedidoPessoaViewModel();
            _pessoaViewModel.NavigateToPedidos += OnNavigateToPedidos;
            NavigateToPessoasCommand = new RelayCommand(p => CurrentViewModel = _pessoaViewModel);
            NavigateToProdutosCommand = new RelayCommand(p => CurrentViewModel = _produtoViewModel);
            NavigateToPedidosCommand = new RelayCommand(p => CurrentViewModel = _pedidoViewModel);
            NavigateToPedidosPessoaCommand = new RelayCommand(p => CurrentViewModel = _pedidoPessoaViewModel);
            CurrentViewModel = _pessoaViewModel;
        }

        private void OnNavigateToPedidos(Pessoa pessoa)
        {
            CurrentViewModel = new PedidoViewModel(pessoa);
        }
    }
}