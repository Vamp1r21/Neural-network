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
        List<HidenNeiron> _closeNeirons = new List<HidenNeiron> { new HidenNeiron(4.8), new HidenNeiron( 11.0 ), 
            new HidenNeiron( 17.3) };
        Neiron _neironExit = new Neiron(3);
        private void button1_Click(object sender, EventArgs e)//Кнопка обучений нейрона
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            List<double> points = ReadTextBox();
            List<double> newPoints = new List<double>();
            List<double> targets = new List<double>();
            List<double> error = new List<double>();
            for (int i=0;i< points.Count;i++)
            {
                targets.Add(Math.Log10(points[i]) / (Math.Sin(points[i]) + 2.0));
            }
            for (int i = 0; i < points.Count; i++)
            {
                List<double> closePoints = new List<double>(3);//Точки полученные из закрытого слоя
                foreach (var closeNeiron in _closeNeirons)
                {
                    closePoints.Add(closeNeiron.CalculateFunctiponBasis(points[i]));
                }
                newPoints.Add(_neironExit.LearningLinner(closePoints, targets[i], 0.1));
                error.Add(_neironExit.CalculateError(newPoints[i], targets[i]));
                PrintInformation(newPoints[i], error[i]);
            }
            DrawGraph(newPoints, targets, points);
            PrintError(error);
            _era++;
        }

        private void button2_Click(object sender, EventArgs e)//Пычисление значений
        {
            List<double> points = ReadTextBox();
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
                    closePoints.Add(closeNeiron.CalculateFunctiponBasis(points[i]));
                }
                newPoints.Add(_neironExit.CalculateLinnerFunction(closePoints));
            }
            DrawGraph(newPoints, targets, points);
        }

        private void DrawGraph(List<double> pointsListOfGFunction, List<double> points, List<double> pointsX)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            for (int i = 0; i < pointsListOfGFunction.Count; i++)
            {
                chart1.Series[0].Points.AddXY(pointsX[i], pointsListOfGFunction[i]);
                chart1.Series[1].Points.AddXY(pointsX[i], points[i]);
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
            listBox1.Items.Add(result);
            listBox2.Items.Add(err);
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
    }
}
