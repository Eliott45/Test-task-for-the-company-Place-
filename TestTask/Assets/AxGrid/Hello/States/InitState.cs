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
            Settings.Model.Set("CardCounterValue", 10);
            
            Settings.Model.Set("CardsA", new List<Card>());
            Settings.Model.Set("CardsB", new List<Card>());
            
            Parent.Change("Ready");
        }

        
        
    }
}