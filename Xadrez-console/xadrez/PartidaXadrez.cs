using tabuleiro;

namespace xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro tab { get; private set; }
        private int turno;
        private Cor jogadorAtual;
        public bool terminada { get; set; }
        
        public PartidaXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            colocarPecas();
            terminada = false;
        }

        public void executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(destino, p);
        }
        
        private void colocarPecas()
        {
            tab.colocarPeca(new PosicaoXadrez('e', 8).toPosicao(), new Rei(tab, Cor.Preto));
            tab.colocarPeca(new PosicaoXadrez('d', 8).toPosicao(), new Torre(tab, Cor.Preto));

        }
    }
}
