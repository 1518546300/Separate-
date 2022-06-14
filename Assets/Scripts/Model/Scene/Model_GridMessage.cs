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

using Global;

namespace Model{
   public class Model_GridMessage
   {
        public int _msg_hero_index;
        public int _msg_type; //信息类型
        public string _msg_name;

        public int _msg_effect_type;
        public int _msg_effect_num;

        public int _msg_hero_change_status;

        public int _msg_flag;
        public int _msg_object_num;
        public string _msg_info;

        public float _msg_time;
        public Color _msg_info_color;

        public void Init_Set_Hero_Msg(string info,int hero_index,float time) {
            _msg_type = 0;
            _msg_info = info;
            _msg_hero_index = hero_index;
            _msg_info_color = GlobalParameter.TEXTCOLOR[1];
            _msg_time = time;
        }

        public void Init_Miss_Msg(string info,float time)
        {
            _msg_type = 1;
            _msg_info = info;
            _msg_info_color = GlobalParameter.TEXTCOLOR[0];
            _msg_time = time;
        }

        public void Init_Update_Hero_Msg(int hero_index,string info, float time)
        {
            _msg_type = 2;
            _msg_info = info;
            _msg_hero_index = hero_index;
            _msg_info_color = GlobalParameter.TEXTCOLOR[1];
            _msg_time = time;
        }

        public void Init_Hero_Damage_Msg(int flag,string name,int num, float time)
        {
            _msg_type = 3;
            _msg_info = "放置成功";
            _msg_name = name;
            _msg_flag = flag;
            _msg_effect_num = num;
            _msg_time = time;
        }

        public void Init_Hero_Material_Msg(int flag,string name,int num, float time)
        {
            _msg_type = 4;
            _msg_info = "放置成功";
            _msg_name = name;
            _msg_flag = flag;
            _msg_effect_num = num;
            _msg_time = time;
        }

        public void Init_Hero_Magic_Msg(float time)
        {
            _msg_type = 8;
            _msg_info = "放置成功";
            
            _msg_time = time;
        }

        public void Init_GridAction_Prepare_Msg(float time)
        {
            _msg_type = 9;
            _msg_time = time;
        }

        public void Init_Player_Material_Msg(int object_num,int flag,int num,float time)
        {
            _msg_type = 8;
            _msg_flag = flag;
            _msg_object_num = object_num;
            _msg_effect_num = num;
            _msg_time = time;
        }

        public void Init_Grid_Choice_Msg()
        {
            _msg_type = 9;
            _msg_time = 0.8f;
        }

        public void Init_Hero_StatusChange_Msg(int grid_class)
        {
            _msg_type = 10;
            _msg_hero_change_status = grid_class;
            _msg_time = 0f;
        }
    }
}
