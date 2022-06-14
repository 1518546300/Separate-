/***
*
*   Title:  "Separate.1" 项目开发
*	
*		视图层：scene1
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
using UnityEngine.UI;

using Control;
using Global;
using Presistent;
using Model;

namespace View{
    public class View_Scene1 : MonoBehaviour
    {
        public static View_Scene1 _Instance;

        public GameObject _card_table;
        public GameObject _grid_prefab;
        public GameObject _grid_map_grids;

        public GameObject _round_prompt;

        public GameObject _crystal_platform;

        public GameObject _set_hero_frame;
        public GameObject _grid_info_box;

        public GameObject _card_info_frame;

        public GameObject _hero_info_frame;
        public GameObject _event_info_frame;

        public GameObject _card_cost_txt_m; //卡西博士按键物质需要txt
        public GameObject _card_cost_txt_c; //卡西博士按键水晶需要txt

        //public GameObject _year_text;

        public GameObject tips;
        public GameObject _inquirybox;
        public GameObject _gameresult_box;
        public GameObject _promptbox;

        public GameObject _player_bts;

        public GameObject _ideology_frame;
        public GameObject _event_card_collect_frame;

        public GameObject _material_exchange_frame;

        public ParticleSystem _deal_achieve;

        public GameObject _material_bag;

        public GameObject _msg_board;

        public GameObject _black_eye;
        public Text _discard_card_text;

        public GameObject _voice_flag;

        public GameObject _loading_curtain;

        public GameObject _guidence;

        public GameObject _year_box;

        public Image _carsh_choice_status_frame;
        public Image _start_curtain;

        void Awake()
        {
            _Instance = this;
        }

        void Start()
        {
            _grid_info_box.SetActive(true);
            _guidence.SetActive(false);
        }

        public void Loading_Curtain_Show(Sprite pic,string saying,string name)
        {
            _loading_curtain.SetActive(true);
            _start_curtain.overrideSprite = pic;
            TouchControl._Instance.Game_Canvas_Shield(true);
            _loading_curtain.transform.GetComponent<View_LoadingCurtain>().Change_Saying(saying,name);
        }

        public void Loading_Curtain_Close()
        {
            _loading_curtain.transform.GetComponent<View_LoadingCurtain>().Curtains_Finally_Closin();
        }

        public void Menu_Show() {
            _player_bts.transform.GetComponent<Animator>().SetTrigger("show");
        }

        public void Menu_Hidden()
        {
            _player_bts.transform.GetComponent<Animator>().SetTrigger("hidden");
        }

        public void guidenceShow()
        {
            Shield_Clicked();
            TouchControl._Instance.Game_Canvas_Shield(true);
            _guidence.GetComponent<View_Guidence>().Init();
            _guidence.SetActive(true);
        }

        public void guidenceHidden()
        {
            _guidence.SetActive(false);
            TouchControl._Instance.Game_Canvas_Shield(false);
        }

        public void Control_Show_Hidden()
        {
            TouchControl._Instance.Game_Canvas_Shield(true);
            _promptbox.transform.GetComponent<View_PromptionScript>().Prompt_Show();
        }

        public void Control_BtClicked() {
            Menu_Hidden();
            Control_Show_Hidden();
        }

        public void Shield_Clicked() {
            if (_promptbox.transform.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name == "prompt_show") {
                Control_Show_Hidden();
                Menu_Show();
                TouchControl._Instance.Game_Canvas_Shield(false);
            }
        }

        public void Round_Change(string name)
        {
            _round_prompt.GetComponent<View_RoundPrompt>().Round_Control_Change(name);
            
        }

        public void Change_Voice_Flag(bool flag) {
            _voice_flag.SetActive(flag);
        }

        /// <summary>
        /// flag = 0,无效选择 1，有效选择，2.选择取消
        /// </summary>
        /// <param name="flag"></param>
        public void Change_Carsh_Choice_Status(int flag)
        {
            switch (flag) {
                case 0:
                    _carsh_choice_status_frame.gameObject.SetActive(true);
                    _carsh_choice_status_frame.color = GlobalParameter.GRIDCOLOR[0];
                    break;
                case 1:
                    _carsh_choice_status_frame.gameObject.SetActive(true);
                    _carsh_choice_status_frame.color = GlobalParameter.GRIDCOLOR[1];
                    break;
                case 2:
                    _carsh_choice_status_frame.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }

        public void Tips_Show(string str)
        {
            tips.SetActive(true);
            tips.transform.Find("bg").Find("text").GetComponent<Text>().text = str;
            tips.transform.Find("bg").Find("text").GetComponent<Text>().color = GlobalParameter.TEXTCOLOR[0];
            Invoke("Tips_Hidden", 1);
        }

        public void Tips_Hidden()
        {
            tips.SetActive(false);
        }

        
        public void Change_Black_Eye_Status(bool flag)
        {
            _black_eye.SetActive(flag);
        }

        public void Grid_Action_Reset() {

        }

        public void Rewrite_Year_Num(int year,int flag) {
            _year_box.GetComponent<View_YearBox>().Rewrite_Year_Num(year, flag);
        }

        public void Next_Year()
        {            
            Clear_Grids_Status();
            Scene1Parameter.HEROCOSTNUM = 0;

            Rewrite_Year_Num(Ctrl_Scene1._Instance._scene_info._scene_year, Ctrl_Scene1._Instance._scene_info.Judge_Year_Flag());

            Rewrite_Buy_Card_Cost(Ctrl_Scene1._Instance._player_info._buy_card_cost[Global.Scene1Parameter.HEROCOSTNUM]);
            _crystal_platform.GetComponent<View_CrystalPlatform>().Crystal_Recovery(Ctrl_Scene1._Instance._player_info.crystal_num);
        }

        public void Collect_Card()
        {
            Event_Card_Collect_Frame_Show();

        }

        public void Event_Card_Collect_Frame_Show() {
            _event_card_collect_frame.SetActive(true);
            
            Rewrite_Event_Table();
        }

        public void Rewrite_Event_Table() {
            _event_card_collect_frame.transform.GetComponent<View_EventCardCollect>().Rewrite_Event_Card_Table(Ctrl_Scene1._Instance._player_info);
        }

        public void Card_Collect_Board_Hidden() {
            _event_card_collect_frame.SetActive(false);
            //TouchControl._Instance.Game_Canvas_Shield(false);
        }

        public void Player_Info_Init(Model_PlayerInfo player,Model_OpponentInfo opponent) {
            _card_cost_txt_c.GetComponent<Text>().text = player._buy_card_crystal_cost.ToString();

            _ideology_frame.transform.GetComponent<View_PlayerPlatform>().Init(Ctrl_Scene1._Instance._res._sprites,player,opponent);
            _crystal_platform.GetComponent<View_CrystalPlatform>().Crystal_Init(player.crystal_num);
            _ideology_frame.transform.GetComponent<View_PlayerPlatform>().Player_Material_Display();
            //_ideology_frame.transform.GetComponent<View_PlayerPlatform>().Opponent_Material_Display();
        }

        public void Change_Grid_Status(int index) {
            Model_Grid grid = Ctrl_Scene1._Instance._scene_info._grids[index];
            _grid_map_grids.transform.GetChild(index).GetComponent<View_GridScript>().Change_HeroStatus(grid);
        }

        public void Write_Material() {
            _ideology_frame.transform.GetComponent<View_PlayerPlatform>().Player_Material_Display();
        }

        public void Rewrite_Crystal(int num) {
            _crystal_platform.GetComponent<View_CrystalPlatform>().Crystal_Rewrite(num);
        }

        public void Grid_Init(Model_Stage scene) {
            for (int i = 0; i < scene._grids.Count; i++) {
                //if (scene._grids[i]._hero_index != -1)
                //{

                //}
                //else
                //{
                //    //未初始化
                //    Debug.Log(i);
                //    GameObject grid = Instantiate(_grid_prefab);
                //    grid.name = i.ToString();
                //    grid.transform.localScale = new Vector3(1f, 1f, 1f);
                //    grid.transform.localPosition = new Vector3(scene._grids[i]._x, scene._grids[i]._y, 0);
                //    grid.SetActive(false);
                //    grid.transform.SetParent(_grid_map_grids.transform);
                //}
                GameObject grid = Instantiate(_grid_prefab);
                grid.name = i.ToString();

                grid.transform.Find("hero").GetComponent<Image>().overrideSprite = scene._grids[i]._hero_pic;

                Model_Grid grid_model = scene._grids[i];

                grid.GetComponent<View_GridScript>().Grid_Init(i,
                                                               grid_model
                                                               //grid_model._class_num,
                                                               //grid_model._hero_willpower_own,
                                                               //grid_model._hero_willpower_constant,
                                                               //grid_model._hero_state
                                                               );

                grid.transform.SetParent(_grid_map_grids.transform);
                grid.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                grid.transform.localPosition = new Vector3(scene._grids[i]._x, scene._grids[i]._y, 0);
                Change_Grid_Status(i);
            }
            
        }

        //public void Grid_Shield_Func(string layer)
        //{
        //    for (int i = 0; i < _grid_map_grids.transform.childCount; i++) {
        //        var child = _grid_map_grids.transform.GetChild(i).gameObject;
        //        child.layer = LayerMask.NameToLayer(layer);
        //    }
        //}

        /// <summary>
        /// flag = 1失去物质，flag = 2获得物质
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="price"></param>
        /// <param name="num"></param>
        public void Rewrite_Player_Material(int flag,int price,int num) {
            string str = "";
            if (flag == 2)
            {
                str = "+" + price;
                _material_bag.transform.Find("tag").GetComponent<Text>().text = str;
                _material_bag.transform.GetComponent<Animator>().SetTrigger("add");
            }
            else
            {
                str = "-" + price;
                _material_bag.transform.Find("tag").GetComponent<Text>().text = str;
                _material_bag.transform.GetComponent<Animator>().SetTrigger("lose");
            }
            _deal_achieve.Play();
            Write_Material();
        }

        public void Choice_Grid(int index,int flag) {
            Clear_Grids_Status();
            
            Color color = new Color32();
            if (flag == 1)
            {
                color = Global.GlobalParameter.COLORLIB[3];
            }
            else if (flag == 0)
            {
                color = Global.GlobalParameter.COLORLIB[4];
            }
            else if (flag == 3)
            {
                color = Global.GlobalParameter.COLORLIB[5];
            }
            _grid_map_grids.transform.GetChild(index).GetComponent<View_GridScript>()._fluorescence.SetActive(true);
            _grid_map_grids.transform.GetChild(index).GetComponent<View_GridScript>()._fluorescence.transform.GetComponent<Image>().color = color;
        }

        public void Clear_Grids_Status() {
            for (int i = 0; i < _grid_map_grids.transform.childCount; i++)
            {
                var child = _grid_map_grids.transform.GetChild(i);
                child.GetComponent<View_GridScript>()._fluorescence.SetActive(false);
            }
        }

        /// <summary>
        /// 放置英雄询问框出现
        /// </summary>
        public void Set_Hero_Frame_Show(string str) {
            _set_hero_frame.SetActive(true);
            _set_hero_frame.transform.Find("dialogue").transform.GetComponent<Text>().text = str;
        }

        public void Set_Hero_Frame_Hidden()
        {
            _set_hero_frame.SetActive(false);
            Clear_Grids_Status();
        }

        public void Run_Grid_Action(int index)
        {
            _grid_map_grids.transform.Find(index.ToString()).GetComponent<View_GridScript>().Run_Grid_ImmAction();
        }

        public void Run_GridOver_Action(int index)
        {
            _grid_map_grids.transform.Find(index.ToString()).GetComponent<View_GridScript>().Run_GridOver_Action();
        }

        //public void Set_Hero(Model_Stage scene)
        //{
        //    //int index = GlobalParameter.CHOICEGRIDINDEX;
        //    //Sprite pic = Ctrl_Scene1._Instance._res._sprites.hero_sprites[scene._grids[index]._hero_index];
        //    //_grid_map_grids.transform.Find(index.ToString()).GetComponent<View_GridScript>().Hero_Set(pic , scene._grids[index]._class_num);
        //    //Change_Grid_Status(index, Ctrl_Scene1._Instance.Get_Grid_Status(scene._grids[index]._hero_willpower_own, scene._grids[index]._hero_willpower_own));
        //}

        public void CardShowBt_Clicked() {
            _card_table.SetActive(!_card_table.activeSelf);
        }

        public Vector3 Get_Grid_Pos(int index)
        {
            Vector3 pos = new Vector3(_grid_map_grids.transform.GetChild(index).transform.localPosition.x, _grid_map_grids.transform.GetChild(index).transform.localPosition.y,0);
            return pos;
        }

        public void CardInfoShow(Model_PlayerCard card)
        {
            if (card._card_type == 1)
            {
                _card_info_frame.GetComponent<View_Card_Info_frame>().EventCard_Info_Show(card);
            }
            else if(card._card_type == 2)
            {
                _card_info_frame.GetComponent<View_Card_Info_frame>().Hero_Info_Show(card);
            }
        }

        public void CardInfoHidden()
        {
            _card_info_frame.GetComponent<View_Card_Info_frame>().Frame_Hidden();
        }

        public void Info_Box_Show(int grid_index,Model_Grid grid)
        {
            int hero_num = grid._hero_uid;
            _card_table.transform.GetComponent<View_CardTable>().Change_Card_Layer("Ignore Raycast");
            _grid_info_box.GetComponent<View_GridInfoBoxScript>().Info_Box_Show(grid_index,grid);
        }

        public void Info_Box_Hidden()
        {
            _card_table.transform.GetComponent<View_CardTable>().Change_Card_Layer("Card");
        }

        /// <summary>
        /// flag = 1为事件卡，2为英雄卡
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="num"></param>
        /// <param name="num2"></param>
        public void AddCard(int flag , int num)
        {
            GameObject card = null;
            card = _card_table.GetComponent<View_CardTable>().AddCard();
            card.GetComponent<View_CardScript>().Init_Card(Init_Card(1, flag, num));
        }

        /// <summary>
        /// card_holder = 1持有者为玩家，= 2持有者为敌方,flag = 1事件卡,2英雄卡
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public Model_PlayerCard Init_Card(int card_holder,int flag,int num) {
            Model_PlayerCard card = new Model_PlayerCard();
            card._card_type = flag;
            if (flag == 1)
            {
                EventCardObject event_card = Ctrl_Scene1._Instance._res._event_card;
                Sprite pic = Ctrl_Scene1._Instance._res._sprites.event_sprites[num];
                string[] str = event_card.card_class[num].Split(',');
                int card_class = int.Parse(str[0]);
                int card_class_uid = int.Parse(str[1]);
                card.Init_Event_Card(
                                        card_holder,
                                        int.Parse(event_card.card_use_object[num]),
                                        num,
                                        card_class,
                                        card_class_uid,
                                        event_card.card_name[num],
                                        event_card.card_describe[num],
                                        (int)float.Parse(event_card.card_crystal_cost[num]),
                                        (int)float.Parse(event_card.card_material_cost[num]),
                                        pic
                                    );
                int class_uid = 0;
                int status_uid = 0;
                switch (card._event_card._card_class) {
                    case 0:
                        class_uid = card._event_card._card_class_uid;
                        card.Init_NormalCard(
                                Ctrl_Scene1._Instance._res._event_normal.main_material_effect[class_uid],
                                Ctrl_Scene1._Instance._res._event_normal.material_effect[class_uid],
                                Ctrl_Scene1._Instance._res._event_normal.willpower_effect[class_uid],
                                Ctrl_Scene1._Instance._res._event_normal.recombination_effect[class_uid]
                            );
                        
                        break;
                    case 1:
                        class_uid = card._event_card._card_class_uid;
                        card.Init_NormalCard(
                                Ctrl_Scene1._Instance._res._event_longterm.main_material_effect[class_uid],
                                Ctrl_Scene1._Instance._res._event_longterm.material_effect[class_uid],
                                Ctrl_Scene1._Instance._res._event_longterm.willpower_effect[class_uid],
                                Ctrl_Scene1._Instance._res._event_normal.recombination_effect[class_uid]
                            );
                        status_uid = int.Parse(Ctrl_Scene1._Instance._res._event_longterm.status_uid[class_uid]);
                        card.Init_Status(
                                Ctrl_Scene1._Instance._res._event_status.status_uid[status_uid],
                                Ctrl_Scene1._Instance._res._event_status.status_name[status_uid],
                                Ctrl_Scene1._Instance._res._event_status.status_describe[status_uid],
                                Ctrl_Scene1._Instance._res._event_status.time[status_uid],
                                Ctrl_Scene1._Instance._res._sprites.event_status_sprites[status_uid]
                            );
                        break;
                    case 2:
                        class_uid = card._event_card._card_class_uid;
                        card.Init_MysteriousCard(
                                Ctrl_Scene1._Instance._res._event_mysterious.type[class_uid]
                            );
                        status_uid = int.Parse(Ctrl_Scene1._Instance._res._event_mysterious.status_uid[class_uid]);
                        card.Init_Status(
                                Ctrl_Scene1._Instance._res._event_status.status_uid[status_uid],
                                Ctrl_Scene1._Instance._res._event_status.status_name[status_uid],
                                Ctrl_Scene1._Instance._res._event_status.status_describe[status_uid],
                                Ctrl_Scene1._Instance._res._event_status.time[status_uid],
                                Ctrl_Scene1._Instance._res._sprites.event_status_sprites[status_uid]
                            );
                        break;
                    default:
                        break;
                }
            }
            else
            {
                card._hero_card = Ctrl_Scene1._Instance._scene_info.Init_Hero_Card(num);
            }
            return card;
        }

        public void Rewrite_Buy_Card_Cost(int cost) {
            _card_cost_txt_m.GetComponent<Text>().text = cost.ToString();
        }

        public void Add_MsgBar(string title,string info,int year,Color color)
        {
            _msg_board.transform.GetComponent<View_MsgBoard>().Add_MsgBar(title,info,year,color);
        }

        public void Grid_Status_Show(Vector3 pos,Model_GridStatus status) {
            _grid_info_box.transform.GetComponent<View_GridInfoBoxScript>().Status_Info_Frame_Show(pos,status);
        }

        public void Grid_Status_Hidden(){
            _grid_info_box.transform.GetComponent<View_GridInfoBoxScript>().Info_Frame_Hidden();
        }


        public void Grid_Attribute_Show(Vector3 pos, Model_GridAttribute attr)
        {
            _grid_info_box.transform.GetComponent<View_GridInfoBoxScript>().Attr_Info_Frame_Show(pos, attr);
        }

        public void Grid_Attribute_Hidden()
        {
            _grid_info_box.transform.GetComponent<View_GridInfoBoxScript>().Info_Frame_Hidden();
        }


        public bool Check_Grid_Condition(int index) {
            return _grid_map_grids.transform.GetChild(index).GetComponent<View_GridScript>()._fluorescence.transform.GetComponent<Image>().color == Global.GlobalParameter.COLORLIB[3];
        }

        public void Exit_Game()
        {
            _inquirybox.SetActive(true);
            _inquirybox.transform.GetComponent<View_InquiryBox>().Exit_Game();
        }

        public void Game_Result(string str,int star_num,Color color)
        {
            _gameresult_box.SetActive(true);
            int flag = 1;
            if (Ctrl_Scene1._Instance._scene_info._scene_num <= StageParameter.MATERIALSTAGENUM) {
                flag = 0;
            }
            _gameresult_box.transform.GetComponent<View_GameResult>().Win_Or_Debeat(flag,str, star_num, color);
        }

        public void InquiryBox_Hidden()
        {
            _inquirybox.SetActive(false);
            //_promptbox.transform.GetComponent<View_PromptionScript>().Prompt_Show();
        }

        //public void Material_Exchange_Frame() {
        //    _material_exchange_frame.SetActive(!_material_exchange_frame.activeSelf);
        //    if (_material_exchange_frame.activeSelf) {
        //        _material_exchange_frame.GetComponent<View_MaterialExchange>().Init();
        //    }
        //}

    }
}

