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
using View;
using Model;
using Global;

namespace Presistent{
    public class TaskQueue:MonoBehaviour
    {
        public Model_Stage _scene;
        public int _current_task_node;

        public List<int> _task_queue = new List<int>();

        public List<Model_PlayerMsg>[] _msg = { new List<Model_PlayerMsg>() , new List<Model_PlayerMsg>() };
        //public List<Model_OpponentMsg> _opponent_msg = new List<Model_OpponentMsg>();
        public int _current_msg_list_index = 0;
        public int _run_msg_flag = 0;

        public void Init_TaskClass(Model_Stage scene) {
            _scene = scene;
        }

        public void Run_Player_Task()
        {
            if (_run_msg_flag == 0) {
                _current_task_node = 0;
                _run_msg_flag = 1;
                StartCoroutine(Player_Task());
            }
        }

        //public void Run_Opponent_Task()
        //{
        //    if (_run_msg_flag == 0)
        //    {
        //        _current_task_node = 0;
        //        _run_msg_flag = 1;
        //        StartCoroutine(Opponent_Task());
        //    }
        //}

        public void Run_Year_Result_Action()
        {
            if (_run_msg_flag == 0) {
                _current_task_node = 0;
                _run_msg_flag = 1;
                StartCoroutine(Year_Result_Task());
            }
        }

        IEnumerator Year_Result_Task()
        {
            while (_msg[0].Count != 0 || _msg[1].Count != 0) {
                while (_msg[_current_msg_list_index].Count > _current_task_node)
                {
                    Model_PlayerMsg msg = _msg[_current_msg_list_index][_current_task_node];
                    float time = msg._msg_time;
                    Decode_Msg(msg);

                    yield return new WaitForSeconds(time);
                    _current_task_node++;
                }

                _msg[_current_msg_list_index].Clear();

                //Debug.Log(_msg[0].Count + ":" + _msg[1].Count);

                if (_current_msg_list_index == 0)
                {
                    if (_msg[1].Count > 0)
                    {
                        _current_msg_list_index = 1;
                        _current_task_node = 0;
                    }
                }
                else
                {
                    if (_msg[0].Count > 0)
                    {
                        _current_msg_list_index = 0;
                        _current_task_node = 0;
                    }
                }
            }
            _run_msg_flag = 0;
        }

        IEnumerator Player_Task()
        {
            while (_msg[0].Count != 0 || _msg[1].Count != 0) {
                while (_msg[_current_msg_list_index].Count > _current_task_node)
                {
                    
                    Model_PlayerMsg msg = _msg[_current_msg_list_index][_current_task_node];
                    float time = msg._msg_time;
                    Decode_Msg(msg);
                    yield return new WaitForSeconds(time);
                    _current_task_node++;
                }

                _msg[_current_msg_list_index].Clear();
                if (_current_msg_list_index == 0)
                {
                    if (_msg[1].Count > 0)
                    {
                        _current_msg_list_index = 1;
                        _current_task_node = 0;
                    }
                }
                else
                {
                    if (_msg[0].Count > 0)
                    {
                        _current_msg_list_index = 0;
                        _current_task_node = 0;
                    }
                }
            }
            _run_msg_flag = 0;
        }

        //IEnumerator Opponent_Task()
        //{
        //    while (_opponent_msg.Count > _current_task_node)
        //    {
        //        Model_OpponentMsg msg = _opponent_msg[_current_task_node];
        //        float time = msg._msg_time;
        //        Decode_Opponent_Msg(msg);
        //        yield return new WaitForSeconds(time);
        //        _current_task_node++;
        //    }
        //    _opponent_msg.Clear();
        //    _run_msg_flag = 0;
        //}

