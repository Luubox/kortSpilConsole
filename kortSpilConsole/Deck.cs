using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kortSpilConsole
{
    class Deck
    {
        private List<Card> cards = new List<Card>();
        private List<Card> cardsRevealed = new List<Card>();
        private UnoGame game;

        public Deck(UnoGame game)
        {
            this.game = game;
            for (int i = 0; i < 10; i++)
            {
                cards.Add(new Card("red", ""+i));
                cards.Add(new Card("blue", ""+i));
                cards.Add(new Card("green", ""+i));
                cards.Add(new Card("yellow", ""+i));

                if (i != 0)
                {
                    cards.Add(new Card("red", ""+i));
                    cards.Add(new Card("blue", ""+i));
                    cards.Add(new Card("green", ""+i));
                    cards.Add(new Card("yellow", "" + i));
                }
            }

            for (int i = 0; i < 2; i++)
            {
                cards.Add(new Card("red", "skip"));
                cards.Add(new Card("red", "reverse"));
                cards.Add(new Card("red", "+2"));

                cards.Add(new Card("blue", "skip"));
                cards.Add(new Card("blue", "reverse"));
                cards.Add(new Card("blue", "+2"));

                cards.Add(new Card("green", "skip"));
                cards.Add(new Card("green", "reverse"));
                cards.Add(new Card("green", "+2"));

                cards.Add(new Card("yellow", "skip"));
                cards.Add(new Card("yellow", "reverse"));
                cards.Add(new Card("yellow", "+2"));
            }

            for (int i = 0; i < 4; i++)
            {
                cards.Add(new Card("black", "changeColor"));
                cards.Add(new Card("black", "+4"));
            }


            Shuffle();

            //move first card to revealed cards
            cardsRevealed.Add(Draw());
#if DEBUG
            cardsRevealed.Add(new Card("black", "+4"));
#endif
        }
        
        public Card Draw()
        {
            Card c = cards[0]; //finder øverste kort
            cards.Remove(c); //fjerner kort fra bunken (c = øverste kort)
            return c; //giver kortet til den der kalder metoden
        }

        public Card DebugDraw(String Color, String Value)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Color == Color && cards[i].Value == Value)
                {
                    Card c = cards[i];
                    cards.Remove(c);
                    return c;
                } 
            }
            return null;
        }

        public void Shuffle()
        {
            // shuffle array
            Random random = new Random();
            cards = cards.OrderBy(x => random.Next()).ToList();
        }

        public bool PlayCard2(Card card, int counter, Player player)
        {
            switch (card.Color)
            {
                case "black":
                    Console.WriteLine("Choose color:");
                    card.Color = Console.ReadLine();
                    cardsRevealed.Add(card);
                    return true;
                default:
                    Console.WriteLine("nope");
                    return false;
            }
        } //duer ikke

        public bool PlayCard(Card card, int counter, Player player)
        {
            if (Peek().Color == card.Color || Peek().Value == card.Value)
            {
                player.Hand.Remove(card);
                cardsRevealed.Add(card);
                return true;
            }
            else if (cardsRevealed.Last().Color == "black" && counter == 1)
            {
                player.Hand.Remove(card);
                cardsRevealed.Add(card);
                return true;
            }
            else if (card.Color == "black")
            {
                Console.WriteLine("Choose color:");
                card.Color = Console.ReadLine();
                cardsRevealed.Add(card);
                return true;
            }
            else return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < cards.Count; i++)
            {
                sb.Append("[");
                sb.Append(cards[i]);
                sb.Append("], ");
            }

            return sb.ToString();
        }

        public Card Peek()
        {
            return cardsRevealed.Last();
        }
    }
}
