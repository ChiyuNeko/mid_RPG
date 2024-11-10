using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameFSM
{

   public abstract class GameStates
   {
          protected GameManager gameManager;
          public GameStates(GameManager _gameManager)
          {
               gameManager = _gameManager;
               //Debug.Log("state created");
          }
          public abstract void OnTrigger();
          public abstract void InState();
          public abstract void ExitTrigger();
   }
   public class GameStart : GameStates
   {
    
          public GameStart(GameManager gameManager) : base(gameManager) { }
          public override void OnTrigger()
          {
               Debug.Log("GameStart");
          }
          public override void InState()
          {
               if(Input.GetKeyDown(KeyCode.Space))
               {
                    gameManager.Init();
                    int i = Random.Range(0, 2);
                    if(i == 0)
                    {
                         gameManager.GoToState(new Player_A_Round(gameManager));
                    }
                    else
                    {
                         gameManager.GoToState(new Player_B_Round(gameManager));
                    }
               }
          }
        public override void ExitTrigger()
        {

        }

   }

   public class Player_A_Round : GameStates
   {
    
          public Player_A_Round(GameManager gameManager) : base(gameManager) { }
          public override void OnTrigger()
          {
               Debug.Log("Player_A_Round");
          }
          public override void InState()
          {
               if(Input.GetKeyDown(KeyCode.Space))
               {
                    Player Attacker = gameManager.player[0];
                    Player Defender = gameManager.player[1];

                    Attacker.ATK = 2; 
                    Defender.LUC = 1;

                    int judgment = Random.Range(0, 101);

                    foreach(PlayerSkill i in Attacker.playerSkill)
                    {
                         
                         if(judgment < i.Chance)
                         {
                              Debug.Log(Attacker.PlayerName + " 發動 " + i.SkillName);
                              Attacker.ATK += i.ATK_buff;
                              Attacker.HP += i.HP_buff;
                              Attacker.LUC += i.LUC_buff;
                         }
                         else
                         {
                              Debug.Log(Attacker.PlayerName + i.SkillName + " 發動失敗");
                         }

                    }

                    int Criticle = 1;
                    if(Random.Range(0, 101) < Attacker.LUC)
                    {
                         Criticle = 2;
                         Debug.Log(Attacker.PlayerName + " 爆擊!!!");
                    }
                    else
                         Criticle = 1;

                    Defender.HP -= Attacker.ATK * Criticle;

                    if(Defender.HP < 0)
                    {
                         Debug.Log(Defender.PlayerName + " 戰敗");
                         Attacker.WinTimes ++;
                         gameManager.GoToState(new GameEnd(gameManager));
                    }
                    else
                         gameManager.GoToState(new Player_B_Round(gameManager));
               }
          }
          public override void ExitTrigger()
          {

          }

   }

   public class Player_B_Round : GameStates
   {
    
          public Player_B_Round(GameManager gameManager) : base(gameManager) { }
          public override void OnTrigger()
          {
               Debug.Log("Player_B_Round");
          }
          public override void InState()
          {
               if(Input.GetKeyDown(KeyCode.Space))
               {
                    Player Attacker = gameManager.player[1];
                    Player Defender = gameManager.player[0];

                    Attacker.ATK = 2; 
                    Defender.LUC = 1;

                    int judgment = Random.Range(0, 101);

                    foreach(PlayerSkill i in Attacker.playerSkill)
                    {
                         
                         if(judgment < i.Chance)
                         {
                              Debug.Log(Attacker.PlayerName + " 發動 " + i.SkillName);
                              Attacker.ATK += i.ATK_buff;
                              Attacker.HP += i.HP_buff;
                              Attacker.LUC += i.LUC_buff;
                         }
                         else
                         {
                              Debug.Log(Attacker.PlayerName + i.SkillName + " 發動失敗");
                         }

                    }

                    int Criticle = 1;
                    if(Random.Range(0, 101) < Attacker.LUC)
                    {
                         Criticle = 2;
                         Debug.Log(Attacker.PlayerName + " 爆擊!!!");
                    }
                    else
                         Criticle = 1;

                    Defender.HP -= Attacker.ATK * Criticle;

                    if(Defender.HP < 0)
                    {
                         Debug.Log(Defender.PlayerName + " 戰敗");
                         Attacker.WinTimes ++;
                         gameManager.GoToState(new GameEnd(gameManager));
                    }
                    else
                         gameManager.GoToState(new Player_A_Round(gameManager));
               }

          }
          public override void ExitTrigger()
          {

          }

   }

   public class GameEnd : GameStates
   {
    
          public GameEnd(GameManager gameManager) : base(gameManager) { }
          public override void OnTrigger()
          {
               Debug.Log("GameEnd");
          }
          public override void InState()
          {
               if(Input.GetKeyDown(KeyCode.Space))
               {
                    gameManager.GoToState(new GameStart(gameManager));
               }
          }
          public override void ExitTrigger()
          {

          }

   }

}
