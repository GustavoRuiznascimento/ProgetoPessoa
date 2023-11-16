using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAPIPessoa.Repository;
using WebAPIPessoa.Repository.Models;

namespace WebAPIPessoaApplication.Pessoa
{
    public class PessoaService
    {
        private readonly PessoaContext _context;
        public PessoaService(PessoaContext context)
        { 
            _context = context;
        }

        public PessoaHistoricoResponse ObterHistoricoPessoa(int id)
        {
            var pessoaDb = _context.Pessoas.FirstOrDefault(x => x.id == id);
            var pessoa = new PessoaHistoricoResponse()
            {
                Aliquota = Convert.ToDouble(pessoaDb.aliquota),
                Altura = pessoaDb.altura,
                Classificacao = pessoaDb.classificacao,
                DataNascimento = pessoaDb.dataNascimento,
                Id = pessoaDb.id,
                Idade = pessoaDb.idade,
                IdUsuario = pessoaDb.idUsuario,
                Imc = pessoaDb.imc,
                Inss = Convert.ToDouble(pessoaDb.inss),
                Nome = pessoaDb.nome,
                Peso = pessoaDb.peso,
                Salario = Convert.ToDouble(pessoaDb.salario),
                SalarioLiquido = Convert.ToDouble(pessoaDb.salarioLiquido),
                Saldo = pessoaDb.saldo,
                SaldoDollar = pessoaDb.saldoDolar
            };

            return pessoa;

        }

        public List<PessoaHistoricoResponse> ObterHistoricoPessoas()
        {
            var pessoasDb = _context.Pessoas.ToList();
            var pessoas = new List<PessoaHistoricoResponse>();

            foreach (var item in pessoasDb)
            {
                pessoas.Add(new PessoaHistoricoResponse()
                {
                   Aliquota = Convert.ToDouble(item.aliquota),
                   Altura = item.altura,
                   Classificacao = item.classificacao,
                   DataNascimento = item.dataNascimento,
                   Id = item.id,
                   Idade = item.idade,
                   IdUsuario = item.idUsuario,
                   Imc = item.imc,
                   Inss = Convert.ToDouble(item.inss),
                   Nome = item.nome,
                   Peso = item.peso,
                   Salario = Convert.ToDouble(item.salario),
                   SalarioLiquido = Convert.ToDouble(item.salarioLiquido),
                   Saldo = item.saldo,
                   SaldoDollar = item.saldoDolar
                });
            }

            return pessoas;
        }
        public PessoaReponse ProcessarInformacoesPessoa(PessoaRequest request, int usuarioId)
        {
            var idade = CalcularIdade(request.DataNascimento);
            var imc = CalcularImc(request.Peso, request.Altura);
            var Classificacao = CalcularClassificacao(imc);
            var aliquota = CalcularAliquota(request.Salario);
            var Inss = CalcularInss(request.Salario, aliquota);
            var salarioLiquido = CalcularSalarioliquido(request.Salario, Inss);
            var saldoDolar = CalcularDolar(request.Saldo);

            var resposta = new PessoaReponse();
            resposta.SaldoDollar = saldoDolar;
            resposta.Aliquora = aliquota;
            resposta.salarioLiquido = salarioLiquido;
            resposta.Classificacao = Classificacao;
            resposta.Idade = idade;
            resposta.Imc = imc;
            resposta.Inss = Inss;
            resposta.Nome = request.Nome;

            var pessoa = new TabPessoa()
            {
                aliquota = Convert.ToDecimal (aliquota),
                altura = request.Altura,
                classificacao = Classificacao,
                dataNascimento = request.DataNascimento,
                idade = idade,
                idUsuario = usuarioId,
                imc = imc,
                inss = Convert.ToDecimal (Inss),
                nome = request.Nome,
                peso = request.Peso,
                salario = Convert.ToDecimal (request.Salario),
                salarioLiquido = Convert.ToDecimal (salarioLiquido),
                saldo = request.Saldo,
                saldoDolar = saldoDolar,

            };

            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();

            return resposta;
        }
        private int CalcularIdade(DateTime dataNascimento)
        {
            var anoatual = DateTime.Now.Year;
            var idade = anoatual - dataNascimento.Year;

            var mesAtual = DateTime.Now.Month;
            if (mesAtual < dataNascimento.Month)
            {
                idade = idade - 1;
            }

            return idade;
        }

        private decimal CalcularImc(decimal peso, decimal altura)
        {
            var imc = Math.Round(peso / (altura * altura), 2);
            return imc;
        }

        private String CalcularClassificacao(decimal imc)
        {
            var classificacao = "";

            if (imc < (decimal)18.5)
            {
                classificacao = "abaixo do peso ideal";
            }
            else if (imc >= (decimal)18.5 && imc <= (decimal)24.99)
            {
                classificacao = "Peso Normal";
            }
            else if (imc >= (decimal)25 && imc <= (decimal)29.99)
            {
                classificacao = "Pré-Obesidade";
            }
            else if (imc >= (decimal)30 && imc <= (decimal)34.99)
            {
                classificacao = "obesidade Grau I";
            }
            else if (imc >= (decimal)35 && (imc <= (decimal)39.99))
            {
                classificacao = "Obesidade Grau II";
            }
            else
            {
                classificacao = "Obesidade Grau III";
            }

            return classificacao;
        }

        private double CalcularAliquota(double salario)
        {
            var aliquota = 7.5;
            if (salario <= 1212)
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

            return aliquota;
        }

        private double CalcularInss(double salario, double aliquota)
        {
            var Inss = (salario * aliquota) / 100;
            return Inss;
        }

        private double CalcularSalarioliquido(double salario, double inss)
        {
            return salario - inss;

        }

        private decimal CalcularDolar(decimal saldo)
        {
            var dolar = (decimal)5.14;
            var saldoDolar = Math.Round(saldo / dolar, 2);

            return saldoDolar;

        }
    }
}
