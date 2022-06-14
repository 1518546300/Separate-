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
   public class Model_PlayerMsg
   {
        public int _msg_type;

        public string _msg_name = "";
        public string _msg_info = "";

        public int _msg_grid_index;
        public int _msg_card_index;

        public int _msg_material_cost;
        public int _msg_crystal_cost;

        public int _msg_get_card_index;
        public int _msg_buy_card_cost;

        public Color _msg_color;
        public int _msg_star_num;

        public int _msg_flag;
        public int _msg_object_num;
        public int _year_tag;

        public float _msg_time;

        public void Init_Player_Material_Msg(int object_num,int flag,int cost,float time) {
            _msg_type = 0;
            _msg_flag = flag;
            _msg_object_num = object_num;
            _msg_material_cost = cost;
            _msg_time = time;
        }

        public void Init_Set_Card_Over_Msg(int flag, float time)
        {
            _msg_type = 1;
            _msg_flag = flag;
            _msg_time = time;
        }

        public void Init_Grid_Msg(int index, float time)
        {
            _msg_type = 2;
            _msg_grid_index = index;
            _msg_time = time;
        }

        public void Init_Notify_Msg(string name,string info,int year,Color color, float time)
        {
            _msg_type = 3;
            _msg_name = name;
            _msg_info = info;
            _year_tag = year;
            _msg_color = color;
            _msg_time = time;
        }

        public void Init_Material_Exchange_Msg(float time)
        {
            _msg_type = 4;
            _msg_time = time;
        }

        public void Init_Player_Crystal_Msg(int flag,int cost, float time)
        {
            _msg_type = 5;
            _msg_flag = flag;
            _msg_crystal_cost = cost;
            _msg_time = time;
        }

        public void Init_BuyCard_Msg(int cost,float time)
        {
            _msg_type = 6;
            _msg_buy_card_cost = cost;
            _msg_time = time;
        }

        public void Init_Get_Card_Msg(int flag,int index, float time)
        {
            _msg_type = 7;
            _msg_flag = flag;
            _msg_get_card_index = index;
            _msg_time = time;
        }

        public void Init_HangOut_Msg(float time)
        {
            _msg_type = 8;
            _msg_time = time;
        }

        public void Init_Taps_Msg(string info, float time)
        {
            _msg_type = 9;
            _msg_info = info;
            _msg_time = time;
        }

        public void Init_RoundPromptShow_Msg(int flag,float time)
        {
            _msg_type = 10;
            _msg_flag = flag;
            _msg_time = time;
        }

        public void Init_RoundStartAction_Msg(int flag,float time)
        {
            _msg_type = 11;
            _msg_flag = flag;
            _msg_time = time;
        }

        public void Init_CollectEventCard_Msg(float time)
        {
            _msg_type = 12;
            _msg_time = time;
        }

        public void Init_GridOver_Msg(int index,float time)
        {
            _msg_type = 13;
            _msg_grid_index = index;
            _msg_time = time;
        }

        public void Init_EnemyRound_Msg(float time)
        {
            _msg_type = 14;
            _msg_time = time;
        }

        public void Init_GameOver_Msg(string info,int type,int star_num,float time)
        {
            _msg_type = 15;

            _msg_name = "游戏结束";
            _msg_info = info;
            _msg_star_num = star_num;
            _msg_color = Global.GlobalParameter.TEXTCOLOR[type];
            _msg_time = time;
        }

        public void Init_DiscardCard_Msg(float time)
        {
            _msg_type = 16;
            _msg_time = time;
        }

        public void Init_EnemyRoundOver_Msg(float time)
        {
            _msg_type = 17;
            _msg_time = time;
        }

        public void Init_EnemyAction_Msg(int grid_index,int card_index,float time)
        {
            _msg_type = 18;
            _msg_grid_index = grid_index;
            _msg_card_index = card_index; 
            _msg_time = time;
        }

        public void Init_EnemyActionOver_Msg(float time)
        {
            _msg_type = 19;
            _msg_time = time;
        }


        public void Init_RoundOver_Msg(float time)
        {
            _msg_type = 20;
            _msg_time = time;
        }

        public void Init_Guidence_Msg(float time)
        {
            _msg_type = 21;
            _msg_time = time;
        }

        public void Init_AddCardOver_Msg(float time)
        {
            _msg_type = 22;
            _msg_time = time;
        }
    }
}
