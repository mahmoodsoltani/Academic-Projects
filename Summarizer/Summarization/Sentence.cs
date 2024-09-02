using System.Collections.Generic;
using System.Linq;
using Lemmatizer;
using posTagger;

namespace Summarization
{
    class Sentence
    {
        private const int NumberOfPrarm = 7;
        public string Text;
        public float[] Features;
        public List<Phrase> Words;
        public int Index;
        public float Importance { get; set; }

        public Sentence(string sentence, bool removeStopWords = false)
        {
            Text = sentence;
            Features = new float[NumberOfPrarm];
            Features.Initialize();
            Words = PosTaggerAlgorithms.Run(sentence, PosTaggerAlgorithmList.ViterbiBrill, LevelOfPOS.Level1);

            if (removeStopWords)
                RemoveStopWord();
        }

        public void RecognizeNamedEntity()
        {
            Words = NER.NER.NameEntityTagger(Words);
        }

        public void RemoveStopWord()
        {
            Words = ResourceManagement.RemoveStopWords(Words).ToList();
        }

        public IEnumerable<string> Tokens
        {
            get
            {
                return Words.Select(w => w.RootWords.Count == 0 ? w.Word : w.RootWords[0]);
            }
        }

        public int WordCount
        {
            get { return Words.Count; }
        }
    }
}
