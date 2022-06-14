/***
*
*   Title:  "Separate.1" 项目开发
*	
*		控制层：scene1
*
*   Description:
*      [描述]
*   Date:2020
*
*   Version:0.1
*
*   Modify Recorder:
*
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using View;
using Global;
using Presistent;
using Model;

namespace Control{
    public class Ctrl_Scene1 : BaseControl
    {
        public static Ctrl_Scene1 _Instance;
        public Ctr_Game game_controllor = new Ctr_Game();

        public AudioClip bg_music;

        public ArrayList CardArry = new ArrayList();

        public bool _card_show_flag = false;
        public bool _card_drag_flag = false;

        public int set_card_color;
        public GameObject card_temp;

        public Model_PlayerInfo _player_info = new Model_PlayerInfo();
        public Model_OpponentInfo _opponent_info = new Model_OpponentInfo();

        public Model_Stage _scene_info = new Model_Stage();

        public Model_PlayerCard _card_wait_set;

        public Model_Funcs _funcs = new Model_Funcs();

        public ConfigManager _game_save_controllor;

        public Model_ResourcesManager _res;
        public TaskQueue _task;

        public int _card_deal_flag;//0出售卡牌，1升级卡牌，2放置卡牌
        public int _round_status;//0玩家回合，1弃牌回合，2地方回合，3结算回合

        void Awake()
        {
            //GlobalParameter.GAMESTATUS = 1;
            _Instance = this;
            //_game_save_control.Read_Json();
            _res = new Model_ResourcesManager();
        }

        void Start()
        {
            _game_save_controllor = ConfigManager._instance;
            game_controllor.Game_Prepare();//游戏开始准备，识别游戏的类型，会调用scene_01_init()等场景准备函数
            Player_Info_Init();
            _funcs.Init(this);
            View_Scene1._Instance.Loading_Curtain_Show(_res._sprites.page_image[_scene_info._scene_num/6], _res._scene_config.saying[_scene_info._scene_num], _res._scene_config.saying_cite[_scene_info._scene_num]);
            Invoke("Game_Start",Scene1Parameter.LOADINGCURTAINSHOWTIME);
        }

        public void Game_Start() {
            View_Scene1._Instance.Loading_Curtain_Close();
            View_Scene1._Instance._round_prompt.GetComponent<View_RoundPrompt>().Change_ShowBox("游戏开始");
            _task.Add_CollectEventCard_Msg(0);

            _task.Run_Year_Result_Action();
        }

        public int Choice_Carsh() {
            if (_player_info.crystal_now_own >= 1)
            {
                View_Scene1._Instance.Change_Carsh_Choice_Status(0);
            }
            else
            {
                View_Scene1._Instance.Change_Carsh_Choice_Status(1);
            }
            return -2;
        }

        public void CardInfoShow(Model_PlayerCard card)
        {
            View_Scene1._Instance.CardInfoShow(card);
        }

        public void Scene_Init() {
            Scene_Info_Init();
        }

        public void Scene_Info_Init() {
            int scene_num = GlobalParameter.SCENENUM;

            //scene_num = 30;

            _scene_info.Init_Model_Stage(GlobalParameter.PAGENUM, scene_num, _res);

            _player_info.Init(_scene_info,scene_num, _res._scene_config);
            _opponent_info.Init(_scene_info,scene_num, _res._scene_config);

            View_Scene1._Instance.Rewrite_Year_Num(_scene_info._scene_year, _scene_info.Judge_Year_Flag());

            AudioManager._Instance.AudioPlay(bg_music);
            View_Scene1._Instance.Grid_Init(_scene_info);

            _task.Init_TaskClass(_scene_info);

            //游戏简介
            //if (_scene_info._scene_num == 0 && _scene_info._scene_year == 1)
            //{
            //    _task.Add_Guidence_Msg();
            //}
        }

        public void Player_Info_Init() {
            View_Scene1._Instance.Player_Info_Init(_player_info, _opponent_info);
        }

        public void Player_Force_Count() {
            _player_info.Rewrite_Player_Grid(_scene_info);
            _opponent_info.Rewrite_Player_Grid(_scene_info);
        }

        /// <summary>
        /// 卡牌放置
        /// </summary>
        /// <param name="card"></param>
        public void Set_Card(GameObject card)
        {
            card_temp = card;
            _card_wait_set = card.transform.GetComponent<View_CardScript>()._card_model;
            string str = "";
            if (GlobalParameter.CHOICEGRIDINDEX == -2)
            {
                //if (card_temp.transform.GetComponent<View_CardScript>()._card_model._card_type == 2)
                //{
                //    str = "是否使用该卡牌交换物质？";
                //    _card_deal_flag = 0;
                //}
                //else
                //{
                //    _task.Add_Set_Card_Over_Msg(0, 0);
                //    _task.Run_Player_Task();
                //    return;
                //}
                str = "是否使用该卡牌交换物质？";
                _card_deal_flag = 0;
            }
            else
            {
                if (_scene_info._grids[GlobalParameter.CHOICEGRIDINDEX]._class_num == 1 && card_temp.transform.GetComponent<View_CardScript>()._card_model._card_type == 2 && card_temp.transform.GetComponent<View_CardScript>()._card_model._hero_card._card_uid == _scene_info._grids[GlobalParameter.CHOICEGRIDINDEX]._hero_uid)
                {
                    Model_Grid grid = _scene_info._grids[GlobalParameter.CHOICEGRIDINDEX];
                    //if (grid._material_own_num >= grid._update_level_need[grid._hero_level - 1])
                    if (grid._material_own_num >= grid._update_level_need && grid._update_level_need != 0)
                    {
                        str = "升级该方格？";
                        _card_deal_flag = 1;
                    }
                    else
                    {
                        str = "升级条件不满足!";
                        if (grid._update_level_need == 0)
                        {
                            str = "已经达到顶级！";
                        }
                        _task.Add_Set_Card_Over_Msg(0, 0);
                        _task.Add_Tips_Msg(str, 0);
                        _task.Run_Player_Task();
                        return;
                    }
                }
                else
                {
                    str = "是否放置该卡牌？";
                    _card_deal_flag = 2;
                }

            }
            View_Scene1._Instance.Set_Hero_Frame_Show(str);
        }

        public void Updata_Grid_Clicked(int grid_index) {
            string str = "";
            GlobalParameter.CHOICEGRIDINDEX = grid_index;
            if (_scene_info._grids[grid_index]._class_num == 1)
            {
                Model_Grid grid = _scene_info._grids[grid_index];
                if (grid._update_level_need == 0 && grid._hero_level == 1)
                {
                    str = "该网格无法升级!";
                    _task.Add_Tips_Msg(str, 0);
                    _task.Run_Player_Task();
                }
                else if (grid._material_own_num >= grid._update_level_need && grid._update_level_need != 0) {
                    str = "升级该方格？";
                    _card_deal_flag = 1;
                    View_Scene1._Instance.Set_Hero_Frame_Show(str);
                }
                else{
                    str = "升级条件不满足!";
                    if (grid._update_level_need == 0)
                    {
                        str = "已经达到顶级！";
                    }
                    _task.Add_Tips_Msg(str, 0);
                    _task.Run_Player_Task();
                }
            }
        }

        /// <summary>
        /// 通知view层展示格子信息
        /// </summary>
        public void Info_Box_Show(int grid_index)
        {
            //正式模式
            //if (_scene_info._grids[grid_index]._class_num == 1)
            //{
            //    View_Scene1._Instance.Info_Box_Show(grid_index, _scene_info._grids[grid_index]);
            //}
            //else
            //{
            //    View_Scene1._Instance.Tips_Show("无法查看敌方阵营信息!!!");
            //}
            //测试模式
            View_Scene1._Instance.Info_Box_Show(grid_index, _scene_info._grids[grid_index]);
        }

        /// <summary>
        /// 通知view层展示格子信息
        /// </summary>
        public void Info_Box_Hidden()
        {
            View_Scene1._Instance.Info_Box_Hidden();
        }

        /// <summary>
        /// 添加卡牌（测试）
        /// </summary>
        public void Buy_Card() {
            int cost = _player_info._buy_card_cost[Scene1Parameter.HEROCOSTNUM];
            
            if (_player_info.material_num >= cost && _player_info.crystal_now_own >= _player_info._buy_card_crystal_cost)
            {
                int index = 0;
                int flag = 1;
                Scene_Music_Control(1);

                _task.Add_Player_Material_Msg(1, 1, cost, 0);
                _task.Add_Player_Crystal_Msg(1, _player_info._buy_card_crystal_cost, 0);
                

                if (Scene1Parameter.HEROCOSTNUM < _player_info._buy_card_cost.Count)
                {
                    Scene1Parameter.HEROCOSTNUM++;
                }
                int random_num = Random.Range(0, 10);
                if (random_num <= 6)
                {
                    flag = 1;
                    index = 39;

                    //flag = Random.Range(1, 2);
                    //if (flag == 1)
                    //{
                    //    index = 39; //购得斩首
                    //}

                    _task.Add_BuyCardBt_Msg(_player_info._buy_card_cost[Scene1Parameter.HEROCOSTNUM], 0);
                    Add_Card_Msg("购得卡牌", flag, index, 0);
                }
                else
                {
                    _task.Add_BuyCardBt_Msg(_player_info._buy_card_cost[Scene1Parameter.HEROCOSTNUM], 0);
                    _task.Add_Tips_Msg("啥都没买到", 0);
                }
            }
            else
            {
                Scene_Music_Control(0);
                _task.Add_Tips_Msg("不满足购买条件", 0);
            }
            _task.Run_Player_Task();
        }


        /// <summary>
        /// 添加事件卡,正式版
        /// </summary>
        public void Random_Add_Event_Card()
        {
            if (_player_info._event_card_libary.Count != 0) {
                int index = _player_info._event_card_libary[Random.Range(0, _player_info._event_card_libary.Count)];
                Add_Card_Msg("抽取卡牌", 1, index, 0.5f);
            }
        }

        public void Add_Card_Msg(string info, int flag, int index, float time) {
            _task.Add_AddCard_Msg(flag, index, time);
            if (flag == 1)
            {
                
                _task.Add_Notify_Msg(info, _res._event_card.card_name[index], _scene_info._scene_year, GlobalParameter.NOTIFYMSGCOLOR[0], 0);
            }
            //else
            //{
            //    _task.Add_Notify_Msg(info, _res._hero_card.heros_name[index], _scene_info._scene_year, GlobalParameter.NOTIFYMSGCOLOR[1], 0);
            //}
        }

        /// <summary>
        /// flag = 1事件卡，flag = 2实体卡
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="index"></param>
        public void Add_Card(int flag, int index) {
            View_Scene1._Instance.AddCard(flag, index);
        }

        /// <summary>
        /// 添加事件卡，测试版
        /// </summary>
        //public void AddEvent_Card(int grid_index,int event_bt_index)
        //{
        //    int temp = _scene_info.Event_Pop(grid_index, event_bt_index)-1;

        //    View_Scene1._Instance.Rewrite_Grid_Info(_scene_info._collect_card_list[grid_index]);
        //    View_Scene1._Instance.AddCard(1, temp , 0);
        //}

        //public List<Sprite> Get_Event_Pic()
        //{
        //    return View_Scene1._Instance.Get_Event_Pic();
        //}

        public void Update_Hero_Action(int num , string name)
        {
            View_Scene1._Instance._grid_info_box.GetComponent<View_GridInfoBoxScript>().InfoBoxHidden();
            Model_Grid grid = _scene_info._grids[GlobalParameter.CHOICEGRIDINDEX];
            if (num == 1)
            {
                grid.Add_IntimeMsg(grid.Add_Hero_Material_Msg(1, "升级", grid._update_level_need));
                grid.Add_IntimeMsg(grid.Add_Update_Hero_Msg());
                
                _task.Add_Notify_Msg("实体升级成功！", name, _scene_info._scene_year, GlobalParameter.NOTIFYMSGCOLOR[2], 0);
            }
            else
            {
                grid.Add_IntimeMsg(grid.Add_Hero_Material_Msg(1, "升级", grid._update_level_need));
                grid.Add_IntimeMsg(grid.Add_Update_Hero_Miss_Msg());
            }
            _task.Add_Grid_Msg(GlobalParameter.CHOICEGRIDINDEX, 0);
        }

        public void Set_Hero_Action(int num)
        {
            Model_Grid grid = _scene_info._grids[GlobalParameter.CHOICEGRIDINDEX];
            if (num == 1)
            {
                _scene_info.Grid_Rewrite(GlobalParameter.CHOICEGRIDINDEX, 1, _card_wait_set._hero_card, _res);
                grid.Add_IntimeMsg(grid.Add_Set_Hero_Msg(_card_wait_set._event_card._card_uid));

                _task.Add_Notify_Msg("实体放置成功！", _card_wait_set._hero_card._name, _scene_info._scene_year, GlobalParameter.NOTIFYMSGCOLOR[1], 0);
            }
            else
            {
                grid.Add_IntimeMsg(grid.Add_Set_Hero_Miss_Msg());
            }
            _task.Add_Grid_Msg(GlobalParameter.CHOICEGRIDINDEX, 0);
        }

        public void Set_Event_Action(int grid_index,Model_PlayerCard card)
        {
            _funcs.Event_Card_Func(grid_index,card._event_card);
        }

        /// <summary>
        /// flag = 0卡牌返回，flag = 1卡牌打出
        /// </summary>
        /// <param name="flag"></param>
        public void Set_Card_Action_Over(int flag) {
            card_temp.transform.GetComponent<View_CardScript>().Card_Set_Ending_Func(flag);
        }

        /// <summary>
        /// 输入参数代表可能性，越小产出的可能性越大，否则越大
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public int HeroSet_Random_Num(float num)
        {
            int top_num = 1;
            if (num >= 0.8)
            {
                top_num = 2;
            }
            else if (num < 0.8 && num >= 0.6)
            {
                top_num = 8;
            }
            else if (num < 0.6 && num >= 0.3)
            {
                top_num = 11;
            }
            else
            {
                top_num = 21;
            }

            int result = Random.Range(1, top_num);

            if (result > 5)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void Rewrite_Crystal(int flag, int cost) {
            if (flag == 1) {
                cost = -cost;
            }
            _player_info.Rewrite_Own_Crystal(cost);
            View_Scene1._Instance.Rewrite_Crystal(_player_info.crystal_now_own);
        }

        /// <summary>
        /// object = 1玩家，= 2 地方，flag = 1失去物质，flag = 2获得物质
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="num"></param>
        public void Change_Player_Material(int object_num,int flag, int num) {
            int final_num = 0;
            if (flag == 1)
            {
                final_num = -num;
            }
            else
            {
                final_num = num;
            }

            if (object_num == 1)
            {
                _player_info.Rewrite_Own_Material(final_num);
                View_Scene1._Instance.Rewrite_Player_Material(flag, num, _player_info.material_num);
            }
            else
            {
                _opponent_info.Rewrite_Own_Material(final_num);
            }
        }

        public Color Get_Grid_Status(int own, int constant)
        {
            Color color_ret = GlobalParameter.GRIDSTATUSCOLORLIB[0];
            float mid = 1 - ((float)(constant - own)) / constant;
            if (mid > 0.7)
            {
                color_ret = GlobalParameter.GRIDSTATUSCOLORLIB[2];
            }
            else if (mid <= 0.7 && mid >= 0.3)
            {
                color_ret = GlobalParameter.GRIDSTATUSCOLORLIB[1];
            }
            else
            {
                color_ret = GlobalParameter.GRIDSTATUSCOLORLIB[0];
            }
            return color_ret;
        }

        /// <summary>
        /// 检测是否满足放置条件，检测持有水晶和物质量是否满足条件
        /// </summary>
        /// <param name="grid_index">暂时未用到</param>
        /// <param name="card"></param>
        public void Choice_Grid(int grid_index, Model_PlayerCard card) {
            int ret = 0;
            if (card._card_type == 1)
            {
                if ((card._event_card._card_use_object == 0 && _scene_info._grids[grid_index]._class_num == 1) || (card._event_card._card_use_object == 1 && _scene_info._grids[grid_index]._class_num == 2))
                {
                    if (_player_info.crystal_now_own >= card._event_card._crystal_cost && _player_info.material_num >= card._event_card._material_cost)
                    {
                        ret = 1;
                    }
                    else
                    {
                        ret = 0;
                    }
                    //if ((card._event_card._card_class == 2 || card._event_card._card_class == 3) && _scene_info._grids[grid_index]._class_num == 2)
                    //{
                    //    ret = 0;
                    //}
                }
                else
                {
                    ret = 0;
                }
                
            }
            else if (card._card_type == 2)
            {
                if (_player_info.crystal_now_own >= card._hero_card._crystal_cost)
                {
                    ret = 1;
                }
                else
                {
                    ret = 0;
                }
            }

            View_Scene1._Instance.Choice_Grid(grid_index, ret);
        }

        public bool Check_Grid_Condition(int index)
        {
            return View_Scene1._Instance.Check_Grid_Condition(index);
        }

        public void Clear_Grid_Status() {
            View_Scene1._Instance.Clear_Grids_Status();
        }

        public void Update_Grid_Info()
        {
            View_Scene1._Instance.Collect_Card();
        }

        public void My_Round_Start(List<string> card_list)
        {
            View_Scene1._Instance.Card_Collect_Board_Hidden();

            Collect_Card(card_list);
            Random_Add_Event_Card();
            _task.Add_AddCardOver_Msg(0);
            _task.Run_Player_Task();

        }

        public void Add_Card_Over() {
            if (_scene_info._game_start_flag != 1)
            {
                _scene_info._scene_year++;
                _scene_info.Round_Start();
            }
            else
            {
                _scene_info._game_start_flag = 2;
            }

            _player_info.crystal_now_own = _player_info.crystal_num;

            View_Scene1._Instance.Menu_Show();
            View_Scene1._Instance.Next_Year();

            _task.Add_RoundPromptShow_Msg(0);
            _task.Add_RoundStartAction_Msg(0);
            _task.Run_Year_Result_Action();
        }

        public void Discard_Card() {
            _player_info._round_discard_card_num--;
            View_Scene1._Instance._discard_card_text.text = _player_info._round_discard_card_num + "/" + _player_info.crystal_num;
            View_Scene1._Instance._black_eye.transform.Find("effect_particle").GetComponent<ParticleSystem>().Play();
        }

        public void Discard_Card_Over()
        {
            if (_player_info._round_discard_card_num > _player_info.crystal_num) {
                View_Scene1._Instance._inquirybox.SetActive(true);
                View_Scene1._Instance._inquirybox.transform.GetComponent<View_InquiryBox>().Discard_Card_Box();
            }
            else
            {
                _task._msg[_task._current_msg_list_index].Clear();
                TouchControl._Instance.Game_Canvas_Shield(true);
                Enemy_Round();
            }
        }

        public void Change_Round_Status(int num) {
            _round_status = num;
            View_Scene1._Instance.Round_Change(GlobalParameter.ROUNDNAME[_round_status]);
        }

        /// <summary>
        /// 添加每年收集事件卡
        /// </summary>
        public void Collect_Card(List<string> card_list)
        {
            foreach (string str in card_list)
            {
                string[] arr = str.Split(',');
                Add_Card_Msg("获得卡牌",int.Parse(arr[0]), int.Parse(arr[1]), 0.5f);
            }
        }

        public void My_Round_Over() {

            _task.Add_RoundPromptShow_Msg(1);
            _task.Add_RoundStartAction_Msg(1);
            _task.Add_DiscardCard_Msg(0);
            _task.Run_Year_Result_Action();
        }

        public void Enemy_Round() {
            _task.Add_EnemyRound_Msg(0);
            _task.Add_RoundPromptShow_Msg(2);
            _task.Add_RoundStartAction_Msg(2);

            _opponent_info._use_card_arr = _scene_info.Enemy_Action(_opponent_info._opponent_use_card_region);
            _opponent_info._use_card_index = 0;
            if (_opponent_info._use_card_arr.Count == 0)
            {
                _task.Add_Tips_Msg("该回合敌方未使用卡牌", 1);

                _task.Add_EnemyRoundOver_Msg(0);
                _opponent_info._use_card_index = 0;
                _task.Run_Player_Task();
            }
            else {
                Enemy_Action_Decode();
            }
            
        }

        public void Enemy_Action_Decode() {
            if (_opponent_info._use_card_index < _opponent_info._use_card_arr.Count)
            {
                int card_index = int.Parse(_opponent_info._use_card_arr[_opponent_info._use_card_index].Split(',')[0]);
                int grid_index = int.Parse(_opponent_info._use_card_arr[_opponent_info._use_card_index].Split(',')[1]);
                _task.Add_EnemyAction_Msg(grid_index, card_index);
                Set_Event_Action(grid_index, Init_Enemy_Event_Card(card_index));
                _task.Add_EnemyActionOver_Msg();
                _opponent_info._use_card_index++;
            }
            else
            {
                _task.Add_EnemyRoundOver_Msg(0);
                _opponent_info._use_card_index = 0;
            }
            _task.Run_Player_Task();
        }

        public void Enemy_Use_Card(int grid_index,int card_index) {
            Model_PlayerCard temp = Init_Enemy_Event_Card(card_index);
            View_Scene1._Instance.CardInfoShow(temp);
            _task.Add_Notify_Msg("敌方成功使用事件！", _res._event_card.card_name[card_index], _scene_info._scene_year, GlobalParameter.NOTIFYMSGCOLOR[0], 0);
            _opponent_info.Rewrite_Own_Material(-temp._event_card._material_cost);
            //Set_Event_Action(grid_index, temp);
        }

        public Model_PlayerCard Init_Enemy_Event_Card(int card_index)
        {
            Model_PlayerCard temp = new Model_PlayerCard();
            temp = View_Scene1._Instance.Init_Card(2, 1, card_index);
            return temp;
        }

        public void Enemy_Use_Card_Over()
        {
            View_Scene1._Instance.CardInfoHidden();
            Enemy_Action_Decode();
        }

        public void Round_Settlement()
        {
            Player_Force_Count();      //统计角色势力范围

            _task.Add_RoundPromptShow_Msg(3);
            _task.Add_RoundStartAction_Msg(3);


            foreach (Model_Grid grid in _scene_info._grids)
            {
                grid.Loading_Msg();
            }
            for (int i = 0; i < _scene_info._grids.Count; i++)
            {
                _task.Add_GridOver_Msg(i, _scene_info._grids[i].Grid_Round_Result_Time());
            }
            _task.Add_RoundOver_Msg();
            _task.Run_Year_Result_Action();
        }

        public void Round_Over_Msg() {
            Round_Over();
        }

        public void Round_Over() {
            Model_GameMsg msg = game_controllor.Game_Judge();
            switch (msg._game_result[0]) {
                case 0:
                    _player_info.Update_Event_Card_Libary(_scene_info);
                    _task.Add_CollectEventCard_Msg(0);
                    break;
                case 1:
                    //if (msg._game_result[1] > _game_save_control._game_save_file._stage_star_num[GlobalParameter.SCENENUM - 1]) {
                    //    _game_save_control._game_save_file._stage_star_num[GlobalParameter.SCENENUM - 1] = msg._game_result[1];
                    //    _game_save_control.Rewrite_Star_Num();
                    //    _game_save_control.Save_Json(GlobalParameter.GAMECONFIGJSONPATH);
                    //}
                    if (_game_save_controllor._game_save_control._game_save_file._stage_num < GlobalParameter.STAGENUM) {
                        _game_save_controllor._game_save_control._game_save_file._stage_num = _game_save_controllor._game_save_control._game_save_file._stage_num + 1;
                        _game_save_controllor._game_save_control._game_save_file._dialogue_num = 0;
                        _game_save_controllor._game_save_control._game_save_file._talking_status = 0;
                        if (_scene_info._scene_num >= StageParameter.MATERIALSTAGENUM) {
                            _game_save_controllor._game_save_control._game_save_file._material_num += msg._game_result[1];

                        }

                        _game_save_controllor.saveJson();
                    }
                    _task.Add_GameOver_Msg("胜利!!!", 1, msg._game_result[1]);
                    break;
                case 2:
                    _game_save_controllor._game_save_control._game_save_file._talking_status = 1;
                    _game_save_controllor.saveJson();
                    _task.Add_GameOver_Msg("超过时间限制，战败", 0, 0);
                    break;
                default:
                    break;
            }
        }

        //public float Grid_Action_Time(Model_Grid grid)
        //{
        //    float time = 0;
        //    foreach (Model_GridMessage msg in grid._message_stack[grid._current_msgstack_index]) {
        //        time += msg._msg_time;
        //    }
        //    return time;
        //}

        public void Give_Up_Game() {
            EnterNextScene(ScenesEnum.StagesView);
        }

        public int Get_Card_Flag(int type)
        {
            int flag = 0;
            switch (type)
            {
                case 0:
                    flag = 1;
                    break;
                case 1:
                    flag = 1;
                    break;
                case 2:
                    flag = 0;
                    break;
                case 3:
                    flag = 0;
                    break;
                case 4:
                    flag = 2;
                    break;
                default:
                    break;
            }
            return flag;
        }

        public string Str_Tailor(string input)
        {
            if (input.Length > 100)
            {
                string mid_str = input.Substring(0, 95);
                return mid_str + "...";
            }
            else
            {
                return input;
            }
        }

        //public void Material_Exchange_Frame()
        //{
        //    View_Scene1._Instance.Material_Exchange_Frame();
        //}

        public int Get_Material_Num() {
            return _player_info.material_num;
        }

        public void Grid_Material_Change(int flag,int index,int num)
        {
            if (flag == 1) {
                num = -num;
            }
            _scene_info._grids[index].Rewrite_Grid_Material(num);
        }

        public void Change_Hero_Willpower(int flag,int index, int damage)
        {
            if (flag == 1)
            {
                damage = -damage;
                Scene_Music_Control(2);
            }
            _scene_info._grids[index].Rewrite_Grid_Willpower(damage);
            View_Scene1._Instance.Change_Grid_Status(index);
        }

        public void Hero_Update(int index) {
            _scene_info._grids[index].Hero_Level_Update(_res);
        }

        public void Show_Opponent_Material() {
            View_Scene1._Instance._ideology_frame.transform.GetComponent<View_PlayerPlatform>().Opponent_Material_Display();
        }

        public void Voice_Bt_Clicked()
        {
            Bt_Music_Control(1);
            View_Scene1._Instance.Change_Voice_Flag(!AudioManager._Instance.Change_Bg_Music_Status());
        }

        public void Hurry_Up_Bt_Clicked()
        {
            Bt_Music_Control(1);
            Scene1Parameter.HURRYUPFLAG = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cost"></param>
        public void Add_Card_Bt_Clicked(int cost)
        {
            View_Scene1._Instance.Rewrite_Buy_Card_Cost(cost);
        }

        public void Set_Card_Bt_Clicked()
        {
            Bt_Music_Control(0);
            View_Scene1._Instance.Set_Hero_Frame_Hidden();
            switch (_card_deal_flag)
            {
                case 0:
                    _task.Add_Set_Card_Over_Msg(1, 0);
                    _task.Add_Player_Material_Msg(_card_wait_set._hero_card._card_holder, 2, Mathf.CeilToInt(_card_wait_set._hero_card._crystal_cost * 10f), 0);
                    _task.Add_Player_Crystal_Msg(1, _player_info._buy_card_crystal_cost, 0);
                    break;
                case 1:
                    //_task.Add_Player_Crystal_Msg(1, _card_wait_set._hero_card._crystal_cost, 0);
                    Update_Hero_Action(HeroSet_Random_Num(1 - (float)(_scene_info._grids[GlobalParameter.CHOICEGRIDINDEX]._hero_willpower_own / _scene_info._grids[GlobalParameter.CHOICEGRIDINDEX]._hero_willpower_constant)) , _scene_info._grids[GlobalParameter.CHOICEGRIDINDEX]._hero_name);
                    break;
                case 2:
                    _task.Add_Set_Card_Over_Msg(1, 0);
                    if (_card_wait_set._card_type == 1)
                    {
                        _task.Add_Player_Crystal_Msg(1, _card_wait_set._event_card._crystal_cost, 0);
                        _task.Add_Player_Material_Msg(_card_wait_set._event_card._card_holder, 1, _card_wait_set._event_card._material_cost, 1);
                        _task.Add_Notify_Msg("成功使用事件！", _res._event_card.card_name[_card_wait_set._event_card._card_uid], _scene_info._scene_year, GlobalParameter.NOTIFYMSGCOLOR[0], 0);
                        Set_Event_Action(GlobalParameter.CHOICEGRIDINDEX, _card_wait_set);
                    }
                    else if (_card_wait_set._card_type == 2)
                    {
                        _task.Add_Player_Crystal_Msg(1, _card_wait_set._hero_card._crystal_cost, 0);
                        Set_Hero_Action(HeroSet_Random_Num(1 - ((float)(_scene_info._grids[GlobalParameter.CHOICEGRIDINDEX]._hero_willpower_constant - _scene_info._grids[GlobalParameter.CHOICEGRIDINDEX]._hero_willpower_own) / _scene_info._grids[GlobalParameter.CHOICEGRIDINDEX]._hero_willpower_constant)));
                    }
                    break;
                default:
                    break;
            }
            _task.Run_Player_Task();
        }

        public void Cancel_Set_Card_Bt_Clicked()
        {
            Bt_Music_Control(0);
            _task.Add_Set_Card_Over_Msg(0, 0);
            View_Scene1._Instance.Set_Hero_Frame_Hidden();
            _task.Run_Player_Task();
        }

        public void Bt_Music_Control(int num) {
            AudioManager._Instance.Bt_Music_Control(num);
        }

        public void Scene_Music_Control(int num) {
            AudioManager._Instance.Scene_Music_Control(num);
        }

        public void Menu_Bt_Clicked() {
            Bt_Music_Control(1);
            View_Scene1._Instance.Control_BtClicked();
        }

        public void My_Round_Over_Btclicked() {
            Bt_Music_Control(1);
            if (GlobalParameter.GAMESTATUS == 0) {
                //GlobalParameter.GAMESTATUS = 1;
                My_Round_Over();
            }
        }

        public void guidenceShow() {
            View_Scene1._Instance.guidenceShow();
        }

        public void guidenceOver() {
            View_Scene1._Instance.guidenceHidden();
        }
    }
}

