using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        
        public PartidaXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            xeque = false;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(destino, p);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // #jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarMovimentos();
                tab.colocarPeca(destinoT, T);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarMovimentos();
                tab.colocarPeca(destinoT, T);
            }

            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQteMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(destino, pecaCapturada);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(origem, p);

            // #jogadaespecial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQteMovimentos();
                tab.colocarPeca(origemT,  T);
            }

            // #jogadaespecial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQteMovimentos();
                tab.colocarPeca(origemT, T);
            }
        }

        public void realizarJogada(Posicao origem, Posicao destino)
        {
           Peca pecaCapturada = executarMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            
            if (testeXequemate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }
        }

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preto;
            }
            else
            {
                return Cor.Branco;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.Linha, R.posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }


        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if(jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça na posição de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino( Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino invalida!");
            }
        }

        private void mudaJogador()
        {
            if(jogadorAtual == Cor.Branco)
            {
                jogadorAtual = Cor.Preto;
            }
            else
            {
                jogadorAtual = Cor.Branco;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in pecas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executarMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(new PosicaoXadrez(coluna, linha).toPosicao(), peca);
            pecas.Add(peca);
        }
        
        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branco));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branco));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branco));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branco));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branco, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branco));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branco));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branco));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branco));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branco));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branco));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branco));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branco));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branco));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branco));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branco));
            //----------------------------------------------------\\
            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preto));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preto));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preto));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preto));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preto, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preto));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preto));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preto));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preto));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preto));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preto));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preto));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preto));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preto));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preto));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preto));
        }
    }
}
