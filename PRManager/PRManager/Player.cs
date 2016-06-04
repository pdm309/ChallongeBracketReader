using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRManager
{
    public class Player
    {
        public int bounty;
        public string name;
        public string tag;
        public int rank;
        public string character;
        
        public Player()
        {
            bounty = 0;
            name = null;
            tag = null;
            rank = 0;
            character = null;
        }
        public Player(int bounty, string name, string tag, int rank, string character)
        {
            this.bounty = bounty;
            this.name = name;
            this.tag = tag;
            this.rank = rank;
            this.character = character;
        }
    }
}
