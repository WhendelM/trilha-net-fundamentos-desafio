using System.Text.RegularExpressions;
using DesafioFundamentos.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Estacionamento es = new Estacionamento(); //Instaciar a Classe Estacionamento.
bool start1 = true; //String usada para permitir que o loop ocorra sempre que um usuário terminar (simulação de um sistema de demanda).
string inicio = string.Empty; //String de input do usuário para escolha entre a area do Administrador ou a Area do Cliente).
string opcao = string.Empty; //String de input do usuário para escolhe entre a área do Estacionamento Rotativo ou do Estacionamento Mensalista.
while(start1){
    ReiniciarAouC:; //Referência de retorno caso o usuário não aperte C(Cliente) ou A (Administrador).
    Console.WriteLine("Você está acessando o Estacionamento Privado do AV2024.\n \n Se você é ou deseja ser nosso cliente, digite C. \n Se você é nosso colaborador ou administrador do nosso programa, digite A. \n \n ");
    //Início do Programa
    inicio = Console.ReadLine();
    inicio = inicio.ToUpper();
    ReiniciarRouM:; //Referência de retorno caso o usuário não tecle R(Rotativo) ou M(Mensalista) na área do cliente.
    switch(inicio){
        //Área do Administrador escolhida.
        case "A":
            ReiniciarSenha:;//Referência de retorno caso o usuário digite a senha errada.
            string padraoSenha = @"^[A-Z]{3}\d$"; //Padrão para senha única do administrador (LETRA LETRA LETRA NÚMERO).
            Console.WriteLine("Você está na área de Administrador. Por questões de segurança, digite aqui seu código exclusivo para entrada: \n \n ");
            string senha = Console.ReadLine();
            ReiniciarOpcoesDeA:; //Referência de retorno caso o usuário não digite 1,2 ou 3 (opções da Área do Administrador).
            //Comparação entre o padrão e a senha digitada.
            if(Regex.IsMatch(senha,padraoSenha)){
                //Senha aceita
                Console.WriteLine("Você estará acessando o Menu da Admnistração. Não esqueca que as informações reitradas são privativas para a segurança do Estacionamento e dos clientes. \n \n");
                Console.WriteLine("Menu: ");
                Console.WriteLine("Opção 1 - Acessar veículos estacionados");
                Console.WriteLine("Opção 2 - Acessar clientes");
                Console.WriteLine("Opção 3 - Acessar Rendimento");

                switch(Console.ReadLine()){
                    case "1": 
                        es.ListarVeiculos(); //Função Listar Veicúlos instanciada.
                    break;
                    
                    case "2":
                        es.ListarClientes(); //Função Listar Clientes instanciada.
                    break;

                    case "3":
                        es.RendimentoTotal();//Função Rendimento Total instanciada.
                    break;
                    default:
                        Console.WriteLine("Opção inválida.Tente Novamente!"); 
                        goto ReiniciarOpcoesDeA;//Opção Default Referenciada (Retorno para as opções da Área do Administrador).
                }

                goto Encerrar; //Encerra o programa e Retorna para o Início.
            }
            //Senha não aceita.
            else{
                Console.WriteLine("Código incorreto. Revise e tente novamente. \n \n");
                goto ReiniciarSenha; //Opção Default Referenciada (Retorno para digitar a senha novamente).
            }
        //Área do Cliente Escolhida.
        case "C":
            Console.WriteLine("Você está na área de cliente! \n \n Para acessar o Estacionamento Rotativo, digite R. \n Para acessar o Estacionamento Mensalista, digite M \n \n ");
            opcao = Console.ReadLine();
            opcao = opcao.ToUpper();
            break;

         default:
            Console.WriteLine("Opção inválida.Tente Novamente!");
            goto ReiniciarAouC; //Opção Default Referenciada (Retorno para o Início do Programa caso não tenha teclado A ou C).
    }

ReiniciarOpcao:; //Referência de retorno caso o usuário não digite nenhuma das opções do Menu do Estacionamento Rotativo(R) ou Mensalista(M).
switch(opcao){
    //Opção Rotativo
    case "R": 
        Console.WriteLine("Você escolheu o Modo Rotativo! \n \n");
        Console.WriteLine("Menu: ");
        Console.WriteLine("Opção 1 - Estacionar Veículo");
        Console.WriteLine("Opção 2 - Remover Veículo");
        string opcaoRotativo = Console.ReadLine();
        switch(opcaoRotativo){
            case "1":
                es.AdicionarVeiculo(opcao); //Função Adicionar Veículo instanciada.
                break;
            case "2":
                es.RemoverVeiculo(opcao); //Função Remover Veículo instanciada.
                break;
            default: 
                Console.WriteLine("Opção inválida.Tente Novamente!");
            goto ReiniciarOpcao;  //Opção Default Referenciada (Retorno para as opções do Rotativo caso o usuário não tenha digitado corretamente).
        }
        break;
    case "M": 
    //Opção Mensalista
        Console.WriteLine("Você escolheu o Modo Mensalista! \n \n Caso seja novo cliente, cadastre=se em nosso sistema antes na opção 'Cadastrar Meus Dados' no menu abaixo. \n \n");
        Console.WriteLine("Menu: ");
        Console.WriteLine("Opção 1 - Cadastrar Meus Dados");
        Console.WriteLine("Opção 2 - Estacionar Veículo");
        Console.WriteLine("Opção 3 - Remover Veículo");
        string opcaoMensalista = Console.ReadLine();
        switch(opcaoMensalista){
            case "1":
                es.CadastrarMensalista(); //FunçãO Cadastrar Veículo instanciada.
                break;
            case "2":
                es.AdicionarVeiculo(opcao); //Função Adicionar Veículo instanciada.
                break;
            case "3":
                es.RemoverVeiculo(opcao); //Função Remover Veículo instanciada.
                break;
            default: 
                Console.WriteLine("Opção inválida.Tente Novamente!");
                goto ReiniciarOpcao; //Opção Default Referenciada (Retorno para as opções do Mensalista caso o usuário não tenha digitado corretamente).
        }
    break;
    default: 
        Console.WriteLine("Opção inválida.Tente Novamente!");
        goto ReiniciarRouM; //Opção Default Referenciada (Retorno para a área do cliente, caso o usuário não tenha teclado R ou M).
}
Encerrar:; //Referencia de Retorno ao início do programa (usado no término do painel de Administrador)
Console.WriteLine("\n \n Finalizando...\n \n ");
Console.WriteLine("O Canal de Serviços do AV2024 está encerrado!\n \n");
Console.WriteLine("Reiniciando...\n \n ");
//Finalizar o Programa e Reiniciá-lo.
}
