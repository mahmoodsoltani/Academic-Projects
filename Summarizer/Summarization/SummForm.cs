using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Lemmatizer;
using MyLib.Fuzzy;

namespace Summarization
{
    public partial class SummForm : Form
    {
        private FuzzyInferenceLoader _fis;
        public SummForm()
        {
            InitializeComponent();
            
            /* chart1.BackColor = Color.Gainsboro;
            chart1.BorderWidth = 2;
            chart1.BorderColor = Color.Black;
            chart1.BorderDashStyle = ChartDashStyle.Solid;*/

            if(File.Exists(ResourceManagement.ResPath+"Summ.fis"))
                LoadFis(ResourceManagement.ResPath + "Summ.fis");
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            if (_fis == null)
            {
                tabControl1.SelectedIndex = 1;
                MessageBox.Show("Please, Load input FIS file.");
                btn_load_fis.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txt_main.Text))
            {
                tabControl1.SelectedIndex = 0;
                MessageBox.Show("Please, Enter input text.");
                txt_main.Focus();
                return;
            }

            tabControl1.Enabled = btn_run.Enabled = false;
            progressBar.Value = 0;
            int limit = Convert.ToInt32(nud_sentence_limit.Value);
            txt_summ.Text = Summerize(txt_main.Text, txt_title.Text, limit, chk_sort_result.Checked, chk_use_extra_feature.Checked,0.7f );
            
            tabControl1.Enabled = btn_run.Enabled = true;
            progressBar.Value = 100;
            progressText.Text = "Finished";
        }

        private void btn_run_over_corpus_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("C:\\Users\\Qiet_PC\\Desktop\\Summarization\\OneSumm.txt");
            var fileDic = ResourceManagement.GetCorpusFilePath();
            int index = 0, count = fileDic.Count;
            //btn_load_fis.Text = "Stop";
            for (float i = 0.1f; i <= 1; i += 0.1f)
            {
                if (!Directory.Exists("result" + (i.ToString())))
                    Directory.CreateDirectory("result" + (i.ToString()));
            }
            //int limit = Convert.ToInt32(nud_sentence_limit.Value);

