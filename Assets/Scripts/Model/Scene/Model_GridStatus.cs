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
   public class Model_GridStatus
   {
        public int _status_index;
        public string _name;    //状态名字
        public int _status_class;    //状态信息1,一般状态，2，特殊状态
        public string _decribe;           //状态伤害
        public int _material_spend = 0;
        public string _damage;
        public int _time;
        public Sprite _pic;

        public void Status_Init(int status_index,int status_class, string status_name ,string info,string damage,int time, Sprite pic)
        {
            _status_index = status_index;
            _name = status_name;
            _status_class = status_class;
            _decribe = info;
            _damage = damage;
            _time = time;
            _pic = pic;
        }
    }
}
