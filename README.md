Sistema de Gerenciamento de Vendas - Desafio Técnico
Visão Geral
Este sistema é uma aplicação WPF desenvolvida utilizando o padrão de arquitetura MVVM (Model-View-ViewModel) para gerenciar o cadastro de pessoas (clientes), produtos e o registro de pedidos de venda. O objetivo deste projeto é demonstrar a aplicação dos princípios de separação de responsabilidades, testabilidade e organização de código em uma aplicação desktop.

Funcionalidades Principais
Cadastro de Pessoas:

Adicionar, editar e excluir informações de clientes (Nome, CPF, Endereço).

Filtrar e buscar clientes por nome e CPF.

Visualização dos pedidos associados a um cliente.

Funcionalidade para marcar pedidos como pagos (implementação pendente de detalhes).

Validação básica do formato do CPF.

Cadastro de Produtos:

Adicionar, editar e excluir informações de produtos (Nome, Código, Valor).

Filtrar e buscar produtos por nome, código e faixa de valor.

Registro de Pedidos:

Selecionar um cliente para o pedido.

Adicionar múltiplos produtos ao pedido, especificando a quantidade.

Cálculo automático do valor total do pedido.

Seleção da forma de pagamento (Dinheiro, Cartão, Boleto).

Registro da data da venda e status (Pendente).

Finalização e salvamento do pedido.

Arquitetura
O sistema foi desenvolvido seguindo o padrão MVVM para separar a interface do usuário, a lógica de apresentação e os dados.

Model: Representa as entidades de dados (Pessoa, Produto, Pedido, ItemPedido).

View: Define a interface do usuário (arquivos .xaml). Liga-se aos ViewModels através de Data Binding.

ViewModel: Contém a lógica de apresentação, expondo comandos e propriedades para a View. Orquestra a interação entre a View e os Services.

Services: Contém a lógica de negócio e a manipulação de dados (leitura e escrita de arquivos .json).

Tecnologias Utilizadas
WPF (.NET Framework 8.0): Framework para a criação da interface de usuário desktop.

C#: Linguagem de programação principal.

MVVM Light Toolkit (ou implementação própria de ViewModelBase e RelayCommand): Para auxiliar na implementação do padrão MVVM.

System.Text.Json: Para serialização e desserialização de dados em formato JSON.

Como Executar
Clone o repositório do projeto.

Abra a solução (.sln file) no Visual Studio.

Certifique-se de que o projeto principal (SistemaDeGestao) esteja definido como o projeto de inicialização (clique com o botão direito no projeto no Solution Explorer e selecione "Definir como Projeto de Inicialização").

Pressione F5 ou clique no botão "Iniciar" para executar a aplicação.

Estrutura do Projeto
SistemaDeGestao/: Raiz do projeto.

Data/: Contém os arquivos .json para persistência de dados.

pessoas.json

produtos.json

pedidos.json

Models/: Define as classes de modelo (Pessoa, Produto, Pedido, ItemPedido).

Services/: Contém as classes de serviço (PessoaService, ProdutoService, PedidoService) com a lógica de negócio.

ViewModels/: Contém as classes ViewModel (MainViewModel, PessoaViewModel, ProdutoViewModel, PedidoViewModel) e classes auxiliares (ViewModelBase, RelayCommand).

Views/: Contém as interfaces de usuário (MainWindow.xaml, PessoaView.xaml, ProdutoView.xaml, PedidoView.xaml).

App.xaml / App.xaml.cs: Lógica de inicialização da aplicação.

MainWindow.xaml / MainWindow.xaml.cs: Janela principal da aplicação.

SistemaDeGestao.csproj: Arquivo de projeto do C#.

README.md: Este arquivo.
