using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameLab3
{
    class Player
    {
        private List<Card> hand;                     // The cards are kept into an Array List rather than a regular array.
                                                     // An Array List just has a lot of useful features that make it easier to program.
        private decimal money;                       // The amount of cash on hand for the player.
                                                     // Useful for storing here as there is a way to keep score.

        public Player(decimal m)
        {
            // Initialize memory for a List of Card objects
            hand = new List<Card>();
            money = m;
        }

        // Get and set accessors for money
        public decimal Money
        {
            get { return money; }

            set { money = value; }
        }

        // Get card from Player's deck according to position
        public Card GetCard(int i)
        {
            return hand[i];
        }

        public void AddCard(Card c)
        {
            // You can add cards to the player's hand with this method
            // In the future, we can add a RemoveCard method if we want to
            // add more functionality to this game.
            hand.Add(c);
        }

        public void ClearHand()
        {
            // This deletes all of the cards in the player's deck.
            hand.Clear();
        }

        public void SortCards()
        {
            // Sort the player's cards
            // I was going to put in a bubble sort, but I found the following web page that described how to
            // sort an Array List here: 
            // https://social.msdn.microsoft.com/Forums/en-US/990b3d83-dca1-4594-b30e-2ac8fbe9716b/how-to-sort-an-arraylist-that-contains-objects-of-data-types?forum=csharpgeneral

            hand = hand.OrderBy(a => a.Rank).ToList();

            // The rationale for sorting each player's hand by rank is to make it easier for the Arcade Poker algorithm to determine
            // what the result of the player's hand is.

            // One improvement would be for Aces to have a possible rank value of 14 and placed at the right side of the player's
            // hand in case a straight to the Ace is possible. (10, J, Q, K, A)
        }

        public override string ToString()
        {
            // This will return a string output of the player's hand

            string output = "";              // this will hold the output to be returned

            // Go through each card in the Player's to be output to string
            foreach (Card c in hand)
            {
                output += c.ToString() + " ";
            }

            return output;
        }

    }
}
