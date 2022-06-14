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
    public class Model_PlayerInfo
    {
        public string _player_name;
        public string _player_dialogue;

        public int _player_avatar_num;

        public int crystal_num = 0;
        public int crystal_now_own = 0;
        public int material_num = 0;

        public int _buy_card_crystal_cost; //购买卡牌水晶花费
        public List<int> _buy_card_cost = new List<int>(); //购买卡牌物质花费

        public int _start_player_collect_card_num;
        public int _collect_card_num = 1;
        public int _round_player_collect_card_num;

        public int _round_discard_card_num;

        public List<int> _grid_occupy = new List<int>();

        public List<Model_PlayerCard> _cards_own = new List<Model_PlayerCard>();

        public List<int> _event_card_libary = new List<int>();
        //public List<int> _hero_card_libary = new List<int>();


        public List<string> _collect_card_libary = new List<string>();

        public void Init(Model_Stage scene_info,int _scene_num,SceneConfigObject config) {
            crystal_num = int.Parse(config.player_config_crystal[_scene_num]);
            material_num = int.Parse(config.player_config_material[_scene_num]);

            _player_avatar_num = int.Parse(config.avatar[_scene_num].Split(',')[0]);

            crystal_now_own = crystal_num;

            string[] str = config.random_card_libary[_scene_num].Split('/');
            string[] str2 = str[0].Split(',');
            foreach (string num in str2)
            {
                if (num != "") {
                    _event_card_libary.Add(int.Parse(num));
                }
            }
            _round_player_collect_card_num = int.Parse(str[1]);

            str = config.card_cost[_scene_num].Split('/');
            for (int i = 0; i < str.Length; i++)
            {
                _buy_card_cost.Add(int.Parse(str[i]));
            }

            str = config.gamestart_card_libary[_scene_num].Split('/');
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == "") {
                    continue;
                }
                string[] arr = str[i].Split(',');
                switch (i) {
                    case 0:
                        foreach (string k in arr)
                        {
                            _collect_card_libary.Add(1 + "," + k);
                        }
                        break;
                    case 1:
                        //可选择数
                        _start_player_collect_card_num = int.Parse(arr[0]);
                        break;
                    default:
                        break;
                }
                
            }

            _buy_card_crystal_cost = 1;

            Rewrite_Player_Grid(scene_info);

            _player_name = config.player_name[_scene_num];
            _player_dialogue = config.player_dialogue[_scene_num];
        }

        public void Rewrite_Own_Material(int num) {
            material_num += num;
        }

        public void Rewrite_Own_Crystal(int lost) {
            crystal_now_own += lost;
        }

        public void Rewrite_Player_Grid(Model_Stage scene) {
            _grid_occupy.Clear();
            for (int i = 0; i < scene._grids.Count; i++) {
                if (scene._grids[i]._class_num == 1) {
                    _grid_occupy.Add(i);
                }
            }
        }

        public void Card_Push(Model_PlayerCard card)
        {
            _cards_own.Add(card);
        }

        public void Card_Pop(Model_PlayerCard card)
        {
            for (int i = 0; i < _cards_own.Count; i++)
            {
                if (card == _cards_own[i]) {
                    _cards_own.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// 清除上一年收集列表的数据，添加本年可以收集的卡牌进入收集卡牌列表 ,1为事件卡，2为实体卡
        /// </summary>
        /// <param name="scene"></param>
        public void Update_Event_Card_Libary(Model_Stage scene)
        {
            _collect_card_libary.Clear();
            for (int i = 0; i < scene._grids.Count; i++)
            {
                if (scene._grids[i]._grid_attribute_list.Count > 0 && scene._grids[i]._class_num == 1) {
                    scene._grids[i].Update_Attr_State();
                    for (int j = 0; j < scene._grids[i]._grid_attribute_list.Count; j++) {
                        if (scene._grids[i]._grid_attribute_list[j]._attr_state == 1 && scene._grids[i]._grid_attribute_list[j]._rest_time == 1) {
                            int index = scene._grids[i]._grid_attribute_list[0]._event_index;
                            string str = 1 + "," + index;
                            _collect_card_libary.Add(str);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 暂时不可用* ，移除收集卡牌列表指定下标数据
        /// </summary>
        /// <param name="index"></param>
        public void Event_Card_Pop(int index) {
            _collect_card_libary.RemoveAt(index);
        }
    }
}

