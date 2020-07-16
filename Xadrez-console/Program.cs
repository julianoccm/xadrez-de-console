using System;
using xadrez;
using tabuleiro;

namespace Xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);
                tab.colocarPeca(new Posicao(0, 0), new Rei(tab, Cor.Preto));
                tab.colocarPeca(new Posicao(1, 1), new Torre(tab, Cor.Preto));

                Tela.imprimirTabuleiro(tab);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
