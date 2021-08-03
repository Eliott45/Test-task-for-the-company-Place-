using System.Collections.Generic;
using AxGrid.Base;
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
        private static readonly List<GameObject> PrefabsCards = CardsKit();
        /// <summary>
        /// Deck of cards in the game.
        /// </summary>
        private static List<GameObject> _cardsA;
        private static List<GameObject> _cardsB;
        private static Transform _collectionAnchorA;
        private Transform _collectionAnchorB;

        [Enter]
        public void Enter()
        {
            CreateAnchors();
            _cardsA = Settings.Model.GetList<GameObject>("CardsA");
            _cardsB = Settings.Model.GetList<GameObject>("CardsB");
            CreateCards();
            Hand(_cardsA);
        }

        private void CreateAnchors()
        {
            var goA = new GameObject("collectionA");
            var goB = new GameObject("collectionB");
            goA.transform.position = new Vector3(0,-4,100);
            goB.transform.position = new Vector3(0,4, 100);
            _collectionAnchorA = goA.transform;
            _collectionAnchorB = goB.transform;
        }

        private static void CreateCards()
        {
            // Populate collection with random cards from prefabs of card.
            for (var i = 0; i < Settings.Model.GetInt("CardCounterValue"); i++)
            {
                var go = Object.Instantiate(PrefabsCards[Random.Range(0, PrefabsCards.Count)], _collectionAnchorA, false);
                go.GetComponent<Card>().CardID = i;
                go.GetComponent<Card>().collection = Card.ECollection.A;
                _cardsA.Add(go);
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
                    CreateCard();
                    Hand(_cardsA);
                    break;
                case "Dec":
                    if (Settings.Model.GetInt("CardCounterValue", 0) > 0)
                    {
                        Settings.Model.Dec("CardCounterValue");
                        RemoveCard();
                        Hand(_cardsA);
                    }
                    break;
            }
        }
        
        [Bind]
        public void SwapCard(GameObject card)
        {
            var data = card.GetComponent<Card>();
            if (data.collection == Card.ECollection.A)
            {
                Settings.Model.Dec("CardCounterValue");
                _cardsA.Remove(card);
                _cardsB.Add(card);
                data.collection = Card.ECollection.B;
                card.transform.parent = _collectionAnchorB;
            }
            else
            {
                Settings.Model.Inc("CardCounterValue");
                _cardsB.Remove(card);
                _cardsA.Add(card);
                data.collection = Card.ECollection.A;
                card.transform.parent = _collectionAnchorA;
            }
            card.transform.localPosition = Vector3.zero;
            Hand(_cardsA);
            Hand(_cardsB);
        }

        private static void CreateCard()
        {
            _cardsA.Add(Object.Instantiate(PrefabsCards[Random.Range(0, PrefabsCards.Count)], _collectionAnchorA, false));
            for (var i = 0; i < _cardsA.Count; i++)
            {
                _cardsA[i].GetComponent<Card>().CardID = i;
            }
        }

        private static void RemoveCard()
        {
            var element = _cardsA[_cardsA.Count - 1];
            Object.Destroy(element);
            _cardsA.Remove(element);
            for (var i = 0; i < _cardsA.Count; i++)
            {
                _cardsA[i].GetComponent<Card>().CardID = i;
            }
        }

        private static void Hand(IReadOnlyList<GameObject> deck)
        {
            int r = 0, l = 0;
            for (var i = 0; i < deck.Count; i++)
            {
                deck[i].GetComponent<Card>().CardID = i;
                var pos = deck[i].transform.position;
                pos.x = 0;
                if (i == 0)
                {
                    pos += Vector3.zero;
                }
                else if (i % 2 == 0)
                {
                    pos += new Vector3(1 + r, 0, 0);
                    r++;
                }
                else
                {
                    pos += new Vector3(-1 - l, 0, 0);
                    l++;
                }
                deck[i].GetComponent<Card>().SetLayer(-i);
                deck[i].transform.position = pos;
            }
        }
    }
}