        public void Decode_Msg(Model_PlayerMsg msg) {
            switch (msg._msg_type)
            {
                case 0:
                    Ctrl_Scene1._Instance.Change_Player_Material(msg._msg_object_num,msg._msg_flag, msg._msg_material_cost);
                    break;
                case 1:
                    Ctrl_Scene1._Instance.Set_Card_Action_Over(msg._msg_flag);
                    break;
                case 2:
                    View_Scene1._Instance.Run_Grid_Action(msg._msg_grid_index);
                    break;
                case 3:
                    View_Scene1._Instance.Add_MsgBar(msg._msg_name, msg._msg_info,msg._year_tag,msg._msg_color);
                    break;
                case 4://玩家实体物质交换框
                    //View_Scene1._Instance.Material_Exchange_Frame();
                    break;
                case 5://玩家水晶数量改变
                    Ctrl_Scene1._Instance.Rewrite_Crystal(msg._msg_flag, msg._msg_crystal_cost);
                    break;
                case 6://购买卡牌
                    Ctrl_Scene1._Instance.Add_Card_Bt_Clicked(msg._msg_buy_card_cost);
                    break;
                case 7://获得卡牌
                    Ctrl_Scene1._Instance.Add_Card(msg._msg_flag,msg._msg_get_card_index);
                    break;
                case 8://Hang Out
                    break;
                case 9://提示框
                    View_Scene1._Instance.Tips_Show(msg._msg_info);
                    break;
                case 10://回合开始提示
                    Ctrl_Scene1._Instance.Change_Round_Status(msg._msg_flag);
                    TouchControl._Instance.Game_Canvas_Shield(true);
                    break;
                case 11://回合正式开始
                    View_Scene1._Instance._round_prompt.GetComponent<View_RoundPrompt>().Prompt_Hidden();
                    if (msg._msg_flag == 0 || msg._msg_flag == 1) {
                        TouchControl._Instance.Game_Canvas_Shield(false);
                    }
                    break;
                case 12://事件卡牌收集
                    Ctrl_Scene1._Instance.Update_Grid_Info();
                    break;
                case 13://格子结算动作
                    View_Scene1._Instance.Run_GridOver_Action(msg._msg_grid_index);
                    break;
                case 14://敌方回合
                    View_Scene1._Instance.Change_Black_Eye_Status(false);
                    break;
                case 15://游戏结束
                    View_Scene1._Instance.Game_Result(msg._msg_info,msg._msg_star_num ,msg._msg_color);
                    break;
                case 16://弃牌回合
                    View_Scene1._Instance.Menu_Hidden();
                    Ctrl_Scene1._Instance._player_info._round_discard_card_num = View_Scene1._Instance._card_table.transform.childCount;
                    View_Scene1._Instance.Change_Black_Eye_Status(true);
                    View_Scene1._Instance._discard_card_text.text = Ctrl_Scene1._Instance._player_info._round_discard_card_num + "/" + Ctrl_Scene1._Instance._player_info.crystal_num;
                    break;
                case 17://敌方回合结束
                    Ctrl_Scene1._Instance.Round_Settlement();
                    break;
                case 18://敌方动作
                    Ctrl_Scene1._Instance.Enemy_Use_Card(msg._msg_grid_index,msg._msg_card_index);
                    break;
                case 19:
                    Ctrl_Scene1._Instance.Enemy_Use_Card_Over();
                    break;
                case 20://一回合游戏结算
                    Ctrl_Scene1._Instance.Round_Over_Msg();
                    break;
                case 21://新手教程
                    //Ctrl_Scene1._Instance.Guidence();
                    break;
                case 22://添加卡牌结束
                    Ctrl_Scene1._Instance.Add_Card_Over();
                    break;
                default:
                    break;
            }
        }

        public void Decode_Opponent_Msg(Model_OpponentMsg msg)
        {
            switch (msg._msg_type)
            {
                case 0:
                    if (msg._msg_flag == 0)
                    {
                        Ctrl_Scene1._Instance._opponent_info._opponent_material -= msg._msg_material_cost;
                    }
                    else
                    {
                        Ctrl_Scene1._Instance._opponent_info._opponent_material += msg._msg_material_cost;
                    }
                    break;
                case 1:
                    
                    break;
                default:
                    break;
            }
        }

        public void Add_DiscardCard_Msg(int time)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_DiscardCard_Msg(time);
            Add_Msg(msg);
        }

