using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.Defs
{
    public class Random
    {
        private static readonly System.Random getrandom = new System.Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(max);
            }
        }
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }
    }
}
