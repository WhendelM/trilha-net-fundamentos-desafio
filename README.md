# DIO - Trilha .NET - Fundamentos
www.dio.me

## Desafio de projeto
Criar um sistema de estacionamento a partir dos conhecimentos sobre Programação Orientada a Obejtos (POO).

## Proposta

## Metodologia 

1. A classe **Estacionamento** abriga 4 variáveis:

**precoInicial**: Tipo decimal. É o preço cobrado para deixar seu veículo estacionado.

**precoPorHora**: Tipo decimal. É o preço por hora que o veículo permanecer estacionado.

**veiculos**: É uma lista de string, representando uma coleção de veículos estacionados. Contém apenas a placa do veículo.

**LimiteMaximo (Incremento)**: A quantidade máxima de veículos que o estacionamento pode comportar. 
Com o passar dos cadastros, o sistema mostrará a quantidade de vagas disponíveis e caso o estacionamento esteja sem vagas, a opção de Adicionar Veículos estará indisponível.

A classe contém quatro métodos, sendo:

**AdicionarVeiculo**: Método responsável por receber uma placa digitada pelo usuário e guardar na variável **veiculos** caso haja vagas disponíveis. 

**RemoverVeiculo**: Método responsável por verificar se um determinado veículo está estacionado, e caso positivo, irá pedir a quantidade de horas que ele permaneceu no estacionamento. Após isso, realiza o seguinte cálculo: **precoInicial** * **precoPorHora**, exibindo para o usuário.

**ListarVeiculos**: Lista todos os veículos presentes atualmente no estacionamento. Caso não haja nenhum, exibir a mensagem "Não há veículos estacionados".

Por último, deverá ser feito um menu interativo com as seguintes ações implementadas:
1. Cadastrar veículo
2. Remover veículo
3. Listar veículos
4. Encerrar

2. No **Programa** será mostrada uma mensagem indicando a quantidade de vagas disponíveis e o menu interativo.
