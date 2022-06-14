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
using UnityEngine.UI;

using Control;
using Global;
using Model;

namespace View{
   public class View_CardScript:MonoBehaviour
   {
        //public SpriteRenderer bg;

        //public int card_type;
        public int card_color_num;

        public Text _crystal_num;
        public Text _material_num;

        public GameObject _fluorescence;
        public GameObject _material_gb;

        public GameObject _event_card;
        public GameObject _hero_card;

        public Model_PlayerCard _card_model = new Model_PlayerCard();

        public Image _card_bg;
        public Sprite[] _card_bgs_pic;

        public void Init_Card(Model_PlayerCard card) {
            _fluorescence.SetActive(false);
            _card_model = card;
            
            if (card._card_type == 1)
            {
                _material_gb.SetActive(true);
                _event_card.SetActive(true);
                _hero_card.SetActive(false);
                Event_Card_Obj_Init(card._event_card);
            }
            else if(card._card_type == 2)
            {
                _material_gb.SetActive(false);
                _event_card.SetActive(false);
                _hero_card.SetActive(true);
                Hero_Card_Obj_Init(card._hero_card);
            }
            Ctrl_Scene1._Instance._player_info.Card_Push(_card_model);
        }

        public void Event_Card_Obj_Init(Model_EventCard card)
        {
            _crystal_num.text = _card_model._event_card._crystal_cost.ToString();
            _material_num.text = _card_model._event_card._material_cost.ToString();

            _event_card.transform.Find("event_pic").Find("pic").GetComponent<Image>().sprite = card._pic;
            _event_card.transform.Find("info_frame").Find("name").GetComponent<Text>().text = card._name;
            
            _card_bg.sprite = _card_bgs_pic[card._bg_num];
        }

        public void Hero_Card_Obj_Init(Model_HeroCard card)
        {
            _crystal_num.text = card._crystal_cost.ToString();
            _hero_card.transform.Find("hero_pic").Find("avatar").GetComponent<Image>().sprite = card._pic;
            _hero_card.transform.Find("info_frame").Find("name").GetComponent<Text>().text = card._name;
        }

        public void MouseEnter() {
            if (!Ctrl_Scene1._Instance._card_show_flag)
            {
                Card_Choice();

                GameObject.Find("cards").GetComponent<View_CardTable>().Card_Restore(this.gameObject);

                Ctrl_Scene1._Instance._card_show_flag = true;
                transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
        }

        public void MouseExit()
        {
            if (!Ctrl_Scene1._Instance._card_drag_flag)
            {
                GameObject.Find("cards").GetComponent<View_CardTable>().Card_Restore(null);

                GameObject.Find("cards").GetComponent<View_CardTable>().Card_Order();
                Ctrl_Scene1._Instance._card_show_flag = false;
            }
        }

        public void Card_Restore()
        {
            _fluorescence.SetActive(false);
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }

        public void Card_Choice() {
            _fluorescence.SetActive(true);
            _fluorescence.transform.GetComponent<Image>().color = Global.GlobalParameter.COLORLIB[2];
        }

        public void Card_Drag()
        {
            if (!Ctrl_Scene1._Instance._card_drag_flag)
            {
                transform.SetParent(GameObject.Find("cards_temp").transform);
                Ctrl_Scene1._Instance._card_drag_flag = true;
                transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

                GameObject.Find("cards").GetComponent<View_CardTable>().Card_Order_Temp();
                GameObject.Find("cards").GetComponent<View_CardTable>().Card_Restore(null);

                View_Scene1._Instance.CardInfoShow(_card_model);

            }
            //弃牌
            if (Ctrl_Scene1._Instance._round_status == 1) {
                RaycastHit2D hit_1 = Physics2D.Raycast(transform.position, Vector2.right, 0.00001f, LayerMask.GetMask("Black_Eye"));
                if (hit_1.collider != null && hit_1.collider.name == "black_eye")
                {
                    GlobalParameter.CHOICEGRIDINDEX = 1;
                }
                else
                {
                    GlobalParameter.CHOICEGRIDINDEX = -1;
                }
            }
            else if(Ctrl_Scene1._Instance._round_status == 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, 0.00001f, LayerMask.GetMask("Grid"));
                if (hit.collider != null)
                {

                    if (hit.collider.name == "gold_exchange_bt")
                    {
                        if (GlobalParameter.CHOICEGRIDINDEX != -2)
                        {
                            if (_card_model._card_type == 2)
                            {
                                GlobalParameter.CHOICEGRIDINDEX = Ctrl_Scene1._Instance.Choice_Carsh();
                            }
                            else
                            {
                                GlobalParameter.CHOICEGRIDINDEX = -3;
                                View_Scene1._Instance.Change_Carsh_Choice_Status(1);
                            }
                        }
                    }
                    else
                    {
                        int index = int.Parse(hit.collider.name);
                        if (index != GlobalParameter.CHOICEGRIDINDEX)
                        {
                            GlobalParameter.CHOICEGRIDINDEX = index;
                            Ctrl_Scene1._Instance.Choice_Grid(index, _card_model);
                        }
                    }
                }
                else if (GlobalParameter.CHOICEGRIDINDEX != -1)
                {
                    GlobalParameter.CHOICEGRIDINDEX = -1;
                    Ctrl_Scene1._Instance.Clear_Grid_Status();
                    View_Scene1._Instance.Change_Carsh_Choice_Status(2);
                }
            }
            
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(pos.x, pos.y, 0);
        }

