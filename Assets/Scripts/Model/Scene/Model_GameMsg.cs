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
   public class Model_GameMsg
   {
        public int[] _game_result = { 0,0 };//0继续游戏，1胜利，2失败,后面一维为胜利获得的星数
        public int _star_num;

        public Model_GameMsg() {
            _star_num = 0;
        }
   }
}
