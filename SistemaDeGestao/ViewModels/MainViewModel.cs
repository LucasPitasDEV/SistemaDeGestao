using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SistemaDeGestao.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
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

        public MainViewModel()
        {
            // Inicialize os comandos de navegação
            NavigateToPessoasCommand = new RelayCommand(p => CurrentViewModel = new PessoaViewModel());
            NavigateToProdutosCommand = new RelayCommand(p => CurrentViewModel = new ProdutoViewModel());
            NavigateToPedidosCommand = new RelayCommand(p => CurrentViewModel = new PedidoViewModel());

            // Tela inicial
            CurrentViewModel = new PessoaViewModel();
        }
    }
}