        /// <summary>
        /// flag = 1失去物质，flag = 2获得物质
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="cost"></param>
        /// <param name="time"></param>
        public void Add_Player_Material_Msg(int object_num,int flag,int cost, int time)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_Player_Material_Msg(object_num,flag,cost, time);
            Add_Msg(msg);
        }

        /// <summary>
        /// flag = 1失去水晶，flag = 2获得水晶
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="cost"></param>
        /// <param name="time"></param>
        public void Add_Player_Crystal_Msg(int flag, int cost, int time)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_Player_Crystal_Msg(flag, cost, time);
            Add_Msg(msg);
        }

        public void Add_Msg(Model_PlayerMsg msg) {
            if (_current_msg_list_index == 0 && _run_msg_flag == 1)
            {
                _msg[1].Add(msg);
            }
            else
            {
                _msg[0].Add(msg);
            }
        }


        /// <summary>
        /// flag = 0卡牌打出失败，flag = 1卡牌打出
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="time"></param>
        public void Add_Set_Card_Over_Msg(int flag, float time)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_Set_Card_Over_Msg(flag,time);
            Add_Msg(msg);
        }

        public void Add_Grid_Msg(int index , float time) {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_Grid_Msg(index,time);
            Add_Msg(msg);
        }

        public void Add_Material_Exchange_Msg()
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_Material_Exchange_Msg(0);
            Add_Msg(msg);
        }

        /// <summary>
        /// flag = 1事件卡，flag = 2实体卡
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="index"></param>
        /// <param name="time"></param>
        public void Add_AddCard_Msg(int flag,int index,float time) {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_Get_Card_Msg(flag,index,time);
            Add_Msg(msg);
        }

        public void Add_BuyCardBt_Msg(int cost, float time)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_BuyCard_Msg(cost, time);
            Add_Msg(msg);
        }

        public void Add_Tips_Msg(string info, float time)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_Taps_Msg(info, time);
            Add_Msg(msg);
        }

        public void Add_RoundPromptShow_Msg(int flag)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_RoundPromptShow_Msg(flag,Scene1Parameter.ROUNDINTTIME);
            Add_Msg(msg);
        }

        public void Add_RoundStartAction_Msg(int flag)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_RoundStartAction_Msg(flag,0);
            Add_Msg(msg);
        }

        public void Add_CollectEventCard_Msg(float time)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_CollectEventCard_Msg(time);
            Add_Msg(msg);
        }

        public void Add_GridOver_Msg(int index,float time)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_GridOver_Msg(index,time);
            Add_Msg(msg);
        }

        public void Add_RoundOver_Msg()
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_RoundOver_Msg(0);
            Add_Msg(msg);
        }

        public void Add_EnemyRound_Msg(float time)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_EnemyRound_Msg(time);
            Add_Msg(msg);
        }

        /// <summary>
        /// info为胜利信息，type = 0失败，1胜利
        /// </summary>
        /// <param name="info"></param>
        /// <param name="type"></param>
        /// <param name="time"></param>
        public void Add_GameOver_Msg(string info,int type,int star_num)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_GameOver_Msg(info,type,star_num,0);
            Add_Msg(msg);
        }

        public void Add_Notify_Msg(string name, string info,int year,Color color ,float time)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_Notify_Msg(name, info, year,color, time);
            Add_Msg(msg);
        }

        public void Add_HangOut_Msg(float time)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_HangOut_Msg(time);
            Add_Msg(msg);
        }

        public void Add_EnemyRoundOver_Msg(float time)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_EnemyRoundOver_Msg(time);
            Add_Msg(msg);
        }

        public void Add_EnemyAction_Msg(int grid_index,int card_index)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_EnemyAction_Msg(grid_index,card_index,0.7f);
            Add_Msg(msg);
        }

        public void Add_EnemyActionOver_Msg()
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_EnemyActionOver_Msg(0.3f);
            Add_Msg(msg);
        }

        public void Add_Guidence_Msg()
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_Guidence_Msg(0);
            Add_Msg(msg);
        }

        public void Add_AddCardOver_Msg(float time)
        {
            Model_PlayerMsg msg = new Model_PlayerMsg();
            msg.Init_AddCardOver_Msg(0);
            Add_Msg(msg);
        }
    }
}
