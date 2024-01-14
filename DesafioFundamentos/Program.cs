using System.Collections;
using System.Text.RegularExpressions;
using DesafioFundamentos.Models;
Console.OutputEncoding = System.Text.Encoding.UTF8;

Estacionamento es = new Estacionamento(); //Instaciar a Classe Estacionamento.
bool start = true; //String usada para permitir que o loop ocorra sempre que um usuário terminar (simulação de um sistema de demanda).
string opcaoAouC = string.Empty; //String de input do usuário para escolha entre a Área de Administrador ou a Area do Cliente).
string opcaoRouM = string.Empty; //String de input do usuário para escolher entre a área do Estacionamento Rotativo ou do Estacionamento Mensalista.
string retornar = string.Empty; //String de imput do usuário para retornar a qualquer painel anterior ao que o usuário estiver.

while(start){
    ReiniciarAouC:; //Referência de retorno caso o usuário não aperte C(Cliente) ou A (Administrador).
    DateTime horario = DateTime.Now; //Variável utilizada para armazenar o horário exato de utilização do programa pelo usuário.
    if(horario.Hour >= 0 && horario.Hour < 12 ){
        //Caso o período do dia seja a manhã (das 00:00 até às 11:59).
        Console.ForegroundColor = ConsoleColor.Yellow; 
        Console.WriteLine(" \nBom dia! \n");
        Console.ResetColor();
        Console.WriteLine("Você está acessando o Painel Inicial do Estacionamento Privado AV2024.\n"+
        "Se você deseja utilizar nossos serviços, digite C para acessar o Painel de Clientes.\n"+
        "Caso você seja membro dos nossos colaboradores, digite A para acessar o Painel de Administradores. \n"
        );
    }
    else if (horario.Hour >= 12 && horario.Hour < 18){
        //Caso o período do dia seja a tarde (das 12:00 até às 17:59).
        Console.ForegroundColor = ConsoleColor.Yellow; 
        Console.WriteLine(" \nBoa Tarde! \n");
        Console.ResetColor();
        Console.WriteLine("Você está acessando o Painel Inicial do Estacionamento Privado AV2024.\n"+
        "Se você deseja utilizar nossos serviços, digite C para acessar o Painel de Clientes.\n"+
        "Caso você seja membro dos nossos colaboradores, digite A para acessar o Painel de Administradores. \n"
        );
    }
    else{
        Console.ForegroundColor = ConsoleColor.Magenta; 
        Console.WriteLine(" \nBoa Noite! \n");
        Console.ResetColor();
        Console.WriteLine("Você está acessando o Painel Inicial do Estacionamento Privado AV2024.\n"+
        "Se você deseja utilizar nossos serviços, digite C para acessar o Painel de Clientes.\n"+
        "Caso você seja membro dos nossos colaboradores, digite A para acessar o Painel de Administradores. \n"
        );
    }
    //Início do Programa
    Console.ForegroundColor = ConsoleColor.White; 
    opcaoAouC = Console.ReadLine();
    opcaoAouC = opcaoAouC.ToUpper();
    ReiniciarRouM:; //Referência de retorno caso o usuário não tecle R(Rotativo) ou M(Mensalista) na área do cliente.
    switch(opcaoAouC){
        //Área do Administrador escolhida.
        case "A":
            ReiniciarSenha:;//Referência de retorno caso o usuário digite a senha errada.
            //Opção de retorno para o início do sistema caso o usuário digite a opção errada por engano.
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(" \nVocê está acessando Painel de Administradores do nosso sistema.\n");
            Console.ResetColor();
            Console.WriteLine(
            "Para continuar, clique na tecla ENTER.\n"+
            "Caso queira retornar para o Painel Inicial,é só digitar E. \n"
            );
            Console.ForegroundColor = ConsoleColor.White;
            retornar = Console.ReadLine();
            retornar = retornar.ToUpper();
            if(retornar == "E" ){
               Console.Clear();
               Console.ResetColor();
               Console.WriteLine(
               " \nEstamos retornando para o Painel Inicial...  \n");
                goto ReiniciarAouC;
            }
            string padraoSenha = @"^[A-Z]{3}\d$"; //Padrão para senha única do administrador (LETRA LETRA LETRA NÚMERO).
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" \nPor questões de segurança, digite aqui sua senha exclusiva para entrada: \n");
            Console.ForegroundColor = ConsoleColor.White;
            string senha = Console.ReadLine();
            ReiniciarOpcoesDeA:; //Referência de retorno caso o usuário não digite 1,2 ou 3 (opções da Área do Administrador).
            //Comparação entre o padrão e a senha digitada.
            if(Regex.IsMatch(senha,padraoSenha)){
                //Senha aceita
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(
                "\nVocê estará acessando o Menu da Admnistração. \n"+
                "Não esqueça que as informações armazenadas são privativas para a segurança da empresa e dos clientes. \n");
                Console.WriteLine(
                " \nMenu:\n"+
                "Opção 1 - Acessar veículos estacionados\n"+
                "Opção 2 - Acessar clientes\n"+
                "Opção 3 - Acessar Rendimento Financeiro\n"+
                "Opção 4 - Retornar para o Painel Inicial \n"
                );
                Console.ForegroundColor = ConsoleColor.White;
                switch(Console.ReadLine()){
                    case "1":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        es.ListarVeiculos(); //Função Listar Veicúlos instanciada.
                        Console.ResetColor();
                        Console.WriteLine(
                        " \nCaso queira retornar para o Menu da Administração, digite A.\n"+
                        "Caso queira encerrar a sessão e retornar para o Painel Principal, é só clicar na tecla ENTER. \n"
                        );
                        Console.ForegroundColor = ConsoleColor.White;
                        retornar = Console.ReadLine();
                        retornar = retornar.ToUpper();
                        if(retornar == "A"){
                            Console.Clear();
                            Console.ResetColor();
                            Console.WriteLine("Retornando para o Menu da Administração...");
                            goto ReiniciarOpcoesDeA; //Retorno para o Menu de Administrador para facilitar a navegação.
                        }
                    break;
                    
                    case "2":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        es.ListarClientes(); //Função Listar Clientes instanciada.
                        Console.ResetColor();
                        Console.WriteLine(
                        " \nCaso queira retornar para o Menu da Administração, digite A.\n"+
                        "Caso queira encerrar a sessão e retornar para o Painel Principal, é só clicar na tecla ENTER. \n"
                        );
                        Console.ForegroundColor = ConsoleColor.White;
                        retornar = Console.ReadLine();
                        retornar = retornar.ToUpper();
                        if(retornar == "A"){
                            Console.Clear();
                            Console.ResetColor();
                            Console.WriteLine("Retornando para o Menu da Administração...");
                            goto ReiniciarOpcoesDeA; //Retorno para o Menu de Administrador para facilitar a navegação.
                        }
                    break;

                    case "3":
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        es.RendimentoTotal();//Função Rendimento Total instanciada.
                        Console.ResetColor();
                        Console.WriteLine(
                        " \nCaso queira retornar para o Menu da Administração, digite A.\n"+
                        "Caso queira encerrar a sessão e retornar para o Painel Principal, é só clicar na tecla ENTER. \n"
                        );
                        Console.ForegroundColor = ConsoleColor.White;
                        retornar = Console.ReadLine();
                        retornar = retornar.ToUpper();
                        if(retornar == "A"){
                            Console.Clear();
                            Console.ResetColor();
                            Console.WriteLine("Retornando para o Menu da Administração...");
                            goto ReiniciarOpcoesDeA; //Retorno para o Menu de Administrador para facilitar a navegação.
                        }
                    break; 

                    case "4":
                        Console.ResetColor();
                        goto Encerrar; //Encerra o programa e Retorna para o Início.

                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(
                        " \nOpção inválida.\n"+
                        "Estamos direcionando para o Menu do Administrador novamente!\n"
                        ); 
                        goto ReiniciarOpcoesDeA;//Opção Default Referenciada (Retorno para as opções da Área do Administrador).
                }

                goto Encerrar; //Encerra o programa e Retorna para o Início.
            }
            //Senha não aceita.
            else{
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                " \nSenha incorreta.\n"+
                "Entre no Painel dos Administradores e tente novamente.\n"
                );
                goto ReiniciarSenha; //Opção Default Referenciada (Retorno para digitar a senha novamente).
            }
        //Área do Cliente Escolhida.
        case "C":
        //Opção de retorno para o início do sistema caso o usuário digite a opção errada por engano.
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" \nVocê está acessando o Painel de Clientes AV2024.\n");
            Console.ResetColor();
            Console.WriteLine(
            "Para continuar, clique na tecla ENTER.\n"+
            "Caso queira retornar para o Painel Inicial,é só digitar E. \n"
            );
            Console.ForegroundColor = ConsoleColor.White;
            retornar = Console.ReadLine();
            retornar = retornar.ToUpper();
            if(retornar == "E" ){
               Console.Clear();
               Console.ResetColor();
               Console.WriteLine(
               " \nEstamos retornando para o Painel Inicial...  \n");
                goto ReiniciarAouC;
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(" \nPara acessar o Estacionamento Rotativo, digite R.\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Para acessar o Estacionamento Mensalista, digite M. \n");
            Console.ForegroundColor = ConsoleColor.White;
            opcaoRouM = Console.ReadLine();
            opcaoRouM = opcaoRouM.ToUpper();
            break;
        default:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
            " \nOpção inválida."+
            "Estamos retornando para o Painel Principal.\n");
            goto ReiniciarAouC; //Opção Default Referenciada (Retorno para o Início do Programa caso não tenha teclado A ou C).
    }
