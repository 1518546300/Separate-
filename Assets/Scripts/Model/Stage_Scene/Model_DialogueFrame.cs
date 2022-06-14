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
   public class Model_DialogueFrame
   {
        public int _choice_frame_num;
        public int[] _choice_frame_next_dialogue_num = new int[4];
        public string[] _choice_frame_next_dialogue_str = new string[4];

        public void Write_Choice_Frame_Dialogue(string raw_dialogue) {
            string[] choice_dialogue = raw_dialogue.Split('|');
            _choice_frame_num = choice_dialogue.Length;
            for (int i = 0; i < _choice_frame_num; i++) {
                string[] raw_temp = choice_dialogue[i].Split('&');
                _choice_frame_next_dialogue_str[i] = raw_temp[0];
                _choice_frame_next_dialogue_num[i] = int.Parse(raw_temp[1]);
            }
        }

        

   }
}
