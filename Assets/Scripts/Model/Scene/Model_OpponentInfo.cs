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

using Control;

namespace Model{
   public class Model_OpponentInfo
   {
        public string _opponent_name;
        public string _opponent_dialogue;

        public int _opponent_avatar_num;

        public int _opponent_use_card_region;
        public int _opponent_material;

        public List<int> _grid_occupy = new List<int>();
        //public List<int> _hold_card = new List<int>();
        public List<int> _event_card_libary_player = new List<int>(); //对玩家使用
        public List<int> _event_card_libary_opponent = new List<int>();//敌方对自己使用
        public List<int> _hero_card_libary = new List<int>();

        public int _use_card_index = 0;
        public List<string> _use_card_arr = new List<string>();

        public void Init(Model_Stage scene_info,int _scene_num, SceneConfigObject config)
        {
            _opponent_name = config.opponent_name[_scene_num];
            _opponent_dialogue = config.opponent_dialogue[_scene_num];
            _opponent_avatar_num = int.Parse(config.avatar[_scene_num].Split(',')[1]);

            _opponent_use_card_region = int.Parse(config.opponent_card_number[_scene_num]);
            _opponent_material = int.Parse(config.opponent_material[_scene_num]);

            //string[] str1 = str[2].Split(',');
            //foreach (string num in str1) {
            //    _hold_card.Add(int.Parse(num));
            //}
            string[] str = config.opponent_card_libary[_scene_num].Split('|');
            //string[] str1 = str[0].Split(',');
            
            //foreach (string num in str1)
            //{
            //    if (num != "") {
            //        _hero_card_libary.Add(int.Parse(num));
            //    }
            //}

            string[] str1 = str[0].Split(',');
            foreach (string num in str1)
            {
                if (num != "")
                {
                    _event_card_libary_opponent.Add(int.Parse(num));
                }
            }

            string[] str2 = null;
            if (str.Length > 1) {
                str2 = str[1].Split(',');
                foreach (string num in str2)
                {
                    if (num != "")
                    {
                        _event_card_libary_player.Add(int.Parse(num));
                    }
                }
            }
            Rewrite_Player_Grid(scene_info);
        }

        public void Rewrite_Player_Grid(Model_Stage scene)
        {
            _grid_occupy.Clear();
            for (int i = 0; i < scene._grids.Count; i++)
            {
                if (scene._grids[i]._class_num == 2)
                {
                    _grid_occupy.Add(i);
                }
            }
        }

        public void Rewrite_Own_Material(int num)
        {
            _opponent_material += num;
        }

        public void Opponent_Control_Grid_Summarize() {
            int ret = _grid_occupy.Count;
            List<int> new_grid_occupy_list = new List<int>();
            for (int i = 0;i < Ctrl_Scene1._Instance._scene_info._grids.Count;i++) {
                if (Ctrl_Scene1._Instance._scene_info._grids[i]._class_num == 2) {
                    new_grid_occupy_list.Add(i);
                }
            }
            _grid_occupy = new_grid_occupy_list;
        }
    }
}
