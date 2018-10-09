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
        public Deck Deck;
        List<Player> _players = new List<Player>();
        private Player _currentPlayer;
        private bool _gameover = false;

        public UnoGame()
        {
            Deck = new Deck(this);
            
            _players.Add(new Player("Alfa", this));
            _players.Add(new Player("Beta", this));
            _currentPlayer =   _players.First();
            //del kort ud til spiller 1;
            _players[0].DrawCard(7);
#if DEBUG
            _players[0].DebugDrawCard("black", "changeColor"); 
#endif
            //del 7 kort ud til resten af spilelrne
            for (int i = 1; i < _players.Count; i++)
            {
                _players[i].DrawCard(1);
            }

            int counter = 0;
            while (_gameover != true)
            {
                counter++;
                // vis vores 'revealed' card
                Console.WriteLine(Deck.Peek());
                if (Deck.Peek().Value == "+4" && counter > 1) //TODO: flyt funktionalitet til switch case / if statement, ellers trækker den ved Pass
                {
                    _currentPlayer.DrawCard(4);
                }

                // print player1 med tostring-metoden (navn: g2, b3, r7....)
                Console.WriteLine(_currentPlayer);

                // spørg spiller1 om hvilket kort han vil ligge ned
                Console.WriteLine("Vælg et kort! (eller pass)");
                //int i = Convert.ToInt32(Console.ReadLine());

                string playerChoice = Console.ReadLine()?.ToUpper();
                if (playerChoice == "PASS")
                {
                    _currentPlayer.DrawCard();
                    NextPlayer();

                }
                else if (Deck.PlayCard(_currentPlayer.Hand[Convert.ToInt32(playerChoice) - 1], counter, _currentPlayer))
                {
                    NextPlayer(); 
                }
                else
                {
                    Console.WriteLine("Invalid card! try again");
                }

            }
            Console.ReadKey();
        }

        private void NextPlayer()
        {
            if (_currentPlayer.Hand.Count == 0)
            {
                Console.WriteLine($"Congratulations {_currentPlayer.name}! You have won!");
                _gameover = true;
            }
            else if (_currentPlayer == _players.Last())
            {
                _currentPlayer = _players.First();
            }
            else
            {
                int currentPlayerPosition = _players.IndexOf(_currentPlayer);
                _currentPlayer = _players[currentPlayerPosition + 1];
            }
        }
    }
}
