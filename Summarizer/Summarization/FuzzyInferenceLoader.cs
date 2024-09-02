using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MyLib.Fuzzy;

namespace Summarization
{
    class FuzzyInferenceLoader
    {
        Dictionary<string, LinguisticVariable> _variables;
        private InferenceSystem fis;

        public FuzzyInferenceLoader(string fisFilePath)
        {
            LoadFis(fisFilePath);
        }

        public Dictionary<string, LinguisticVariable> Variables
        {
            get { return _variables; }
        }

        public InferenceSystem FIS
        {
            get { return fis; }
        }

        public LinguisticVariable GetVariable(string name)
        {
            return Variables.FirstOrDefault(v => v.Value.Name.Equals(name)).Value;
        }

        /// <summary>
        /// Run one epoch of the Fuzzy Inference System 
        /// </summary>
        /// <param name="inputValues">List of input values</param>
        /// <returns></returns>
        public Dictionary<string, float> DoInference(List<float> inputValues)
        {
            int index = 0;
            Dictionary<string, float> inputValuesDic = 
                (from v in _variables where v.Key.ToLower().StartsWith("input") select v.Value.Name).
                    ToDictionary(v => v, v => inputValues[index++]);

            return DoInference(inputValuesDic);
        }

        /// <summary>
        /// Run one epoch of the Fuzzy Inference System 
        /// </summary>
        /// <param name="inputValues">List of input values</param>
        /// <returns></returns>
        public Dictionary<string, float> DoInference(float[] inputValues)
        {
            int index = 0;
            Dictionary<string, float> inputValuesDic =
                (from v in _variables where v.Key.ToLower().StartsWith("input") select v.Value.Name).
                    ToDictionary(v => v, v => inputValues[index++]);

            return DoInference(inputValuesDic);
        }


        /// <summary>
        /// Run one epoch of the Fuzzy Inference System 
        /// </summary>
        /// <param name="inputValues">List of input values [Input Variable Name, Value] </param>
        /// <returns></returns>
        public Dictionary<string, float> DoInference(Dictionary<string, float> inputValues)
        {
            // Setting inputs
            foreach (KeyValuePair<string, float> pair in inputValues)
                fis.SetInput(pair.Key, pair.Value);
            
            // Setting outputs
            /* var outputs = new Dictionary<string, float>();
            try
            {
                foreach (KeyValuePair<string, LinguisticVariable> pair in _variables)
                    if (pair.Key.ToLower().StartsWith("output"))
                        outputs.Add(pair.Value.Name, fis.Evaluate(pair.Value.Name));
            }
            catch (Exception)
            {
            }*/

            Dictionary<string, float> outputs =
                (from v in _variables where v.Key.ToLower().StartsWith("output") select v.Value.Name).
                    ToDictionary(v => v, v => fis.Evaluate(v));
            return outputs;
        }

        private void LoadFis(string fisFilePath)
        {
            _variables = new Dictionary<string, LinguisticVariable>();
            
            string[] parts = File.ReadAllText(fisFilePath).Split(new[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in parts)
                if (part.StartsWith("[Input") || part.StartsWith("[Output"))
                {
                    string alias = part.Substring(0, part.IndexOf('\n')).Trim('[', ']');
                    LinguisticVariable variable = ParseVariable(part);

                    _variables.Add(alias, variable);
                }
                else if (part.StartsWith("[Rules]"))
                {
                    List<LinguisticVariable> variables = _variables.Values.ToList();
                    
                    // The database
                    Database fuzzyDB = new Database();
                    foreach (LinguisticVariable variable in variables)
                        fuzzyDB.AddVariable(variable);
                    
                    // Creating the inference system
                    fis = new InferenceSystem(fuzzyDB, new CentroidDefuzzifier(1000));

                    string[] lines = part.Split('\n');
                    for (int i = 1; i < lines.Length; i++)
                        if (!string.IsNullOrEmpty(lines[i]))
                        {
                            string rule = ParseRule(lines[i], variables);
                            if (!string.IsNullOrEmpty(rule))
                            {
                                // Going Straight
                                fis.NewRule("Rule " + i, rule); // "IF FrontalDistance IS Far THEN Angle IS Zero"
                            }
                        }
                }
        }

        private LinguisticVariable ParseVariable(string variableStr)
        {
            string[] lines = variableStr.Split('\n');
            string name = "";
            //int mfCount;
            LinguisticVariable variable = null;

            foreach (string line in lines)
            {
                string[] param = line.Split('=');
                if (param.Length != 2)
                    continue;

                switch (param[0].ToLower())
                {
                    case "name":
                        name = param[1].Trim('\'');
                        break;
                    case "range":
                        string[] range = param[1].Trim('[', ']', ' ').Split();
                        variable = new LinguisticVariable(name, float.Parse(range[0]), float.Parse(range[1]));
                        break;
                    case "nummfs":
                        //mfCount = int.Parse(param[1]);
                        break;
                    default:
                        if (param[0].ToLower().StartsWith("mf"))
                        {
                            string[] parts = (from p in param[1].Split(new[] { ':', ',' }, StringSplitOptions.RemoveEmptyEntries)
                                              select p.Trim('\'', '[', ']', ' ')).ToArray();

                            if (parts.Length == 3)
                            {
                                IMembershipFunction mf = null;
                                float[] values = (from v in parts[2].Trim('[', ']').Split() select float.Parse(v)).ToArray();

                                // Trapezoidal MF
                                if (parts[1].ToLower().Equals("trapmf") && values.Length == 4)
                                {
                                    mf = new TrapezoidalFunction(values[0], values[1], values[2], values[3]);
                                }
                                // Triangular MF
                                else if (parts[1].ToLower().Equals("trimf") && values.Length == 3)
                                {
                                    mf = new TrapezoidalFunction(values[0], values[1], values[2]);
                                }
                                // TODO: Guassian MF, ...
                                // http://www.bindichen.co.uk/post/AI/fuzzy-inference-membership-function.html

                                if (mf != null && variable != null)
                                    variable.AddLabel(new FuzzySet(parts[0].Trim('\''), mf));
                            }
                        }
                        break;
                }
            }
            return variable;
        }

        private string ParseRule(string ruleFis, List<LinguisticVariable> variables)
        {
            //"IF FrontalDistance IS Far THEN Angle IS Zero"
            string[] sides = ruleFis.Split(new[] {',', ':'}, StringSplitOptions.RemoveEmptyEntries);
            if (sides.Length != 3)
                return null;

            string connector = sides[2].Trim() == "1" ? " AND " : " OR ";
            
            string[] left = sides[0].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            int leftVariableCount = left.Length;
            var LHS = new StringBuilder();
            for (int i = 0; i < left.Length; i++)
            {
                int val = int.Parse(left[i]);
                if (val > 0)
                {
                    if (LHS.Length > 0)
                        LHS.Append(connector);
                    LHS.Append(variables[i].Name + " IS " + variables[i].GetLabel(val - 1).Name);
                }
            }

            string[] right = sides[1].Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            var RHS = new StringBuilder();
            for (int i = 0; i < right.Length - 1; i++)
            {
                int val = int.Parse(right[i]);
                if (val > 0)
                {
                    if (RHS.Length > 0)
                        RHS.Append(" AND ");
                    RHS.Append(variables[i + leftVariableCount].Name + " IS " + variables[i + leftVariableCount].GetLabel(val - 1).Name);
                }
            }

            return String.Format("IF {0} THEN {1}", LHS, RHS);
        }
    }
}
