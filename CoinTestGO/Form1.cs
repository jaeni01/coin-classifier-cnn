using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoinTestGOML.Model;

namespace CoinTestGO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog(); //파일 염재니
            openFileDialog1.Filter = "lmages (*.JPG; *.PNG)|*.JPG;*.PNG|" + "All files (*.*)|*.*";
            openFileDialog1.Title = "Select Photo";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox1.Text = openFileDialog1.FileName;
                    pictureBox1.Image = Image.FromFile(textBox1.Text);
                    Result(textBox1.Text);
                }
                catch
                {
                    MessageBox.Show("이미지 파일 변환 요청");
                }
            }
        }
        private void Result(string _adress)
        {
            var input = new ModelInput()
            {
                ImageSource = _adress
            };
            ModelOutput result = ConsumeModel.Predict(input);

            float ex_temp;
            for (int i = 0; i < result.Score.Length ; i++)
            {
                for (int j = 1; j < result.Score.Length ; j++)
                {
                    if (result.Score[i] < result.Score[j])
                    {
                        ex_temp = result.Score[j];
                        result.Score[j] = result.Score[i];
                        result.Score[i] = ex_temp;
                    }
                }
            }

            textBox2.Text = $"확률은 {result.Score[0] * 100:N2} % ";
            textBox3.Text = $"결과는 {result.Prediction} 입니다. ";
        }

    }
}
