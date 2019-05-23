using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ApProjetoII
{
    public partial class Forca : Form
    {
        bool comecou = false;
        int repeticoes = 60,
            indice = 0,
            NumeroSorteado,
            quantosErros = 0,
            quantosPontos = 0,
            quantosJogadores = 0,
            ultimoIndice = 0;

        string palavraSorteada,      //Instancia de algumas variaveis para o uso no programa
               DicaSorteada;
        Palavra palavra = new Palavra();
        String[] Nome = new String[1000];
        int[] pontuacao = new int[1000];
        List<Button> letras = new List<Button>();
        PictureBox[] Homem = new PictureBox[13];
        Jogador[] vetorJogadores = new Jogador[20]; //vetor de objetos da classe jogador

        public Forca()
        {
            InitializeComponent();
        }

        private void Forca_Load(object sender, EventArgs e)
        {
            if (dlgAbrir.ShowDialog() == DialogResult.OK)//Abre o OpenFileDialog para a escolha do arquivo com as palavras
                ProcessarDados(); //Processa os dados do arquivo

            Homem[0] = pbHomem;
            Homem[1] = pbHomem2;
            Homem[2] = pbCamiseta;
            Homem[3] = pbBracoEsquerdo;
            Homem[4] = pbBracodireito;
            Homem[5] = pbCalca;       //Adicionando as picture box no vetor de Picture Box para mostrar eles de uma forma mais fácil
            Homem[6] = pbPernaDireita;
            Homem[7] = pbPernaEsquerda;
            Homem[8] = pbHomemMorto;
            Homem[9] = pbFantasma;
            Homem[10] = pbBandeira1;
            Homem[11] = pbBandeira2;
            Homem[12] = pbMaoDireitaComBandeiras;

            letras.Add(btnA);
            letras.Add(btnB);
            letras.Add(btnC);
            letras.Add(btnD);
            letras.Add(btnE);
            letras.Add(btnF);
            letras.Add(btnG);
            letras.Add(btnH);
            letras.Add(btnI);
            letras.Add(btnJ);
            letras.Add(btnK);
            letras.Add(btnL);
            letras.Add(btnM);
            letras.Add(btnN);
            letras.Add(btnO);
            letras.Add(btnP);
            letras.Add(btnQ);
            letras.Add(btnR);
            letras.Add(btnS);
            letras.Add(btnT);
            letras.Add(btnU);  //Adicionando os botões na list de botões para mostrar eles de uma forma mais fácil
            letras.Add(btnV);
            letras.Add(btnW);
            letras.Add(btnX);
            letras.Add(btnY);
            letras.Add(btnZ);
            letras.Add(btnAgudo);
            letras.Add(btnCdilha);
            letras.Add(btnChapeu);
            letras.Add(btnChapeuE);
            letras.Add(btnChapeuO);
            letras.Add(btnEgudo);
            letras.Add(btnEspaco);
            letras.Add(btnHifen);
            letras.Add(btnIgudo);
            letras.Add(btnOgudo);
            letras.Add(btnTiO);
            letras.Add(btnTiu);
            letras.Add(btnUgudo);
        }

        private void tmrFantasma_Tick_1(object sender, EventArgs e)
        {
            pbFantasma.Location = new Point(pbFantasma.Location.X, pbFantasma.Location.Y - 5);
            //Faz o fantasma subir quando você perder no jogo
        }

        private void tier1_Tick(object sender, EventArgs e)
        {
            if (tmrTempo.Enabled) // Se o tempo estiver ativo
            {
                if (repeticoes == 0) //Ve se o tempo acabou
                {
                    tmrTempo.Stop();
                    tmrTempo.Enabled = false; //Para o tempo

                    for (int indice = 0; indice < Homem.Length; indice++)
                        if (Homem[indice].Name != "pbBandeira1" && Homem[indice].Name != "pbBandeira2" && Homem[indice].Name != "pbMaoDireitaComBandeiras")
                            Homem[indice].Visible = true; // Faz os picture box aparecer 

                    Homem[9].Visible = true;
                    tmrFantasma.Enabled = true;
                    tmrFantasma.Start(); // Ativa o timer do fantasma , fazendo ele voar

                    if (MessageBox.Show("Você perdeu !! - A palavra era: " + Environment.NewLine + "Você quer continuar jogando ? ", "Acabou o tempo",
                       MessageBoxButtons.YesNo,
                           MessageBoxIcon.Exclamation) == DialogResult.No) //Mensagem que você perdeu o jogo
                        Close();//Fecha o jogo
                    else
                    {
                        tmrFantasma.Stop();//Para o timer do Fantasma
                        tmrFantasma.Enabled = false;
                        tbForca1.Enabled = false;
                        Recomecar();//Recomeça o jogo
                    }
                }
                else
                {
                    if (tmrTempo.Enabled)
                    {
                        lbTempo.Text = ($"Tempo restante : {repeticoes}s");//Escreve no formulario o tempo restante
                        repeticoes--;//E diminue um segundo
                    }
                }
            }
        }
        int letrasQueJaForam;
        private void letras_Click(object sender, EventArgs e)
        {
            if (comecou)
            {
                bool FoiEscrito = false;
                bool continuar = false;

                ultimoIndice = palavraSorteada.Length;

                Button Letra = (Button)sender;
                // Faz com que o texto do botão clicado seja passado para essa instancia (Letra)

                Letra.Enabled = false; //Deixa ela com enabled false para essa letra não poder ser clicada mais   

                for (indice = 0; indice < palavraSorteada.Length; indice++)
                {
                    if (Convert.ToString(palavraSorteada[indice]) == Letra.Text || Convert.ToString(palavraSorteada[indice]).ToUpper() == Letra.Text)
                    // Ve se a letra clicada existe em alguma posição da palavra
                    {
                        quantosPontos++;
                        letrasQueJaForam++;
                        gvLetras.Rows[0].Cells[indice].Value = Letra.Text; //Escreve a letra clicada na posição no GridView
                        lbPontos.Text = $"Pontos : {quantosPontos}";
                        FoiEscrito = true;
                    }
                }
                if (!FoiEscrito) //Ve se a letra clicada não foi escrita
                {
                    quantosErros++;
                    quantosPontos--;
                    lbPontos.Text = ($"Pontos : {quantosPontos}");
                    lbErro.Text = ($"Erros(8) : {quantosErros}");
                }
                if (quantosErros == 1) //Ve se tem erros, para a ativação das picture box
                {
                    Homem[quantosErros - 1].Visible = true;
                    Homem[quantosErros].Visible = true;
                }
                if (quantosErros == 2)
                    Homem[quantosErros].Visible = true;
                if (quantosErros == 3)
                    Homem[quantosErros].Visible = true;
                if (quantosErros == 4)
                    Homem[quantosErros].Visible = true;
                if (quantosErros == 5)
                    Homem[quantosErros].Visible = true;
                if (quantosErros == 6)
                    Homem[quantosErros].Visible = true;
                if (quantosErros == 7)
                    Homem[quantosErros].Visible = true;
                if (quantosErros == 8)
                {
                    tmrTempo.Stop();
                    tmrTempo.Enabled = false;
                    Homem[quantosErros].Visible = true;
                    Homem[9].Visible = true;
                    tmrFantasma.Enabled = true;
                    tmrFantasma.Start();

                    if (MessageBox.Show($"Você perdeu !! - A palavra era: {palavraSorteada}" + Environment.NewLine + "Você quer continuar jogando ? ", "Acabou o tempo",
                       MessageBoxButtons.YesNo,
                           MessageBoxIcon.Exclamation) == DialogResult.No)
                    {
                        SalvarNoArquivo();
                        Close();
                    }

                    else
                    {
                        SalvarNoArquivo();
                        tmrFantasma.Stop();
                        comecou = false;
                        tmrFantasma.Enabled = false;
                        tbForca.Enabled = false;
                        Recomecar();
                    }
                }
                
                if (letrasQueJaForam == palavraSorteada.Length)
                {
                    tmrTempo.Enabled = false; //Para o tempo 
                    tmrTempo.Stop();

                    for (int indice = 0; indice < Homem.Length; indice++)
                        if (Homem[indice].Name != "pbFantasma" && Homem[indice].Name != "pbHomemMorto" && Homem[indice].Name != "pbBracodireito")
                            Homem[indice].Visible = true; //Ativa todas as picture box do homem, exceto algumas               

                    if (MessageBox.Show("VOCÊ VENCEU !!! PARABÉNS !!!" + Environment.NewLine + "Você quer continuar jogando ? ", "MUITO BEM",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information) == DialogResult.No) //Mensagem que você ganhou, e pergunta se você quer continuar a jogar
                    {
                        SalvarNoArquivo();
                        Close(); //Para o funcionamento do programa
                    }
                    else
                    {
                        SalvarNoArquivo();
                        tbForca.Enabled = false;
                        comecou = false;
                        Recomecar();//Recomeça o jogo
                    }
                }
            }
            else
            {
                MessageBox.Show("Ainda não começou");   //Para não dar erro caso o usuário clique em algum botão antes de iniciar a forca
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text == "") // Ve se tem algo escrito no text box do nome
            {
                MessageBox.Show("O seu nome não foi digitado", "Erro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                txtNome.Focus();
            }
            else // Se não começa o jogo
            {
                comecou = true;
                SortearPalavra(); //Sorteia a palavra
                gvLetras.Rows.Add();
                gvLetras.ColumnCount = palavraSorteada.Length;
                Recomecar();
                if (chkDica.Checked)
                {
                    chkDica.Enabled = false;
                    lbDica.Text = ($"Dica : {DicaSorteada}"); // Escreve a dica
                    tmrTempo.Enabled = true; //Começa o tempo
                    tmrTempo.Start();
                    tbForca.Enabled = true;
                    btnIniciar.Enabled = false;
                    chkDica.Enabled = true;
                    txtNome.Visible = false;
                    lbNome.Visible = false;
                }               
                else
                {                   
                    tbForca.Enabled = true;
                    btnIniciar.Enabled = false;
                    chkDica.Enabled = true;
                    txtNome.Visible = false;
                    lbNome.Visible = false;
                }
            }
        }

        private void ProcessarDados()
        {
            StreamReader arquivo = new StreamReader(dlgAbrir.FileName, Encoding.Default);
            //StreamReader para a leitura do arquivo, com Encoding.Default para aparecer acentos

            while (!arquivo.EndOfStream)//Lê o arquivo e processa com ajuda da classe Palavra
                palavra.LerUmRegistro(arquivo);
            arquivo.Close();
        }

        private void Recomecar()
        {
            for (int indice = 0; indice < palavraSorteada.Length; indice++)
                gvLetras.Rows[0].Cells[indice].Value = ""; //Apaga todos os caracteres do Grid View

            foreach (Button letra in letras)
                letra.Enabled = true;//Deixa todos os botões do jogo ativo

            foreach (PictureBox homem in Homem)
                homem.Visible = false; //Deixa todos os picture box do homem invisivel

            btnIniciar.Enabled = true;
            gvLetras.Enabled = false;
            tbForca.Enabled = true;
            txtNome.Visible = true;

            quantosErros = 0;
            quantosPontos = 0;   //Reseta os pontos, erros e o tempo
            repeticoes = 60;  

            lbDica.Text = "Dica : ";
            lbTempo.Text = "Tempo restante : s";
            lbPontos.Text = "Pontos : ";        //Reinicia as labels
            lbErro.Text = "Erros(8) : " +
                "";
        }

        bool NomeDoJogadorFoiEscrito (string nome, ref int posicao)
        {
            bool existe = false;
            for (int x = 0; x < quantosJogadores && !existe; x++) // percorremos o vetor dos jogadores já registrados
            {
                if (nome == vetorJogadores[x].NomeJogador) //se o nome usado na jogada for igual a um já usado anteriormente
                {
                    existe = true; // o nome já existe
                    posicao = x; //devolvemos em qual indice do vetor de jogadores o nome foi encontrado
                }
            }
            return existe; //retorna o bool se achou ou não o nome
        }

        private void SortearPalavra()
        {
            Random novaPalavra = new Random(); //instancia da classe random para o sorteio da palavra
            NumeroSorteado = novaPalavra.Next(0, palavra.Indice - 1);
            palavraSorteada = palavra.VetorPalavra[NumeroSorteado];
            DicaSorteada = palavra.VetorDica[NumeroSorteado];
        }       
        void SalvarNoArquivo()
        {
            StreamReader arquivo = new StreamReader(@"C:/Temp/Jogadores.txt");

            for (int indice = 0; indice < quantosJogadores; indice++)
                Nome[indice] = null;

            quantosJogadores = 0;
            bool nenhumJogador = true;
            while (!arquivo.EndOfStream) //Le o arquivo, para ver se tem algum jogador, ja no arquivo de texto
            {
               
                string linha = arquivo.ReadLine();
                if (linha == "Nome      Pontuacao")
                    continue;
                nenhumJogador = false;

                if (Nome[0] == null)
                {
                    Nome[quantosJogadores] = linha.Substring(0, 10).Trim();
                    pontuacao[quantosJogadores] = int.Parse(linha.Substring(10));
                    quantosJogadores++;
                    continue;
                }

                for (int i = 0; i < quantosJogadores; i++)
                    if (Nome[i].Trim() != linha.Substring(0, 10).Trim())
                    {
                        Nome[quantosJogadores] = linha.Substring(0, 10).Trim();
                        pontuacao[quantosJogadores] = int.Parse(linha.Substring(10));
                        quantosJogadores++;
                        break;
                    }
            }
            arquivo.Close();
            if (nenhumJogador)
                quantosJogadores++;
            StreamWriter arqJogadores = new StreamWriter(@"C:/Temp/Jogadores.txt"); // como não foi pedido um local para salvar o arquivo, salvamos na temp do computador
            int posJogador = -1;

            for (int indice = 0; indice < quantosJogadores; indice++)
                if (Nome[indice] == txtNome.Text)
                {
                    pontuacao[indice] += quantosPontos;                   
                    break;
                }
            else
                {
                    if (nenhumJogador || !NomeDoJogadorFoiEscrito(Nome[indice], ref posJogador)) // caso o nome ainda não tenha sido utilizado
                    {
                        Jogador jogador = new Jogador(txtNome.Text, quantosPontos); // cria-se um objeto da classe Jogador
                        vetorJogadores[quantosJogadores] = jogador; // atribui-se o objeto ao vetor
                        quantosJogadores++; // conta-se um jogador
                        arqJogadores.WriteLine(txtNome.Text.PadRight(15, ' ') + " " + quantosPontos); // escrevemos o nome e a sua respectiva pontuação
                    }
                    else //se o nome já tiver sido utilizado
                    {
                        vetorJogadores[posJogador].PtsJogador += quantosPontos; // somamos a pontuação dessa jogada à pontuação das jogadas anterioriores, já armazenadas no vetorJogadores
                        arqJogadores.WriteLine(txtNome.Text.PadRight(15, ' ') + " " + vetorJogadores[posJogador].PtsJogador); // escrevemos o nome a as somas da pontuação das rodadas anteriores e a dessa vez.
                    }
                }           
            arqJogadores.Close();
        }
    }
}