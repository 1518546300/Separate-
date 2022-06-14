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
using Control;

namespace Model{
   public class Model_Stage
   {
        public string _game_config;

        public int _scene_year;
        public int _game_start_flag;
        public int _deadline;
        public int _scene_num;
        public int _page_num = 0;

        public List<int> _card_libary = new List<int>();
        public List<Model_Grid> _grids = new List<Model_Grid>();
        public List<Model_PlayerCard> _hero_card = new List<Model_PlayerCard>();

        public void Init_Model_Stage(int page_num,int scene_num,Model_ResourcesManager res)
        {
            _page_num = page_num;
            _scene_num = scene_num;
            _game_start_flag = 1;
            _scene_year = 1;//测试用，第一年

            _game_config = res._scene_config.game_class[_scene_num];

            string[] str = res._scene_config.gamestart_card_libary[_scene_num].Split('/');

            string[] str2 = str[1].Split(',');
            for (int i = 0; i < str2.Length; i++)
            {
                _card_libary.Add(int.Parse(str2[i]) - 1);
            }

            str = res._scene_config.hero_card_config[_scene_num].Split('/');

            foreach (string card in str)
            {
                _hero_card.Add(Create_Hero_Card(card,res));
            }

            str = res._scene_config.grid_array[_scene_num].Split('/');
            for (int i = 0; i < 9; i++)
            {
                string[] info = str[i].Split(',');

                int class_num = int.Parse(info[0]);

                if (class_num != 0)
                {
                    int index = int.Parse(info[1]);
                    int willpower = int.Parse(info[2]);
                    int material = int.Parse(info[3]);
                    Model_Grid grid = new Model_Grid();
                    if (class_num == 1)
                    {
                        grid.Init_Player_Grid(class_num,
                              material,
                              Init_Hero_Card(index),
                              Ctrl_Scene1._Instance._res
                              );
                    }
                    else
                    {
                        grid.Init_Opponent_Grid(class_num,
                              index,
                              willpower,
                              material,
                              Ctrl_Scene1._Instance._res
                              );
                    }
                    
                    grid.Init_Pos(
                                (i % Scene1Parameter.GRIDNUM) * Scene1Parameter.GRIDITL + Scene1Parameter.x,
                                Scene1Parameter.y - ((int)(i / Scene1Parameter.GRIDNUM)) * Scene1Parameter.GRIDITL
                                );
                    _grids.Add(grid);
                }
            }
        }

        public Model_PlayerCard Create_Hero_Card(string str, Model_ResourcesManager res) {
            Model_PlayerCard card = new Model_PlayerCard();
            string[] arr = str.Split('[');
            string[] info = arr[0].Split(',');
            int card_index = int.Parse(info[0]);
            int crystal_cost = int.Parse(info[1]);
            int willpower = int.Parse(info[2]);

            //int material_cost = int.Parse(info[3]);
            string states = "1";
            
            string card_attribute = arr[1].Substring(0, arr[1].Length-1);

            card.Init_Hero_Card(
                                1,
                                card_index,
                                res._hero_card.heros_name[card_index],
                                res._hero_card.heros_describe[card_index],
                                crystal_cost,
                                willpower,
                                states,
                                card_attribute,
                                res._sprites.hero_sprites[card_index]
                                );
            return card;
        }

        public int Judge_Year_Flag() {
            int ret = 0;
            string[] year_condition = _game_config.Split('/')[0].Split(',');

            int pre_year = int.Parse(year_condition[0]);
            int last_year = int.Parse(year_condition[1]);
            if (_scene_year < pre_year)
            {
                ret = 0;
            }
            else if (_scene_year >= pre_year && _scene_year < last_year)
            {
                ret = 1;
            }
            else {
                ret = 2;
            }
            return ret;
        }

        public Model_HeroCard Init_Hero_Card(int num) {
            Model_HeroCard _ret_card = new Model_HeroCard();
            foreach(Model_PlayerCard card in _hero_card) {
                if (card._hero_card._card_uid == num) {
                    _ret_card = card._hero_card;
                    break;
                }
            }
            return _ret_card;
        }

        public void Grid_Rewrite(int grid_index,int grid_class,Model_HeroCard hero_card,Model_ResourcesManager res)
        {
            _grids[grid_index].Clear_Attribute();
            _grids[grid_index].Clear_Status();
            _grids[grid_index]._message_stack[_grids[grid_index]._current_msgstack_index].Clear();
            _grids[grid_index].Init_Player_Grid(
                                    grid_class,
                                    0,
                                    hero_card, 
                                    res
                                    );
        }

