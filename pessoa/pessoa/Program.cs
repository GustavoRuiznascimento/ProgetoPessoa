using System;

namespace pessoa
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("digite o nome da pessoa");
            var nomePessoa = Console.ReadLine();

            Console.WriteLine("digite a data de nascimento");
            var dataNasmientoPessoa = Console.ReadLine();
            var dataNascimento = Convert.ToDateTime(dataNasmientoPessoa);

            var AnoAtual = DateTime.Now.Year;
            var idade = AnoAtual - dataNascimento.Year;

            Console.WriteLine("Digite a altura");
            var altura = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Digite o peso");
            var peso = Convert.ToDecimal(Console.ReadLine());

            var imc = Math.Round(peso / (altura * altura), 2);
            var classificacao = "";

            if (imc < (decimal)18.5)
            {
                classificacao = "Abaixo do peso ideal";              
            }
            else if (imc >= (decimal)18.5 && imc <= (decimal)24.99)
            {
                classificacao = "peso ideal";
            }
            else if (imc >= (decimal)25 && imc <= (decimal)29.99)
            {
                classificacao = "pré-obesidade";
            }
            else if (imc >= (decimal) 30 && imc <= (decimal)34.99)
            {
                classificacao = "Obesidade Grau 1";
            }
            else if (imc >= (decimal)35 && imc <= (decimal)39.99)
            {
                classificacao = "Obesidade Grau 2";
            }


            else
            {
                classificacao = "Obesidade Grau 3";
            }
             
            Console.WriteLine("Digite o salario bruto da pessoa");
            var salario = Convert.ToDouble(Console.ReadLine());

            var aliquota = 7.5;
            if ( salario <= 1212)
            {
                aliquota = 7.5;
            }
            else if (salario <= 1212.01 && salario <= 2427.35)
            {
                aliquota = 9;
            }
            if (aliquota <= 2427.36 && salario <= 3641.03)
            {
                aliquota = 12;
            }
            else
            {
                aliquota = 14;
            }
            var Inss = (salario * aliquota) / 100;
            var salarioLiquido = salario - Inss;

            Console.WriteLine("Digite o saldo da conta");
                var saldo = Convert.ToDecimal(Console.ReadLine());

                var dolar = (decimal)4.98;

            var saldoDolar = Math.Round(saldo / dolar, 2);


            Console.WriteLine("O nome da pessoa é: " + nomePessoa);
            Console.WriteLine("A idade da pesssoa " + nomePessoa + " é " + idade + " anos");
            Console.WriteLine("O Imc da pessoa é: " + imc + " classificação: " + classificacao );
            Console.WriteLine("O desconto do INSS é: " + Inss + " aliquota: " + aliquota);
            Console.WriteLine("O salario liquido é:" + salarioLiquido);
            Console.WriteLine("O saldo em dolar é: " + saldoDolar);

        }
    }
}
