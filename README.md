# Projeto de E-commerce - README

## Introdução

Este projeto é uma implementação de um sistema de e-commerce com foco em uma Loja Virtual para produtos diversos. A plataforma é construída seguindo as boas práticas de desenvolvimento, como a arquitetura MVC e orientação a objetos, e padrões de projetos amplamente reconhecidos pela comunidade de desenvolvimento de software.

## Decisões Técnicas e Arquiteturais

- **Arquitetura MVC:** O projeto segue o padrão arquitetural Model-View-Controller (MVC) para separar as preocupações de apresentação, lógica de negócios e persistência de dados.

- **Banco de Dados:** Foi escolhido o banco de dados relacional MySQL para persistência dos dados. Certifique-se de ter o MySQL instalado em sua máquina antes de executar o projeto.

- **Exceções Personalizadas:** Foram implementadas exceções personalizadas para lidar com situações específicas da aplicação. Isso permite um melhor controle sobre os erros e facilita a depuração.

## Frameworks e Bibliotecas

- **ASP.NET Core MVC:** Utilizado como framework principal para desenvolvimento web, seguindo o padrão MVC para separar as responsabilidades.

- **Entity Framework:** Utilizado como ORM para a comunicação com o banco de dados relacional, simplificando a persistência dos dados.

- **Newtonsoft.Json:** Utilizado para a serialização e desserialização de objetos JSON, permitindo a persistência do carrinho de compras na sessão.

## Compilação e Execução do Projeto

1. Clone o repositório para sua máquina local.
2. Abrir a solução no Visual Studio (recomendada a versão 2022 ou superior).
3. Configurar a conexão com o banco de dados no arquivo `appsettings.json`.
4. No Console do Gerenciador de Pacotes, executar o comando: `Update-Database`, para criar as tabelas no banco de dados.
5. Executar o projeto.

## Práticas de Desenvolvimento

- Utilização de padrões de projeto como Service para separação de responsabilidades.
- Chamadas de métodos ocorrem de maneira assíncrona, melhorando a escalabilidade e responsividade da aplicação.
- Validações rigorosas implementadas nos formulários, tanto no lado do cliente quanto do servidor, para preservar a integridade dos dados e fornecer uma experiência confiável ao usuário e prevenir possíveis problemas de envio de dados nulos.

## Notas Adicionais

- A parte de Frontend do projeto foi ajustada para ser responsiva, então, se desejarem testar em outras telas por meio do Dev Console do navegador sintam-se a vontade para verem os ajustes dos elementos ao tamanho da tela e para fazerem quaisquer observações sobre o projeto

