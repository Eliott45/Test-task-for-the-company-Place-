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
        
        [OnStart]
        private void GoStart()
        {
            CreateCards();
        }

        private void CreateCards()
        {
            Debug.Log(Settings.Model.GetInt("CardCounterValue"));
            for (var i = 0; i < Settings.Model.GetInt("CardCounterValue"); i++)
            {
               
                var go = Instantiate(_prefabsCards[Random.Range(0, _prefabsCards.Length)]);
                go.GetComponent<Card>().collection = Card.ECollection.A;
            }
        }
        

    }
}
