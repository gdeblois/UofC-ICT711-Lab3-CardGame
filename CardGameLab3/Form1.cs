using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
ICT711 Lab Assignment 3
-----------------------

Submitted by: Ghislain de Blois (alone) ghislaindeblois@gmail.com

In this lab you will practice creating application with multiple classes.
As most of programming is done in team environment, it is preferred that you to complete this lab in collaboration with another student 
(or in a group of three). However, if you choose, you may work alone. Inform the instructor as soon as possible the composition of your 
group (if you work alone, you are considered a group of one).

The scope of this lab depends on the size of your group. 
Step 1 is mandatory if you work in a group of one.
Steps 1 and 2 are mandatory if you work in a group of two. 
Steps 1, 2, and 3 are mandatory if you work in a group of three.

It is up to your group how you divide the work between the group members. You are encouraged to collaborate tightly in the design phase, 
and then you may assign code modules (classes, methods) to individual people to code. Alternatively, you may choose to practice “extreme 
programming” and do peer-coding – it is all up to you. However, keep in mind to divide the work in equitable manner, so that each person 
gets a good practice from working on this lab. Make sure to include comments in each piece of code that indicate who is responsible for 
developing it. The grades will be assigned on individual basis, based on contribution of each person (scope), achieved functionality and 
quality of the code.

Your Windows Application project will simulate a simple card game. Assume a 52- cards deck that is made of 4 suits (clubs, diamonds, 
hearts, and spaces) and 13 ranks in each suit (Ace, 2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, and King).

Details
=======

Step 1 
------
Create class Card that represents one card.
It should have private data suit and rank.
The Card constructor assigns suit and rank to the values passed to it as parameters.
There is a method ToString() that returns a string containing suit and rank of the card for display purposes

Tip: If you want to display the cards using special characters for each suit, you may use the following arrays:
char[] suitChar = { '\u2663', '\u2666', '\u2665', '\u2660' };
string[] rankString = { "", " A", " 2", " 3", " 4", " 5", " 6", " 7", " 8", " 9", "10", " J", " Q", " K" };

To display element on position i from suitChar array, you will need to convert it to string: suitChar[i].ToString();
Create class Deck that represents a deck of 52 cards.
It has a private data which is an array or list of Card objects, which is called cards.
The class constructor creates 52 objects of the Card class, one for each suit-rank combination, an adds them to the cards array or 
list.The carts should be added in the order of increasing value, first all clubs, then all diamonds, then hearts, then spades. The 
Ace is assumed to have the lowest value in each suit.
Class Deck also has a function GetCard that accepts an int i parameter and returns the Card object on position i from the deck.The 
function should validate the parameter to be between 0 and 52.
You may add more members to the class Deck if needed.
The main form class at the very minimum displays the deck of cards(for example in a list box).
As the form loads, and object of class Deck is created, and all cards in the deck are displayed in the initial order.

Step 2
------
Add method Shuffle() to the Deck class that reorganizes the deck of cards in the random order.
On the main form add button Shuffle that calls the Shuffle method from the Deck from the object of class Deck, and redisplays the 
cards in the new order.
Tip: to shuffle cards in the random order you will need to use a random number generator.You create an object of random number generator: 
Random sourceGen = new Random();
After that, assuming that NR_CARDS is an int constant with value 52, expression sourceGen.Next(NR_CARDS)  returns a random value from 
0..51 range.

Step 3
------
Add class Player to the project that represents a player.
Player has hand, which is an array or list of Card objects.
Add appropriate methods to the Player class.
In the main form class, create four Player objects.
Add a button Deal to the form and when the user clicks on it, deal the cards from the deck between the four players. Display all 
players’ hands.
Optional: You may go further and have the players play a simple card game of your choice. The game can be a game of chance, or may 
require user’s interaction. Make sure to include game instructions for the user. 
Have fun!!

Marking Scheme
--------------
Contributed substantial amount of code to the groups project	9	   
Code achieves required functionality	8	   
Code has good style and is adequately commended	5	   
Total: 	22	 
*/


namespace CardGameLab3
{
    public partial class Form1 : Form
    {

        // declare global variables here

