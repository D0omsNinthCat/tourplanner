using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tourplanner.BL
{
    public static class Factory
    {
        private static IFactory fact;
        public static IFactory get_Fact()
        {
            if (fact == null)
            {
                fact = new Search_Implementation();
            }
            return fact;
        }
    }
}
