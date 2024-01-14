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
        //Em ordem: Listas com os veículos diários,com os nomes, os cpfs,os horários (de estacionamento e retirada) o rendimento de cada parte do estacionamento (Rotativo e Mensalista) e o limite máximo dos veículos das duas partes. 
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
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(
                    " \nNo momento, nosso estacionamento está lotado.\n"+
                    "Por gentileza, procure outro estabelecimento ou espere por uma vaga. \n"
                    );
                }
                else{
                    //Função de Estacionar implementada.
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(
                    $" \nAtualmente, há {Estacionamento.LimiteMaximo - Estacionamento.veiculosRot.Count} vagas disponíveis.\n"+
                    "A tarifa é de R$ 10,00 e o preço adicional por hora é de R$ 3,00.\n"+
                    "Digite o seu nome e sobrenome para continuar: \n"
                    );
                    //Armazenamento de Nome e CPF para visualização do Administrador do sistema.
                    Console.ForegroundColor = ConsoleColor.White;
                    NomesRotAdm.Add(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Agora, digite o seu CPF para continuar: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    CpfRotAdm.Add(Console.ReadLine());
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Informe a placa do seu veículo: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string placaRot = Console.ReadLine();
                    placaRot = placaRot.ToUpper();

                    //Armazenamento do veículo no estacionamento rotativo e no ambiente do Administrador do sistema.
                    veiculosRot.Add(placaRot);
                    veiculosRotAdm.Add(placaRot);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(
                    " \nO seu veículo foi estacionado!\n"+
                    "Agradecemos a preferência e confiança! \n"
                    );
                }

            }

            //Opção Mensalista.
            else{
                //Verificar se o estacionamento mensalista está lotado.
                if(veiculosMes.Count == LimiteMaximo){
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(
                        " \nNo momento, nosso estacionamento está lotado.\n"+
                        "Por gentileza, procure outro estabelecimento ou espere por uma vaga. \n"
                        );
                }
                else{
                    //Função de Estacionar implementada.
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(
                    $" \nAtualmente, há {Estacionamento.LimiteMaximo - Estacionamento.veiculosMes.Count} vagas disponíveis.\n"+
                    "A tarifa é de R$ 250,00 e o preço adicional por mês é de R$ 50,00. \n");
                    Console.WriteLine(" \nDigite seu nome e sobrenome para verificação: \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    string verificacao = Console.ReadLine();

                    //Verificar se o usuário já está cadastrado no sistema de estacionamento mensalista.
                        if (CadastroNomeMes.Any(x => x == verificacao)){
                            //Armazenamento de Nome e CPF para visualização do Administrador do sistema.
                            NomesMesAdm.Add(verificacao);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Digite o seu CPF: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            CpfMesAdm.Add(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("Digite a placa do seu veículo: ");

                            //Armazenamento do veículo no estacionamento mensalista e no ambiente do Administrador do sistema. 
                            Console.ForegroundColor = ConsoleColor.White;
                            string placaMes = Console.ReadLine();
                            placaMes = placaMes.ToUpper();
                            veiculosMes.Add(placaMes);
                            veiculosMesAdm.Add(placaMes);

                            //Cálculo do valor total para o usuário pagar por uma vaga no estacionamento mensalista (o pagamento é feito  ANTES da ENTRADA do estacionamento).
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(" \nDigite a quantidade de meses que o veículo permanecerá estacionado:");
                            Console.ForegroundColor = ConsoleColor.White;
                            int Mes = Convert.ToInt32(Console.ReadLine());
                            decimal valorTotalMes = PrecoInicialMes + ((Mes-1)* PrecoAdicionalMes);

                            //O valor é armazenado no Rendimento total do estacionamento mensalista.
                            RendimentoMes.Add(valorTotalMes);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(
                            $" \nO seu carro foi estacionado e o preço total foi de: R$ {valorTotalMes}.\n"+
                            "Agradecemos a preferência e confiança! \n"
                            );
                        }
                        else{
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine(
                            " \nSeu nome não está cadastrado no sistema de Estacionamento Mensalista.\n"+
                            "Para aproveitar esse serviço, é só digitar M e se cadastrar!\n"+
                            "Ou clique na tecla ENTER para sair desse painel. \n"
                            );
                            Console.ForegroundColor = ConsoleColor.White;
                            string realocar = Console.ReadLine();
                            realocar = realocar.ToUpper();
                            if(realocar == "M"){
                            CadastrarMensalista();
                            }
                        }
                }
            }
        }

        //Função de Cadastrar Clientes Mensalistas.
        //Diferentemente do cliente rotativo, o cliente mensalista faz parte de um plano mais rebuscado, incluindo cadastro, já que passará mais tempo.
        public void CadastrarMensalista(){
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Sistema de cadastramento para o plano mensalista de Estacionamento. \n");
            Console.WriteLine("Insira seu nome e sobrenone: \n");

            //Cadastro do nome e do cpf do cliente mensalista.
            Console.ForegroundColor = ConsoleColor.White;
            string nome = Console.ReadLine();
            CadastroNomeMes.Add(nome);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Insira seu CPF: ");
            Console.ForegroundColor = ConsoleColor.White;
            string cpf = Console.ReadLine();
            CadastroCpfMes.Add(cpf);

            //Opção do cliente, agora cadastrado, estacionar o veículo no modo mensalista. 
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(
            " \nAgradecemos o cadastro no sistema Estacionamento Mensalista AV2024.\n"+
            "Caso queira já estacionar seu veículo no modo mensalista, é só digitar M.\n"+
            "Caso não, é só clicar na tecla ENTER. \n"
            );
            Console.ForegroundColor = ConsoleColor.White;
            string tecla = Console.ReadLine();
            tecla = tecla.ToUpper();

            //O cliente é direcionado para a opção de Adicionar Veículo do modo Mensalista.
            if(tecla == "M"){
                AdicionarVeiculo(tecla);
            }
        }

        //Função de Remover Veículos.
        public void RemoverVeiculo(string letra){ 
            //Opção Rotativo.  
            if(letra == "R") {

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Digite a placa do veículo para remover: ");
                Console.ForegroundColor = ConsoleColor.White;
                string placaRot = Console.ReadLine();

                //Verificar se o veículo foi estacionado. 
                if (veiculosRot.Any(x => x.ToUpper() == placaRot.ToUpper())){
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                    //Retirada do Veículo e cálculo do valor total do pagamento do cliente 
                    //(o pagamento é feito ANTES da SAÍDA do estacionamento)
                    Console.ForegroundColor = ConsoleColor.White;
                    int horas = Convert.ToInt32(Console.ReadLine());
                    decimal valorTotalRot = PrecoInicialRot + ((horas-1)* PrecoAdicionalRot);

                    //O valor é armazenado no Rendimento total da parte rotativa e o veículo é removido do estacionamento.
                    RendimentoRot.Add(valorTotalRot);
                    veiculosRot.Remove(placaRot);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(
                    $" \nO veículo {placaRot} foi removido e o preço total foi de: R$ {valorTotalRot}.\n"+
                    "Agradecemos a preferência e confiança! \n)"
                    );
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" \nGostou do nosso serviço de estacionamento?\n"+
                    "Também oferecemos a opção de Estacionamento Mensalista para estacionar veículos por um período maior de tempo!\n"+
                    "Caso tenha se interessado, clique na tecla M.\n"+
                    "Caso não, é só clicar na tecla ENTER. \n"
                    );
                    Console.ForegroundColor = ConsoleColor.White;
                    string realocar = Console.ReadLine();
                    realocar = realocar.ToUpper();
                    if(realocar == "M"){
                        CadastrarMensalista();
                    }
                }
                else{
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(
                    "\n Desculpe, esse veículo não está estacionado aqui.\n"+
                    "Confira se digitou a placa de maneira correta!\n"+
                    "Para tentar novamente, digite R.\n"+
                    "Para sair desse painel, é só clicar na tecla ENTER. \n"
                    );
                    Console.ForegroundColor = ConsoleColor.White;
                    string realocar = Console.ReadLine();
                    realocar = realocar.ToUpper();
                    if(realocar == "R"){
                        RemoverVeiculo("R");
                    }
                }
            }
            //Opção Mensalista.
            else{
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Digite seu nome e sobrenome para verificação: ");
                Console.ForegroundColor = ConsoleColor.White;
                string verificacao = Console.ReadLine();

                //Verificar se o usuário já está cadastrado no sistema de estacionamento mensalista.
                if (CadastroNomeMes.Any(x => x == verificacao)){
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Digite a placa do veículo para remover: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string placaMes = Console.ReadLine();

                   //Verificar se o veículo foi estacionado e Remoção do veículo.
                    if (veiculosMes.Any(x => x.ToUpper() == placaMes.ToUpper())){
                        veiculosRot.Remove(placaMes);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(
                        $"O veículo {placaMes} foi removido!\n"+
                        "Agradecemos a preferência e confiança! \n"
                        );
                    }
                    else{
                       Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(
                    "\n Desculpe, esse veículo não está estacionado aqui.\n"+
                    "Confira se digitou a placa de maneira correta!\n"+
                    "Para tentar novamente, digite M.\n"+
                    "Para sair desse painel, é só clicar na tecla ENTER. \n"
                    );
                    Console.ForegroundColor = ConsoleColor.White;
                    string realocar = Console.ReadLine();
                    realocar = realocar.ToUpper();
                    if(realocar == "M"){
                        RemoverVeiculo("M");
                    }
                    }
                } 
                else{
                    Console.WriteLine("Seu nome não está cadastrado no sistema de Estacionamento Mensalista. \n");
                    Console.WriteLine(
                    "Para se cadastrar e aproveitar nossos serviços,digite M.\n"+
                    "Para sair desse painel, é só clicar na tecla ENTER. \n"
                    );
                    Console.ForegroundColor = ConsoleColor.White;
                    string realocar = Console.ReadLine();
                    realocar = realocar.ToUpper();
                    if(realocar == "M"){
                        CadastrarMensalista();
                    }
                }
            }
        }
        //Função de Listar Veículos (Apenas para Administradores)
        public void ListarVeiculos(){
            //Listar todos os veículos rotativos que foram estacionados (incluindo os que já foram removidos).
            if (veiculosRot.Any()){
                Console.WriteLine(" \nOs veículos rotativos estacionados são: \n");
                for (int contador = 0; contador < veiculosRot.Count; contador++){
                    Console.WriteLine($"Veículo {contador + 1}: {veiculosRot[contador]} \n");
                }
            }
            else{
                Console.WriteLine(" \nNão há veículos rotativos estacionados. \n");
            }

            //Listar todos os veículos mensalistas que foram estacionados (incluindo os que já foram removidos).
            if(veiculosMes.Any()){
                Console.WriteLine(" \nOs veículos mensalistas estacionados são: \n");
                for (int contador = 0; contador < veiculosMes.Count; contador++){
                    Console.WriteLine($"Veículo {contador + 1}: {veiculosMes[contador]} \n");
                }
            }
            else{
                Console.WriteLine(" \nNão há veículos mensalistas estacionados. \n");
            }    
        }

        //Listar as informações dos clientes (apenas para Administradores).
        public void ListarClientes(){
                if(NomesRotAdm.Any()){
                    //Lista os nomes, os cpfs e os veículos vinculados aos clientes rotativos que estacionaram os carros e removeram no dia.
                    Console.WriteLine(" \nOs clientes rotativos são: \n");
                    for (int contador = 0; contador < NomesRotAdm.Count; contador++){
                        Console.WriteLine(
                            $"Nome: {NomesRotAdm[contador]}\n"+
                            $"CPF: {CpfRotAdm[contador]}\n"+
                            $"Placa do Veículo: {veiculosRotAdm[contador]} \n"
                        );
                    }  
                    }   
                else{
                    Console.WriteLine(" \nNão há clientes rotativos no momento. \n");
                }

            //Opção Mensalista (Apenas dos que estacionaram).
                if(NomesMesAdm.Any()){
                    //Lista os nomes, os cpfs e os veículos vinculados aos clientes mensalistas que estacionaram os carros e removeram no dia.
                    Console.WriteLine(" \nOs clientes mensalistas ativos são: \n");
                    for (int contador = 0; contador < NomesMesAdm.Count; contador++){
                        Console.WriteLine(
                            $"Nome {contador + 1}: {NomesMesAdm[contador]}\n"+
                            $"CPF: {CpfMesAdm[contador]}\n"+
                            $"Placa do Veículo: {veiculosMesAdm[contador]} \n"
                            );
                        }
                }
                else{
                    Console.WriteLine(" \nNão há clientes mensalistas no momento. \n");
                }
                
            //Opção Mensalista (TODOS os cadastrados).
                if(CadastroNomeMes.Any()){
                    Console.WriteLine(" \nOs clientes mensalistas cadastrados são: \n");
                    //Lista os nomes e os cpfs dos clientes mensalistas cadastrados 
                        for (int contador = 0; contador < CadastroNomeMes.Count; contador++){
                                Console.WriteLine(
                                $"Nome: {CadastroNomeMes[contador]}\n"+
                                $"CPF: {CadastroCpfMes[contador]} \n"
                                );
                            }
                }
                else{
                    Console.WriteLine(" \nNão há cadastro de clientes mensalistas no momento. \n");
                }
        }
    

        //Função para mostrar o Rendimento Total do Estacionamento (Apenas para Administradores).
        public void RendimentoTotal(){
            //Rendimento Rotativo
            if(RendimentoRot.Any()){
                decimal SomaRot = RendimentoRot.Sum();
                Console.WriteLine($" \nO rendimento do Painel do Estacionamento Rotativo foi de: R$ {SomaRot}. \n");
            }
            else{
                Console.WriteLine(" \nNão houve rendimeto do Painel do Estacionamento Rotativo. \n");
            }

            //Rendimento Mensalista
            if(RendimentoMes.Any()){
                decimal SomaMes = RendimentoMes.Sum();
                Console.WriteLine($" \nO rendimento do Painel do Estacionamento Mensalista foi de: R$ {SomaMes}. \n");
            }
            else{
                Console.WriteLine(" \nNão houve rendimeto do Painel do Estacionamento Mensal. \n");
            }
            
            //Rendimento Total
            Console.WriteLine($" \nO rendimento total foi de: R${RendimentoRot.Sum() + RendimentoMes.Sum()}. \n");
        }
    }
}
