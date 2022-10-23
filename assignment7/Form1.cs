using System.Text;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics;

namespace assignment7
{
    public partial class Form1 : Form
    {
        Dictionary<int, string> questionWithAnswers = new Dictionary<int, string>()
        {
            {1, "B"},
            {2, "D"},
            {3, "A"},
            {4, "A"},
            {5, "C"},
            {6, "A"},
            {7, "B"},
            {8, "A"},
            {9, "C"},
            {10, "D"},
            {11, "B" },
            {12, "C"},
            {13, "D"},
            {14, "A"},
            {15, "D"},
            {16, "C"},
            {17, "C"},
            {18, "B"},
            {19, "D"},
            {20, "A"}
        };


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           OpenFileDialog openFileDialog = new OpenFileDialog();

           openFileDialog.ShowDialog();
           openFileDialog.Title = "Select File to check";

           if(openFileDialog.FileName != "")
            {
                String fileNameWithAPath = openFileDialog.FileName;
                textBox1.Text = fileNameWithAPath;
                using (FileStream fs = File.Open(fileNameWithAPath, FileMode.Open))
                {
                    byte[] data = new byte[fs.Length];
                    ArrayList dataElem = new ArrayList();
                    UTF8Encoding temp = new UTF8Encoding(true);

                    while(fs.Read(data, 0, data.Length) > 0)
                    {
                        dataElem.Add(temp.GetString(data));
                    }

                    String fullStrArr = (string)dataElem[0];

                    String[] answerLineArr = fullStrArr.Split("\n");

                    ArrayList extarctedValuesFromUserTextFile = new ArrayList();

                    foreach(String answerLine in answerLineArr)
                    {
                        String[] splitedLineByWords = answerLine.Split(" ");
                        extarctedValuesFromUserTextFile.Add(splitedLineByWords[1].Replace("\n", ""));
                    }
                    Dictionary<string, int> numberOfCorrectAndInccorectAnswers = checkAnswers(extarctedValuesFromUserTextFile, questionWithAnswers);
                    textBox2.Text = numberOfCorrectAndInccorectAnswers["right"].ToString();
                    textBox3.Text = getAMark(numberOfCorrectAndInccorectAnswers["right"]);
                textBox4.Text = numberOfCorrectAndInccorectAnswers["incorrect"].ToString();
                }
            }
            else
            {
                textBox1.Text = "You haven't selected a file";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxElemarr = new TextBox[] { textBox1, textBox2, textBox3, textBox4, textBox5 };

            clearTextElements(textBoxElemarr);
        }
        private Dictionary<string, int> checkAnswers(ArrayList userAnswerArr, Dictionary<int, string> rightAnswersArr)
        {
            Dictionary<string, int> questionWithAnswers = new Dictionary<string, int>() {
            { "right", 0},
            { "incorrect", 0} 
            };

            ArrayList inccorectQuestionsName = new ArrayList();

            int rightAnswer = 0;
            foreach (var pair in rightAnswersArr)
            {
                String answerElem = (string)userAnswerArr[pair.Key - 1];
                if (answerElem.ToLower().Contains(pair.Value.ToLower()))
                {
                    questionWithAnswers["right"] += 1;
                }
                else
                {
                    inccorectQuestionsName.Add(pair.Key);
                }
            }
            questionWithAnswers["incorrect"] = rightAnswersArr.Count - questionWithAnswers["right"];
            List<int> inccorectQuestionsNameList = inccorectQuestionsName.OfType<int>().ToList();
            textBox5.Text = String.Join(", ", inccorectQuestionsNameList);
            return questionWithAnswers;
        }

        private String getAMark(int numberOfCorrectAnswers)
        {
            if (numberOfCorrectAnswers >= 15 && numberOfCorrectAnswers <= 20) {
                return "pass";
                    }
            else
            {
                return "not pass";
            }
        }

        private void clearTextElements(TextBox[] textBoxElemArr)
        {
            foreach(TextBox textBoxElem in textBoxElemArr)
            {
                textBoxElem.Text = "";
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
