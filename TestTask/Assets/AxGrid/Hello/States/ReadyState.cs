using System.Collections.Generic;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

namespace AxGrid.Hello.States
{
    [State("Ready")]
    public class ReadyState : FSMState
    {
        [Enter]
        public void Enter()
        {
            var cards = CardsKit();
            // Populate collection with random cards from cards.
            for (var i = 0; i < Settings.Model.GetInt("CardCounterValue"); i++)
            {
                Settings.Model.GetList<GameObject>("Cards").Add(cards[Random.Range(0, cards.Count)]);
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
                    break;
                case "Dec":
                    if (Settings.Model.GetInt("CardCounterValue", 0) > 0)
                        Settings.Model.Dec("CardCounterValue");
                    break;
            }
        }
    }
}