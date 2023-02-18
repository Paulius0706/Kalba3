using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramLanguage
{
    
    internal class Sampler
    {
        public int doStuff(int i)
        {
            return i;
        }
        public void doStuff1(ref int i)
        {
            i++;
            if (i < 1)
            {
                i = i + 1;

            }
            else if (i >= 1)
            {
                i = i - 1;
            }
            for (int j = 0; j < i; j++)
            {
                i = i + 1;
                i = i - 1;
            }

        }
        public void Recursion(int i)
        {
            if (i < 0) return;
            Recursion(i - 1);
        }
        public void Main()
        {
            int number1;
            int number = 10;

            doStuff(number);

            doStuff1(ref number);

            Recursion(number);

        }
    }
}
