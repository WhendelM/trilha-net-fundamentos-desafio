# DIO - Trilha .NET - Fundamentos
www.dio.me

## Desafio de projeto
Criar um sistema de estacionamento privado a partir dos conhecimentos sobre **Programação Orientada a Obejtos (POO)** vistos no **Bootcamp Decola Tech 2024 - Avanade**. 

**Feito por: Whendel Muniz dos Santos**

## Proposta Incrementada 
O modelo de estacionammento foi incrementado com 2 modalidades:

**1. Área do Cliente:** Área aberta para todos os clientes que venham a se interessar no estacionamento. Neste projeto, há dois modelos:

   **1.1 Estacionamento Rotativo:** Modelo baseado em estacionar veículos durante até 1 dia e o valor é pago na saída (retirada do veículo após o tempo de estacionamento).
         Neste método, apesar de nao haver cadastro obrigatório, são solicitados o nome e o CPF do usuário que estacionar o veículo. 
         
   **1.1.1 Opções no Modo Rotativo:**
             Estacionar Veículos
             Remover Veículos
             
  **1.2 Estacionamento Mensalista:** Modelo baseado em estacionar veículos durante, pelo menos, 1 mês com valor pago na entrada (é feito um cadastro e um plano de estacionamento previamente com pagamento imediato).
        Neste método, para acessar o Modo Mnesalista, é necessário, obrigatoriamente, estar cadastrado previamente no sistema, sendo solicitados o nome e o CPF do usuário que 
        estacionar o veículo.  
        
**1.2.1 Opções no Modo Mensalista:**
             Cadastrar Cliente
             Estacionar Veículos
             Remover Veículos

  
**2. Área do Administrador**: Área fechada e com entrada liberada apenas para funcionários ou colaboradores que possuam uma senha específica disponibilizada pela empresa. É uma área privativa com informações confindenciais e utilizada como banco de dados para a segurança dos usuários e da empresa, além de auxiliar na elaboração de relatórios financeiros.       

**2.1 Opções da Área do Administrador**
               Listar Veículos
               Listar Clientes
                Acessar Rendimento Financeiro

## Metodologia de Código 

