using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRManager
{
    class Program
    {
        //placement or win/loss or both
        //every recording adds to winner and subtracts from loser
        //  add the bounty of who you beat, subtract from them what yours is

        static void Main(string[] args)
        {
            Console.WriteLine("Enter a player tag/name");
            string nameOrTag = Console.ReadLine();
            string line;
            bool nameOrTagFound = false;
            System.IO.StreamReader file = new System.IO.StreamReader("smashcharacters.txt");
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains(nameOrTag))
                {
                    nameOrTagFound = true;
                }
            }

            if (nameOrTagFound == true)
            {

            }
            else
            {

            }


            Console.ReadLine();
        }

        static void updatePlayer(Player player)
        {

        }


        static void addPlayer(Player player)
        {

        }

        static void addResult(Player playerWon, Player playerLost)
        {

        }
    }
}