        /// <summary>
        /// 更新格子状态，改变英雄属性的计数
        /// </summary>
        public void Round_Start() {
            foreach (Model_Grid grid in _grids) {
                grid._message_stack[grid._current_msgstack_index].Clear();
                foreach (Model_GridStatus status in grid._grid_status_list) {
                    status._time--;
                }

                for (int i = 0; i < grid._grid_status_list.Count; i++) {
                    if (grid._grid_status_list[i]._time <= 0)
                    {
                        grid.Status_Pop(i);
                        i--;
                    }
                }
            }
            Update_Grid_Attribute();
        }

        /// <summary>
        /// 更改英雄属性计数
        /// </summary>
        public void Update_Grid_Attribute()
        {
            for (int i = 0; i < _grids.Count; i++)
            {
                _grids[i].Attr_Deal();
            }
        }

        public List<string> Enemy_Action(int use_card_num_range) {
            List<string> ret = new List<string>();

            Ctrl_Scene1._Instance._opponent_info.Opponent_Control_Grid_Summarize();

            int use_card_num = Random.Range(0, use_card_num_range+1);
            //int use_card_num = 2;
            for (int i = 0; i < use_card_num; i++) {
                ret.Add(Test_Enemy_Use_Card());
            }

            return ret;
        }

        public string Enemy_Use_Card() {
            int card_class = Random.Range(0, 2);
            int card_index = 0;
            int grid_index = 0;
            if (card_class == 1) {
                card_index = Ctrl_Scene1._Instance._opponent_info._hero_card_libary[Random.Range(0, Ctrl_Scene1._Instance._opponent_info._hero_card_libary.Count)];
                grid_index = Ctrl_Scene1._Instance._opponent_info._grid_occupy[Random.Range(0, Ctrl_Scene1._Instance._opponent_info._grid_occupy.Count)];
            }
            else {
                card_index = Ctrl_Scene1._Instance._opponent_info._event_card_libary_player [Random.Range(0, Ctrl_Scene1._Instance._opponent_info._event_card_libary_player.Count)];
                grid_index = Ctrl_Scene1._Instance._player_info._grid_occupy[Random.Range(0, Ctrl_Scene1._Instance._player_info._grid_occupy.Count)];
            }
            string ret_str = card_class.ToString() + ',' + card_index.ToString() + ',' + grid_index.ToString();
            return ret_str;
        }

        public string Test_Enemy_Use_Card() {
            int flag = 0;
            int card_index = 0;
            
            if (Ctrl_Scene1._Instance._opponent_info._event_card_libary_opponent.Count > 0)
            {
                Ctrl_Scene1._Instance._opponent_info.Opponent_Control_Grid_Summarize();
                if (Ctrl_Scene1._Instance._opponent_info._grid_occupy.Count > 0)
                {
                    flag = Random.Range(0, 4);
                }
                else {
                    flag = 1;
                }

                if (flag <= 1)
                {
                    card_index = Ctrl_Scene1._Instance._opponent_info._event_card_libary_player[Random.Range(0, Ctrl_Scene1._Instance._opponent_info._event_card_libary_player.Count)];
                }
                else
                {
                    card_index = Ctrl_Scene1._Instance._opponent_info._event_card_libary_opponent[Random.Range(0, Ctrl_Scene1._Instance._opponent_info._event_card_libary_opponent.Count)];
                }
            }
            else
            {
                card_index = Ctrl_Scene1._Instance._opponent_info._event_card_libary_player[Random.Range(0, Ctrl_Scene1._Instance._opponent_info._event_card_libary_player.Count)];

            }
            
            int card_use_object = int.Parse(Ctrl_Scene1._Instance._res._event_card.card_use_object[card_index]);
            int grid_index = 0;
            if (card_use_object == 0)
            {
                grid_index = Ctrl_Scene1._Instance._opponent_info._grid_occupy[Random.Range(0, Ctrl_Scene1._Instance._opponent_info._grid_occupy.Count)];
            }
            else if (card_use_object == 1)
            {
                grid_index = Ctrl_Scene1._Instance._player_info._grid_occupy[Random.Range(0, Ctrl_Scene1._Instance._player_info._grid_occupy.Count)];
            }


            string ret_str = card_index.ToString() + ',' + grid_index.ToString();
            return ret_str;
        }
    }
}
