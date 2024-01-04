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
- **2.1 Adicionar Veículo**: Método responsável por, a partir da resposta do usuário (cliente), direcioná-lo para a área escolhida( rotativo ou mensalista), adicionar o veículo a partir da placa entregue ao sistema e armazenar os dados do usuário para visualização do administrador.

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

## Diagrama de Funcionamento 
<img src="Mapa Conceitual II - Whendel Muniz dos Santos - Página 2.png">

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
