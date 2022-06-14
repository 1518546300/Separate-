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

namespace View{
   public class View_Card_Info_frame:MonoBehaviour
   {
        public GameObject _event_info_frame;
        public GameObject _hero_info_frame;

        public List<GameObject> _hero_level_info = new List<GameObject>();

        public Text _willpower_num;

        public Text _hero_name;
        public Text _event_name;

        public GameObject _level_info_table;
        Animator anim;

        public Image _bg;
        public Sprite[] _bg_sprite;

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        public void Frame_Show() {
            anim.Play("card_info_vanish");
            anim.Update(0f);
            anim.SetTrigger("show");
            anim.ResetTrigger("hidden");
        }

        public void Frame_Hidden()
        {
            //AnimatorStateInfo stateinfo2 = anim.GetCurrentAnimatorStateInfo(0);
            //if (stateinfo2.IsName("card_info_display") && stateinfo2.normalizedTime >= 1.0f)
            //{
                
            //}
            anim.SetTrigger("hidden");
        }

        public void Hero_Info_Show(Model_PlayerCard card)
        {
            _event_info_frame.SetActive(false);
            _hero_info_frame.SetActive(true);
            _hero_name.text = card._hero_card._name;
            _hero_info_frame.transform.Find("head").Find("mask").Find("avatar").GetComponent<Image>().overrideSprite = card._hero_card._pic;

            _hero_info_frame.transform.Find("effect").Find("card_info").Find("describe").GetComponent<Text>().text = Ctrl_Scene1._Instance.Str_Tailor(card._hero_card._decribe);
            _willpower_num.text = card._hero_card._willpower.ToString();

            foreach (GameObject info in _hero_level_info)
            {
                Destroy(info);
            }

            string[] attr_str = card._hero_card._attr.Split('|');

            for (int i = 0; i < attr_str.Length; i++) {
                if (attr_str[i] != "") {
                    GameObject level_bar = null;
                    level_bar = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Scene/level_info"));
                    level_bar.transform.SetParent(_level_info_table.transform);
                    level_bar.transform.localScale = new Vector2(1, 1);
                    string[] attr = attr_str[i].Split(',');
                    level_bar.transform.Find("level").GetComponent<Text>().text = (i + 1) + "级";
                    level_bar.transform.Find("info").GetComponent<Text>().text = Ctrl_Scene1._Instance._res._event_card.card_name[int.Parse(attr[0])] + "(" + int.Parse(attr[1]) + ")";
                    _hero_level_info.Add(level_bar);
                }
            }
            Frame_Show();
        }

        public void EventCard_Info_Show(Model_PlayerCard card)
        {
            _event_info_frame.SetActive(true);
            _hero_info_frame.SetActive(false);

            _event_name.text = card._event_card._name;

            _event_info_frame.transform.Find("head").Find("pic").Find("main_pic").GetComponent<Image>().overrideSprite = card._event_card._pic;
            _event_info_frame.transform.Find("card_info").Find("describe").GetComponent<Text>().text = Ctrl_Scene1._Instance.Str_Tailor(card._event_card._decribe);
            _bg.sprite = _bg_sprite[card._event_card._bg_num];
            Frame_Show();
        }
    }
}

