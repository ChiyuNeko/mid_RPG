using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameFSM
{
    public class UIManager : MonoBehaviour
    {
        public GameManager gameManager;

        public Text PlayerA_UI;
        public Text PlayerB_UI;
        public Text GameLog;
        public Text Info_UI;
        public Text State;
        public GameObject Game_UI;
        public GameObject DebugOverlay;
        public GameObject[] ScoreLight;

        bool trigger;

        void Start()
        {
        }

        
        void Update()
        {
            PlayerA_UI.text = gameManager.player[0].PlayerName + "\n" + 
            "HP:" + gameManager.player[0].HP + "\n" +
            "ATK:" + gameManager.player[0].ATK + "\n" +
            "LUC:" + gameManager.player[0].LUC + "\n";


            PlayerB_UI.text = gameManager.player[1].PlayerName + "\n" + 
            "HP:" + gameManager.player[1].HP + "\n" +
            "ATK:" + gameManager.player[1].ATK + "\n" +
            "LUC:" + gameManager.player[1].LUC + "\n";

            DebugOverlay.GetComponentInChildren<Text>().text = "Rounds:" + gameManager.GameRounds.ToString() + "\n" + "State:"  + gameManager.State + "\n\n" +
            gameManager.player[0].PlayerName + "\n" + 
            "HP:" + gameManager.player[0].HP + "\n" +
            "ATK:" + gameManager.player[0].ATK + "\n" +
            "LUC:" + gameManager.player[0].LUC + "\n\n" +
            gameManager.player[1].PlayerName + "\n" + 
            "HP:" + gameManager.player[1].HP + "\n" +
            "ATK:" + gameManager.player[1].ATK + "\n" +
            "LUC:" + gameManager.player[1].LUC;

            if(Input.GetKeyDown(KeyCode.Tab) && !trigger)
            {
                DebugOverlay.SetActive(true);
                trigger = true;
            }
            else if(Input.GetKeyUp(KeyCode.Tab))
            {
                DebugOverlay.SetActive(false);
                trigger = false;
            }
            
        }
    }
}
