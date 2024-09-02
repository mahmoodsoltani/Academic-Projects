using System.Collections.Generic;
using System.Linq;
using Lemmatizer;
using System;
using TextSimilarity;

namespace Summarization
{
    class SentenceWeighting
    {
        public static Sentence[] CreatVector(string document, string titleOfDoc)
        {
            if (string.IsNullOrEmpty(document))
                return null;

            Sentence title = null;
            if (!string.IsNullOrEmpty(titleOfDoc))
                title = new Sentence(titleOfDoc, true);

            var sentences = Tokenizer.AdvancedSentenceSplitter(UsefulTextInfo.RefineAndNormalizedPersianWord(document)); //.ToDictionary(w => index++, w => w));
            var preprocess = new Sentence[sentences.Count];
            var tf = new Dictionary<string, int>();

            for (int i = 0; i < sentences.Count; i++)
            {
                preprocess[i] = new Sentence(sentences[i], true) { Index = i };
                // شمارش تعداد تکرار کلمات موجود در متن
                foreach (string token in preprocess[i].Tokens)
                {
                    if (tf.ContainsKey(token))
                        tf[token]++;
                    else tf.Add(token, 1);
                }
            }
            int MaxLen = preprocess.Max(s => s.WordCount);
            // محاسبه پارامترهای (وزن) هر جمله
            for (int i = 0; i < preprocess.Length; i++)
            {
                // شباهت جمله با تیتر
                preprocess[i].Features[0] = title == null
                    ? 0
                    : GetSentenceSimilarity(title, preprocess[i]);

                //  طول جمله
                double SL = (double)preprocess[i].WordCount / MaxLen;

                float temp = (float)(-SL * Math.Log(SL) - ((1 - SL) * Math.Log(1 - SL)));
                preprocess[i].Features[1] = float.IsNaN(temp) ? 0 : temp;

                // وزن کلمه
                // باید این مقدار بر ماکزیمیم وزن کلمه بین همه جملات تقسیم شود
                foreach (var phrase in preprocess[i].Tokens)
                    preprocess[i].Features[2] += tf[phrase] / (float)preprocess[i].WordCount;

                // موقعیت جمله
                //  preprocess[i].Features[3] = (i + 1f) / preprocess.Length;
                //  preprocess[i].Features[3] = (float)-Math.Exp(-(Math.Pow((float)i / (float)preprocess.Length - 0.5, 2)) / 0.05) + 1;
                preprocess[i].Features[3] = 1f / ((float)i + 1f);
                // preprocess[i].Features[3] =(float) (sentences .Count  + Math.Pow(i + 1, 2)) / ((i + 1) * sentences.Count);
                // تشابه جمله به جمله ==> وجود گیومه و سایر علائم نقل قول، عبارات مهم و پیشوند مربوط به نام اشخاص
                preprocess[i].Features[4] = ResourceManagement.GetNumberOfHumanPrefix(preprocess[i].Words)
                                            + ResourceManagement.GetNumberOfImportantPhrase(preprocess[i].Words)
                                            + ResourceManagement.GetNumberOfQuotationNotations(preprocess[i].Words)
                                            // بخش دوم جملات مرکب
                                            - (UsefulTextInfo.SentenceSplitterWords.Contains(preprocess[i].Tokens.FirstOrDefault() ?? "") ? 1 : 0)
                                            - ResourceManagement.GetNumberOfUnImportantPhrase(preprocess[i].Words);

                // اطلاعات عددی
                foreach (var phrase in preprocess[i].Words)
                    if (phrase.Tags.Any(t => t.Value.ToLower().Contains("num")))
                        preprocess[i].Features[5]++;

                // اسم حقیقی
                preprocess[i].RecognizeNamedEntity();
                foreach (var phrase in preprocess[i].Words)
                    if (phrase.Tags.Any(t => t.Key.ToLower().Contains("ner")))
                        preprocess[i].Features[6]++;
            }

            // نرمالسازی مقادیر ویژگی ها بین صفر و یک
            for (int i = 0; i < preprocess.Length; i++)
            {
                float max = preprocess.Max(s => s.Features[0]);
                float min = preprocess.Min(s => s.Features[0]);
                preprocess[i].Features[0] -= min;
                preprocess[i].Features[0] /= max - min;

                max = preprocess.Max(s => s.Features[1]);
                min = preprocess.Min(s => s.Features[1]);
                preprocess[i].Features[1] -= min;
                preprocess[i].Features[1] /= max - min;

                max = preprocess.Max(s => s.Features[2]);
                min = preprocess.Min(s => s.Features[2]);
                preprocess[i].Features[2] -= min;
                preprocess[i].Features[2] /= max - min;

                max = preprocess.Max(s => s.Features[3]);
                min = preprocess.Min(s => s.Features[3]);
                preprocess[i].Features[3] -= min;
                preprocess[i].Features[3] /= max - min;

                max = preprocess.Max(s => s.Features[4]);
                min = preprocess.Min(s => s.Features[4]);
                preprocess[i].Features[4] -= min;
                preprocess[i].Features[4] /= max - min;

                max = preprocess.Max(s => s.Features[5]);
                min = preprocess.Min(s => s.Features[5]);
                preprocess[i].Features[5] -= min;
                preprocess[i].Features[5] /= max - min;

                max = preprocess.Max(s => s.Features[6]);
                min = preprocess.Min(s => s.Features[6]);
                preprocess[i].Features[6] -= min;
                preprocess[i].Features[6] /= max - min;


            }

            return preprocess;
            /*var sw1 = new StreamWriter("result_sentence.txt");
            var sw2 = new StreamWriter("result_weights.txt");
            // نرمالسازی مقادیر ویژگی ها بین صفر و یک
            for (int i = 0; i < preprocess.Length; i++)
            {
                preprocess[i].Features[1] /= preprocess.Max(s => s.Features[1]);
                preprocess[i].Features[2] /= preprocess.Max(s => s.Features[2]);
                preprocess[i].Features[4] /= preprocess.Max(s => s.Features[4]);
                preprocess[i].Features[5] /= preprocess.Max(s => s.Features[5]);
                preprocess[i].Features[6] /= preprocess.Max(s => s.Features[6]);

                sw1.WriteLine(preprocess[i].Text.Trim());
                sw2.WriteLine("{0:f3}\t{1:f3}\t{2:f3}\t{3:f3}\t{4:f3}\t{5:f3}\t{6:f3}", preprocess[i].Features[0],
                    preprocess[i].Features[1], preprocess[i].Features[2], preprocess[i].Features[3],
                    preprocess[i].Features[4], preprocess[i].Features[5], preprocess[i].Features[6]);
            }
            sw1.Close();
            sw2.Close();*/
        }

        public static float GetSentenceSimilarity(Sentence sentence1, Sentence sentence2)
        {
            return SentenceSimilarity.GetScore(sentence1.Tokens.ToArray(), sentence2.Tokens.ToArray(),
                        WordComparisonOptions.HammingDistance, true);
        }
    }
}
