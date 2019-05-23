using System;
using System.IO;

namespace ApProjetoII
{
    class Palavra
    {
        private string[] vetorPalavra = new string[100];
        private string[] vetorDica = new string[100];

        int indice = 0;

        const int tamanhoDaPalavra = 15,
                  inicioPalavra = 0,
                  tamanhoDaDica = 200,
                  inicioDaDica = inicioPalavra + tamanhoDaPalavra;

        public string[] VetorPalavra
        {
            get => vetorPalavra;
            set => vetorPalavra = value;
        }
        public string[] VetorDica
        {
            get => vetorDica;
            set => vetorDica = value;
        }
        public int Indice
        {
            get => indice;
            set => indice = value;
        }

        public void LerUmRegistro(StreamReader arquivo)
        {
            if (!arquivo.EndOfStream)
            {
                string linha = arquivo.ReadLine();
                vetorPalavra[indice] = linha.Substring(inicioPalavra, tamanhoDaPalavra).Trim();
                vetorDica[indice] = linha.Substring(inicioDaDica).Trim();
                indice++;
            }
        }
    }
}
