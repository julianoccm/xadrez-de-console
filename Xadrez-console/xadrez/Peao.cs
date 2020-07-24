using tabuleiro;

namespace xadrez
{

    class Peao : Peca
    {
        private PartidaXadrez partida;

        public Peao(Tabuleiro tab, Cor cor, PartidaXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Posicao pos)
        {
            return tab.peca(pos) == null;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branco)
            {
                pos.definirValores(posicao.Linha - 1, posicao.Coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha - 2, posicao.Coluna);
                Posicao p2 = new Posicao(posicao.Linha - 1, posicao.Coluna);
                if (tab.posicaoValida(p2) && livre(p2) && tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha - 1, posicao.Coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha - 1, posicao.Coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // #jogadaespecial en passant
                if (posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant)
                    {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant)
                    {
                        mat[direita.Linha - 1, direita.Coluna] = true;
                    }
                }

            }
            else
            {
                pos.definirValores(posicao.Linha + 1, posicao.Coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha + 2, posicao.Coluna);
                Posicao p2 = new Posicao(posicao.Linha + 1, posicao.Coluna);
                if (tab.posicaoValida(p2) && livre(p2) && tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha + 1, posicao.Coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.definirValores(posicao.Linha + 1, posicao.Coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // #jogadaespecial en passant
                if (posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant)
                    {
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant)
                    {
                        mat[direita.Linha + 1, direita.Coluna] = true;
                    }
                }

            }

            return mat;
        }
    }
}