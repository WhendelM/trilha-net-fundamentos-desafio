using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Text;
using System.Runtime.InteropServices.Marshalling;
namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        //Objetos para o modo ROTATIVO de estacionamento (rot). 
        //Em ordem: Preço Inicial Fixo, Preço por Hora, Lista com os Veículos (Placas) e Limite Máximo de vagas.
        private const decimal PrecoInicialRot = 10;
        private const decimal PrecoAdicionalRot = 3;
        public static List<string> veiculosRot = new List<string>();
        private const int LimiteMaximoRot = 3; 

        //Objetos para o modo MENSALISTA de estacionamento (Mes).
        //Em ordem: Preço Inicial Fixo, Quantidade de Vagas Disponíveis para verificar a capacidade atual de reservas e e Limite Máximo de vagas.
        private const decimal PrecoInicialMes = 250;
        public static int qtdVagasVerificar = 0;
        private const int LimiteMaximoMes = 3;

        //Listas para o controle do Administrador.
        //Em ordem: Carros estacionados (Rotativo e Mensalista), Carros Removidos (Rotativo e Mensalista), Clientes Cadastrados (Mensalista), Armazenamento de Vagas Restantes (Mensalistas)
        private static List<(string nome,string cpf, string veiculo, string data,TimeSpan horarioEntrada)> clientesEstRot = new List<(string,string, string, string, TimeSpan)>();
        private static List<(string nome,string cpf, string veiculo, string data,TimeSpan horarioEntrada, TimeSpan horarioSaida,string valor)> clientesRemRot = new List<(string,string, string, string, TimeSpan, TimeSpan, string)>();
        private static List<(string nome,string cpf, string veiculo, string data,TimeSpan horarioEntrada)> clientesEstMes = new List<(string,string, string, string,TimeSpan)>();
        private static List<(string nome,string cpf, string veiculo, string data,TimeSpan horarioEntrada, TimeSpan horarioSaida)> clientesRemMes = new List<(string,string, string, string, TimeSpan,TimeSpan)>();
        private static List<(string nome,string cpf, int qtdVagas, string dataCadastro,string dataVencimento, string pagamento)> clientesMesCadastro = new List<(string,string, int, string, string,string)>();
        private static List<(string cpf, int qtdVagas)> verificarVagasMes = new List<(string,int)>();
        private static decimal valorTot = 0;
        private static List<decimal> rendimentoRot = new List<decimal>();
        private static List<decimal> rendimentoMes = new List<decimal>();
        //Função de Estacionar Veículos.
        public void AdicionarVeiculo(String letra)
        {   
            letra = letra.ToUpper();
            //Opção Rotativo.
            if(letra == "R"){
                Console.Clear();
                //Verificar se o estacionamento rotativo está lotado.
                if(veiculosRot.Count == LimiteMaximoRot){
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
                    $" \nAtualmente, há {Estacionamento.LimiteMaximoRot - Estacionamento.veiculosRot.Count} vagas disponíveis.\n"+
                    "A tarifa é de R$ 10,00 e o preço adicional por hora é de R$ 3,00.\n"+
                    "Digite o seu nome e sobrenome para continuar: \n"
                    );

                    //Armazenamento de Nome, CPF. Placa, Data(Dia/Mês/Ano) e Horário de Entrada (Horas/Minutos/Segundos) para visualização do Administrador do sistema.
                    Console.ForegroundColor = ConsoleColor.White;
                    string nomeRot = Console.ReadLine();
                    nomeRot = nomeRot.ToUpper();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Agora, digite o seu CPF para continuar: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string cpfRot = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Informe a placa do seu veículo: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string placaRot = Console.ReadLine();
                    placaRot = placaRot.ToUpper();
                    DateTime agora = DateTime.Now;
                    string dataRot = agora.ToString("dd/MM/yyyy"); //Data de estacionamento do veículo
                    TimeSpan horarioAuxRot = agora.TimeOfDay; 
                    TimeSpan horarioEntradaRot = new TimeSpan(horarioAuxRot.Hours, horarioAuxRot.Minutes, horarioAuxRot.Seconds);//Horário de Estacionamento.
                   
                    //Armazenamento do veículo na Lista de Estacionar e de Remover Veículos
                    //A lista de remover veículos será utilizada ativanmente na sua respectiva função, mas é necessário armazenar o horário de entrada na hora que estacionar.
                    clientesEstRot.Add((nomeRot,cpfRot,placaRot,dataRot,horarioEntradaRot)); 
                    clientesRemRot.Add((nomeRot,cpfRot,placaRot,dataRot,horarioEntradaRot,TimeSpan.Zero,null));
                    
                    //Armazenamento do veículo na Lista de Vagas para controlar a entrada de veículos.
                    veiculosRot.Add(placaRot);

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(
                    " \nO seu veículo foi estacionado!\n"+
                    "Agradecemos a preferência e confiança! \n"
                    );
                }
            }      

            //Opção Mensalista.
            else{

                //Verificação de cadastro de clientes 
               //O programa irá procurar o CPF na lista de cadastros e se esse clientes já utilizou suas reservas.
               //O sistema irá liberar o estacionamento caso encontre o CPF do cliente entregue e caso esse cliente ainda tenha vagas para reservar.
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Digite seu CPF cadastrado para verificação: ");
                Console.ForegroundColor = ConsoleColor.White;
                string cpfMes = Console.ReadLine();

                //Encontro do cadastro específico e liberação do estacionamento.
                var cadastroVerificar = verificarVagasMes.FirstOrDefault(x => x.Item1 == cpfMes); 
                if(cadastroVerificar != default && cadastroVerificar.Item2 != 0){ 
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Digite o seu nome e sobrenome cadastrados: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string nomeMes = Console.ReadLine();
                    nomeMes = nomeMes.ToUpper();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Digite a placa do seu veículo para estacionar: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    string placaMes = Console.ReadLine();
                    placaMes = placaMes.ToUpper();
                    DateTime agora = DateTime.Now;
                    string dataMes = agora.ToString("dd/MM/yyyy"); //Data de estacionamento do veículo
                    TimeSpan horarioAuxMes = agora.TimeOfDay; 
                    TimeSpan horarioEntradaMes = new TimeSpan(horarioAuxMes.Hours, horarioAuxMes.Minutes, horarioAuxMes.Seconds);//Horário de Estacionamento.
                    
                    //Armazenamento do veículo na Lista de Estacionar e de Remover Veículos
                    //A lista de remover veículos será utilizada ativanmente na sua respectiva função, mas é necessário armazenar o horário de entrada na hora que estacionar.
                    clientesEstMes.Add((nomeMes,cpfMes,placaMes,dataMes,horarioEntradaMes));
                    clientesRemMes.Add((nomeMes,cpfMes,placaMes,dataMes,horarioEntradaMes, TimeSpan.Zero));
                    //A partir do estacionamento concluido, o programa guardará a informação para verificação caso o mesmo cliente venha a solicitar o estacionamento novamente.
                    verificarVagasMes[verificarVagasMes.IndexOf(cadastroVerificar)] =
                    (
                    cadastroVerificar.Item1,
                    cadastroVerificar.Item2 -1
                    );

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(
                    " \nO seu veículo foi estacionado!\n"+
                    "Agradecemos a preferência e confiança! \n"
                    );
                }

                //Verificação de existência do cadastro, mas utilização de todas as vagas reservadas. 
                //É sugerido o Estacionamento Rotativo como tentativa de não perder o cliente.
                else if(cadastroVerificar != default && cadastroVerificar.Item2 == 0){
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(
                    " \n Você já utilizou todas as suas reservas mensalistas!\n"+
                    "Também oferecemos o serviço de Estacionamento Rotativo!\n"+
                    "Caso se interesse, é só digitar R\n"+
                    "Caso não, é só clicar na tecla ENTER. \n"
                    );
                    string rotativo = Console.ReadLine();
                    rotativo = rotativo.ToUpper();
                    if(rotativo == "R"){
                        Console.ResetColor();
                        Console.WriteLine("Estamos te transferindo para o Estacionamento Rotativo...");
                        AdicionarVeiculo("R"); 
                    }
                }

                //Cliente não cadastrado.
                else{
                    Console.Clear();
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

        //Função de Cadastrar Clientes Mensalistas.
        //Diferentemente do cliente rotativo, o cliente mensalista faz parte de um plano mais rebuscado, incluindo cadastro, já que passará mais tempo.
        public void CadastrarMensalista(){

            //Estacionamento Mensalista Lotado.
            if(qtdVagasVerificar == LimiteMaximoMes){
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(
                    " \nNo momento, nosso estacionamento com reservas indisponíveis.\n"+
                    "Mas não se preocupe: oferecemos reservas diárias no Modo Rotativo!\n"+
                    "Para conhecer mais, digite R e direcionaremos para o Estacionamento Rotativo.\n"+
                    "Ou clique na tecla ENTER para encerrar sua sessão. \n"
                    );
                    string opcao = Console.ReadLine();
                    if( opcao == "R"){
                        Console.ResetColor();
                        Console.WriteLine(" \nEstamos direcionando para o Estacionamento Rotativo... \n");
                        AdicionarVeiculo("R"); 
                    }
            }

            //Cadastro no Sistema Mensalista.
            else{
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(
                " \nSistema de cadastramento para o plano mensalista de Estacionamento. \n"+
                "De acordo com a política da AV2024, cada cliente pode reservar até 2 vagas no nosso estacionamento.\n"+
                "A tarifa é de R$ 250,00 por mês. \n"
                );
                Console.WriteLine($" \nAtualmente, há {Estacionamento.LimiteMaximoMes - Estacionamento.qtdVagasVerificar} vagas disponíveis. \n");
                
                //Solicitação da quantidade de vagas que o cliente deseja.
                Console.WriteLine("Primeiramente, digite a quantidade de vagas que você deseja reservar respeitando a quantidade de reservas disponíveis: ");
                Console.ForegroundColor = ConsoleColor.White;
                int qtdVagasMes = int.Parse(Console.ReadLine());
                
                //O cliente pode ter apenas 2 reservas por CPF.
                if(qtdVagasMes > 2){
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(
                    " \nQuantidade de reservas inválida. \n"+
                    "Tente Novamente!\n"+
                    "Estamos retornando para o início do cadastramento... \n"
                    );
                    CadastrarMensalista();
                }

                //Caso haja apenas uma reserva não cadastrada, mas o usuário queira 2, será impossibilitado. 
                else if(LimiteMaximoMes - qtdVagasVerificar == 1 && qtdVagasMes == 2 ){
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(
                    " \nO estacionamento só possui 1 vaga restante atualmente!\n"+
                    "Tente novamente!"+
                    "Estamos retornando para o início do cadastramento... \n"
                    );
                    CadastrarMensalista();
                }

                //Armazenamento na Lista de Cadastro e na Lista de Verificação para controle de reservas utilizadas.
                else{
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" \nInsira seu nome e sobrenone para iniciar o cadastro: \n");
                
                //Cadastro do nome, do cpf e da quantidade de vagas reservadas para estacionar.
                Console.ForegroundColor = ConsoleColor.White;
                string nomeMes = Console.ReadLine();
                nomeMes = nomeMes.ToUpper();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Insira seu CPF: ");
                Console.ForegroundColor = ConsoleColor.White;
                string cpfMes = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Digite a quantidade de meses que você deseja assinar o plano: ");
                Console.ForegroundColor = ConsoleColor.White;
                int meses = int.Parse(Console.ReadLine());
                DateTime agora = DateTime.Now;
                string dataInicioMes = agora.ToString("dd/MM/yyyy");
                string dataFimMes = agora.AddMonths(meses).ToString("dd/MM/yyyy");
                string valorCadastroMes = (250*meses).ToString("C");
                rendimentoRot.Add(250*meses);
                clientesMesCadastro.Add((nomeMes,cpfMes,qtdVagasMes,dataInicioMes,dataFimMes,valorCadastroMes));
                verificarVagasMes.Add((cpfMes,qtdVagasMes));
                //Atualização da quantidade de vagas atual. 
                qtdVagasVerificar = qtdVagasVerificar + qtdVagasMes;
                //Opção do cliente, agora cadastrado, estacionar o veículo no modo mensalista. 
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                $" \nO seu plano foi autorizado com pagamento de {valorCadastroMes}.\n"+
                "Agradecemos o cadastro no sistema Estacionamento Mensalista AV2024.\n"+
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

        }
    }

        //Função de Remover Veículos.
        public void RemoverVeiculo(string letra){ 
            //Opção Rotativo.  
            if(letra == "R") {
                //Verificação de reconhecimento de placas
               //O programa irá procurar o CPF na lista de veículos estacionados e a placa do veículo solicitada.
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Digite o seu CPF para verificação: ");
                Console.ForegroundColor = ConsoleColor.White;
                string cpfRot = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Digite a placa para remover seu veículo: ");
                Console.ForegroundColor = ConsoleColor.White;
                string placaRot = Console.ReadLine();
                placaRot = placaRot.ToUpper();
                //Remoção do Veículo caso seja encontrado o CPF e a placa do veículo vinculado.
                    if(clientesRemRot.Any(x => x.Item2 == cpfRot) && clientesRemRot.Any(x => x.Item3 == placaRot)){
                        DateTime agora = DateTime.Now;
                        TimeSpan horarioAuxRot = agora.TimeOfDay;
                        TimeSpan horarioSaídaRot = new TimeSpan(horarioAuxRot.Hours, horarioAuxRot.Minutes, horarioAuxRot.Seconds);//Horário de Estacionamento.
                        var cadastroEncontrado = clientesRemRot.FirstOrDefault(t => t.Item3 == placaRot);
                        TimeSpan horas = horarioSaídaRot - cadastroEncontrado.Item5;
                        int horasAprox = (int)Math.Ceiling(horas.TotalHours);
                        decimal valorTotal = PrecoInicialRot + ((horasAprox-1)* PrecoAdicionalRot);
                        rendimentoMes.Add(valorTotal);
                        string valorTotalRot = valorTotal.ToString("C");
                        //Alteração do valor do horário de saída do veículo e do valor total a pagar na lista de remoções.
                        clientesRemRot[clientesRemRot.IndexOf(cadastroEncontrado)] =
                        (
                        cadastroEncontrado.Item1,
                        cadastroEncontrado.Item2,
                        cadastroEncontrado.Item3,
                        cadastroEncontrado.Item4,
                        cadastroEncontrado.Item5,
                        horarioSaídaRot,
                        valorTotalRot
                        );
                        //Remoção do veículo para controlar o fluxo do estacionamento rotativo.
                        veiculosRot.Remove(placaRot);
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(
                        $" \nO veículo {placaRot} foi removido e o preço total foi de: R$ {valorTotalRot}.\n"+
                        "Agradecemos a preferência e confiança! \n");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        //Alternativa para o cliente se cadastrar no Estaciomaneto Mensalista, caso haja interesse.
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
                    
                //O veículo não foi encontrado.
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
                //Verificação de reconhecimento de placas
               //O programa irá procurar o CPF na lista de veículos estacionados e a placa do veículo solicitada.
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Digite o seu CPF para verificação: ");
                Console.ForegroundColor = ConsoleColor.White;
                string cpfMes = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Digite a placa para remover seu veículo: ");
                Console.ForegroundColor = ConsoleColor.White;
                string placaMes = Console.ReadLine();
                //Remoção do Veículo caso seja encontrado o CPF e a placa do veículo vinculado.
                    if(clientesRemMes.Any(x => x.Item2 == cpfMes) && clientesRemMes.Any(x => x.Item3 == placaMes)){
                        DateTime agora = DateTime.Now;
                        TimeSpan horarioAuxMes = agora.TimeOfDay; 
                        TimeSpan horarioSaídaMes = new TimeSpan(horarioAuxMes.Hours, horarioAuxMes.Minutes, horarioAuxMes.Seconds);
                        var cadastroEncontrado = clientesRemMes.FirstOrDefault(t => t.Item3 == placaMes);
                        TimeSpan horas = horarioSaídaMes - cadastroEncontrado.Item5;
                        int horasAprox = (int)Math.Ceiling(horas.TotalHours);
                        //Alteração do valor do horário de saída do veículo na lista de remoções.
                        clientesRemMes[clientesRemMes.IndexOf(cadastroEncontrado)] =
                        (
                        cadastroEncontrado.Item1,
                        cadastroEncontrado.Item2,
                        cadastroEncontrado.Item3,
                        cadastroEncontrado.Item4,
                        cadastroEncontrado.Item5,
                        horarioSaídaMes
                        );
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(
                        $"O veículo {placaMes} foi removido!\n"+
                        "\nAgradecemos a preferência e confiança! \n"
                        );
                    }
                else{
                    ////O veículo não foi encontrado.
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
            //Listar todos os veículos rotativos que foram estacionados.
            public void ListarCadastroVeiculosEstacionados(){
            //Listar todos os veículos rotativos que foram estacionados.
            if (clientesEstRot.Any()){
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(" \nOs veículos rotativos estacionados e seus respectivos clientes podem ser visualizados nesse Relatório: \n");
                List<string> titulos = new List<string>{"Cliente", "CPF do Cliente", "Placa do Veículo", "Data", "Entrada"};
                List<string> nomes = clientesEstRot.Select(t => t.Item1).ToList();
                List<string> cpfs = clientesEstRot.Select(t => t.Item2).ToList();
                List<string> veiculos = clientesEstRot.Select(t => t.Item3).ToList();
                List<string> datas = clientesEstRot.Select(t => t.Item4).ToList();
                List<TimeSpan> horariosEntrada = clientesEstRot.Select(t => t.Item5).ToList();
                CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
                csvConfig.Delimiter = ";";
                csvConfig.Encoding = Encoding.UTF8;
                using (var escrita = new StreamWriter("RelatórioEstacionarRotativo.csv", false, Encoding.UTF8))
                using (var csv = new CsvWriter(escrita, csvConfig)){
                        csv.WriteField("Cliente");
                        csv.WriteField("CPF do Cliente");
                        csv.WriteField("Placa do Veículo");
                        csv.WriteField("Data");
                        csv.WriteField("Entrada");
                        csv.NextRecord();
                            for (int i = 0; i < nomes.Count; i++)
                            {
                                csv.WriteField(nomes[i]);
                                csv.WriteField(cpfs[i]);
                                csv.WriteField(veiculos[i]);
                                csv.WriteField(datas[i]);
                                csv.WriteField(horariosEntrada[i].ToString("hh\\:mm\\:ss"));
                                csv.NextRecord();
                            }
                        Console.WriteLine("file://" + Path.GetFullPath("RelatórioEstacionarRotativo.csv \n"));
                }
            }
            else{
                Console.WriteLine(" \nNão há veículos rotativos estacionados. \n");
            }

            //Listar todos os veículos mensalistas que foram estacionados (incluindo os que já foram removidos).
            if(clientesEstMes.Any()){
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(" \nOs veículos mensalistas estacionados e seus respectivos clientes podem ser visualizados nesse Relatório: \n");
                List<string> titulos = new List<string>{"Cliente", "CPF do Cliente", "Placa do Veículo", "Data", "Entrada"};
                List<string> nomes = clientesEstMes.Select(t => t.Item1).ToList();
                List<string> cpfs = clientesEstMes.Select(t => t.Item2).ToList();
                List<string> veiculos = clientesEstMes.Select(t => t.Item3).ToList();
                List<string> datas = clientesEstMes.Select(t => t.Item4).ToList();
                List<TimeSpan> horariosEntrada = clientesEstMes.Select(t => t.Item5).ToList();
                CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
                csvConfig.Delimiter = ";";
                csvConfig.Encoding = Encoding.UTF8;
                using (var escrita = new StreamWriter("RelatórioEstacionarMensalista.csv", false, Encoding.UTF8))
                using (var csv = new CsvWriter(escrita, csvConfig)){
                        csv.WriteField("Cliente");
                        csv.WriteField("CPF do Cliente");
                        csv.WriteField("Placa do Veículo");
                        csv.WriteField("Data");
                        csv.WriteField("Entrada");
                        csv.NextRecord();
                            for (int i = 0; i < nomes.Count; i++)
                            {
                                csv.WriteField(nomes[i]);
                                csv.WriteField(cpfs[i]);
                                csv.WriteField(veiculos[i]);
                                csv.WriteField(datas[i]);
                                csv.WriteField(horariosEntrada[i].ToString("hh\\:mm\\:ss"));
                                csv.NextRecord();
                            }
                            Console.WriteLine("file://" + Path.GetFullPath("RelatórioEstacionarMensalista.csv \n"));
                        }
                
            }
            else{
                Console.WriteLine(" \nNão há veículos mensalistas estacionados. \n");
            }    
        }
        public void ListarCadastroVeiculosRemovidos(){
            if (clientesRemRot.Any()){
                Console.WriteLine(" \nOs veículos rotativos removidos e seus respectivos clientes podem ser visualizados nesse Relatório: \n");
                List<string> titulos = new List<string>{"Cliente", "CPF do Cliente", "Placa do Veículo", "Data", "Entrada","Saída","Pagamento"};
                List<string> nomes = clientesRemRot.Select(t => t.Item1).ToList();
                List<string> cpfs = clientesRemRot.Select(t => t.Item2).ToList();
                List<string> veiculos = clientesRemRot.Select(t => t.Item3).ToList();
                List<string> datas = clientesRemRot.Select(t => t.Item4).ToList();
                List<TimeSpan> horariosEntrada = clientesRemRot.Select(t => t.Item5).ToList();
                List<TimeSpan> horariosSaida = clientesRemRot.Select(t => t.Item6).ToList();
                List<string> valores = clientesRemRot.Select(t => t.Item7).ToList();
                CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
                csvConfig.Delimiter = ";";
                csvConfig.Encoding = Encoding.UTF8;
                using (var escrita = new StreamWriter("RelatórioRemoverRotativo.csv", false, Encoding.UTF8))
                using (var csv = new CsvWriter(escrita, csvConfig)){
                    csv.WriteField("Cliente");
                    csv.WriteField("CPF do Cliente");
                    csv.WriteField("Placa do Veículo");
                    csv.WriteField("Data");
                    csv.WriteField("Entrada");
                    csv.WriteField("Saída");
                    csv.WriteField("Pagamento");
                    csv.NextRecord();

                    for (int i = 0; i < nomes.Count; i++)
                    {
                        if (horariosSaida[i] != TimeSpan.Zero){
                        csv.WriteField(nomes[i]);
                        csv.WriteField(cpfs[i]);
                        csv.WriteField(veiculos[i]);
                        csv.WriteField(datas[i]);
                        csv.WriteField(horariosEntrada[i].ToString("hh\\:mm\\:ss"));
                        csv.WriteField(horariosSaida[i].ToString("hh\\:mm\\:ss"));
                        csv.WriteField(valores[i]);
                        csv.NextRecord();
                        }   
                    }
                    Console.WriteLine("file://" + Path.GetFullPath("RelatórioRemoverRotativo.csv"));
                }
            }
            else{
                Console.WriteLine(" \nNão há veículos rotativos removidos. \n");
            }
            
            if (clientesRemMes.Any()){
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(" \nOs veículos rotativos removidos e seus respectivos clientes podem ser visualizados nesse Relatório: \n");
                List<string> titulos = new List<string>{"Cliente", "CPF do Cliente", "Placa do Veículo", "Data", "Entrada","Saída"};
                List<string> nomes = clientesRemMes.Select(t => t.Item1).ToList();
                List<string> cpfs = clientesRemMes.Select(t => t.Item2).ToList();
                List<string> veiculos = clientesRemMes.Select(t => t.Item3).ToList();
                List<string> datas = clientesRemMes.Select(t => t.Item4).ToList();
                List<TimeSpan> horariosEntrada = clientesRemMes.Select(t => t.Item5).ToList();
                List<TimeSpan> horariosSaida = clientesRemMes.Select(t => t.Item6).ToList();
                CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
                csvConfig.Delimiter = ";";
                csvConfig.Encoding = Encoding.UTF8;
                using (var escrita = new StreamWriter("RelatórioRemoverMensalista.csv", false, Encoding.UTF8))
                using (var csv = new CsvWriter(escrita, csvConfig)){
                    csv.WriteField("Cliente");
                    csv.WriteField("CPF do Cliente");
                    csv.WriteField("Placa do Veículo");
                    csv.WriteField("Data");
                    csv.WriteField("Entrada");
                    csv.WriteField("Saída");
                    csv.NextRecord();

                    for (int i = 0; i < nomes.Count; i++)
                    {
                        if (horariosSaida[i] != TimeSpan.Zero){
                        csv.WriteField(nomes[i]);
                        csv.WriteField(cpfs[i]);
                        csv.WriteField(veiculos[i]);
                        csv.WriteField(datas[i]);
                        csv.WriteField(horariosEntrada[i].ToString("hh\\:mm\\:ss"));
                        csv.WriteField(horariosSaida[i].ToString("hh\\:mm\\:ss"));
                        csv.NextRecord();
                        }   
                    }
                    Console.WriteLine("file://" + Path.GetFullPath("RelatórioRemoverMensalista.csv"));
                }
            }
            else{
                Console.WriteLine(" \nNão há veículos Mensalistas removidos. \n");
            }

        }

        //Listar as informações dos clientes mensalistas (apenas para Administradores).
        public void ListarClientesMensalistas(){
        if (clientesMesCadastro.Any()){
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(" \nOs clientes mensalistas podem ser visualizados nesse Relatório: \n");
                List<string> titulos = new List<string>{"Cliente", "CPF do Cliente", "Número de Reservas", "Entrada","Saída","Pagamento"};
                List<string> nomes = clientesMesCadastro.Select(t => t.Item1).ToList();
                List<string> cpfs = clientesMesCadastro.Select(t => t.Item2).ToList();
                List<int> qtdVagas = clientesMesCadastro.Select(t => t.Item3).ToList();
                List<string> diaEntrada = clientesMesCadastro.Select(t => t.Item4).ToList();
                List<string> diaSaida = clientesMesCadastro.Select(t => t.Item5).ToList();
                List<string> pagamento = clientesMesCadastro.Select(t => t.Item6).ToList();
                CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
                csvConfig.Delimiter = ";";
                csvConfig.Encoding = Encoding.UTF8;
                using (var escrita = new StreamWriter("RelatórioRemoverMensalista.csv", false, Encoding.UTF8))
                using (var csv = new CsvWriter(escrita, csvConfig)){
                    csv.WriteField("Cliente");
                    csv.WriteField("CPF do Cliente");
                    csv.WriteField("Número de Reservas");
                    csv.WriteField("Entrada");
                    csv.WriteField("Saída");
                    csv.WriteField("Pagamento");
                    csv.NextRecord();

                    for (int i = 0; i < nomes.Count; i++)
                    {
                        csv.WriteField(nomes[i]);
                        csv.WriteField(cpfs[i]);
                        csv.WriteField(qtdVagas[i]);
                        csv.WriteField(diaEntrada[i]);
                        csv.WriteField(diaSaida[i]);
                        csv.WriteField(pagamento[i]);
                        csv.NextRecord();   
                    }
                    Console.WriteLine("file://" + Path.GetFullPath("RelatórioRemoverMensalista.csv"));
                }
            }
            else{
                Console.WriteLine(" \nNão há veículos Mensalistas removidos. \n");
            }
        }

        //Função para mostrar o Rendimento Total do Estacionamento (Apenas para Administradores).
        public void RendimentoTotal(){
            decimal somaRot = rendimentoRot.Sum();
            decimal somaMes = rendimentoMes.Sum();
            //Rendimento Rotativo
            if (rendimentoRot.Count > 0){
                Console.WriteLine($" \nO rendimento do Painel do Estacionamento Rotativo foi de: R$ {somaRot}. \n");
            }
            else{
                Console.WriteLine(" \nNão houve rendimeto do Painel do Estacionamento Rotativo. \n");
            }

            //Rendimento Mensalista
            if (rendimentoMes.Count > 0){
                Console.WriteLine($" \nO rendimento do Painel do Estacionamento Mensalista foi de: R$ {somaMes}. \n");
            }
            else{
                Console.WriteLine(" \nNão houve rendimeto do Painel do Estacionamento Mensal. \n");
            }
            
            //Rendimento Total
            Console.WriteLine($" \nO rendimento total foi de: R${somaRot + somaMes}. \n");
        }
    }
}
