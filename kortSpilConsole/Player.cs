using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kortSpilConsole
{
    class Player
    {
        private UnoGame game;
        public String name;
        public List<Card> Hand { get; set; }

        public Player(string name, UnoGame game )
        {
            this.game = game;
            this.name = name;
            this.Hand = new List<Card>();
        }

       
        public void DrawCard()
        {
            Hand.Add(game.deck.Draw());
        }

        public void DrawCard(int numberOfCards)
        {
            for (int i = 0; i < numberOfCards; i++)
            {
                DrawCard();
            }
        }

        public void DebugDrawCard(String Color, String Value)
        {
            Hand.Add(game.deck.DebugDraw(Color, Value));
        }

        public override string ToString()
        {
           StringBuilder sb = new StringBuilder();
            sb.Append(name);
            sb.Append(": ");
            for (int i = 0; i < Hand.Count; i++)
            {
                sb.Append("[");
                sb.Append(Hand[i].ToString());
                sb.Append("]");
            }

            return sb.ToString();
        }
    }
}
