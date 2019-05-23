using System;
using static System.Console;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;

class VetorPalavra
  {
    int tamanhoMaximo;  // tamanho físico do vetor dados
    int qtosDados;      // tamanho lógico do vetor dados
    string[] dados;

    public VetorPalavra(int tamanhoDesejado)
    {
      dados = new string[tamanhoDesejado];
      qtosDados = 0;
      tamanhoMaximo = tamanhoDesejado;
    }
    public void LerDados(string nomeArq)   // ler de um arquivo texto
    { 
      var arq = new StreamReader(nomeArq);
      qtosDados = 0;  // esvaziamos o vetor
      while (!arq.EndOfStream)
      {
        string dadoLido = arq.ReadLine();
        InserirAposFim(dadoLido);
      }
      arq.Close();
    }

    public string[] LerPalavra()
    {
        int indice = 0;
        VetorPalavra palavra = new VetorPalavra(100);
        while(indice < qtosDados)
        {
            string dadoLido = dados[indice].Substring(0, 15);
            if (dadoLido != "")
                dadoLido = dadoLido.Trim();
            palavra.dados[indice] = dadoLido;
            indice++;
        }
        return palavra.dados;
    }

    public string[] LerDica()
    {
        int indice = 0;
        VetorPalavra dicas = new VetorPalavra(100);
        while (indice < qtosDados)
        {
            string dadoLido = dados[indice].Substring(16, 100);
            if (dadoLido != "")
                dadoLido = dadoLido.Trim();
            dicas.dados[indice] = dadoLido;
            indice++;
        }
        return dicas.dados;
    }
    public void InserirAposFim(string valorAInserir)
    {
      if (qtosDados >= tamanhoMaximo)
        ExpandirVetor();

      dados[qtosDados] = valorAInserir;
      qtosDados++;
    }
    private void ExpandirVetor()
    {
      tamanhoMaximo += 10;
      string[] vetorMaior = new string[tamanhoMaximo];
      for (int indice = 0; indice < qtosDados; indice++)
        vetorMaior[indice] = dados[indice];

      dados = vetorMaior;
    }

    private void Excluir(int posicaoAExcluir)
    {
      qtosDados--;
      for (int indice = posicaoAExcluir; indice < qtosDados; indice++)
        dados[indice] = dados[indice + 1];

      // pensar em como diminuir o tamanho físico do vetor, para economizar
    }
    public void LerDados()   // ler do teclado, segunda assinatura --> sobrecarga
    {
      Write("Quantos valores serão digitados?");
      qtosDados = int.Parse(ReadLine()); // informa quantos elementos lidos
      dados = new string[qtosDados]; // instancia com tamanho correto
      for (int indice = 0; indice < qtosDados; indice++)
      {
        Write($"Digite o {indice + 1}º valor:");
        dados[indice] = ReadLine();
      }
    }

    public void Listar()
    {
      for (int indice = 0; indice < qtosDados; indice++)
          Write($"{dados[indice],5} ");
    }
    public void Listar(ListBox lista)
    {
      lista.Items.Clear();
      for (int indice = 0; indice < qtosDados; indice++)
          lista.Items.Add(dados[indice]);
    }
    public void Listar(ComboBox lista)
    {
      lista.Items.Clear();
      for (int indice = 0; indice < qtosDados; indice++)
        lista.Items.Add(dados[indice]);
    }
    public void Listar(TextBox lista)
    {
      lista.Multiline = true;
      lista.ScrollBars = ScrollBars.Both;
      lista.Clear();
      for (int indice = 0; indice < qtosDados; indice++)
        lista.AppendText(dados[indice]+Environment.NewLine);
    }

    public void GravarDados(string nomeArquivo)
    {
      var arquivo = new StreamWriter(nomeArquivo);        // abre arquivo para escrita
      for (int indice = 0; indice < qtosDados; indice++)  // percorre elementos do vetor
          arquivo.WriteLine($"{dados[indice], 5}");       // grava cada elemento
      arquivo.Close();
    }
    public override string ToString()  // retorna lista de valores separados por 
    {                                  // espaço
      return ToString(" ");  
    }

    public string ToString(string separador) // retorna lista de valores separados 
    {                                        // por separador
      string resultado = "";
      for (int indice = 0; indice < qtosDados; indice++)
        resultado += dados[indice] + separador;
      return resultado;
    }
  }