            tabControl1.Enabled = btn_run.Enabled = false;
            foreach (KeyValuePair<string, string> pair in fileDic)
            {
                progressBar.Value = Convert.ToInt32(Math.Ceiling(index * 100.0 / count));
                progressBar.ToolTipText = progressBar.Value.ToString() + "%";
                string temp = File.ReadAllText(pair.Value);
                int limit = Tokenizer.GetWordCount(temp) * Convert.ToInt32(sr.ReadLine()) / 100;
                //for (float i = 0.1f; i <= 1; i += 0.1f)
                //{
                    string summ = Summerize(temp, File.ReadAllText(pair.Key), limit, chk_sort_result.Checked, chk_use_extra_feature.Checked,0.6f);

               //  StreamWriter sw = new StreamWriter("result"+(i.ToString()) + "\\" + Path.GetFileName(pair.Key));
                StreamWriter sw = new StreamWriter("result\\" + Path.GetFileName(pair.Key));
                sw.WriteLine(summ);
                sw.Close();
                // }
                index++;
            }
            btn_load_fis.Text = "Load Pasokh Corpus";
            tabControl1.Enabled = btn_run.Enabled = true;
            progressBar.Value = 100;
            progressText.Text = "Finished";
        }
        float CalculateSentenceImportance(float [] Feature)
        {
            float[] Linear = { 0.0443f, 0.1557f, 0.7582f, 0.941f, -0.0894f, 0.11f, -0.0757f,  0.4925f };
            float[] weightSimple = { 0.0f, 0.0f, 0.7618f, 0.9766f, 0.0f, 0.0889f, 0.0f, 0.508f };
            float[] weightSMO = { -0.0184f, 0.0286f, 0.1858f, -0.1339f, 0.0315f, 0.0257f, 0.0276f, 0.1108f };

            float Score = Linear[7];
            for (int i = 0; i < 7; i++)
                Score += (float.IsNaN(Feature[i]) || float.IsInfinity(Feature[i]) ? 0 : Feature[i]) * Linear[i];
            return Score;
        }
  
        private string Summerize(string text, string title, int limit, bool sortByPosition, bool useSentenceSimFeature,float fl)
        {
            // اضافه برای درصد فشرده سازی منطبق با خلاصه ها

            progressText.Text = "The weights vector Weights of each sentence calculate...";
            Application.DoEvents();
            var sentences = SentenceWeighting.CreatVector(text, title).ToList();

            progressText.Text = "The inference process start to claculate final importance of each sentence...";
            Application.DoEvents();
            foreach (Sentence sentence in sentences)
                //  sentence.Importance = _fis.DoInference(sentence.Features).First().Value;
                 sentence.Importance = CalculateSentenceImportance(sentence.Features );
            //sentence.Importance = (float)-Math.Exp(-(Math.Pow((((++i)/sentences .Count ) - 0.5), 2)) / 0.05) + 1;
            //  sentence.Importance = ++i/sentences .Count();
            float Max = sentences.Max(s => s.Importance);
            float min = sentences.Min(s => s.Importance);

            foreach (Sentence sentence in sentences)
            {
               sentence .Importance = (sentence.Importance-min ) / (Max - min); 
            }
            List<Sentence> orderedSentence = new List<Sentence>();
            int lengthOfSelectedSentences = 0, sentenceCount = sentences.Count;
            while (lengthOfSelectedSentences < limit && sentences.Count>0)
            {
                float[] scores = new float[sentences.Count];
                float maxScore = -10000f;
                int maxIndex = -1;
                for (int j = 0; j < sentences.Count; j++)
                {
                    if (useSentenceSimFeature && orderedSentence.Count > 0 && sentences.Count>1)
                    {
                        // Average similarity of currenct sentence with prevoius selected sentences
                        float simWithPrev =
                            orderedSentence.Max(s => SentenceWeighting.GetSentenceSimilarity(s, sentences[j]));
                        // Average distance of position of currenct sentence with prevoius selected sentences


                        scores[j] = (sentences[j].Importance*fl) - (simWithPrev )*(1-fl);
                    }
                    else scores[j] = sentences[j].Importance;

                    if (scores[j] > maxScore)
                    {
                        maxScore = scores[j];
                        maxIndex = j;
                    }
                }

                orderedSentence.Add(sentences[maxIndex]);
                lengthOfSelectedSentences += Tokenizer.GetWordCount(sentences[maxIndex].Text);
                sentences.RemoveAt(maxIndex);
            }


            //progressText.Text = "The order of sentences sort based importance and return results...";
            if (sortByPosition)
                orderedSentence = orderedSentence.OrderBy(d => d.Index).ToList();

            var result = new StringBuilder();
            foreach (Sentence sentence in orderedSentence)
                result.AppendLine(sentence.Text.Trim());

            return result.ToString();
        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Text File Format|*.txt|All File Format|*.*",
                Multiselect = false,
                CheckFileExists = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txt_main.Text = File.ReadAllText(ofd.FileName);
            }
        }
        


        #region Membership Function Tab
        private void btn_load_fis_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Fuzzy Infrence System File Format|*.fis|All File Format|*.*",
                Multiselect = false,
                CheckFileExists = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadFis(ofd.FileName);
            }
        }

        private void lst_io_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_io.SelectedItems.Count > 0)
                LoadVariable(_fis.Variables[lst_io.SelectedItems[0].SubItems[1].Text]);
            ResetMfValues();
        }

        private void txt_double_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !e.KeyChar.Equals(Convert.ToChar(Keys.Back)) &&
                !(e.KeyChar.Equals('.') && !((TextBox)sender).Text.Contains('.')))
                e.Handled = true;
        }

        private void lst_mf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_mf.SelectedItems.Count > 0)
            {
                var var = _fis.GetVariable(txt_variable_name.Text);
                if (var != null)
                    LoadMembershipFunction(var, var.GetLabel(lst_mf.SelectedItems[0].SubItems[0].Text));
            }
        }

        void ResetMfValues()
        {
            txt_mf_name.Clear();
            comboBox1.SelectedIndex = 0;
            txt_p1.Clear();
            txt_p2.Clear();
            txt_p3.Clear();
            txt_p4.Clear();
        }

        private void LoadFis(string fisFilePath)
        {
            _fis = new FuzzyInferenceLoader(fisFilePath);

            lst_io.Items.Clear();
            foreach (KeyValuePair<string, LinguisticVariable> pair in _fis.Variables)
                lst_io.Items.Add(new ListViewItem(new[] { pair.Value.Name, pair.Key }));

            txt_rules.Clear();
            foreach (Rule rule in _fis.FIS.GetRules())
                txt_rules.Text += rule + Environment.NewLine;
        }

        private void LoadVariable(LinguisticVariable variable)
        {
            lst_mf.Items.Clear();
            txt_variable_name.Text = variable.Name; //lst_io.SelectedItems[0].SubItems[0].Text;
            txt_range_low.Text = variable.Start.ToString(CultureInfo.InvariantCulture);
            txt_range_high.Text = variable.End.ToString(CultureInfo.InvariantCulture);

            DrawMembershipFuctionOnChart(variable);
            foreach (KeyValuePair<string, FuzzySet> pair in variable.GetLabels())
            {
                lst_mf.Items.Add(new ListViewItem(new[] { pair.Key }));
            }
        }

        private void LoadMembershipFunction(LinguisticVariable variable, FuzzySet mf)
        {
            txt_mf_name.Text = mf.Name;
            comboBox1.SelectedItem = mf.FunctionType;
            txt_p1.Text = mf.FunctionValues.Count > 0 ? mf.FunctionValues[0].ToString("f3", CultureInfo.InvariantCulture) : "";
            txt_p2.Text = mf.FunctionValues.Count > 1 ? mf.FunctionValues[1].ToString("f3", CultureInfo.InvariantCulture) : "";
            txt_p3.Text = mf.FunctionValues.Count > 2 ? mf.FunctionValues[2].ToString("f3", CultureInfo.InvariantCulture) : "";
            txt_p4.Text = mf.FunctionValues.Count > 3 ? mf.FunctionValues[3].ToString("f3", CultureInfo.InvariantCulture) : "";

            SelectChartMf(variable, mf.Name);
        }

        private void DrawMembershipFuctionOnChart(LinguisticVariable variable)
        {
            /*  Using MyLib.Controls.Chart :
            
            chart_mf.RangeX = new Range(variable.Start, variable.End);
            
            // Clear all data series data
            chart_mf.RemoveAllDataSeries();
            /*chart_mf.UpdateDataSeries("COLD", null);
            ...
             * chart_mf.UpdateDataSeries("HOT", null);#1#
            
            // showing the shape of the linguistic variable - the shape of its labels memberships from start to end
            int index = 0;
            foreach (KeyValuePair<string, FuzzySet> pair in variable.GetLabels())
            {
                // get membership of some points to the cool fuzzy set
                double[,] chartValues = new double[101, 2];

                int j = 0;
                for (float x = 0f; x <= 1f; x += 0.01f, j++)
                {
                    x = Convert.ToSingle(Math.Round(x, 2));
                    double y = variable.GetLabelMembership(pair.Key, x);
                    
                    chartValues[j, 0] = x;
                    chartValues[j, 1] = y;
                }
                // for correction of bug in last point of values:
                int length = chartValues.GetLength(0);
                if (chartValues[length - 1, 1].Equals(0f) && chartValues[length - 2, 1] > .95f)
                    chartValues[length - 1, 1] = 1f;

                // plot membership to a chart
                chart_mf.AddDataSeries(pair.Key, _chartColor[index++%_chartColor.Length], Chart.SeriesType.Line, 3, true);
                chart_mf.UpdateDataSeries(pair.Key, chartValues);
            }*/

            chart1.Series.Clear();

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0.00";//"{0:0,}K";
            chart1.ChartAreas[0].AxisY.Minimum = variable.Start;
            chart1.ChartAreas[0].AxisY.Maximum = variable.End + 0.05;
            chart1.ChartAreas[0].AxisY.Interval = 0.2;

            /* index = 0;
            Color[] _chartColor =
            {
                Color.Firebrick, Color.LightCoral, Color.LightBlue, Color.CornflowerBlue,
                Color.Aqua, Color.Chartreuse
            };*/
            foreach (KeyValuePair<string, FuzzySet> pair in variable.GetLabels())
            {
                var series1 = new Series
                {
                    Name = pair.Key,
                    //Color = _chartColor[index++ % _chartColor.Length],
                    IsVisibleInLegend = false,
                    IsXValueIndexed = true,
                    ChartType = SeriesChartType.Line,
                    BorderWidth = 2, 
                    ToolTip = pair.Value.Name
                };

                // get membership of some points to the cool fuzzy set
                int j = 0;
                for (float x = 0f; x <= 1f; x += 0.01f, j++)
                {
                    x = Convert.ToSingle(Math.Round(x, 2));
                    double y = variable.GetLabelMembership(pair.Key, x);
                    // for correction of bug in last point of values:
                    if (x.Equals(1f) && y.Equals(0f) && series1.Points[series1.Points.Count - 1].YValues[0] > .95f)
                        y = 1f;

                    series1.Points.AddXY(x, y);
                }

                // plot membership to a chart
                chart1.Series.Add(series1);
                chart1.Invalidate();
            }
        }

        void SelectChartMf(LinguisticVariable variable, string mfKey)
        {
            DrawMembershipFuctionOnChart(variable);

            Series mf = chart1.Series.FirstOrDefault(s => s.Name.Equals(mfKey));
            if (mf != null)
            {
                mf.Color = Color.Black;
                mf.BorderWidth = 4;
            }
        }
        #endregion

        private void SummForm_Load(object sender, EventArgs e)
        {
          
        }
    }
}