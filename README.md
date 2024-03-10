> # Event Bus Inbox System

> Introdução

Projeto para simulação do design pattern "Inbox", o qual visa centralizar a comunicação entre sistemas em ambiente orientado a eventos.

Este projeto consiste em uma API (construída em .NET 6), um site (construído em React Typescript), um banco de dados NoSql (MongoDB) e um barramento de eventos (no caso foi utilizado o RabbitMQ, porém facilmente convertido para outros modelos de mensageria).

Dentre as funcionalidades do sistema, é possível criar filas para recebimento de mensagens automaticamente, fazer envio de mensagens no barramento e alterar o estado das mensagens.

> IDE'S utilizadas

Foram utilizadas as seguintes IDE's:

- Docker Desktop: 4.27.1
- Visual Studio Community 2022 17.9.0
- Visual Studio Code 1.87.1
- Studio 3T for MongoDB 2024.1.0

> Contexto da utilização

Pensando em um ambiente complexo, mais precisamente um ambiente com microsserviços, o sistema de inbox foi contruído para facilitar a leitura das mensagens, além de tratar um problema que é corriqueiro na utilização de mensagerias: "deadletter".

Como caso de uso, imagine uma empresa na qual precisa enviar notificações entre setores, e que além disso, para algumas notificações é preciso realizar algumas ações, como conversão e enriquecimento de dados, armazenamento, envio para sistemas de terceiros. Nesse ambiente foi utilizado mensageria, para facilitar a comunicação.

Nesta mesma empresa, existe um setor que precisa ler absolutamente todas as notificações que foram emitidas pelos outros setores, e que precisa realizar alguns tratamentos, que podem ter um tempo de execução de médio a longo. Logo nesse ponto, em algumas ocasiões, o sistema desse setor pode não estar preparado para realizar a leitura da mensagem, ou pode estar indisponível. Essa situação faz com que a mensagem caia na chamada "deadletter".

Outro problema que ocorre nesta situação, é que, por falta de conhecimento, algumas pessoas tendem a achar que a mensageria é como um banco de dados, onde é possível realizar querys, localizar um item no meio da fila, etc.

Além disso pode haver a necessidade de gerar alguns relatórios relacionados à essa comunicação dos sistemas.

Para tentar sanar alguns desses problemas, é que desenvolvi este sistema de inbox, o qual:
- Cuida do gerenciamento das filas, desde sua criação, como seu estado (habilitado ou desabilitado para leitura);
- Cuida da leitura das mensagens trafegadas na fila, além de armazená-las no banco de dados da aplicação;
- Cuida do estado da mensagem em si, permitindo marcar como "completada"/"falha temporária"/"falha permanente";
- Cuida da habilitação de mensagens em estado concluído ou de falha permanente para reprocessamento.

> Evoluções futuras

Como evolução, existem alguns pontos que podem ser melhorados/criados:

- Correção na paginação da lista de filas/mensagens;
- Criação de painel de controle para poder trabalhar com diferentes tipos de mensagerias, como RabbitMQ/Kafka/Azure Service Bus.

> Execução do sistema

Para executar o sistema em modo hospedado, foi utilizado o Docker. Abaixo segue um pequeno manual para executar o sistema, utilizando o prompt de comando do sistema operacional (em caso de ambiente Windows, recomendo executar o prompt como administrador, além de utilizar o Powershell).

1. Baixar as imagens dependentes utilizadas pelo sistema:

Para baixar a imagem do MongoDB, execute o comando:

``
docker volume create mongo
``

Para baixar a imagem do RabbitMQ, execute o comando:

``
docker volume create rabbitmq
``

2. Criar rede para comunicação entre os containers

Para que aja a devida comunicação entre os containers (site chamando API, API chamando o MongoDB e RabbitMQ), faz-se necessário criar uma rede no docker. Para isso, execute o comando abaixo:

``
docker network create -d bridge eventbus-inbox
``

3. Criar os volumes utilizados por alguns dos containers

Para execução dos containers, também faz-se necessário criar os volumes, para armazenamento dos arquivos do banco de dados, como também da mensageria. Para isso, execute os comandos abaixo:

``
docker volume create mongo
``

``
docker volume create rabbitmq
``

4. Criação das imagens do site e API

Para criação da imagem do site, no prompt de comando, navegue até a pasta ["./Site"](https://github.com/GustavoAraujo26/eventbus-inbox/tree/master/Site), e execute o comando abaixo:

``
docker build -t gustavoaraujo26/eventbus-inbox-site
``

Para criação da imagem da API, no prompt de comando, navegue até a pasta ["./API"](https://github.com/GustavoAraujo26/eventbus-inbox/tree/master/API), e execute o comando abaixo:

``
docker build -t gustavoaraujo26/eventbus-inbox-api
``

5. Execução do ambiente em si

Por fim, para executar o ambiente, na pasta raiz do repositório (onde está localizado o arquivo ["docker-compose.yaml"](https://github.com/GustavoAraujo26/eventbus-inbox/blob/master/docker-compose.yaml)), execute o comando abaixo:

``
docker compose up -d
``

Ao final da execução, você poderá acessar as aplicações nas seguintes URL's:

* API: [localhost:9000](http://localhost:9000);
* Site: [localhost:7500](http://localhost:7500);

**Observação:**

Ao executar o "docker compose", pode ser que o site demore a subir, questão de 10 minitos ou mais. Caso tenha passado esse tempo, ao acessar o endereço esteja tomando o erro "ERR_EMPTY_RESPONSE" no browser, acesse os logs do container no Docker Desktop. Caso os logs estejam paralizados como na imagem abaixo, pare o "docker compose" e execute novamente o comando do item 5.

![Erro Docker Container Site](/Images/DockerSiteError.png)

 Projeto de exemplo de sistema de inbox para barramento de eventos

//Comando para criar a rede
docker network create -d bridge eventbus-inbox

//Comando para criar os volumes
docker volume create mongo
docker volume create rabbitmq

//Comando para criar as imagens
docker pull rabbitmq
docker pull mongo
docker build -t gustavoaraujo26/eventbus-inbox-site
docker build -t gustavoaraujo26/eventbus-inbox-api

//Comando para subir o ambiente
docker compose up -d

//Versões das IDE's
Docker Desktop: 4.27.1
Visual Studio Community 2022 17.9.0
Visual Studio Code 1.87.1
Studio 3T for MongoDB 2024.1.0

// Bibliotecas da API
.NET Core 6
Asp.Versioning.Mvc 6.4.1
Asp.Versioning.Mvc.ApiExplorer 6.4.0
AutoMapper 13.0.1
MediatR 12.2.0
FluentValidation 11.9.0
MongoDB.Driver 2.24.0
RabbitMQ.Client 6.8.1
Serilog 3.1.1
Serilog.AspNetCore 8.0.1
Serilog.Sinks.File 5.0.0
Serilog.Sinks.MongoDB 5.4.
Swashbuckle.AspNetCore 6.5.0
Swashbuckle.AspNetcore.Annotations 6.5.0
xunit 2.4.2
xunit.runner.visualstudio 2.4.5
Bogus 35.4.0
Moq 4.20.70

// Bibliotecas do Site
react 18.2.0
react-router-dom 6.22.1
react-redux 9.1.0
@reduxjs/toolkit 2.2.1
@types/react-redux 7.1.33
uuid 9.0.1
axios 1.6.7
@mui/material 5.15.11
@mui/icons-material 5.15.11
@mui/x-date-pickers 6.19.6
@fontsource/roboto 5.0.8
dayjs 1.11.0
