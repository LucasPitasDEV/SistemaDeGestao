using SistemaDeGestao.Models;
using SistemaDeGestao.Services;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Windows.Input;

namespace SistemaDeGestao.ViewModels
{
    public class PessoaViewModel : ViewModelBase
    {
        private readonly PessoaService _pessoaService = new PessoaService();

        private ObservableCollection<Pessoa> _pessoas;
        private Pessoa _pessoaSelecionada;
        private string _nomeFiltro;
        private string _cpfFiltro;
        private int _id;
        private string _nome;
        private string _cpf;
        private string _endereco;

        public ObservableCollection<Pessoa> Pessoas
        {
            get => _pessoas;
            set
            {
                _pessoas = value;
                OnPropertyChanged();
            }
        }
        public Pessoa PessoaSelecionada
        {
            get => _pessoaSelecionada;
            set
            {
                _pessoaSelecionada = value;
                OnPropertyChanged();
                if (value != null)
                {
                    Id = value.Id;
                    Nome = value.Nome;
                    Cpf = value.Cpf;
                    Endereco = value.Endereco;
                }
            }
        }
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
        public string Cpf
        {
            get => _cpf;
            set { _cpf = value; OnPropertyChanged(); }
        }
        public string Endereco
        {
            get => _endereco;
            set { _endereco = value; OnPropertyChanged(); }
        }
        public string NomeFiltro
        {
            get => _nomeFiltro;
            set { _nomeFiltro = value; OnPropertyChanged(); }
        }
        public string CpfFiltro
        {
            get => _cpfFiltro;
            set { _cpfFiltro = value; OnPropertyChanged(); }
        }

        public ICommand BuscarPessoasCommand { get; }
        public ICommand SalvarPessoaCommand { get; }
        public ICommand ExcluirPessoaCommand { get; }
        public ICommand NovaPessoaCommand { get; }
        public ICommand VerPedidosCommand { get; }

        public event Action<Pessoa> NavigateToPedidos;

        public void LoadPessoas()
        {
            Pessoas = new ObservableCollection<Pessoa>(_pessoaService.GetAll());
        }


        public PessoaViewModel()
        {
            Pessoas = new ObservableCollection<Pessoa>(_pessoaService.GetAll());
            BuscarPessoasCommand = new RelayCommand(BuscarPessoasExecute);
            SalvarPessoaCommand = new RelayCommand(SalvarPessoaExecute, CanSalvarPessoa);
            ExcluirPessoaCommand = new RelayCommand(ExcluirPessoaExecute, CanExcluirPessoa);
            NovaPessoaCommand = new RelayCommand(NovaPessoaExecute);
            VerPedidosCommand = new RelayCommand(VerPedidosExecute, CanVerPedidos);
        }

        private void BuscarPessoasExecute(object parameter)
        {
            var pessoasFiltradas = _pessoaService.Search(NomeFiltro, CpfFiltro);
            Pessoas = new ObservableCollection<Pessoa>(pessoasFiltradas);
        }

        private void SalvarPessoaExecute(object parameter)
        {
            var pessoa = new Pessoa
            {
                Id = Id,
                Nome = Nome,
                Cpf = Cpf,
                Endereco = Endereco
            };

            _pessoaService.AddOrUpdate(pessoa);
            Pessoas = new ObservableCollection<Pessoa>(_pessoaService.GetAll());
            NovaPessoaExecute(null);
        }

        private bool CanSalvarPessoa(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Nome) && !string.IsNullOrWhiteSpace(Cpf);
        }

        private void ExcluirPessoaExecute(object parameter)
        {
            if (PessoaSelecionada != null)
            {
                _pessoaService.Delete(PessoaSelecionada.Id);
                Pessoas.Remove(PessoaSelecionada);
                NovaPessoaExecute(null);
            }
        }

        private bool CanExcluirPessoa(object parameter)
        {
            return PessoaSelecionada != null;
        }

        private void NovaPessoaExecute(object parameter)
        {
            Id = 0;
            Nome = string.Empty;
            Cpf = string.Empty;
            Endereco = string.Empty;
            PessoaSelecionada = null;
        }

        private void VerPedidosExecute(object parameter)
        {
            if (PessoaSelecionada != null)
            {
                NavigateToPedidos?.Invoke(PessoaSelecionada);
            }
        }

        private bool CanVerPedidos(object parameter)
        {
            return PessoaSelecionada != null;
        }
    }
}