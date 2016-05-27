using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLab3
{
    class Card
    {
        // The suit and string representation of the cards are static so they can be seen across _ALL_ instances of the Card object.
        protected static char[] suitChar = { '?','\u2663', '\u2666', '\u2665', '\u2660' };
        protected static string[] rankString = { "?", " A", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", " J", " Q", " K" };

        private int suit;               // has value of 1 to 4
        private int rank;               // has value of 1 to 13 (Ace being 1, King being 13)

        // constructor to create Card object
        public Card(int s, int v)
        {
            Suit = s;                   // Call the set accessor to make sure the values fall within the required range (1-4)
            Rank = v;                   // Call the set accessor as well so it has a value between 1-13
        }

        // Accessors for suit, should fall within the values 1-4 for set, otherwise it is set to 0
        public int Suit
        {
            get { return suit; }

            set
            {
                if(value<1 || value>4)
                {
                    suit = 0;
                }
                else
                {
                    suit = value;
                }
            }
        }

        // Accessors for cardValue, should fall within values 1-13 for set, otherwise it is set to 0
        public int Rank
        {
            get { return rank; }

            set
            {
                if(value<1 || value>13)
                {
                    rank = 0;
                }
                else
                {
                    rank = value;
                }
            }
        }

        public override string ToString()
        {
            // this will return the card value and it's respective suit in a string
            return rankString[rank] + suitChar[suit];
        }
    }
}
