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
        #region FrameworkMethods

        [Enter]
        public void Enter()
        {
            Settings.GlobalModel.EventManager.Invoke("Shuffle");
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
                    Settings.GlobalModel.EventManager.Invoke("Add");
                    break;
                case "Dec":
                    if (Settings.Model.GetInt("CardCounterValue", 0) > 0)
                    {
                        Settings.Model.Dec("CardCounterValue");
                        Settings.GlobalModel.EventManager.Invoke("Remove");
                    }
                    break;
            }
            Settings.GlobalModel.EventManager.Invoke("Shuffle");
        }
        
        [Bind]
        public void SwapCard(Card card)
        {
            if (card.collection == Card.ECollection.A)
            {
                Settings.Model.Dec("CardCounterValue");
                Settings.Model.GetList<Card>("CardsA").Remove(card);
                Settings.Model.GetList<Card>("CardsB").Add(card);
                card.collection = Card.ECollection.B;
            }
            else
            {
                Settings.Model.Inc("CardCounterValue");
                Settings.Model.GetList<Card>("CardsB").Remove(card);
                Settings.Model.GetList<Card>("CardsA").Add(card);
                card.collection = Card.ECollection.A;
            }
            Settings.GlobalModel.EventManager.Invoke("Shuffle");
        }

        #endregion

    }
}