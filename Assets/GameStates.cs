using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
               gameManager.State = STATE.GameStart;
               Debug.Log("GameStart");
               gameManager.uIManager.GameLog.text = "";
               gameManager.Init();
               if((gameManager.player[1].WinTimes == 0) && (gameManager.player[0].WinTimes == 0))
               {
                    foreach(GameObject i in gameManager.uIManager.ScoreLight)
                    {
                         i.GetComponent<Renderer>().material.color = Color.white;
                    }
               }
               gameManager.uIManager.Game_UI.gameObject.SetActive(true);
               gameManager.uIManager.Game_UI.GetComponentInChildren<Text>().text = "GameStart\n- Press Space to Continue";
          }
          public override void InState()
          {
               if(Input.GetKeyDown(KeyCode.Space))
               {
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
               gameManager.uIManager.Game_UI.gameObject.SetActive(false);
        }

   }

   public class Player_A_Round : GameStates
   {
    
          public Player_A_Round(GameManager gameManager) : base(gameManager) { }
          public override void OnTrigger()
          {
               gameManager.State = STATE.Player_A_Round;
               Debug.Log("Player_A_Round");
               gameManager.uIManager.GameLog.text += gameManager.player[0].PlayerName + " 的回合\n";
               gameManager.uIManager.State.text = gameManager.player[0].PlayerName + " 的回合";
               
               gameManager.GameRounds++;
          }
          public override void InState()
          {
               if(gameManager.GameRounds > 10)
               {
                    gameManager.GoToState(new GameEnd(gameManager));
               }
               Player Attacker = gameManager.player[0];
               Player Defender = gameManager.player[1];

               if(Input.GetKeyDown(KeyCode.A))
               {
                    gameManager.uIManager.GameLog.text += Attacker.PlayerName + " 發動 " + gameManager.file_IO.Skills[1][0] + "\n";
                    Attacker.HP += int.Parse(gameManager.file_IO.Skills[1][1]);
                    Attacker.ATK += int.Parse(gameManager.file_IO.Skills[1][2]);
                    Attacker.LUC += int.Parse(gameManager.file_IO.Skills[1][3]);
                    gameManager.GoToState(new Player_B_Round(gameManager));
                    gameManager.uIManager.Info_UI.text = Attacker.PlayerName + " 發動 " + gameManager.file_IO.Skills[1][0] + "\n" + Attacker.PlayerName + " 恢復了" + int.Parse(gameManager.file_IO.Skills[1][1]) + "HP";
               }

               if(Input.GetKeyDown(KeyCode.W))
               {
                    gameManager.uIManager.GameLog.text += Attacker.PlayerName + " 發動 " + gameManager.file_IO.Skills[2][0] + "\n";
                    gameManager.uIManager.Info_UI.text = Attacker.PlayerName + " 發動 " + gameManager.file_IO.Skills[2][0] + "\n" + Attacker.PlayerName + " ATK與LUC增加了";
                    Attacker.HP += int.Parse(gameManager.file_IO.Skills[2][1]);
                    Attacker.ATK += int.Parse(gameManager.file_IO.Skills[2][2]);
                    Attacker.LUC += int.Parse(gameManager.file_IO.Skills[2][3]);
                    
                    int Criticle = 1;
                    if(Random.Range(0, 101) < Attacker.LUC)
                    {
                         Criticle = 2;
                         Debug.Log(Attacker.PlayerName + " 爆擊!!!");
                         gameManager.uIManager.GameLog.text += Attacker.PlayerName + " 爆擊!!!\n";
                         gameManager.uIManager.Info_UI.text =Attacker.PlayerName + " 爆擊!!!\n" + Attacker.PlayerName + " 造成了" + Attacker.ATK * Criticle + "傷害";
                    }
                    else

                    {
                         Criticle = 1;
                         gameManager.uIManager.Info_UI.text = Attacker.PlayerName + " 造成了" + Attacker.ATK * Criticle + "傷害";
                    }

                    Defender.HP -= Attacker.ATK * Criticle;

                    if(Defender.HP <= 0)
                    {
                         Debug.Log(Defender.PlayerName + " 戰敗");
                         Defender.HP = 0;
                         gameManager.uIManager.GameLog.text += Defender.PlayerName + " 戰敗\n";
                         gameManager.uIManager.Game_UI.gameObject.SetActive(true);
                         gameManager.uIManager.Game_UI.GetComponentInChildren<Text>().text = Defender.PlayerName + " K.O";
                         gameManager.uIManager.GameLog.text += Defender.PlayerName + " K.O\n";
                         Attacker.WinTimes ++;
                         gameManager.uIManager.ScoreLight[1 + Attacker.WinTimes].GetComponent<Renderer>().material.color = Color.green;
                         gameManager.GoToState(new GameEnd(gameManager));
                    }
                    else
                         gameManager.GoToState(new Player_B_Round(gameManager));
               }

               if(Input.GetKeyDown(KeyCode.D))
               {
                    gameManager.uIManager.GameLog.text += Attacker.PlayerName + " 發動 " + gameManager.file_IO.Skills[3][0] + "\n";
                    Attacker.HP += int.Parse(gameManager.file_IO.Skills[3][1]);
                    Attacker.ATK += int.Parse(gameManager.file_IO.Skills[3][2]);
                    Attacker.LUC += int.Parse(gameManager.file_IO.Skills[3][3]);
                    int Criticle = 1;
                    if(Random.Range(0, 101) < Attacker.LUC)
                    {
                         Criticle = 2;
                         Debug.Log(Attacker.PlayerName + " 爆擊!!!");
                         gameManager.uIManager.GameLog.text += Attacker.PlayerName + " 爆擊!!!\n";
                         gameManager.uIManager.Info_UI.text =Attacker.PlayerName + " 爆擊!!!\n" + Attacker.PlayerName + " 造成了" + Attacker.ATK * Criticle + "傷害";
                    }
                    else
                    {
                         Criticle = 1;
                         gameManager.uIManager.Info_UI.text = Attacker.PlayerName + " 造成了" + Attacker.ATK * Criticle + "傷害";
                    }

                    Defender.HP -= Attacker.ATK * Criticle;

                    if(Defender.HP <= 0)
                    {
                         Debug.Log(Defender.PlayerName + " 戰敗");
                         Defender.HP = 0;
                         gameManager.uIManager.GameLog.text += Defender.PlayerName + " 戰敗\n";
                         gameManager.uIManager.Game_UI.gameObject.SetActive(true);
                         gameManager.uIManager.Game_UI.GetComponentInChildren<Text>().text = Defender.PlayerName + " K.O";
                         gameManager.uIManager.GameLog.text += Defender.PlayerName + " K.O\n";
                         Attacker.WinTimes ++;
                         gameManager.uIManager.ScoreLight[1 + Attacker.WinTimes].GetComponent<Renderer>().material.color = Color.green;
                         gameManager.GoToState(new GameEnd(gameManager));
                    }
                    else
                         gameManager.GoToState(new Player_B_Round(gameManager));
               }


               if(Input.GetKeyDown(KeyCode.S))
               {

                    Attacker.ATK = int.Parse(gameManager.file_IO.playerA[2]); 
                    Defender.LUC = int.Parse(gameManager.file_IO.playerA[3]);

                    int judgment = Random.Range(0, 101);

                    foreach(PlayerSkill i in Attacker.playerSkill)
                    {
                         
                         if(judgment < i.Chance)
                         {
                              Debug.Log(Attacker.PlayerName + " 發動 " + i.SkillName);
                              gameManager.uIManager.GameLog.text += Attacker.PlayerName + " 發動 " + i.SkillName + "\n";
                              Attacker.ATK += i.ATK_buff;
                              Attacker.HP += i.HP_buff;
                              Attacker.LUC += i.LUC_buff;
                         }
                         else
                         {
                              Debug.Log(Attacker.PlayerName + i.SkillName + " 發動失敗");
                              gameManager.uIManager.GameLog.text += Attacker.PlayerName + i.SkillName + " 發動失敗\n";
                         }

                    }

                    int Criticle = 1;
                    if(Random.Range(0, 101) < Attacker.LUC)
                    {
                         Criticle = 2;
                         Debug.Log(Attacker.PlayerName + " 爆擊!!!");
                         gameManager.uIManager.GameLog.text += Attacker.PlayerName + " 爆擊!!!\n";
                         gameManager.uIManager.Info_UI.text =Attacker.PlayerName + " 爆擊!!!\n" + Attacker.PlayerName + " 造成了" + Attacker.ATK * Criticle + "傷害";
                    }
                    else
                    {
                         Criticle = 1;
                         gameManager.uIManager.Info_UI.text = Attacker.PlayerName + " 造成了" + Attacker.ATK * Criticle + "傷害";
                    }

                    Defender.HP -= Attacker.ATK * Criticle;

                    if(Defender.HP <= 0)
                    {
                         Debug.Log(Defender.PlayerName + " 戰敗");
                         Defender.HP = 0;
                         gameManager.uIManager.GameLog.text += Defender.PlayerName + " 戰敗\n";
                         gameManager.uIManager.Game_UI.gameObject.SetActive(true);
                         gameManager.uIManager.Game_UI.GetComponentInChildren<Text>().text = Defender.PlayerName + " K.O";
                         gameManager.uIManager.GameLog.text += Defender.PlayerName + " K.O\n";
                         Attacker.WinTimes ++;
                         gameManager.uIManager.ScoreLight[Attacker.WinTimes - 1].GetComponent<Renderer>().material.color = Color.green;
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
               gameManager.State = STATE.Player_B_Round;
               Debug.Log("Player_B_Round");
               gameManager.uIManager.GameLog.text += gameManager.player[1].PlayerName + " 的回合\n";
               gameManager.uIManager.State.text = gameManager.player[1].PlayerName + " 的回合";
               gameManager.GameRounds++;
          }
          public override void InState()
          {
               if(gameManager.GameRounds > 10)
               {
                    gameManager.GoToState(new GameEnd(gameManager));
               }
               Player Attacker = gameManager.player[1];
               Player Defender = gameManager.player[0];

               if(Input.GetKeyDown(KeyCode.A))
               {
                    gameManager.uIManager.GameLog.text += Attacker.PlayerName + " 發動 " + gameManager.file_IO.Skills[1][0] + "\n";
                    Attacker.HP += int.Parse(gameManager.file_IO.Skills[1][1]);
                    Attacker.ATK += int.Parse(gameManager.file_IO.Skills[1][2]);
                    Attacker.LUC += int.Parse(gameManager.file_IO.Skills[1][3]);
                    gameManager.GoToState(new Player_A_Round(gameManager));
                    gameManager.uIManager.Info_UI.text = Attacker.PlayerName + " 發動 " + gameManager.file_IO.Skills[1][0] + "\n" + Attacker.PlayerName + " 恢復了" + int.Parse(gameManager.file_IO.Skills[1][1]) + "HP";
               }

               if(Input.GetKeyDown(KeyCode.W))
               {
                    gameManager.uIManager.GameLog.text += Attacker.PlayerName + " 發動 " + gameManager.file_IO.Skills[2][0] + "\n";
                    Attacker.HP += int.Parse(gameManager.file_IO.Skills[2][1]);
                    Attacker.ATK += int.Parse(gameManager.file_IO.Skills[2][2]);
                    Attacker.LUC += int.Parse(gameManager.file_IO.Skills[2][3]);
                    int Criticle = 1;
                    if(Random.Range(0, 101) < Attacker.LUC)
                    {
                         Criticle = 2;
                         Debug.Log(Attacker.PlayerName + " 爆擊!!!");
                         gameManager.uIManager.GameLog.text += Attacker.PlayerName + " 爆擊!!!\n";
                         gameManager.uIManager.Info_UI.text =Attacker.PlayerName + " 爆擊!!!\n" + Attacker.PlayerName + " 造成了" + Attacker.ATK * Criticle + "傷害";
                    }
                    else
                    {
                         Criticle = 1;
                         gameManager.uIManager.Info_UI.text = Attacker.PlayerName + " 造成了" + Attacker.ATK * Criticle + "傷害";
                    }

                    Defender.HP -= Attacker.ATK * Criticle;

                    if(Defender.HP <= 0)
                    {
                         Debug.Log(Defender.PlayerName + " 戰敗");
                         Defender.HP = 0;
                         gameManager.uIManager.GameLog.text += Defender.PlayerName + " 戰敗\n";
                         gameManager.uIManager.Game_UI.gameObject.SetActive(true);
                         gameManager.uIManager.Game_UI.GetComponentInChildren<Text>().text = Defender.PlayerName + " K.O";
                         gameManager.uIManager.GameLog.text += Defender.PlayerName + " K.O\n";
                         Attacker.WinTimes ++;
                         gameManager.uIManager.ScoreLight[1 + Attacker.WinTimes].GetComponent<Renderer>().material.color = Color.green;
                         gameManager.GoToState(new GameEnd(gameManager));
                    }
                    else
                         gameManager.GoToState(new Player_A_Round(gameManager));
               }

               if(Input.GetKeyDown(KeyCode.D))
               {
                    gameManager.uIManager.GameLog.text += Attacker.PlayerName + " 發動 " + gameManager.file_IO.Skills[3][0] + "\n";
                    Attacker.HP += int.Parse(gameManager.file_IO.Skills[3][1]);
                    Attacker.ATK += int.Parse(gameManager.file_IO.Skills[3][2]);
                    Attacker.LUC += int.Parse(gameManager.file_IO.Skills[3][3]);
                    int Criticle = 1;
                    if(Random.Range(0, 101) < Attacker.LUC)
                    {
                         Criticle = 2;
                         Debug.Log(Attacker.PlayerName + " 爆擊!!!");
                         gameManager.uIManager.GameLog.text += Attacker.PlayerName + " 爆擊!!!\n";
                         gameManager.uIManager.Info_UI.text =Attacker.PlayerName + " 爆擊!!!\n" + Attacker.PlayerName + " 造成了" + Attacker.ATK * Criticle + "傷害";
                    }
                    else
                    {
                         Criticle = 1;
                         gameManager.uIManager.Info_UI.text = Attacker.PlayerName + " 造成了" + Attacker.ATK * Criticle + "傷害";
                    }

                    Defender.HP -= Attacker.ATK * Criticle;

                    if(Defender.HP <= 0)
                    {
                         Debug.Log(Defender.PlayerName + " 戰敗");
                         Defender.HP = 0;
                         gameManager.uIManager.GameLog.text += Defender.PlayerName + " 戰敗\n";
                         gameManager.uIManager.Game_UI.gameObject.SetActive(true);
                         gameManager.uIManager.Game_UI.GetComponentInChildren<Text>().text = Defender.PlayerName + " K.O";
                         gameManager.uIManager.GameLog.text += Defender.PlayerName + " K.O\n";
                         Attacker.WinTimes ++;
                         gameManager.uIManager.ScoreLight[1 + Attacker.WinTimes].GetComponent<Renderer>().material.color = Color.green;
                         gameManager.GoToState(new GameEnd(gameManager));
                    }
                    else
                         gameManager.GoToState(new Player_A_Round(gameManager));
               }

               if(Input.GetKeyDown(KeyCode.S))
               {

                    Attacker.ATK = int.Parse(gameManager.file_IO.playerB[2]); 
                    Defender.LUC = int.Parse(gameManager.file_IO.playerB[3]);

                    int judgment = Random.Range(0, 101);

                    foreach(PlayerSkill i in Attacker.playerSkill)
                    {
                         
                         if(judgment < i.Chance)
                         {
                              Debug.Log(Attacker.PlayerName + " 發動 " + i.SkillName);
                              gameManager.uIManager.GameLog.text += Attacker.PlayerName + " 發動 " + i.SkillName + "\n";
                              Attacker.ATK += i.ATK_buff;
                              Attacker.HP += i.HP_buff;
                              Attacker.LUC += i.LUC_buff;
                         }
                         else
                         {
                              Debug.Log(Attacker.PlayerName + i.SkillName + " 發動失敗");
                              gameManager.uIManager.GameLog.text += Attacker.PlayerName + i.SkillName + " 發動失敗\n";
                         }

                    }

                    int Criticle = 1;
                    if(Random.Range(0, 101) < Attacker.LUC)
                    {
                         Criticle = 2;
                         Debug.Log(Attacker.PlayerName + " 爆擊!!!");
                         gameManager.uIManager.GameLog.text += Attacker.PlayerName + " 爆擊!!!\n";
                         gameManager.uIManager.Info_UI.text =Attacker.PlayerName + " 爆擊!!!\n" + Attacker.PlayerName + " 造成了" + Attacker.ATK * Criticle + "傷害";
                    }
                    else
                    {
                         Criticle = 1;
                         gameManager.uIManager.Info_UI.text = Attacker.PlayerName + " 造成了" + Attacker.ATK * Criticle + "傷害";
                    }

                    Defender.HP -= Attacker.ATK * Criticle;

                    if(Defender.HP <= 0)
                    {
                         Debug.Log(Defender.PlayerName + " 戰敗");
                         Defender.HP = 0;
                         gameManager.uIManager.GameLog.text += Defender.PlayerName + " 戰敗\n";
                         gameManager.uIManager.Game_UI.gameObject.SetActive(true);
                         gameManager.uIManager.Game_UI.GetComponentInChildren<Text>().text = Defender.PlayerName + " K.O";
                         gameManager.uIManager.GameLog.text += Defender.PlayerName + " K.O\n";
                         Attacker.WinTimes ++;
                         gameManager.uIManager.ScoreLight[1 + Attacker.WinTimes].GetComponent<Renderer>().material.color = Color.green;
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
               gameManager.State = STATE.GameOver;
               Debug.Log("GameEnd");
               gameManager.uIManager.Game_UI.gameObject.SetActive(true);


               if(gameManager.player[0].WinTimes >= 2)
               {
                    gameManager.uIManager.Game_UI.GetComponentInChildren<Text>().text = gameManager.player[0].PlayerName + "獲勝";
                    Debug.Log(gameManager.player[0].PlayerName + "獲勝");
                    gameManager.uIManager.GameLog.text += gameManager.player[0].PlayerName + "獲勝\n";
                    gameManager.player[0].WinTimes = 0;
                    gameManager.player[1].WinTimes = 0;
               }

               if(gameManager.player[1].WinTimes >= 2)
               {
                    gameManager.uIManager.Game_UI.GetComponentInChildren<Text>().text = gameManager.player[1].PlayerName + "獲勝";
                    Debug.Log(gameManager.player[1].PlayerName + "獲勝");
                    gameManager.uIManager.GameLog.text += gameManager.player[1].PlayerName + "獲勝\n";
                    gameManager.player[0].WinTimes = 0;
                    gameManager.player[1].WinTimes = 0;
               }

               if(gameManager.GameRounds >= 10)
               {
                    gameManager.uIManager.Game_UI.GetComponentInChildren<Text>().text = "平手";
                    gameManager.uIManager.GameLog.text += "平手\n";
               }

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
               gameManager.uIManager.Game_UI.gameObject.SetActive(false);
          }

   }

}
