using System.Collections.Generic;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

namespace AxGrid.Hello.States
{
    [State("Ready")]
    public class ReadyState : FSMState
    {
        /// <summary>
        /// Possible cards in the game.
        /// </summary>
        private static readonly List<GameObject> PrefabCards = CardsKit();
        /// <summary>
        /// Deck of cards in the game.
        /// </summary>
        private static List<GameObject> _cards;
        
        [Enter]
        public void Enter()
        {
            _cards = Settings.Model.GetList<GameObject>("Cards");
            CreateCards();
        }

        private static void CreateCards()
        {
            // Populate collection with random cards from cards.
            for (var i = 0; i < Settings.Model.GetInt("CardCounterValue"); i++)
            {
                var go = Object.Instantiate(PrefabCards[Random.Range(0, PrefabCards.Count)]);
                _cards.Add(go);
                // Debug.Log(Settings.Model.GetList<GameObject>("Cards")[i]);
            }
        }

        /// <summary>
        /// List of all possible cards in the game.
        /// </summary>
        /// <returns>Deck of Possible Cards.</returns>
        private static List<GameObject> CardsKit()
        {
            var cards = new List<GameObject>
            {
                Load("BronzeCard"),   
                Load("SilverCard"),
                Load("GoldCard"),
            };
            return cards;
        }

        /// <summary>
        /// Load game object from folder "Resources". 
        /// </summary>
        /// <param name="name">Name of game object.</param>
        /// <returns>Game object from "Resources".</returns>
        private static GameObject Load(string name)
        {
            return Resources.Load(name) as GameObject;
        }

        [Bind]
        public void OnBtn(string name)
        {
            switch (name)
            {
                case "Inc":
                    Settings.Model.Inc("CardCounterValue");
                    AddCard();
                    break;
                case "Dec":
                    if (Settings.Model.GetInt("CardCounterValue", 0) > 0)
                    {
                        Settings.Model.Dec("CardCounterValue");
                        RemoveCard();
                    }
                    break;
            }
        }
        
        private static void AddCard()
        {
            _cards.Add(Object.Instantiate(PrefabCards[Random.Range(0, PrefabCards.Count)]));
        }
        
        private static void RemoveCard()
        {
            var lastElement = _cards[_cards.Count - 1];
            Object.Destroy(lastElement);
            _cards.Remove(lastElement);
        }
        
    }
}