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

namespace Model{
   public class Model_EventCard
   {
        public int _card_holder;

        public int _card_use_object;
        public int _card_uid;    //卡牌uid
        public int _card_class;
        public int _card_class_uid;
        public string _name;
        public string _decribe;
        public int _crystal_cost;
        public int _material_cost;

        public string _effect;

        public int _status_uid; //状态uid
        public string _status_name = "";
        public string _status_describe = "";
        public int _time;

        public int _mysterious_type;

        public int _bg_num;

        public Sprite _pic;
        public Sprite _status_pic;

    }
}
