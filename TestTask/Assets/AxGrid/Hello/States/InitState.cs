using System;
using System.Collections;
using System.Collections.Generic;
using AxGrid.FSM;
using UnityEngine;

namespace AxGrid.Hello.States
{
    [State("Init")]
    public class InitState : FSMState
    {
        
        [Enter]
        public void Enter()
        {
            Log.Info("Init objects");
            Settings.Model.Set("CardCounterValue", 5);
            
            var cards = new List<GameObject>();
            Settings.Model.Set("Cards", cards);
            
            Parent.Change("Ready");
        }

        
        
    }
}