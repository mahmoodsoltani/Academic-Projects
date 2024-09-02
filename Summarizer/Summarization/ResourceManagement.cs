using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Lemmatizer;

namespace Summarization
{
    static class ResourceManagement
    {
        private static readonly HashSet<string> stopWords;
        private static readonly HashSet<string> important;
        private static readonly HashSet<string> unimportant;
        private static readonly HashSet<string> humanPrefix;
        private static readonly HashSet<string> quotationNotations;
        public static string ResPath;

        static ResourceManagement()
        {
            ResPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\res\\";
            stopWords = new HashSet<string>(File.ReadAllLines(ResPath + "StopWords.txt"));
            humanPrefix = new HashSet<string>(File.ReadAllLines(ResPath + "HumanPrefix.txt"));
            important = new HashSet<string>(File.ReadAllLines(ResPath + "Important.txt"));
            unimportant = new HashSet<string>(File.ReadAllLines(ResPath + "Unimportant.txt"));
            quotationNotations = new HashSet<string>(File.ReadAllLines(ResPath + "QuotationNotations.txt"));
        }

        public static Dictionary<string, string> GetCorpusFilePath()
        {
            string source = ResPath + "pasokhLesser\\Source\\DUC\\";
            string title = ResPath + "pasokhLesser\\Source\\Title\\";
            var filePath = new Dictionary<string, string>();
            
            foreach (string file in Directory.GetFiles(title))
            {
                string fileName = Path.GetFileName(file);
                filePath.Add(file, source+fileName);
            }

            return filePath;
        }

        public static IEnumerable<Phrase> RemoveStopWords(IEnumerable<Phrase> mainWords)
        {
            return mainWords == null || stopWords == null
                ? mainWords
                : mainWords.Where(w => (quotationNotations.Contains(w.Word) || !w.IsPunc) && !stopWords.Contains(w.Word));
        }

        public static int GetNumberOfImportantPhrase(List<Phrase> mainWords)
        {
            return GetCountOfWordListInText(mainWords, important);
        }

        public static int GetNumberOfUnImportantPhrase(List<Phrase> mainWords)
        {
            return GetCountOfWordListInText(mainWords, unimportant);
        }

        public static int GetNumberOfHumanPrefix(List<Phrase> mainWords)
        {
            return GetCountOfWordListInText(mainWords, humanPrefix);
        }

        public static int GetNumberOfQuotationNotations(List<Phrase> mainWords)
        {
            return GetCountOfWordListInText(mainWords, quotationNotations);
        }

        private static int GetCountOfWordListInText(List<Phrase> mainText, HashSet<string> targetList)
        {
            return mainText == null || targetList == null
                ? 0
                : mainText.Where((t, i) =>
                    targetList.Contains(t.Word) || (t.RootWords.Count > 0 && targetList.Contains(t.RootWords[0])) ||
                    (i < mainText.Count - 1 && targetList.Contains(t.Word + " " + mainText[i + 1].Word))).Count();

            /*int cnt = 0;
            for (int i = 0; i < mainText.Count; i++)
                if (targetList.Contains(mainText[i].Word) ||
                    (mainText[i].RootWords.Count > 0 && targetList.Contains(mainText[i].RootWords[0])) ||
                    (i < mainText.Count - 1 && targetList.Contains(mainText[i].Word + " " + mainText[i + 1].Word)))
                {
                    cnt++;
                }

            return cnt;*/
        }

        /*public static IEnumerable<string> RemoveStopWords(IEnumerable<string> mainWords)
        {
            return mainWords == null || stopWords == null
                ? mainWords
                : mainWords.Where(w => (w.Equals("\"") || !UsefulTextInfo.IsSeparator(w)) && !stopWords.Contains(w));
        }*/
    }
}