        // Arcade Poker pays out for these types of hands:
        string[] payoutText = { "ROYAL FLUSH",        // This is a straight flush to the King (for the sake of this project).
                                "STRAIGHT FLUSH",
                                "FOUR OF A KIND",
                                "FULL HOUSE",
                                "FLUSH",
                                "STRAIGHT",           // Since Aces only have a rank of 1 in this project, straight hands can only go as high as the King.
                                "THREE OF A KIND",
                                "TWO PAIRS",
                                "ONE PAIR",
                                "NOTHING" };

        // These are the payout amounts that correspond to the above for every $1 bet.
        int[] payoutAmount = { 500,             // Royal Flush
                               200,             // Straight Flush
                               100,             // Four of a Kind
                               40,              // Full House
                               25,              // Flush
                               10,              // Straight
                               5,               // Three of a Kind
                               2,               // Two Pairs
                               1,               // One Pair
                               0 };             // Nothing


        const int NUM_HANDS = 10;        // Number of possible hand values
        const int NR_CARDS = 52;        // Number of cards in a deck
        const int NUM_PLAYERS = 4;      // 4 players for this game

        Deck mainDeck;
        Player[] players = new Player[4];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // When the program is first run, clear everything
            this.ClearEverything();
        }

        public void ClearEverything()
        {
            string ts;          // temp string to hold the payout to display.

            // First clear the list of Payouts
            lbPayouts.Items.Clear();

            // Now let us display the payout ratios for each winning hand.
            for (int i = 0; i<NUM_HANDS; i++)
            {
                // Format the output so that it appears properly in the payouts list box.
                ts = (payoutText[i] + ".................................................................").Substring(0, 50)
                                    + payoutAmount[i].ToString() + " TO 1";

                lbPayouts.Items.Add(ts);
            }

            mainDeck = new Deck();      // Create a new deck of cards
            lbDeck.Items.Clear();       // Empty the deck display listbox

            // Create 4 new players and display them
            for (int i=0; i < NUM_PLAYERS; i++)
            {
                players[i] = new Player(100);           // Each player begins with $100 cash on hand
            }

            // Display no results on players' display

            txtResult1.Text = "";
            txtResult2.Text = "";
            txtResult3.Text = "";
            txtResult4.Text = "";

            // Display the newly created players now.
            DisplayPlayers();

        }

        public void DisplayDeck()
        {
            // Let us clear the deck list box on the form.
            lbDeck.Items.Clear();

            // Let's display the deck of cards in the list box
            for (int i = 0; i < NR_CARDS; i++)
            {
                lbDeck.Items.Add(mainDeck.GetCardToString(i));
            }
        }

        public void DisplayPlayers()
        {
            // This method displays all players hands and their cash on hand.
            // However, the result field is not handled here.

            // Display Player 1
            txtCardsPlayer1.Text = players[0].ToString();
            txtCashPlayer1.Text = players[0].Money.ToString("c");

            // Display Player 2
            txtCardsPlayer2.Text = players[1].ToString();
            txtCashPlayer2.Text = players[1].Money.ToString("c");
            
            // Display Player 3
            txtCardsPlayer3.Text = players[2].ToString();
            txtCashPlayer3.Text = players[2].Money.ToString("c");
            
            // Display Player 4
            txtCardsPlayer4.Text = players[3].ToString();
            txtCashPlayer4.Text = players[3].Money.ToString("c");
        }

        private void btnDeal_Click(object sender, EventArgs e)
        {
            // The main gameplay is handled here

            int[] result = new int[NUM_PLAYERS];                              // Hold each player's result in this array.

            // Call the deck's Shuffle method
            mainDeck.Shuffle();

            // And then call the form's method to display the contents of the deck
            DisplayDeck();

            // Call method to deal the cards to each player
            DealCards();

            // Now let's determine the result of each player's hand.
            for (int i = 0; i < NUM_PLAYERS; i++)
            {
                // Deduct $1.00 from the player's money
                players[i].Money -= 1;

                // Call seperate method to find out the result of the respective player's hand
                // and display the result on the screen.
                result[i] = ResultHand(players[i]);

                // Add winnings to the player's cash on hand
                // This game doesn't check to see if the player has run out of money .. the house
                // will keep a tab on any negative cash amounts.
                players[i].Money += payoutAmount[result[i]];
            }

            // Now display the players' hands.
            DisplayPlayers();

            // And finally display each player's hand result (flush, pairs, etc)
            txtResult1.Text = payoutText[result[0]] + " " + payoutAmount[result[0]].ToString("c");
            txtResult2.Text = payoutText[result[1]] + " " + payoutAmount[result[1]].ToString("c");
            txtResult3.Text = payoutText[result[2]] + " " + payoutAmount[result[2]].ToString("c");
            txtResult4.Text = payoutText[result[3]] + " " + payoutAmount[result[3]].ToString("c");
        }

