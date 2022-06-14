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
using Global;

namespace Model{
   public class Model_Funcs
   {
        public Ctrl_Scene1 _scene;

        public Model_EventCard _card_wait_set;

        public int _grid_index;

        public void Init(Ctrl_Scene1 scene) {
            _scene = scene;
        }

        /// <summary>
        /// 0.伤害卡（即时），1.伤害卡（延时），2.增益卡（即时）3.增益卡（延时）4.效果卡5.获取敌方物质卡
        /// </summary>
        /// <param name="card"></param>
        public void Event_Card_Func(int grid_index,Model_EventCard card)
        {
            _grid_index = grid_index;
            _card_wait_set = card;
            Model_Grid grid = _scene._scene_info._grids[grid_index];
            switch (_card_wait_set._card_class)
            {
                case 0://即时伤害卡
                    Intime_Event_Card(_card_wait_set);
                    break;
                case 1://延时效果卡
                    grid.Add_Choice_Message();
                    grid.Add_Status(
                                                                           _card_wait_set._status_uid,
                                                                           1,
                                                                           _card_wait_set._status_name,
                                                                           _card_wait_set._status_describe,
                                                                           _card_wait_set._effect,
                                                                           _card_wait_set._time,
                                                                           _card_wait_set._status_pic
                                                                           );
                    break;
                case 2://延时卡
                    grid.Add_Choice_Message();
                    grid.Add_Status(
                                                                           _card_wait_set._status_uid,
                                                                           1,
                                                                           _card_wait_set._status_name,
                                                                           _card_wait_set._status_describe,
                                                                           _card_wait_set._effect,
                                                                           _card_wait_set._time,
                                                                           _card_wait_set._status_pic
                                                                           );
                    Intime_Magic_Card(_card_wait_set);
                    break;    
                default:
                    Debug.Log("error!!!");
                    break;
            }
            _scene._task.Add_Grid_Msg(grid_index,_scene._scene_info._grids[grid_index].Grid_Action_Time());
        }

        public void Intime_Magic_Card(Model_EventCard card)
        {
            //int damage = 0;
            //Model_Grid grid = _scene._scene_info._grids[_grid_index];
            //if (card._status_class == 1)
            //{
            //    damage = int.Parse(card._card_damage.Split('/')[1].Split(',')[1]);
            //    if (damage > _scene._scene_info._grids[_grid_index]._material_own_num) {
            //        damage = _scene._scene_info._grids[_grid_index]._material_own_num;
            //    }
            //    grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Hero_Material_Msg(1,card._name, damage));
            //    grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Player_Matreial_Msg(_card_wait_set._card_holder,2, damage));
            //}
            //else if (card._status_class == 2)
            //{
            //    damage = int.Parse(card._card_damage.Split('/')[0].Split(',')[1]);
            //    if (damage > Ctrl_Scene1._Instance._player_info.material_num)
            //    {
            //        damage = Ctrl_Scene1._Instance._player_info.material_num;

            //    }
            //    grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Player_Matreial_Msg(_card_wait_set._card_holder, 1,damage));
            //    grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Hero_Material_Msg(2,card._name, damage));
            //}
        }

        public void Intime_Event_Card(Model_EventCard card) {
            int k = 0;
            int flag = 0;

            foreach(string str in card._effect.Split('/')) {
                flag = int.Parse(str.Split(',')[0]);
                if (flag != 0) {
                    Intime_Event_Decode(card._name,k, flag, int.Parse(str.Split(',')[1]));
                }
                k++;
            }
        }

