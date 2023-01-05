# Xadres de Console
<p align="center">
  <img width="700" height="200" src="https://user-images.githubusercontent.com/66191563/88411134-ca94b100-cdad-11ea-9cd9-b6ef821dbf45.png">
</p>

# Índice
   - [**Sobre o projeto**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#sobre-o-projeto)
   - [**Sobre o tabuleiro**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#sobre-o-tabuleiro)
     - [**Impressão do tabuleiro no console**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#impress%C3%A3o-do-tabuleiro-no-console)
     - [**A exceção tabuleiroException**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#a-exce%C3%A7%C3%A3o-tabuleiroexception)
   - [**Sobre as peças**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#sobre-as-pe%C3%A7as)
     - [**Peças presentes no tabuleiro**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#pe%C3%A7as-presentes-no-tabuleiro)
     - [**Método para colocar as peças no tabuleiro**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#m%C3%A9todo-para-colocar-as-pe%C3%A7as-no-tabuleiro)
   - [**Como foi criada a restrição de movimento para cada peça**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#como-foi-criada-a-restri%C3%A7%C3%A3o-de-movimento-para-cada-pe%C3%A7a)
      - [**Validar posição de origem**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#validar-posi%C3%A7%C3%A3o-de-origem)
         - [**Posição nula**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#1-posi%C3%A7%C3%A3o-nula)
         - [**Se a peça é do jogador**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#2-se-a-pe%C3%A7a-escolhida-%C3%A9-do-jogador)
         - [**Se não existe movimentos possíveis**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#3-se-n%C3%A3o-existem-movimentos-poss%C3%ADveis)
      - [**Validar posição de destino**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#validando-posi%C3%A7%C3%A3o-de-destino)
   - [**Jogadas especiais**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#jogadas-especiais)
      - [**Roque pequeno**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#1-roque-pequeno)
      - [**Roque grande**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#2-roque-grande)
      - [**En Passant**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#3-en-passant)
      - [**Promoção**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#4-promo%C3%A7%C3%A3o)
   - [**Baixar o jogo!**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#baixar-o-jogo)
   - [**Contato**](https://github.com/JulianoCCMoreira/Xadrez-de-Console/new/master?readme=1#contato-julianocolerecmoreiragmailcom)

# Sobre o projeto
Este foi um projeto desenvolvido durante o curso de C# do @acenelio. A proposta principal dele é aplicar nossos conhecimentos na linguagem C# para criar um jogo de Xadrez que rodasse via console. Ele pode parecer um projeto simples, porém ele é muito mais complicado do que você imagina.

# Sobre o tabuleiro
  ### Impressão do tabuleiro no console
  A impressão do tabuleiro foi a parte mais simples desse projeto. Eu utilizei um *for* para criar "-" com um espaço entre eles no tamanho de oito por oito, totalizando sessenta e quatro casas (o tamanho padrão de um tabuleiro de Xadrez).

##### Código do método imprimirTabuleiro:
```cs
public static void imprimirTabuleiro(Tabuleiro tab)
{
            for (int i = 0; i < tab.linhas; i++)
            {
                ConsoleColor aux1 = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(8 - i + " ");
                Console.ForegroundColor = aux1;

                for (int j = 0; j < tab.colunas; j++)
                { 
                        imprimirPeca(tab.peca(i, j));
                }
                Console.WriteLine();
            }
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  A B C D E F G H");
            Console.ForegroundColor = aux;
}
```
##### E o resultado final foi este:
<p align="center">
   <img width="142" height="165" src="https://user-images.githubusercontent.com/66191563/88415065-3e39bc80-cdb4-11ea-9c31-ece24c2dda8d.PNG">
</p>
    
  ### A exceção tabuleiroException
  Foi necessária a criação de uma exceção para o tabuleiro, ela ocorre quando uma casa invalida é selecionada.
  ##### Alguns exemplos de exceções:
  ```cs
  throw new TabuleiroException("Não existe peça na posição de origem escolhida!"); 
  ```
  ```cs
  throw new TabuleiroException("A peça na posição de origem escolhida não é sua!");          
  ```
  ```cs
  throw new TabuleiroException("Não há movimentos possiveis para a peça de origem escolhida!");          
  ```
  ```cs
  throw new TabuleiroException("Posição de destino invalida!");
  ```        

# Sobre as peças
 ### Peças presentes no tabuleiro:
 - Rei `(x1 Branco & x1 Preto)`, representado pela letra `R`;
 - Dama `(x1 Branco & x1 Preto)`, representado pela letra `D`;
 - Bispo `(x2 Branco & x2 Preto)`, representado pela letra `B`;
 - Cavalo `(x1 Branco & x1 Preto)`, representado pela letra `C`;
 - Torre `(x1 Branco & x1 Preto)`, representado pela letra `T`;
 - Peão `(x8 Branco & x8 Preto)`, representado pela letra `P`.
 
 ##### Imagem do tabuleiro com as peças:
 <p align="center">
   <img width="141" height="164" src="https://user-images.githubusercontent.com/66191563/88418677-53b1e500-cdba-11ea-981f-723ef7543664.PNG">
</p>
 
 ### Método para colocar as peças no tabuleiro
 Eu implementei um método para colocar as peças em determinada posição. Cada peça possui a sua letra especifica, o método que mostra essa letra é este:
 ##### Código:
 ```cs
 public override string ToString()
 {
            return "R";
 }
 ```
 > *Neste caso a letra R é retornada, pois, este pedaço do código é do Rei.*
 
 Então eu necessitava de um modo de colocar esta letra no tabuleiro, então na classe *PartidaXadrez* eu criei dois métodos:
 
 ##### Método colocarNovaPeca:
 ```cs
 public void colocarNovaPeca(char coluna, int linha, Peca peca)
 {
            tab.colocarPeca(new PosicaoXadrez(coluna, linha).toPosicao(), peca);
            pecas.Add(peca);
 }
 ```
 
 > Este método recebe uma coluna em char e um número, e o método *toPosicao()* converte esses dados em uma posição valida na matriz.
 
 ##### Método colocarPecas:
 Neste método eu criava uma peça com o *colocarNovaPeca()* deste modo:

 
 ```cs
 colocarNovaPeca('a', 1, new Torre(tab, Cor.Branco));
 ```

# Como foi criada a restrição de movimento para cada peça
Na classe *PartidaXadrez* foram criados dois métodos, o método *validarPosicaoDeOrigem* e o método *validarPosicaoDeDestino*.

## Validar posição de origem:
Este método recebe uma posição informada pelo usuário (a coordenada de peça que ele quer mover). Ele foi dividido em tês *if's*.

#### 1. Posição nula
   Para testar se uma posição é nula eu utilizei a posição informada pelo usuário e a testei com o seguinte código:

   ```cs
if ( tab.peca(pos) == null)
{
       throw new TabuleiroException(" Não existe peça na posição de origem escolhida!");
}
   
   ```

#### 2. Se a peça escolhida é do jogador
   Eu tive que testar se a peça escolhida era da cor do jogador atual, para testar foi utilizado o seguinte código:

   ```cs
 if (jogadorAtual != tab.peca(pos).cor)
 {
        throw new TabuleiroException("Uma peça de origem escolhida não é sua!");
 }
   ```
   
#### 3. Se não existem movimentos possíveis
 Eu testei se a peça selecionada possuía movimentos possíveis, caso fosse escolhida uma peça que não podia se mover ocorria uma exceção.

 ```cs
 if ( !tab.peca(pos).existeMovimentosPossiveis())
 {
          throw new TabuleiroException("Não há movimentos possíveis para uma peça de origem escolhida!");
 }

 ```

## Validando posição de destino
Este método recebe uma posição informada pelo usuário (a coordenada para onde ele quer ir). Para este método foi utilizado apenas um *if*.

```cs
if ( !tab.peca(origem).podeMoverPara(destino))
{
           throw new TabuleiroException ("Posição de destino invalida!");
}
```

# Jogadas especiais
 Neste tópico eu mencionarei as jogadas especiais implementadas no jogo.

 #### 1. Roque Pequeno
 O roque pequeno ocorre quando o rei e uma torre não se moveram, e entre eles possuem duas casas vazias.

 ```cs
 // #jogadaespecial roque pequeno
 if (p is Rei && destino.Coluna == origem.Coluna + 2)
 {
        Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
        Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
        Peca T = tab.retirarPeca(origemT);
        T.incrementarMovimentos();
        tab.colocarPeca(destinoT, T);
 }

 ```
 
 #### 2. Roque Grande
 O roque pequeno ocorre quando o rei e uma torre não se moveram, e entre eles têm que possuir quatro casas vazias.

 ```cs
// #jogadaespecial roque grande
if (p is Rei && destino.Coluna == origem.Coluna - 2)
{
        Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
        Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
        Peca T = tab.retirarPeca(origemT);
        T.incrementarMovimentos();
        tab.colocarPeca(destinoT, T);
}
 ```
 
 #### 3. En Passant
  O En Passant ocorre quando um peão adversário avança duas casas no seu primeiro movimento na tentativa de evitar um confronto com um peão avançado (se for um peão branco na linha 5, se for um preto na linha 4) e um peão pode fazer a captura do mesmo modo.

  
##### Imagem ilustrativa
<p align="center">
   <img  src="https://qph.fs.quoracdn.net/main-qimg-945c1c2845899be55fad08abe4c010f9">
</p>

> *Imagem obtida neste [site](https://www.quora.com/How-would-you-explain-en-passant-to-a-beginner)*

##### Codigo
  ```cs
// #jogadaespecial en passant
if (p is Peao)
{
       if (origem.Coluna != destino.Coluna && pecaCapturada == null)
       {
             Posicao posP;
             if (p.cor == Cor.Branco)
             {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
             }
             else
             {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
             }
             pecaCapturada = tab.retirarPeca(posP);
             capturadas.Add(pecaCapturada);
       }
}
```
  
 #### 4. Promoção
A jogada promoção ocorre quando um peão chega no limite adversário do tabuleiro, então o peão pode se torna: Dama, Bispo, Cavalo e uma Torre. Para esta jogada especial foi necessário criar uma interação com o usuário para ele escolher qual peça ele quer tornar, e depois promover o peão.

 ##### Exemplo de promoção de um peão para um cavalo:
```cs
case 'C':
     Peca cavalo = new Cavalo(tab, p.cor);
     tab.colocarPeca(destino, cavalo);
     pecas.Add(cavalo);
     break;
case 'c':
     Peca cavalo1 = new Cavalo(tab, p.cor);
     tab.colocarPeca(destino, cavalo1);
     pecas.Add(cavalo1);
     break;
```

### Contato: julianocolerecmoreira@gmail.com
