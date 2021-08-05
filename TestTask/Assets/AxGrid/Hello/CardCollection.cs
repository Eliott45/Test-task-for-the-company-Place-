using System;
using System.Collections.Generic;
using UnityEngine;

namespace AxGrid.Hello
{
    public class CardCollection : MonoBehaviour
    {
        public string nameField = "Test";
        public List<GameObject> cards;

        private void Awake()
        {
            Settings.GlobalModel.EventManager.AddAction("Shuffle", Refresh);
        }

        /// <summary>
        /// Set the correct position of the cards.
        /// </summary>
        private void Refresh()
        {
            var b = cards.Count / 2;
            var f = 0;
            for (var i = 0; i < cards.Count; i++)
            {
                cards[i].GetComponent<Card>().cardID = i;
                cards[i].transform.parent = transform;
                var pos = cards[i].transform.position;
                pos.x = 0;
                pos.y = transform.position.y;
                if (i < cards.Count / 2)
                {
                    pos += new Vector3(-b, 0, 0);
                    b--;
                }
                else
                {
                    pos += new Vector3(f, 0, 0);
                    f++;
                }

                var cardData = cards[i].GetComponent<Card>();
                cardData.SetLayer(-i);
                cardData.target = pos;
                cardData.moving = true;
            }
        }
    }
}
