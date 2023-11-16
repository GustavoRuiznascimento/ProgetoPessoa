using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPIPessoaApplication.Pessoa
{
    public class PessoaReponse
    {
        public string Nome { get; set; } 

        public int Idade { get; set; }

        public decimal Imc { get; set; }

        public string Classificacao { get; set; }

        public double Inss { get; set; }

        public double Aliquora { get; set; }

        public double salarioLiquido { get; set; }

        public decimal SaldoDollar { get; set; }

    }
}
