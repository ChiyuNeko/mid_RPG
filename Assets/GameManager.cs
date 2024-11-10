using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameFSM
{
    public enum STATE
    {
        GameStart,
        Player_A_Round,
        Player_B_Round,
        Cauculate,
        GameOver
    }

    public class GameManager : MonoBehaviour
    {
        private GameStates gameStates;

        public STATE State;
        public List<Player> player = new List<Player>();
        
        bool Trigger;
        void Start()
        {
            GoToState(new GameStart(this));
        }

        void Update()
        {
            if(Trigger)
            {
                gameStates?.OnTrigger();
                Trigger = false;
            }
            gameStates?.InState();
        }

        public void GoToState(GameStates target)
        {
            gameStates?.ExitTrigger();
            gameStates = target;
            Trigger = true;
        }

        public void Init()
        {
            player[0].HP = 10;
            player[1].HP = 10;
        }
    }
}
