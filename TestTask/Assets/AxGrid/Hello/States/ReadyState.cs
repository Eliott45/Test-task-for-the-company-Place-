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

        #region Fields 
        
        /// <summary>
        /// Possible cards in the game.
        /// </summary>
        private static readonly List<GameObject> PrefabsCards = CardsKit();
        /// <summary>
        /// Deck of cards in the game (collection A).
        /// </summary>
        private static List<GameObject> _cardsA;
        private static List<GameObject> _cardsB;
        private static GameObject _collectionAnchorA;
        private static GameObject _collectionAnchorB;


        #endregion

        #region FrameworkMethods

        [Enter]
        public void Enter()
        {
            CreateAnchors();
            _cardsA = Settings.Model.GetList<GameObject>("CardsA");
            _cardsB = Settings.Model.GetList<GameObject>("CardsB");
            CreateCards();
            _collectionAnchorA.GetComponent<CardCollection>().cards = _cardsA;
            _collectionAnchorB.GetComponent<CardCollection>().cards = _cardsB;
            _collectionAnchorA.GetComponent<CardCollection>().Refresh();
        }
        
        
        #endregion

        #region Binds

        [Bind]
        public void OnBtn(string name)
        {
            switch (name)
            {
                case "Inc":
                    Settings.Model.Inc("CardCounterValue");
                    CreateCard();
                    break;
                case "Dec":
                    if (Settings.Model.GetInt("CardCounterValue", 0) > 0)
                    {
                        Settings.Model.Dec("CardCounterValue");
                        RemoveCard();
                    }
                    break;
            }
            _collectionAnchorA.GetComponent<CardCollection>().Refresh();
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
            }
            else
            {
                Settings.Model.Inc("CardCounterValue");
                _cardsB.Remove(card);
                _cardsA.Add(card);
                data.collection = Card.ECollection.A;
            }
            _collectionAnchorA.GetComponent<CardCollection>().Refresh();
            _collectionAnchorB.GetComponent<CardCollection>().Refresh();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creating empty objects in the scene.
        /// </summary>
        private static void CreateAnchors()
        {
            _collectionAnchorA = new GameObject("collectionA");
            _collectionAnchorB = new GameObject("collectionB");
            _collectionAnchorA.AddComponent<CardCollection>().nameField = "Collection A";
            _collectionAnchorB.AddComponent<CardCollection>().nameField = "Collection B";
            _collectionAnchorA.transform.position = new Vector3(0,-2,100);
            _collectionAnchorB.transform.position = new Vector3(0,2, 100);
        }
        
        private static void CreateCards()
        {
            for (var i = 0; i < Settings.Model.GetInt("CardCounterValue"); i++)
            {
                var go = Object.Instantiate(PrefabsCards[Random.Range(0, PrefabsCards.Count)], _collectionAnchorA.transform, false);
                go.GetComponent<Card>().collection = Card.ECollection.A; 
                _cardsA.Add(go);
            }
        }
        
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
        
        private static GameObject Load(string name)
        {
            return Resources.Load(name) as GameObject;
        }
        
        private static void CreateCard()
        {
            _cardsA.Add(Object.Instantiate(PrefabsCards[Random.Range(0, PrefabsCards.Count)], _collectionAnchorA.transform, false));
        }

        private static void RemoveCard()
        {
            var element = _cardsA[_cardsA.Count - 1];
            Object.Destroy(element);
            _cardsA.Remove(element);
        }
        
        #endregion
        
    }
}