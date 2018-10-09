using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace kortSpilConsole
{
    class UnoGame
    {
        public Deck deck;
        List<Player> players = new List<Player>();
        private Player currentPlayer;
        private bool gameover = false;

        public UnoGame()
        {
            deck = new Deck(this);
            
            players.Add(new Player("Alfa", this));
            players.Add(new Player("Beta", this));
            currentPlayer = players.First();
            //del kort ud til spiller 1;
            players[0].DrawCard(6);
            players[0].DebugDrawCard("black", "changeColor");
            //del 7 kort ud til resten af spilelrne
            for (int i = 1; i < players.Count; i++)
            {
                players[i].DrawCard(7);
            }

            int counter = 0;
            while (gameover != true)
            {
                counter++;
                // vis vores 'revealed' card
                Console.WriteLine(deck.Peek());
                if (deck.Peek().Value == "+4")
                {
                    currentPlayer.DrawCard(4);
                }

                // print player1 med tostring-metoden (navn: g2, b3, r7....)
                Console.WriteLine(currentPlayer);

                // spørg spiller1 om hvilket kort han vil ligge ned
                Console.WriteLine("Vælg et kort!");
                //int i = Convert.ToInt32(Console.ReadLine());

                string playerChoice = Console.ReadLine();
                if (playerChoice == "Pass")
                {
                    currentPlayer.DrawCard();
                    nextPlayer();

                }
                else if (deck.PlayCard(currentPlayer.Hand[Convert.ToInt32(playerChoice) - 1]))
                {
                    Console.WriteLine($"Pile: {deck.Peek()}");
                    nextPlayer();
                }
                else
                {
                    Console.WriteLine("Invalid card! try again");
                }
            }
        }

        private void nextPlayer()
        {
            if (currentPlayer == players.Last())
            {
                currentPlayer = players.First();
            }
            else
            {
                int currentPlayerPosition = players.IndexOf(currentPlayer);
                currentPlayer = players[currentPlayerPosition + 1];
            }
        }
    }
}