        public void Card_End_Drap()
        {
            Debug.Log("!!!");
            if (!Ctrl_Scene1._Instance._card_show_flag)
            {
                Card_Choice();

                GameObject.Find("cards").GetComponent<View_CardTable>().Card_Restore(this.gameObject);

                Ctrl_Scene1._Instance._card_show_flag = true;
                transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
        }

        public void Card_Drop()
        {
            View_Scene1._Instance.CardInfoHidden();
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            if (GlobalParameter.CHOICEGRIDINDEX == -1 || GlobalParameter.CHOICEGRIDINDEX == -3)
            {
                Card_Set_Ending_Func(0);
                return;
            }
            if (Ctrl_Scene1._Instance._round_status == 1)
            {
                Ctrl_Scene1._Instance.Discard_Card();
                Card_Set_Ending_Func(1);
            }
            else if(Ctrl_Scene1._Instance._round_status == 0)
            {
                if (GlobalParameter.CHOICEGRIDINDEX == -2)
                {
                    this.gameObject.SetActive(false);
                    Ctrl_Scene1._Instance.Set_Card(this.gameObject);
                }
                else
                {
                    if (Ctrl_Scene1._Instance.Check_Grid_Condition(GlobalParameter.CHOICEGRIDINDEX) && Ctrl_Scene1._Instance._task._run_msg_flag == 0)
                    {
                        this.gameObject.SetActive(false);
                        Ctrl_Scene1._Instance.Set_Card(this.gameObject);
                    }
                    else
                    {
                        Card_Set_Ending_Func(0);
                    }
                }
            }
        }

        public bool Distance_Func(Vector3 pos1,Vector3 pos2)
        {
            return true;
        }

        public void Card_Set_Ending_Func(int flag) {
            this.gameObject.SetActive(true);
            _fluorescence.SetActive(false);

            if (flag == 0){
                transform.SetParent(GameObject.Find("cards").transform);
            }
            else {
                GameObject.Find("cards").GetComponent<View_CardTable>().PushCardToPool(this.gameObject);
                Ctrl_Scene1._Instance._player_info.Card_Pop(_card_model);
            }

            Ctrl_Scene1._Instance.Clear_Grid_Status();
            View_Scene1._Instance.Change_Carsh_Choice_Status(2);

            GameObject.Find("cards").GetComponent<View_CardTable>().Card_Order();
            GlobalParameter.CHOICEGRIDINDEX = -1;

            GameObject.Find("cards").GetComponent<View_CardTable>().Card_Restore(null);

            Ctrl_Scene1._Instance._card_drag_flag = false;
            Ctrl_Scene1._Instance._card_show_flag = false;
        }

        private void OnMouseOver()     //鼠标悬停时每帧调用
        {
            if (GlobalParameter.GAMESTATUS == 0 && !Ctrl_Scene1._Instance._card_show_flag)
            {
                Card_Choice();

                GameObject.Find("cards").GetComponent<View_CardTable>().Card_Restore(this.gameObject);

                Ctrl_Scene1._Instance._card_show_flag = true;
                transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            }
        }
    }
}

