using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS2
{
    class Neiron
    {
        List<double> _weight = new List<double>();
        double _b;

        public Neiron(int number)
        {
            Random rand = new Random();
            for (int j = 0; j < number; j++)
            {
                _weight.Add(rand.Next(1, number));
            }
            _b = rand.NextDouble();
        }

        public double CalculateError(double result, double target)//Расчет ошибки
        {
            double error = (Math.Pow(target - result, 2.0)) / 2.0;
            return error;
        }
        double SumWeight(List<double> x)//Сумма произведение весов на x
        {
            double sum = 0;
            for(int i=0;i<_weight.Count;i++)
            {
                sum += _weight[i]*x[i] + _b;
            }
            return sum;
        }
        public double CalculateLinnerFunction(List<double> x)//Расчет линейной функции
        {
            return (SumWeight(x));
        }

        public double DiffLinner(double result)
        {
            return 1;
        }

        public double LearningLinner(List<double> enteries, double target, double learningRate)//Обучение нейрона
        {
            double result = CalculateLinnerFunction(enteries);
            for (int i = 0; i < enteries.Count; i++)
            {
                _weight[i] += learningRate * (target - result) * DiffLinner(result) * enteries[i];
            }
            _b += learningRate * (target - result) * DiffLinner(result);
            return result;
        }

    }
}