ReiniciarOpcoesCliente:; //Referência de retorno caso o usuário não digite nenhuma das opções do Menu do Estacionamento Rotativo(R) ou Mensalista(M).
switch(opcaoRouM){
    //Opção Rotativo
    case "R":
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(
        " \nVocê está acessando o sistema de Estacionamento Rotativo AV2024!\n"+
        "Logo abaixo, será exibido o nosso menu.\n"+
        "Leia atentamente as opções \n");
        Console.WriteLine(
        " \nMenu:\n"+
        "Opção 1 - Estacionar Veículo\n"+
        "Opção 2 - Remover Veículo\n"+
        "Opção 3 - Retornar à Área do Cliente\n"+
        "Opção 4 - Encerrar Sessão \n"
        );
        Console.ForegroundColor = ConsoleColor.White;
        string opcaoRotativo = Console.ReadLine();
        switch(opcaoRotativo){
            //Função Adicionar Veículo instanciada.
            case "1":
                Console.Clear();
                es.AdicionarVeiculo(opcaoRouM);
                Console.ResetColor();
                Console.WriteLine(
                " \nCaso queira retornar para o Menu da Área do Estacionamento Rotativo, digite R.\n"+
                "Caso queira encerrar a sessão e retornar para o Painel Inicial, é só clicar na tecla ENTER. \n"
                );
                Console.ForegroundColor = ConsoleColor.White;
                retornar = Console.ReadLine();
                retornar = retornar.ToUpper();
                if(retornar == "R"){
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("Retornando para o Menu do Estacionamento Rotativo...");
                    goto ReiniciarOpcoesCliente; //Retorno para o Menu do Estacionamento Rotativo para facilitar a navegação do usuário.
                }
                break;
            //Função Remover Veículo instanciada.
            case "2":
                Console.Clear();
                es.RemoverVeiculo(opcaoRouM);
                Console.ResetColor();
                Console.WriteLine(
                " \nCaso queira retornar para o Menu da Área do Estacionamento Rotativo, digite R.\n"+
                "Caso queira encerrar a sessão e retornar para o Painel Inicial, é só clicar na tecla ENTER. \n"
                );
                Console.ForegroundColor = ConsoleColor.White;
                retornar = Console.ReadLine();
                retornar = retornar.ToUpper();
                if(retornar == "R"){
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("Retornando para o Menu do Estacionamento Rotativo...");
                    goto ReiniciarOpcoesCliente; //Retorno para o Menu do Estacionamento Rotativo para facilitar a navegação do usuário.
                }
                break;

            case "3":
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("Retornando à Área do Cliente.... \n");
                goto ReiniciarRouM; //Retorno para a Área do Cliente

            case "4":
                    Console.ResetColor();
                    goto Encerrar; //Encerra o programa e Retorna para o Início.
    
            default:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
            " \nOpção inválida.\n"+
            "Estamos retornando para o Painel dos Clientes Rotativos.\n");
            goto ReiniciarOpcoesCliente;  //Opção Default Referenciada (Retorno para as opções do Rotativo caso o usuário não tenha digitado corretamente).
        }
        break;
    //Opção Mensalista
    case "M":
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(
        " \nSeja bem vindo ao sistema de Estacionamento Mensalista do AV2024!\n"+
        "Caso seja novo cliente, cadastre-se em nosso sistema antes na opção 'Cadastrar Meus Dados' no menu abaixo. \n"
        );
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(
        " \nMenu:\n"+
        "Opção 1 - Cadastrar Meus Dados\n"+
        "Opção 2 - Estacionar Veículo\n"+
        "Opção 3 - Remover Veículo\n"+
        "Opção 4 - Retornar à Área do Cliente\n"+
        "Opção 5 - Encerrar Sessão \n"
        );
        Console.ForegroundColor = ConsoleColor.White;
        string opcaoMensalista = Console.ReadLine();
        switch(opcaoMensalista){
            case "1":
            //Função Cadastrar Veículo instanciada.
                Console.Clear();
                es.CadastrarMensalista();
                Console.ResetColor();
                Console.WriteLine(
                " \nCaso queira retornar para o Menu da Área do Estacionamento Mensalista, digite M.\n"+
                "Caso queira encerrar a sessão e retornar para o Painel Inicial, é só clicar na tecla ENTER. \n"
                );
                Console.ForegroundColor = ConsoleColor.White;
                retornar = Console.ReadLine();
                retornar = retornar.ToUpper();
                if(retornar == "M"){
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("Retornando para o Menu do Estacionamento Mensalista...");
                    goto ReiniciarOpcoesCliente; //Retorno para o Menu do Estacionamento Rotativo para facilitar a navegação do usuário.
                }
                break;
            case "2":
            //Função Adicionar Veículo instanciada.
                Console.Clear();
                es.AdicionarVeiculo(opcaoRouM);
                Console.ResetColor();
                Console.WriteLine(
                " \nCaso queira retornar para o Menu da Área do Estacionamento Mensalista, digite M.\n"+
                "Caso queira encerrar a sessão e retornar para o Painel Inicial, é só clicar na tecla ENTER. \n"
                );
                Console.ForegroundColor = ConsoleColor.White;
                retornar = Console.ReadLine();
                retornar = retornar.ToUpper();
                if(retornar == "M"){
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("Retornando para o Menu do Estacionamento Mensalista...");
                    goto ReiniciarOpcoesCliente; //Retorno para o Menu do Estacionamento Rotativo para facilitar a navegação do usuário.
                }
                break;
            case "3":
            //Função Remover Veículo instanciada.
                Console.Clear();
                es.RemoverVeiculo(opcaoRouM);
                Console.ResetColor();
                Console.WriteLine(
                " \nCaso queira retornar para o Menu da Área do Estacionamento Mensalista, digite M.\n"+
                "Caso queira encerrar a sessão e retornar para o Painel Inicial, é só clicar na tecla ENTER. \n"
                );
                Console.ForegroundColor = ConsoleColor.White;
                retornar = Console.ReadLine();
                retornar = retornar.ToUpper();
                if(retornar == "M"){
                    Console.Clear();
                    Console.ResetColor();
                    Console.WriteLine("Retornando para o Menu do Estacionamento Mensalista...");
                    goto ReiniciarOpcoesCliente; //Retorno para o Menu do Estacionamento Rotativo para facilitar a navegação do usuário.
                }
                break;
            case "4": 
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("Retornando à Área do Cliente.... \n");
                goto ReiniciarRouM; //Retorno para a Área do Cliente
            case "5":
                Console.ResetColor();
                goto Encerrar; //Encerra o programa e Retorna para o Início.
            default:
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(
                " \nOpção inválida."+
                "Estamos retornando para o Painel dos Clientes Mnesalistas.\n");
                goto ReiniciarOpcoesCliente; //Opção Default Referenciada (Retorno para as opções do Mensalista caso o usuário não tenha digitado corretamente).
        }
    break;
    default:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
            " \nOpção inválida."+
            "Estamos retornando para a Área do Cliente.\n");
            goto ReiniciarRouM;  //Opção Default Referenciada (Retorno para a área do cliente, caso o usuário não tenha teclado R ou M).
}
Encerrar:; //Referência de encerramento e início do programa (usado no término do painel de Administrador).
Console.Clear();
Console.WriteLine( 
//Finalizar o Programa e Reiniciá-lo no fim de cada ação do programa.
" \nFinalizando... \n"+
"O Canal de Serviços do AV2024 está encerrado! \n"+
"Reiniciando...\n\n"
);
}
