using SistemaDeGestao.Models;
using SistemaDeGestao.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SistemaDeGestao.ViewModels
{
    public class PessoaViewModel : ViewModelBase
    {
        private readonly PessoaService _pessoaService = new PessoaService();

        private ObservableCollection<Pessoa> _pessoas;
        public ObservableCollection<Pessoa> Pessoas
        {
            get => _pessoas;
            set
            {
                _pessoas = value;
                OnPropertyChanged();
            }
        }

        public ICommand LoadPessoasCommand { get; }
        public ICommand SalvarPessoaCommand { get; }
        public ICommand ExcluirPessoaCommand { get; }

        public PessoaViewModel()
        {
            Pessoas = new ObservableCollection<Pessoa>(_pessoaService.GetAll());
            LoadPessoasCommand = new RelayCommand(p => Pessoas = new ObservableCollection<Pessoa>(_pessoaService.GetAll()));
            // Adicione a lógica para os comandos de Salvar, Excluir, etc.
        }
    }
}
