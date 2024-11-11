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
        public File_IO file_IO;
        public UIManager uIManager;
        public List<Player> player = new List<Player>();
        public int GameRounds;
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
            player[0].PlayerName = file_IO.playerA[0];
            player[0].HP = int.Parse(file_IO.playerA[1]);
            player[0].ATK = int.Parse(file_IO.playerA[2]);
            player[0].LUC = int.Parse(file_IO.playerA[3]);

            player[1].PlayerName = file_IO.playerB[0];
            player[1].HP = int.Parse(file_IO.playerB[1]);
            player[1].ATK = int.Parse(file_IO.playerB[2]);
            player[1].LUC = int.Parse(file_IO.playerB[3]);

            GameRounds = 0;

            return;
        }

    }
}
