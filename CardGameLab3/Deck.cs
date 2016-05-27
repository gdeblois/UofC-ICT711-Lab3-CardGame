using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLab3
{
    class Deck
    {
        const int NR_CARDS = 52;

        public Card[] cards;

        public Deck()
        {
            cards = new Card[NR_CARDS];

            // the constructor creates an unshuffled deck of cards
            for (int s = 0; s < 4; s++)
            {
                for (int r = 0; r < 13; r++)
                {
                    // remember that suit and rank values begin with 1 for the sake of the Card object, hence adding + 1 in each of
                    // the parameters
                    cards[s * 13 + r] = new Card(s + 1, r + 1);
                }
            }
        }

        public Card GetCard(int i)
        {
            // Get the card at the position of the deck.

            if (i < 0 || i >= NR_CARDS)
            {
                // if out of range value is passed to this method, just return the first card;
                return cards[0];
            }
            else
            {
                // otherwise, just return the card at the appropriate position in the deck
                return cards[i];
            }
        }

        public String GetCardToString(int i)
        {
            // A separate method to return the string reprentation of the card
            return GetCard(i).ToString();
        }

        public void Shuffle()
        {
            // Create a new array to represent the shuffled cards

            Random sourceGen = new Random();
            Card temp;                                          // placeholder to hold the temporary value of a card                  

            // let's switch one card position with another one for 999 times to make sure that it's *REALLY* shuffled.
            for (int i = 0; i < 999; i++)
            {
                int x = sourceGen.Next(NR_CARDS);
                int y = sourceGen.Next(NR_CARDS);

                temp = cards[x];
                cards[x] = cards[y];
                cards[y] = temp;
            }


        }

    }
}
