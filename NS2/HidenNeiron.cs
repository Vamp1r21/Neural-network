using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NS2
{
    class HidenNeiron
    {
        double _weight;
        public HidenNeiron(double weight)
        {
            _weight = weight;
        }

        public double CalculateFunctiponBasis(double x)
        {
            return (Math.Exp(-(Math.Pow(Math.Abs(x - _weight), 2.0) / (2.0 * (Math.Pow(1.0, 2.0))))));
        }
    }
}