**1. Varíavies**:
- **1.1 Modo Rotativo**


       namespace DesafioFundamentos.Models
      {

      public class Estacionamento
      {
        //Objetos para o modo ROTATIVO de estacionamento (rot). 
        //Em ordem: Preço Inicial Fixo, Preço por Hora e Lista com os Veículos (Placas).
        private const decimal PrecoInicialRot = 10;
        private const decimal PrecoAdicionalRot = 3;
        public static List<string> veiculosRot = new List<string>();

- **1.2 Modo Mensalista**

        
        //Objetos para o modo ROTATIVO de estacionamento (rot).
        //Em ordem: Preço Inicial Fixo, Preço por Hora e Lista com os Veículos (Placas).
        private const decimal PrecoInicialRot = 10;
        private const decimal PrecoAdicionalRot = 3;
        public static List<string> veiculosRot = new List<string>();


- **1.3 Administrador**

        //Objetos para o controle do Administrador.
        //Em ordem: Listas com os veículos diários,com os nomes, os cpfs, o rendimento de cada parte do estacionamento (Rotativo e Mensalista) e o limite máximo dos veículos das duas partes. 
        private static List<string> veiculosRotAdm = new List<string>();
        private static List<string> NomesRotAdm = new List<string>();
        private static List<string> CpfRotAdm = new List<string>();
        private static List<decimal> RendimentoRot = new List<decimal>();
        private static List<string> veiculosMesAdm = new List<string>();
        private static List<string> NomesMesAdm = new List<string>();
        private static List<string> CpfMesAdm = new List<string>();
        private static List<decimal> RendimentoMes = new List<decimal>();
        private const int LimiteMaximo = 3; 

**2. Funções**:
- **2.1 Adicionar Veículo**: Método responsável por, a partir da resposta do usuário (cliente), direcioná-lo para a área escolhida( rotativo ou mensalista), adicionar o veículo a partir da placa entregue ao sistema e armazenar os dados do usuário para visualização do administrador. Ná área Rotativa, ao estacionar o carro, não é feito o pagamento, apenas na retiraada; já na área Mensalista, o pagamento é feito na entrada. Nessa função, também é possível controlar a quantidade de veículos em cada área do estacionamento, ou seja, se o estacionamento estiver lotado, o usuário não cnnsegue estacionar o veículo. 

        //Função de Estacionar Veículos.
        public void AdicionarVeiculo(String letra)
        {
            letra = letra.ToUpper();
            //Opção Rotativo.
            if(letra == "R"){
                //Verificar se o estacionamento rotativo está lotado.
                if(veiculosRot.Count == LimiteMaximo){
                    Console.WriteLine("O estacionamento está lotado.");
                    Console.WriteLine("Por gentileza, procure outro estabelecimento ou espere por uma vaga.");
                }
                else{
                    //Função de Estacionar implementada.
                    Console.WriteLine($"Seja bem vindo ao sistema de Estacionamento Rotativo!\n Atualmente, há {Estacionamento.LimiteMaximo - Estacionamento.veiculosRot.Count} vagas disponíveis. ");
                    Console.WriteLine("A tarifa é de R$ 10,00 e o preço adicional por hora é de R$ 3,00.");
                    Console.WriteLine("Digite o seu nome e sobrenome para continuar: ");

                    //Armazenamento de Nome e CPF para visualização do Administrador do sistema.
                    NomesRotAdm.Add(Console.ReadLine());
                    Console.WriteLine("Agora, digite o seu CPF para continuar: ");
                    CpfRotAdm.Add(Console.ReadLine());
                    Console.WriteLine("Informe a placa do seu veículo: ");
                    string placaRot = Console.ReadLine();

                    //Armazenamento do veículo no estacionamento rotativo e no ambiente do Administrador do sistema.
                    veiculosRot.Add(placaRot);
                    veiculosRotAdm.Add(placaRot);
                    placaRot = placaRot.ToUpper();
                    Console.WriteLine("O seu carro foi estacionado! Agradecemos a preferência.");
                }

            }

            //Opção Mensalista.
            else{
                //Verificar se o estacionamento mensalista está lotado.
                if(veiculosMes.Count == LimiteMaximo){
                        Console.WriteLine("O estacionamento está lotado. Por gentileza, procure outro estabelecimento ou espere por uma vaga. ");
                }
                else{
                    //Função de Estacionar implementada.
                    Console.WriteLine($"Seja bem vindo ao sistema de Estacionamento Mensalista!\n Atualmente, há {Estacionamento.LimiteMaximo - Estacionamento.veiculosMes.Count} vagas disponíveis. ");
                    Console.WriteLine("A tarifa é de R$ 250,00 e o preço adicional por mês é de R$ 50,00.");
                    Console.WriteLine("Digite seu nome e sobrenome para verificação: ");
                    string verificacao = Console.ReadLine();

                    //Verificar se o usuário já está cadastrado no sistema de estacionamento mensalista.
                        if (CadastroNomeMes.Any(x => x == verificacao)){
                            //Armazenamento de Nome e CPF para visualização do Administrador do sistema.
                            NomesMesAdm.Add(verificacao);
                            Console.WriteLine("Digite o seu CPF: ");
                            CpfMesAdm.Add(Console.ReadLine());
                            Console.WriteLine("Digite a placa do seu veículo: ");

                            //Armazenamento do veículo no estacionamento mensalista e no ambiente do Administrador do sistema. 
                            string placaMes = Console.ReadLine();
                            placaMes = placaMes.ToUpper();
                            veiculosMes.Add(placaMes);
                            veiculosMesAdm.Add(placaMes);

                            //Cálculo do valor total para o usuário pagar por uma vaga no estacionamento mensalista (o pagamento é feito  ANTES da ENTRADA do estacionamento).
                            Console.WriteLine("Digite a quantidade de meses que o veículo permanecerá estacionado:");
                            int Mes = Convert.ToInt32(Console.ReadLine());
                            decimal valorTotalMes = PrecoInicialMes + (PrecoAdicionalMes* Mes);

                            //O valor é armazenado no Rendimento total do estacionamento mensalista.
                            RendimentoMes.Add(valorTotalMes);
                            Console.WriteLine($"O seu carro foi estacionado e o preço total foi de: R$ {valorTotalMes}. Agradecemos a preferência!");
                        }
                        else{
                            Console.WriteLine("Seu nome não está cadastrado no sistema de Estacionamento Mensalista.");
                            Console.WriteLine("Por gentileza, faça o cadastro antes para aproveitar essa opção.");
                        }
                }
            }
        }

- **2.2 Cadastrar Clientes**: Função responsável por cadastrar clientes **mrnsalistas** para que possam usufruir das funcionalidades da área no estacionamento. O usuário, após o cadastro, pode ser redirecionado para estacionar o veículo, caso queira.
-       //Função de Cadastrar Clientes Mensalistas.
        //Diferentemente do cliente rotativo, o cliente mensalista faz parte de um plano mais rebuscado, incluindo cadastro, já que passará mais tempo.
        public void CadastrarMensalista(){
            Console.WriteLine("Sistema de cadastramento para o plano mensalista de Estacionamento.");
            Console.WriteLine("Insira seu nome e sobrenone: ");

            //Cadastro do nome e do cpf do cliente mensalista.
            string nome = Console.ReadLine();
            CadastroNomeMes.Add(nome);
            Console.WriteLine("Insira seu CPF: ");
            string cpf = Console.ReadLine();
            CadastroCpfMes.Add(cpf);

            //Opção do cliente, agora cadastrado, estacionar o veículo no modo mensalista. 
            Console.WriteLine("Agradecemos o cadastro no sistema Estacionamento Mensalista.");
            Console.WriteLine("Caso queira já estacionar seu veículo no modo mensalista, é só clicar a tecla M.");
            Console.WriteLine("Caso não, é só clicar qualquer outra tecla.");
            string tecla = Console.ReadLine();
            tecla = tecla.ToUpper();

            //O cliente é direcionado para a opção de Adicionar Veículo do modo Mensalista.
            if(tecla == "M"){
                AdicionarVeiculo(tecla);
            }
        }

- **2.3 Remover Veículos**: Função responsável por remover os veículos de cada área do estacionamento. Nessa função, o pagamento do cliente rotativo é efetuado.

            //Função de Remover Veículos
            public void RemoverVeiculo(string letra){ 
            //Opção Rotativo.  
            if(letra == "R") {
                Console.WriteLine("Digite a placa do veículo para remover: ");
                string placaRot = Console.ReadLine();
                //Verificar se o veículo foi estacionado. 
                if (veiculosRot.Any(x => x.ToUpper() == placaRot.ToUpper())){
                    Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                    //Retirada do Veículo e cálculo do valor total do pagamento do cliente (o pagamento é feito ANTES da SAÍDA do estacionamento)
                    int horas = Convert.ToInt32(Console.ReadLine());
                    decimal valorTotalRot = PrecoInicialRot + (PrecoAdicionalRot * horas);

                    //O valor é armazenado no Rendimento total da parte rotativa e o veículo é removido do estacionamento.
                    RendimentoRot.Add(valorTotalRot);
                    veiculosRot.Remove(placaRot);
                    Console.WriteLine($"O veículo {placaRot} foi removido e o preço total foi de: R$ {valorTotalRot}. Agradecemos a preferência!");
                }
                else{
                    Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
                }
            }
            //Opção Mensalista.
            else{
                Console.WriteLine("Digite seu nome e sobrenome para verificação: ");
                string verificacao = Console.ReadLine();

                //Verificar se o usuário já está cadastrado no sistema de estacionamento mensalista.
                if (CadastroNomeMes.Any(x => x == verificacao)){
                    Console.WriteLine("Digite a placa do veículo para remover: ");
                    string placaMes = Console.ReadLine();

                   //Verificar se o veículo foi estacionado e Remoção do veículo.
                    if (veiculosMes.Any(x => x.ToUpper() == placaMes.ToUpper())){
                        veiculosRot.Remove(placaMes);
                        Console.WriteLine($"O veículo {placaMes} foi removido! Agradecemos a preferência!");
                    }
                    else{
                        Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente!");
                    }
                } 
                else{
                    Console.WriteLine("Seu nome não está cadastrado no sistema de Estacionamento Mensalista.");
                    Console.WriteLine("Por gentileza, faça o cadastro antes para aproveitar essa opção!");
                }
            }
        }

   **2.4 Listar Veículos**: Função utilizada apenas por administradores e responsável poe listar todos os veículos de ambas as áreas.

             public void ListarVeiculos(){
            //Listar todos os veículos rotativos que foram estacionados (incluindo os que já foram removidos).
            if (veiculosRot.Any()){
                Console.WriteLine("Os veículos rotativos estacionados são:");
                for (int contador = 0; contador < veiculosRot.Count; contador++){
                    Console.WriteLine($"Veículo {contador + 1}: {veiculosRot[contador]}\n");
                }
            }
            else{
                Console.WriteLine("Não há veículos rotativos estacionados.");
            }

            //Listar todos os veículos mensalistas que foram estacionados (incluindo os que já foram removidos).
            if(veiculosMes.Any()){
                Console.WriteLine("Os veículos mensalistas estacionados são:");
                for (int contador = 0; contador < veiculosMes.Count; contador++){
                    Console.WriteLine($"Veículo {contador + 1}: {veiculosMes[contador]}\n");
                }
            }
            else{
                Console.WriteLine("Não há veículos mensalistas estacionados.");
            }    
        }

**2.5 Listar Clientes**: Função utilizada apenas por administradores e responsável poe listar todos os clientes e seus respectivos veículos estacionados. 
 
        //Listar as informações dos clientes (apenas para Administradores).
        public void ListarClientes(){
            Console.WriteLine("Para listar o total de clientes rotativos, digite R \n Para listar o total de clientes mensalistas ativos, digite M1 \n Para listar todos os clientes mensalistas cadastrados, digite M2 ");
            string opcoes = Console.ReadLine();
            opcoes = opcoes.ToUpper();
            //Opção Rotativo (Apenas dos que estacionaram).
            if(opcoes == "R"){
                if(NomesRotAdm.Any()){
                    //Lista os nomes, os cpfs e os veículos vinculados aos clientes rotativos que estacionaram os carros e removeram no dia.
                    Console.WriteLine("Os clientes rotativos são:");
                    for (int contador = 0; contador < NomesRotAdm.Count; contador++){
                                Console.WriteLine($"Nome: {NomesRotAdm[contador]}");
                                Console.WriteLine($"CPF: {CpfRotAdm[contador]}");
                                Console.WriteLine($"Placa do Veículo: {veiculosRotAdm[contador]}\n");
                            }  
                    }
                    else{
                        Console.WriteLine("Não há clientes rotativos no momento.");
                    }
                }

            //Opção Mensalista (Apenas dos que estacionaram).
            else if(opcoes == "M1"){
                if(NomesMesAdm.Any()){
                    //Lista os nomes, os cpfs e os veículos vinculados aos clientes mensalistas que estacionaram os carros e removeram no dia.
                    Console.WriteLine("Os clientes mensalistas são:");
                        for (int contador = 0; contador < NomesMesAdm.Count; contador++){
                                    Console.WriteLine($"Nome {contador + 1}: {NomesMesAdm[contador]}");
                                    Console.WriteLine($"CPF: {CpfMesAdm[contador]}");
                                    Console.WriteLine($"Placa do Veículo: {veiculosMesAdm[contador]}");
                        }
                    }
                else{
                    Console.WriteLine("Não há clientes mensalistas no momento.");
                }
            }
                

            //Opção Mensalista (TODOS os cadastrados).
            else{
                if(CadastroNomeMes.Any()){
                    Console.WriteLine("Os clientes mensalistas cadastrados são:");
                    //Lista os nomes e os cpfs dos clientes mensalistas cadastrados 
                        for (int contador = 0; contador < CadastroNomeMes.Count; contador++){
                                Console.WriteLine($"Nome: {CadastroNomeMes[contador]}");
                                Console.WriteLine($"CPF: {CadastroCpfMes[contador]}");
                            }
                        }
                else{
                    Console.WriteLine("Não há cadastro de clientes mensalistas no momento.");
                }
            }
        }

**2.6 Listar Clientes**: Função utilizada apenas por administradores e responsável poe disponibilizar o rendimento financeiro de cada área e, consequentemente, o renimento total do estacionamento. 
        
        //Função para mostrar o Rendimento Total do Estacionamento (Apenas para Administradores).
        public void RendimentoTotal(){
            //Rendimento Rotativo
            if(RendimentoRot.Any()){
                decimal SomaRot = RendimentoRot.Sum();
                Console.WriteLine($"O rendimento da parte do estacionamento rotativo foi de: R$ {SomaRot}.");
            }
            else{
                Console.WriteLine("Não houve rendimeto do estacionamento rotativo.");
            }

            //Rendimento Mensalista
            if(RendimentoMes.Any()){
                decimal SomaMes = RendimentoMes.Sum();
                Console.WriteLine($"O rendimento da parte do estacionamento menaalista foi de: R$ {SomaMes}.");
            }
            else{
                Console.WriteLine("Não houve rendimeto do estacionamento mensalista.");
            }
            
            //Rendimento Total
            Console.WriteLine($"O rendimento total foi de: R${RendimentoRot.Sum() + RendimentoMes.Sum()}.");
        }
    }
    }
## Diagrama de Funcionamento 
<img src="Diagrama - Whendel Muniz dos Santos.png">

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
