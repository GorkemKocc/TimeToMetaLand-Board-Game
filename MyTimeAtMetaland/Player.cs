using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTimeAtMetaland
{
    internal class Player
    {
        public int food, money, item;
        public string workplace = "none";
        public int shift, salary;
        public bool isInShift = false;
        public int property;
        public Player()
        {
            // 10 days need
            money = 60;
            food = 60;
            item = 60;
            property = 0;
        }
    }
}
