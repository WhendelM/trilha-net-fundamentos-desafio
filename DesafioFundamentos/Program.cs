using System.Text.RegularExpressions;
using DesafioFundamentos.Models;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Estacionamento es = new Estacionamento();
bool start1 = true;
string opcao1 = string.Empty;
string inicio = string.Empty;
while(start1){
    ReiniciarDefault1:;
    Console.WriteLine("Você está acessando o Estacionamento Privado do AV2024.\n \n Se você é ou deseja ser nosso cliente, digite C. \n Se você é nosso colaborador ou administrador do nosso programa, digite A. \n \n ");
    inicio = Console.ReadLine();
    inicio = inicio.ToUpper();
    switch(inicio){
        case "C":
            Console.WriteLine("Você está na área de cliente! \n \n Para acessar o Estacionamento Rotativo, digite R. \n Para acessar o Estacionamento Mensalista, digite M \n \n ");
            opcao1 = Console.ReadLine();
            opcao1 = opcao1.ToUpper();
            break;
        case "A": 
            string padraoSenha = @"^[A-Z]{3}\d$";
            Console.WriteLine("Você está na área de Administrador. Por questões de segurança, digite aqui seu código exclusivo para entrada: \n \n ");
            string senha = Console.ReadLine();
            if(Regex.IsMatch(senha,padraoSenha)){
                Console.WriteLine("Você estará acessando o Menu da Admnistração. Não esqueca que as informações reitradas são privativas para a segurança do Estacionamento e dos clientes. \n \n");
                Console.WriteLine("Menu: ");
                Console.WriteLine("Opção 1 - Acessar veículos estacionados");
                Console.WriteLine("Opção 2 - Acessar clientes");
                Console.WriteLine("Opção 3 - Acessar Rendimento");

                switch(Console.ReadLine()){
                    case "1": 
                        es.ListarVeiculos();
                    break;
                    
                    case "2":
                        es.ListarClientes();
                    break;

                    case "3":
                        es.RendimentoTotal();
                    break;
                }

                goto Reiniciar1;
            }
            else{
                Console.WriteLine("Código incorreto. Revise e tente novamente. \n \n");
                goto Reiniciar1;
            }
         default:
            Console.WriteLine("Opção inválida.Tente Novamente!");
            goto ReiniciarDefault1;
    }

switch(opcao1){
    case "R": 
        Console.WriteLine("Você escolheu o Modo Rotativo! \n \n");
        Console.WriteLine("Menu: ");
        Console.WriteLine("Opção 1 - Estacionar Veículo");
        Console.WriteLine("Opção 2 - Remover Veículo");
        string opcaoRotativo = Console.ReadLine();
        switch(opcaoRotativo){
            case "1":
                es.AdicionarVeiculo(opcao1);
                break;
            case "2":
                es.RemoverVeiculo(opcao1);
                break;
        }
        break;
    case "M": 
        Console.WriteLine("Você escolheu o Modo Mensalista! \n \n Caso seja novo cliente, cadastre=se em nosso sistema antes na opção 'Cadastrar Meus Dados' no menu abaixo. \n \n");
        Console.WriteLine("Menu: ");
        Console.WriteLine("Opção 1 - Cadastrar Meus Dados");
        Console.WriteLine("Opção 2 - Estacionar Veículo");
        Console.WriteLine("Opção 3 - Remover Veículo");
        string opcaoMensalista = Console.ReadLine();
        switch(opcaoMensalista){
            case "1":
                es.CadastrarMensalista();
                break;
            case "2":
                es.AdicionarVeiculo(opcao1);
                break;
            case "3":
                es.RemoverVeiculo(opcao1);
                break;
        }
    break;

}
Reiniciar1:;
Console.WriteLine("\n \n Finalizando...\n \n ");
Console.WriteLine("O Canal de Serviços do AV2024 está encerrado!\n \n");
Console.WriteLine("Reiniciando...\n \n ");
}
