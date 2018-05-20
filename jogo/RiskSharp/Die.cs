using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskSharp
{
    public class Die
    {
        private int result = 0;
        public int Result
        {
            get { return this.result; }
        }

        public void Roll()
        {
            Random rnd = new Random();
            this.result = rnd.Next(1, 7);
        }
    }
}
