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

using Model;
using Control;
using View;

namespace View{
   public class View_EventCardCollect : MonoBehaviour
   {
        public GameObject _event_card_table;
        public Text _card_total_num;
        public Text _wait_choice_num;

        public int _collect_card_count;

        public List<string> _collect_list = new List<string>();
        public List<GameObject> _events_object = new List<GameObject>();

        public void Rewrite_Collect_Count() {
            _wait_choice_num.text = _collect_card_count.ToString();
        }

        public void Rewrite_Event_Card_Table(Model_PlayerInfo player)
        {
            foreach (GameObject event_bt in _events_object)
            {
                Destroy(event_bt);
            }

            _events_object.Clear();
            _collect_list.Clear();

            _card_total_num.text = player._collect_card_libary.Count.ToString();

            if (Ctrl_Scene1._Instance._scene_info._scene_year == 1)
            {
                _collect_card_count = player._collect_card_num;
            }
            else {
                _collect_card_count = player._collect_card_num;
            }
            
            Rewrite_Collect_Count();


            for (int i = 0; i < player._collect_card_libary.Count; i++)
            {
                string[] arr = player._collect_card_libary[i].Split(',');
                Add_Bt(int.Parse(arr[0]),i, int.Parse(arr[1]));
            }
            _event_card_table.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (player._collect_card_libary.Count / 4 + 1) * 80);
        }

        public void Add_Bt(int card_class,int i, int index)
        {
            GameObject event_bt = null;
            event_bt = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Scene/event_card_bt"));
            event_bt.transform.SetParent(_event_card_table.transform);
            event_bt.transform.localScale = new Vector3(1f, 1f, 1f);

            string name = Ctrl_Scene1._Instance._res._event_card.card_name[index];
            Sprite pic = Ctrl_Scene1._Instance._res._sprites.event_sprites[index];

            //Debug.Log(card_class);
            //if (card_class == 2)
            //{
            //    name = Ctrl_Scene1._Instance._res._hero_card.heros_name[index];
            //    pic = Ctrl_Scene1._Instance._res._sprites.hero_sprites[index];
            //}

            if (name.Length > 5) {
                name = name.Substring(0, 3)+"...";
            }

            event_bt.transform.GetComponent<View_EventCardBt>().Init(card_class,
                                                                     i,
                                                                     index, 
                                                                     name,
                                                                     pic,
                                                                     this.gameObject);
            _events_object.Add(event_bt);
        }

        public void Confirm_Bt_Clicked() {
            for (int i = 0; i < _events_object.Count; i++) {
                View_EventCardBt card = _events_object[i].GetComponent<View_EventCardBt>();
                if (card._frame.activeSelf == true) {
                    _collect_list.Add(card._card_type+","+_events_object[i].GetComponent<View_EventCardBt>()._card_index);
                }
            }
            Ctrl_Scene1._Instance.Bt_Music_Control(1);
            Ctrl_Scene1._Instance.My_Round_Start(_collect_list);
        }
    }
}
