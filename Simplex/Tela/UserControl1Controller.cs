using Simplex.Calculo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex
{
    public class UserControl1Controller : Notify
    {
        public UserControl1Controller()
        {
            ListaMaxMin = new List<string> { "MAX", "MIN" };
            FuncaoObjetiva = new FuncaoObjetiva();
            ListaInequacao = new ObservableCollection<Inequacao>();
            CurrentInequacao = new Inequacao();
            Travar = false;

        }

        private List<string> listaMaxMin;
        public List<string> ListaMaxMin
        {
            get => listaMaxMin;
            set { listaMaxMin = value; OnPropertyChanged(new PropertyChangedEventArgs("ListaMaxMin")); }
        }

        private string calculo;
        public string Calculo
        {
            get => calculo;
            set { calculo = value; OnPropertyChanged(new PropertyChangedEventArgs("Calculo")); }
        }

        private bool travar;
        public bool Travar
        {
            get => travar;
            set { travar = value; OnPropertyChanged(new PropertyChangedEventArgs("Travar")); }
        }

        private FuncaoObjetiva funcaoObjetiva;
        public FuncaoObjetiva FuncaoObjetiva
        {
            get => funcaoObjetiva;
            set { funcaoObjetiva = value; OnPropertyChanged(new PropertyChangedEventArgs("FuncaoObjetiva")); }
        }

        private Inequacao currentInequacao;
        public Inequacao CurrentInequacao
        {
            get => currentInequacao;
            set { currentInequacao = value; OnPropertyChanged(new PropertyChangedEventArgs("CurrentInequacao")); }
        }

        private ObservableCollection<Inequacao> listaInequacao;
        public ObservableCollection<Inequacao> ListaInequacao
        {
            get => listaInequacao;
            set { listaInequacao = value; OnPropertyChanged(new PropertyChangedEventArgs("ListaInequacao")); }
        }

        internal void Calcular()
        {
            Calculo = String.Empty;
            var funcaoObj = FuncaoObjetiva.DuplicarObjeto<FuncaoObjetiva>();
            funcaoObj.IgualarZero();

            int quantidadeColunas = CalcularColunas();
            int quantidadeLinhas = CalcularLinhas();

            decimal[,] tabela = new decimal[quantidadeLinhas, quantidadeColunas];

            PreencherTabela(tabela, funcaoObj, quantidadeColunas, quantidadeLinhas);
            ImprimirResultado(tabela, quantidadeColunas, quantidadeLinhas);

            CalcularRecursivo(tabela, quantidadeColunas, quantidadeLinhas);
            ImprimirResultado(tabela, quantidadeColunas, quantidadeLinhas);
        }

        private void CalcularRecursivo(decimal[,]tabela, int quantidadeColunas, int quantidadeLinhas)
        {
            bool fim = VerificarFimSimplex(tabela, quantidadeColunas);

            if (!fim)
            {
                int indiceColunaPivo = SelecionarIndiceColunaPivo(tabela, quantidadeColunas);
                int indiceLinhaPivo = SelecionarIndiceLinhaPivo(tabela, quantidadeLinhas, indiceColunaPivo);

                CalcularLinhaPivo(tabela, indiceColunaPivo, indiceLinhaPivo, quantidadeColunas);
                CalcularTabela(tabela, indiceColunaPivo, indiceLinhaPivo, quantidadeLinhas, quantidadeColunas);
                CalcularRecursivo(tabela, quantidadeColunas, quantidadeLinhas);
            }
        }

        private void ImprimirResultado(decimal[,] tabela, int quantidadeColunas, int quantidadeLinhas)
        {
            Calculo += "=================================================\n";

            for(int i =0; i < quantidadeLinhas; i++)
            {
                for(int j = 0; j < quantidadeColunas; j++)
                {
                    Calculo += $"{tabela[i, j]}\t";
                }

                Calculo += "\n";
            }
        }

        private bool VerificarFimSimplex(decimal[,] tabela, int quantidadeColunas)
        {
            for(int i = 0; i< quantidadeColunas - 1; i++)
            {
                if(tabela[0,i] < 0)
                {
                    return false;
                }
            }

            return true;
        }

        private void CalcularTabela(decimal[,] tabela, int indiceColunaPivo, int indiceLinhaPivo, int quantidadeLinhas, int quantidadeColunas)
        {
            for (int i = 0; i < quantidadeLinhas; i++)
            {
                if (i != indiceLinhaPivo)
                {
                    decimal valorPivo = tabela[i, indiceColunaPivo];

                    for (int j = 0; j < quantidadeColunas; j++)
                    {
                        tabela[i, j] = Decimal.Round((valorPivo * -1) * tabela[indiceLinhaPivo, j] + tabela[i, j], 2, MidpointRounding.AwayFromZero);
                    }
                }
            }
        }

        private void CalcularLinhaPivo(decimal[,] tabela, int indiceColunaPivo, int indiceLinhaPivo, int quantidadeColunas)
        {
            decimal valorPivo = tabela[indiceLinhaPivo, indiceColunaPivo];

            for (int i = 0; i < quantidadeColunas; i++)
            {
                if (tabela[indiceLinhaPivo, i] != 0)
                {
                    tabela[indiceLinhaPivo, i] = Decimal.Round((tabela[indiceLinhaPivo, i] / valorPivo), 2, MidpointRounding.AwayFromZero);
                }
            }
        }

        private int SelecionarIndiceLinhaPivo(decimal[,] tabela, int quantidadeLinhas, int indiceColunaPivo)
        {
            int indiceLinhaPivo = 0;
            int indiceUltimaColuna = tabela.GetLength(1) - 1;
            decimal menorValor = 0;

            for (int i = 1; i < quantidadeLinhas; i++)
            {
                if (tabela[i, indiceColunaPivo] != 0)
                {
                    decimal valorDivisao = tabela[i, indiceUltimaColuna] / tabela[i, indiceColunaPivo];

                    if (i == 1)
                    {
                        indiceLinhaPivo = i;
                        menorValor = valorDivisao;
                    }
                    else if (valorDivisao < menorValor)
                    {
                        indiceLinhaPivo = i;
                        menorValor = valorDivisao;
                    }
                }
            }

            return indiceLinhaPivo;
        }

        private int SelecionarIndiceColunaPivo(decimal[,] tabela, int quantidadeColunas)
        {
            int indiceValorMenor = 0;
            decimal menorValor = 0;

            for (int i = 0; i < quantidadeColunas; i++)
            {
                if (tabela[0, i] < menorValor)
                {
                    indiceValorMenor = i;
                    menorValor = tabela[0, i];
                }
            }

            return indiceValorMenor;
        }

        private void PreencherTabela(decimal[,] tabela, FuncaoObjetiva funcaoObjetiva, int quantidadeColunas, int quantidadeLinhas)
        {
            for (int i = 0; i < quantidadeLinhas; i++)
            {
                if (i == 0)
                {
                    for (int j = 0; j < quantidadeColunas; j++)
                    {
                        if (j == 0)
                        {
                            tabela[i, j] = funcaoObjetiva.X1;
                        }
                        else if (j == 1)
                        {
                            tabela[i, j] = funcaoObjetiva.X2;
                        }
                        else if (j == 2)
                        {
                            tabela[i, j] = funcaoObjetiva.X3;
                        }
                        else
                        {
                            tabela[i, j] = 0;
                        }
                    }
                }
                else
                {
                    var ineq = ListaInequacao[i - 1];

                    for (int j = 0; j < quantidadeColunas; j++)
                    {

                        tabela[i, quantidadeColunas - 1] = ineq.Total;

                        if (j == 0)
                        {
                            tabela[i, j] = ineq.X1;
                        }
                        else if (j == 1)
                        {
                            tabela[i, j] = ineq.X2;
                        }
                        else if (j == 2)
                        {
                            tabela[i, j] = ineq.X3;
                        }
                        else
                        {
                            tabela[i, j + ineq.Ordem] = 1;
                            break;
                        }
                    }
                }
            }
        }

        private int CalcularLinhas()
        {
            int quantidadeLinhas = 1;
            quantidadeLinhas += ListaInequacao.Count();

            return quantidadeLinhas;
        }

        private int CalcularColunas()
        {
            int quantidadeColunas = 1;

            if (funcaoObjetiva.X1 > 0 && funcaoObjetiva.X2 > 0 && funcaoObjetiva.X3 > 0)
            {
                quantidadeColunas += 3;
            }
            else if (funcaoObjetiva.X1 > 0 && funcaoObjetiva.X2 > 0)
            {
                quantidadeColunas += 2;
            }
            else
            {
                quantidadeColunas += 1;
            }

            quantidadeColunas += ListaInequacao.Count();

            return quantidadeColunas++;
        }
    }
}
