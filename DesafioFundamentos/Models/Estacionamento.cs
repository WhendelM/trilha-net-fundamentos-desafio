using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public void AdicionarVeiculo()
        {
            //Função AdicionarVeiculo implementada!
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = Console.ReadLine();
            placa = placa.ToUpper(); //Coloca todas as letras em maiúsculo
            if (VerificarPadraoPlaca(placa))
            {
                veiculos.Add(placa);
                Console.WriteLine("O seu carro foi estacionado!");
            }
            else
            {
                Console.WriteLine("A placa solicitada não está correta. Verifique-a e tente novamente.");
            }
        }

        public static bool VerificarPadraoPlaca(string placa)
        {
            string ModeloPlaca = @"^[A-Z]{3}\d[A-Z]\d{2}$"; //Modelo de placa (LETRA LETRA LETRA NÚMERO LETRA NÚMERO NÚMERO)
            Regex padraoPlaca = new Regex(ModeloPlaca); //Configurar o modelo como padrão para verificação
            return padraoPlaca.IsMatch(placa); //Retorna a compatibilidade da placa com o modelo
        }

        public void RemoverVeiculo()
        {
            Console.WriteLine("Digite a placa do veículo para remover:");

            //Função RemoverVeiculo implementada!
            string placa = Console.ReadLine();

            // Verifica se o veículo existe
            if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            {
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");
                int horas = Convert.ToInt32(Console.ReadLine());
                decimal valorTotal = precoInicial + (precoPorHora * horas);

                veiculos.Remove(placa);
                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            //Função ListarVeiculos implementada!
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                for (int contador = 0; contador < veiculos.Count; contador++)
                {
                    Console.WriteLine($"Veículo {contador + 1}: {veiculos[contador]}");
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
}
