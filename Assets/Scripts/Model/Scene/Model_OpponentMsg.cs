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
   public class Model_OpponentMsg
   {
        public int _msg_type;

        public string _msg_name = "";
        public string _msg_info = "";

        public int _msg_grid_index;
        

        public int _msg_material_cost;
        public int _msg_crystal_cost;

        public int _msg_card_type;
        public int _msg_card_index;

        public Color _msg_color;

        public int _msg_flag;
        public int _year_tag;

        public float _msg_time;

        public void Init_Player_Material_Msg(int flag, int cost, float time)
        {
            _msg_type = 0;
            _msg_flag = flag;
            _msg_material_cost = cost;
            _msg_time = time;
        }

        public void Init_Set_Card_Msg(int flag,int card_type,int card_index,int grid_index,float time)
        {
            _msg_type = 1;
            _msg_flag = flag;
            _msg_card_type = card_type;
            _msg_card_index = card_index;
            _msg_grid_index = grid_index;
            _msg_time = time;
        }

        public void Init_Set_Card_Over_Msg(int flag, int card_type, int card_index, int grid_index, float time)
        {
            _msg_type = 2;
            _msg_time = time;
        }
    }
}
