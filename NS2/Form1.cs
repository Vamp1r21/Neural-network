using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NS2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int _era = 0;
        List<HidenNeiron> _closeNeirons = new List<HidenNeiron> { new HidenNeiron(4),
            new HidenNeiron( 5), new HidenNeiron( 7.5 ),
            new HidenNeiron( 8 ), new HidenNeiron( 11 ),
            new HidenNeiron( 12 ),new HidenNeiron( 14 ), new HidenNeiron( 15 ),
            new HidenNeiron( 16 ), new HidenNeiron( 17)};
        Neiron _neironExit = new Neiron(10);
        private void button1_Click(object sender, EventArgs e)//Кнопка обучений нейрона
        {
            Learning();
        }

        private void Learning()
        {
            ResultsChart.Series[2].Points.Clear();
            PointsLB.Items.Clear();
            ErrorsLB.Items.Clear();
            List<double> points = new List<double>();
            for(double i=3; i<=15; i+=0.1)
            {
                points.Add(i);
            }
            //List<double> points = ReadTextBox();
            List<double> newPoints = new List<double>();
            List<double> targets = new List<double>();
            List<double> error = new List<double>();
            for (int i = 0; i < points.Count; i++)
            {
                targets.Add(Math.Log10(points[i]) / (Math.Sin(points[i]) + 2.0));
            }
            for (int i = 0; i < points.Count; i++)
            {
                List<double> closePoints = new List<double>(3);//Точки полученные из закрытого слоя
                foreach (var closeNeiron in _closeNeirons)
                {
                    closePoints.Add(closeNeiron.CalculateFunctiponBasis(points[i], double.Parse(textBox2.Text)));
                }
                newPoints.Add(_neironExit.LearningLinner(closePoints, targets[i], 0.1));
                error.Add(_neironExit.CalculateError(newPoints[i], targets[i]));
                PrintInformation(newPoints[i], error[i]);
            }
            DrawGraph(newPoints, targets, points,0);
            PrintError(error);
            _era++;
        }
        private void button2_Click(object sender, EventArgs e)//Вычисление значений
        {
            //List<double> points = ReadTextBox();
            List<double> points = new List<double>();
            for (double i = 3; i <= 15; i+=0.01)
            {
                points.Add(i);
            }
            List<double> newPoints = new List<double>();
            List<double> targets = new List<double>();
            for (int i = 0; i < points.Count; i++)
            {
                targets.Add(Math.Log10(points[i]) / (Math.Sin(points[i]) + 2.0));
            }
            for (int i = 0; i < points.Count; i++)
            {
                List<double> closePoints = new List<double>(3);//Точки полученные из закрытого слоя
                foreach (var closeNeiron in _closeNeirons)
                {
                    closePoints.Add(closeNeiron.CalculateFunctiponBasis(points[i], double.Parse(textBox2.Text)));
                }
                newPoints.Add(_neironExit.CalculateLinnerFunction(closePoints));
            }
            DrawGraph(newPoints, targets, points,2);
        }

        private void DrawGraph(List<double> pointsListOfGFunction, List<double> points, List<double> pointsX, int number)
        {
            ResultsChart.Series[1].Points.Clear();
            ResultsChart.Series[number].Points.Clear();
            for (int i = 0; i < pointsListOfGFunction.Count; i++)
            {
                ResultsChart.Series[number].Points.AddXY(pointsX[i], pointsListOfGFunction[i]);
                ResultsChart.Series[1].Points.AddXY(pointsX[i], points[i]);
            }
        }
        private List<double> ReadTextBox()
        {
            string[] decemitional_0 = { " ", "  ", "\t" };
            string[] fileElement = textBox1.Text.Split(decemitional_0,
                StringSplitOptions.RemoveEmptyEntries);
            List<double> points = new List<double>();
            for (int i = 0; i < fileElement.Length; i++)
            {
                points.Add(double.Parse(fileElement[i]));
            }
            return points;
        }

        private void PrintInformation(double result, double err)
        {
            PointsLB.Items.Add(result);
            ErrorsLB.Items.Add(err);
        }
        private void PrintError(List<double> error)
        {
            double sumError = 0;
            chart2.Series[0].Points.Clear();
            for (int i = 0; i < error.Count; i++)
            {
                chart2.Series[0].Points.AddXY(i, error[i]);
                sumError += error[i];
            }
            chart3.Series[0].Points.AddXY(_era, sumError);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int sum = 0;
            for(int i=0;i<numericUpDown1.Value;i++)
            {
                Learning();
                sum++;
            }
            int old = int.Parse(label1.Text);
            old += sum;
            label1.Text = old.ToString();
        }
    }
}
