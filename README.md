## Sobre o projeto

Esta **API**, desenvolvida em **.NET** 8. adota os princípios de **Domain-Drive Design (DDD)** para oferecer uma solução estruturada e eficaz no gerenciamento de despesas pessoais. O principal objetivo é permitir que os usuários registrem suas despesas, detalhando informações como título, data e hora, descrição, valor e tipo de pagamento, com os dados sendo armazenados de forma segura em um banco de dados **MySQL**.

A arquitetura da **API** baseia-se em **REST**, utilizando métodos **HTTP** padrão para uma comunicação eficiente e simplificada. Além disso, é complementada por uma documentação **Swagger**, que proporciona uma interface gráfica interativa para que os desenvolvedores possam explorar e testar os endpoints de maneira fácil.

Dentre os pacotes NuGet utilizados, o **AutoMapper** é o responsável pelo mapeamento entre objetos de domínio e requisição/resposta, reduzindo a necessidade de código repetitivo e manual. O **FluentAssertions** é utilizado nos testes de unidade para tornar as verificações mais legíveis, ajudando a escrever testes claros e compreensíveis. Para as validações, o **FluentValidation** é usado para implementar regras de validação de forma simples e intuitiva nas classes de requisição, mantendo o código limpo e fácil de manter. Por fim, o **EntityFramework** atua como um ORM (Object-Relational Mapper) que simplifica as interações com o banco de dados, permitindo o uso de objetos .NET para manipular dados diretamente, sem a necessidade de lidar com consultas SQL.

### Features

- **Domain-Drive Design (DDD)**: Estrutura modular que facilita o entendimento e a manutenção do domínio da aplicação.
- **Testes de Unidade**: Testes abrangentes com FluentAssertions para garantir a funcionalidade e a qualidade.
- **Geração de relatórios**: Capacidade de exportar relatórios detalhados para **PDF** e **Excel**, oferecendo uma análize visual e eficaz das despesas.
- **RESTful API com Documentação Swagger**: Interface documentada que facilita a integração e o teste por parte dos desenvolvedores.

### Construído com

![badge_dot_net]
![badge_windows]
![badgevisual_studio]
![badge_mysql]
![badge_swagger]

## Getting Started

Para obter uma cópia local funcionando, siga estes passos simples.

### Requisitos

* Visual Studio versão 2022+ ou Visual Studio Code.
* Windows 10+ ou Linux/MacOS com [.NET SDK][dot_net_sdk] instalado
* MySql Server

### Instalação

1. Clone o repositório:

    ```sh
    git clone https://github.com/JefersonPFernandes/CashFlow.git
    ```

2. Preencha as informações no arquivo `appsettings.Development.json`.
3. Execute a API e aproveite o teste.

## Exemplo de relatório em PDF.

![pdf_image]




<!-- links -->
[dot_net_sdk]: https://dotnet.microsoft.com/en-us/download/dotnet/8.0


<!-- images -->
[pdf_image]: pdfimage.png

<!-- Badges -->

[badge_dot_net]: https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge
[badge_windows]: https://img.shields.io/badge/Windows-blue?style=for-the-badge&logo=Windows&logoColor=white
[badgevisual_studio]: https://img.shields.io/badge/Visual%20Studio-purple?style=for-the-badge&logo=visualstudio&logoColor=white
[badge_mysql]: https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=for-the-badge
[badge_swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge

