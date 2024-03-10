# eventbus-inbox
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