        public void Intime_Event_Decode(string name,int damage_flag,int damage_class_flag,int cost) {
            Model_Grid grid = _scene._scene_info._grids[_grid_index];
            
            switch (damage_flag)
            {
                case 0:
                    grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Player_Matreial_Msg(_card_wait_set._card_holder, damage_class_flag,cost));
                    break;
                case 1:
                    grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Hero_Material_Msg(damage_class_flag,name,cost));
                    break;
                case 2:
                    //Debug.Log(damage_class_flag);
                    if (damage_class_flag == 1 && grid.Find_Status(4))
                    {
                        grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Hero_Damage_Msg(damage_class_flag, name+"+正义", cost*2));
                        grid.Remove_Status(4);
                    }
                    else
                    {
                        grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Hero_Damage_Msg(damage_class_flag, name, cost));
                    }
                    break;
                case 3:
                    if (damage_class_flag == 1)
                    {
                        if (cost > grid._material_own_num)
                        {
                            grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Hero_Material_Msg(1, name, grid._material_own_num));
                            grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Player_Matreial_Msg(_card_wait_set._card_holder, 2, grid._material_own_num));
                        }
                        else
                        {
                            grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Hero_Material_Msg(1, name, cost));
                            grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Player_Matreial_Msg(_card_wait_set._card_holder, 2, cost));
                        }
                    }
                    else if (damage_class_flag == 2) {
                        if (grid._hero_willpower_own > 5)
                        {
                            grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Hero_Damage_Msg(1, name, 5));
                        }
                        else {
                            grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Hero_Damage_Msg(1, name, grid._hero_willpower_own));
                            grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Hero_StatusChange_Msg(3));
                        }
                    }
                    else if (damage_class_flag == 3)
                    {
                        if (cost > grid._material_own_num)
                        {
                            grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Hero_Material_Msg(1, name, grid._material_own_num));
                            grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Hero_Damage_Msg(2, name, grid._material_own_num));
                        }
                        else
                        {
                            grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Hero_Material_Msg(1, name, cost));
                            grid._message_stack[grid._current_msgstack_index].Add(grid.Add_Hero_Damage_Msg(2, name, cost));
                        }
                    }
                    break;
                default:
                    Debug.Log("error!");
                    break;
            }
        }

        public void Mysterious_Card_Func(int num)
        {
            switch (num)
            {
                case 1:
                    Mysterious_Func1();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    Debug.Log("error!!!");
                    break;
            }
        }

        public void Event_Card_Effect_Func0()
        {
            //加动画
            //_scene._player_info.Rewrite_Own_Material(-_card_wait_set._card_material_damage);
            //_scene.Attack_Hero(GlobalParameter.CHOICEGRIDINDEX,_card_wait_set._card_willpower_damage);

            //_scene._scene_info._grids[GlobalParameter.CHOICEGRIDINDEX].Add_Hero_Damage_Msg(_card_wait_set._card_willpower_damage);
        }

        public void Event_Card_Effect_Func2()
        {
            //_scene._scene_info._grids[GlobalParameter.CHOICEGRIDINDEX].Add_Hero_Heal_Msg(_card_wait_set._card_willpower_damage);
        }

        public void Event_Card_Effect_Func3()
        {
            //Sprite status_pic = Ctrl_Scene1._Instance._res._sprites.event_status_sprites[_card_wait_set._status_index - 1];
            //_scene._scene_info._grids[GlobalParameter.CHOICEGRIDINDEX].Add_Status(_card_wait_set._card_status_name,
            //                                                               _card_wait_set._card_status_describe,
            //                                                               _card_wait_set._card_willpower_damage,
            //                                                               _card_wait_set._card_material_damage,
            //                                                               _card_wait_set._card_status_time,
            //                                                               status_pic
            //                                                               );
        }

        public void Event_Card_Effect_Func4()
        {
            //_scene._scene_info._grids[GlobalParameter.CHOICEGRIDINDEX].Add_Hero_Material_Msg(1,_card_wait_set._card_material_damage);
        }

        public void Event_Card_Effect_Func5()
        {
            //_scene._scene_info._grids[GlobalParameter.CHOICEGRIDINDEX].Add_Hero_Material_Msg(2, _card_wait_set._card_material_damage);
        }

        public void Mysterious_Func1()
        {
            //_scene._task.Add_Player_Material_Msg(2, _card_wait_set._material_cost, 0);
            //_scene._task.Add_Material_Exchange_Msg();
        }
    }
}
