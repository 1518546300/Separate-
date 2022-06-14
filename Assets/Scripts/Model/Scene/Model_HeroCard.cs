/***
*
*   Title:  "Separate.1" 项目开发
*	
*		
*
*   Description:
*      [描述]
*   Date:2020
*
*   Version:0.1
*
*   Modify Recorder:
*
*   开发者：Hujj
*
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model{
   public class Model_HeroCard
   {
        public int _card_holder;

        public int _card_uid;    //卡牌uid
        public string _name;
        public string _decribe;
        public int _crystal_cost;

        public int _willpower;

        public string _states;
        public string _attr;

        public Sprite _pic;
    }
}