        private void DealCards()
        {
            // Seperate method to deal cards to all of the 4 players.

            int deckPointer = 0;                    // First point to the top of the deck

            // Now we deal the cards to each player
            for (int i = 0; i < NUM_PLAYERS; i++)
            {
                players[i].ClearHand();             // remove all the cards from the player's hand

                for (int j = 0; j < 5; j++)
                {
                    players[i].AddCard(mainDeck.GetCard(deckPointer));
                    deckPointer++;
                }

                players[i].SortCards();             // now sort the cards within the player's hand
            }

        }

        private int ResultHand(Player pl)
        {
            // This method is to determine what the result of a player's hand is.
            // I could include it in the Player object, but I thought it best to put the method here
            // in the main body of the program because the rules of Arcade Poker, which are the game
            // algorithm, should be visibly available here in the program listing.

            // Some things I'd do to improve in the future is to account for Aces which can have a nominal card value of 1 or 14.

            int[] rank = new int[5];                // Hold the rank values for each card in an array
            bool flush = true;                      // Use this as a flag to determine if all the player's cards are of the same suit or not.
            bool straight = true;                   // Use this flag to determine if all the cards are in sequence.
            bool pair = false;                      // Use this flag to make it easier to check for a single pair
            int firstSuit = pl.GetCard(0).Suit;     // Get the first card's suit and check it against the other cards in the following loop.
            int firstRank = pl.GetCard(0).Rank;     // Get the first card's value for the purpose to find a straight

            for (int i = 0; i < 5; i++)
            {
                // I iterate through all of the player's cards and place them into int arrays so that it is easier to write the several
                // if statements to determine the result of each player's hand (better to do this than
                // doing something like if (pl.GetCard(0).Rank == pl.GetCard(1).Rank)... etc
                Card c = pl.GetCard(i);
                rank[i] = c.Rank;

                // Since we are already inside of a loop, it's simple to check here for basic flush, straight and single pair

                // Check to see if the current suit in this loop's iteration matches that of the first card.
                if (firstSuit != c.Suit)
                    flush = false;

                // Check to see if the current card has a value of +i over the previous card
                if (firstRank + i != rank[i])
                    straight = false;

                // Check to see if there is at least one single pair
                if (i > 0 && rank[i] == rank[i-1])
                    pair = true;
            }

            // To make the program listing easier to read, I'm not going to put in several nested if statements, and I will check
            // for each possible result in the order of the Arcade Poker rules.  This method will exit with an int value between 0-9
            // for which the program can calculate cash winnings and display each player's hand result.

            // *** ROYAL FLUSH (straight flush to the highest card, which is the King)
            if (flush == true && straight == true  && rank[4] == 13)
                return 0;

            // STRAIGHT FLUSH (because the highest card was not a King)
            if (flush == true && straight == true)
                return 1;

            // FOUR OF A KIND
            if ((rank[0] == rank[1] && rank[0] == rank[2] && rank[0] == rank[3])
                || (rank[1] == rank[2] && rank[1] == rank[3] && rank[1] == rank[4]))
                return 2;

            // FULL HOUSE
            if ((rank[0] == rank[1] && rank[2] == rank[3] && rank[2] == rank[4])
                || (rank[0] == rank[1] && rank[0] == rank[2] && rank[3] == rank[4]))
                return 3;

            // FLUSH
            if (flush == true)
                return 4;

            // STRAIGHT
            if (straight == true)
                return 5;

            // THREE OF A KIND
            if ((rank[0] == rank[1] && rank[0] == rank[2])
                || (rank[1] == rank[1] && rank[1] == rank[3])
                || (rank[2] == rank[1] && rank[2] == rank[4]))
                return 6;

            // TWO PAIRS
            if ((rank[0] == rank[1] && rank[2] == rank[3])
                || (rank[0] == rank[1] && rank[3] == rank[4])
                || (rank[1] == rank[2] && rank[3] == rank[4]))
                return 7;

            // ONE PAIR
            if (pair == true)
                return 8;

            // Player has Nothing. So exit here.
            return 9;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset the game
            ClearEverything();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // If the exit button is pressed, close the program.
            Close();
        }
    }
}
