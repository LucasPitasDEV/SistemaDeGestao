🚀 Sistema de Gerenciamento de Vendas

Este projeto é uma aplicação desktop desenvolvida em C# com WPF e o padrão MVVM, focada na gestão completa de clientes, produtos e pedidos. Ele demonstra como uma arquitetura bem-definida pode tornar o código limpo, modular e fácil de manter.

✨ Funcionalidades
O sistema é dividido em três módulos principais, cada um com sua tela e lógica dedicada.

👤 Gerenciamento de Pessoas (Clientes)
Adicione, edite e remova clientes de forma intuitiva.

Busque clientes rapidamente por nome e CPF.

Validação básica de CPF para garantir que os dados de entrada estejam no formato correto.

Futuramente, visualize o histórico de pedidos de cada cliente, com a possibilidade de marcar o status da venda como "pago".

📦 Gerenciamento de Produtos
Controle completo sobre o catálogo de produtos: adicione, edite e remova itens.

Filtre produtos por nome, código ou faixa de valor.

🛒 Registro de Pedidos
O coração do sistema! Crie novos pedidos selecionando clientes e produtos.

Adicione itens ao pedido com quantidade e veja o cálculo do valor total em tempo real.

Finalize a venda escolhendo a forma de pagamento (dinheiro, cartão, boleto) e registre o pedido para a história.

🏗️ Arquitetura e Estrutura
Este projeto foi construído sobre uma arquitetura MVVM (Model-View-ViewModel), garantindo uma separação clara entre a interface, a lógica e os dados.

View (.xaml): A interface do usuário. Simples e direta, apenas exibe os dados e envia comandos.

ViewModel (.cs): O cérebro da aplicação. Contém a lógica de negócios, comandos para os botões e propriedades que a View "enxerga".

Model (.cs): A representação dos dados (Pessoa, Produto, etc.). Pura e simples, sem nenhuma lógica.

Services (.cs): Responsáveis por interagir com a persistência de dados (no nosso caso, arquivos .json).

🛠️ Tecnologias e Ferramentas
Linguagem: C#

Framework: WPF (.NET Framework 8.0)

Padrão de Arquitetura: MVVM (com classes auxiliares ViewModelBase e RelayCommand)

Persistência: Arquivos JSON, manipulados com System.Text.Json

▶️ Como Executar
Clone o projeto para a sua máquina.

Abra a solução (.sln) no Visual Studio.

Pressione F5 e pronto! A aplicação estará rodando.

🙋‍♂️ Autor
Lucas Pitas
