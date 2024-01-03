namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        //Objetos para o modo ROTATIVO de estacionamento (rot). 
        //Em ordem: Preço Inicial Fixo, Preço por Hora e Lista com os Veículos (Placas).
        private const decimal PrecoInicialRot = 10;
        private const decimal PrecoAdicionalRot = 3;
        public static List<string> veiculosRot = new List<string>();

        //Objetos para o modo MENSALISTA de estacionamento (Mes).
        //Em ordem: Preço Inicial Fixo, Preço por Hora, Lista com os Veículos (Placas), Lista para Cadastro com o Nome do Cliente e o CPF. 
        private const decimal PrecoInicialMes = 250;
        private const decimal PrecoAdicionalMes = 50;
        public static List<string> veiculosMes = new List<string>();
        public static List<string> CadastroNomeMes = new List<string>();
        public static List<string> CadastroCpfMes = new List<string>();

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

        //Função de Estacionar Veículos.
        public void AdicionarVeiculo(String letra)
        {
            letra = letra.ToUpper();
            //Opção Rotativo.
            if(letra == "R"){
                //Verificar se o estacionamento rotativo está lotado.
                if(veiculosRot.Count == LimiteMaximo){
                    Console.WriteLine("O estacionamento está lotado. Por gentileza, procure outro estabelecimento ou espere por uma vaga. ");
                }
                else{
                    //Função de Estacionar implementada.
                    Console.WriteLine($"Seja bem vindo ao sistema de estacionamento!\n Atualmente, há {Estacionamento.LimiteMaximo - Estacionamento.veiculosRot.Count} vagas disponíveis. ");
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
                    Console.WriteLine("O seu carro foi estacionado! Agradecemos a confiança");
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
                    Console.WriteLine($"Seja bem vindo ao sistema de estacionamento!\n Atualmente, há {Estacionamento.LimiteMaximo - Estacionamento.veiculosMes.Count} vagas disponíveis. ");
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
                            Console.WriteLine($"O seu carro foi estacionado e o preço total foi de: R$ {valorTotalMes}. Agradecemos a preferência! Agradecemos a confiança");
                        }
                        else{
                        Console.WriteLine("Seu nome não está cadastrado no sistema de Estacionamento Mensalista. Por gentileza, faça o cadastro antes para aproveitar essa opção.");
                        }
                }
            }
        }

        //Função de Cadastrar Clientes Mensalistas.
        //Diferentemente do cliente rotativo, o cliente mensalista faz parte de um plano mais rebuscado, incluindo cadastro, já que passará mais tempo.
        public void CadastrarMensalista(){
            Console.WriteLine("Sistema de cadastramento para o plano mensalista de Estacionamento");
            Console.WriteLine("Insira seu nome e sobrenone: ");
            //Cadastro do nome e do cpf do cliente mensalista.
            string nome = Console.ReadLine();
            CadastroNomeMes.Add(nome);
            Console.WriteLine("Insira seu CPF: ");
            string cpf = Console.ReadLine();
            CadastroCpfMes.Add(cpf);
            //Opção do cliente, agora cadastrado, estacionar o veículo no modo mensalista. 
            Console.WriteLine("Agradecemos o cadastro no sistema Estacionamento Mensalista. Caso queira já estacionar seu veículo no modo mensalista, é só clicar a tecla M. Caso não, é só clicar qualquer outra tecla.");
            string tecla = Console.ReadLine();
            tecla = tecla.ToUpper();
            if(tecla == "M"){
                AdicionarVeiculo(tecla);
            }
        }

        //Função de Remover Veículos.
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
                        Console.WriteLine($"O veículo {placaMes} foi removido");
                    }
                    else{
                        Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
                    }
                } 
                else{
                    Console.WriteLine("Seu nome não está cadastrado no sistema de Estacionamento Mensalista. Por gentileza, faça o cadastro antes para aproveitar essa opção.");
                }
            }
        }
            
        //Função de Listar Veículos (Apenas para Administradores)
        public void ListarVeiculos(){
            //Listar todos os veículos rotativos que foram estacionados (incluindo os que já foram removidos).
            if (veiculosRot.Any()){
                Console.WriteLine("Os veículos rotativos estacionados são:");
                for (int contador = 0; contador < veiculosRot.Count; contador++){
                    Console.WriteLine($"Veículo {contador + 1}: {veiculosRot[contador]}\n");
                }
            }
            else{
                //Listar todos os veículos mensalistas que foram estacionados (incluindo os que já foram removidos).
                Console.WriteLine("Não há veículos rotativos estacionados.");
            }
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

        //Listar as informações dos clientes (apenas para os administradores).
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
    

        //Função para mostrar o Rendimento Total do Estacionamento.
        public void RendimentoTotal(){
            //Rendimento Rotativo
            if(RendimentoRot.Any()){
                decimal SomaRot = RendimentoRot.Sum();
                Console.WriteLine($"O rendimento da parte do estacionamento rotativo foi de: R$ {SomaRot}");
            }
            else{
                Console.WriteLine("Não houve rendimeto do estacionamento rotativo ");
            }

            //Rendimento Mensalista
            if(RendimentoMes.Any()){
                decimal SomaMes = RendimentoMes.Sum();
                Console.WriteLine($"O rendimento da parte do estacionamento menaalista foi de: R$ {SomaMes}");
            }
            else{
                Console.WriteLine("Não houve rendimeto do estacionamento mensalista ");
            }
            
            //Rendimento Total
            Console.WriteLine($"O rendimento total foi de: R${RendimentoRot.Sum() + RendimentoMes.Sum()}");
        }
    }
}
