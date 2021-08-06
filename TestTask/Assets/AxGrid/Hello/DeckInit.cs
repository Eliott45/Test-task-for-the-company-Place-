using System;
using System.Collections.Generic;
using AxGrid.Base;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AxGrid.Hello
{
    public class DeckInit : MonoBehaviour
    {
        [Header("Set in inspector:")]
        [SerializeField] private GameObject[] _prefabsCards;

        private bool _spawned;

        private void Awake()
        {
            Settings.GlobalModel.EventManager.AddAction("Add", CreateCard);
            Settings.GlobalModel.EventManager.AddAction("Remove", RemoveCard);

        }
        
        private void Update()
        {
            if (_spawned || Settings.Model.GetInt("CardCounterValue") <= 0) return;
            CreateCards();
            _spawned = true;
        }
        
        private void CreateCards()
        {
            for (var i = 0; i < Settings.Model.GetInt("CardCounterValue"); i++)
            {
               
                var go = Instantiate(_prefabsCards[Random.Range(0, _prefabsCards.Length)], transform, false);
                go.GetComponent<Card>().collection = Card.ECollection.A;
                Settings.Model.GetList<Card>("CardsA").Add(go.GetComponent<Card>());
            }
            
            Settings.GlobalModel.EventManager.Invoke("Shuffle");
        }
        
        
        private void RemoveCard()
        {
            Debug.Log("!");
            var element = Settings.Model.GetList<Card>("CardsA")[Settings.Model.GetList<Card>("CardsA").Count - 1];
            element.Delete();
            Settings.Model.GetList<Card>("CardsA").Remove(element);
            
        }
        
        private void CreateCard()
        {
            Settings.Model.GetList<Card>("CardsA").Add(
                Instantiate(_prefabsCards[Random.Range(0, _prefabsCards.Length)].GetComponent<Card>(), 
                    transform, 
                    false));
        }
    }
}
