using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFSM
{
    [Serializable]
    public class Player
    {
        public String PlayerName;
        public int HP;
        public int ATK;
        public int LUC; //Clamp(0, 100)
        public List<PlayerSkill> playerSkill = new List<PlayerSkill>();
        public int WinTimes;

    }

    [Serializable]
    public class PlayerSkill
    {
        public String SkillName;
        public int Chance;
        public int HP_buff;
        public int ATK_buff;
        public int DEF_buff;
        public int LUC_buff;
        public int Duration;
    }
}

