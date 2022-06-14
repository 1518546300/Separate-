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
   public class Model_GridAttribute
   {
        public string _name;    //状态名字
        public int _class_num;    //状态信息        
        public int _time;
        public int _event_index;

        public int _attr_state; // = 0无法获得卡牌，= 1正常状态

        public int _rest_time;
        
        public Sprite _pic;

        public void Init(int event_index, string event_name,int time, Sprite pic_attr)
        {
            _name = event_name;
            _event_index = event_index;
            _time = time;
            _pic = pic_attr;
            _attr_state = 1;
            _rest_time = _time;
        }

        public void Rewrite_Time()
        {
            _rest_time--;
            if (_rest_time == 0) {
                //添加卡牌
                _rest_time = _time;
            }
        }
    }
}
