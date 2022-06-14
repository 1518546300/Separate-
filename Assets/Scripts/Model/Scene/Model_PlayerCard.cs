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

namespace Model{
   public class Model_PlayerCard
   {

        public int _card_type; //1 事件卡，2 英雄卡
        public Model_EventCard _event_card;
        public Model_HeroCard _hero_card;

        public Model_PlayerCard() {
            _event_card = new Model_EventCard();
            _hero_card = new Model_HeroCard();
        }

        //public int _card_uid;    //卡牌编号
        ////字段,描述的特征
        //public int _card_main_class;    //卡牌类别 1为事件卡，2为英雄卡

        //public int _card_holder;

        //public int _card_class;

        //public string _name;
        //public string _decribe;
        //public int _crystal_cost;
        //public int _material_cost;

        //public int _willpower_num;

        //public int _status_index; //状态下标
        //public int _status_class;
        //public string _card_status_name = "";
        //public string _card_status_describe = "";

        //public string _card_damage;
        //public int _card_status_time;

        //public string _states;
        //public string _attr;

        //public int _card_mysterious_num;

        //public Sprite _pic;

        //public void Init_Event_Card(
        //                            int card_uid,
        //                            int card_holder,
        //                            int card_main_class,
        //                            int card_type_class,
        //                            string name,
        //                            string decribe,
        //                            int crystal,
        //                            int material,
        //                            //string status_config,
        //                            //string status_name,
        //                            //string status_describe,
        //                            //string card_damage,
        //                            //int status_time,
        //                            //int mysterious_num,
        //                            Sprite pic
        //                            )
        //{
        //    _card_uid = card_uid;
        //    _card_holder = card_holder;
        //    _name = name;

        //    _card_main_class = card_main_class;
        //    _card_class = card_type_class;
        //    _decribe = decribe;
        //    _crystal_cost = crystal;
        //    _material_cost = material;

        //    //string[] config = status_config.Split(',');
        //    //_status_class = int.Parse(config[0]);
        //    //if (_status_class != 0) {
        //    //    _status_index = int.Parse(config[1]);
        //    //}
        //    //else
        //    //{
        //    //    _status_index = -1;
        //    //}

        //    //_card_status_name = status_name;
        //    //_card_status_describe = status_describe;
        //    //_card_damage = card_damage;
        //    //_card_status_time = status_time;
        //    //_card_mysterious_num = mysterious_num;
        //    _pic = pic;
        //}

        //public void Init_Hero_Card(
        //                           int card_uid,
        //                           int card_holder,
        //                           int card_main_class,
        //                           string card_name,
        //                           string card_decribe,
        //                           int crystal,
        //                           //int material,
        //                           int willpower,
        //                           string states,
        //                           string attr,
        //                           Sprite pic

        //    )
        //{
        //    _card_uid = card_uid;
        //    _card_holder = card_holder;

        //    _card_main_class = card_main_class;
        //    _name = card_name;
        //    _decribe = card_decribe;
        //    _crystal_cost = crystal;

        //    _willpower_num = willpower;
        //    _states = states;
        //    _attr = attr;
        //    _pic = pic;
        //}

        public void Init_Event_Card(
                                    int card_holder,
                                    int card_use_object,
                                    int card_uid,
                                    int card_class,
                                    int card_class_uid,
                                    string name,
                                    string decribe,
                                    int crystal,
                                    int material,
                                    Sprite pic
                                    )
        {
            _event_card._card_holder = card_holder;
            _event_card._card_use_object = card_use_object;
            _event_card._card_uid = card_uid;
            _event_card._card_class = card_class;
            _event_card._card_class_uid = card_class_uid;
            _event_card._name = name;
            _event_card._decribe = decribe;
            _event_card._crystal_cost = crystal;
            _event_card._material_cost = material;
            _event_card._pic = pic;
            _event_card._bg_num = Random.Range(0, 3);
        }

        public void Init_NormalCard(
                                    string main_material_effect,
                                    string material_effect,
                                    string willpower_effect,
                                    string recombination_effect
            )
        {
            _event_card._effect = main_material_effect + "/" + material_effect + "/" + willpower_effect + "/" + recombination_effect;
        }

        //public void Init_LongTermCard(
        //                            string main_material_effect,
        //                            string material_effect,
        //                            string willpower_effect,
        //                            string recombination_effect
        //    )
        //{
        //    _event_card._effect = main_material_effect + "/" + material_effect + "/" + willpower_effect + "/" + recombination_effect;
        //}

        public void Init_MysteriousCard(
                                    string mystrious_type
            )
        {
            _event_card._mysterious_type = int.Parse(mystrious_type);
        }

        public void Init_Status(
                                    string status_uid,
                                    string status_name,
                                    string status_describe,
                                    string status_time,
                                    Sprite status_pic
                                    )
        {
            _event_card._status_uid = int.Parse(status_uid);
            _event_card._status_name = status_name;
            _event_card._status_describe = status_describe;
            _event_card._time = int.Parse(status_time);
            _event_card._status_pic = status_pic;
        }

        public void Init_Hero_Card(
                                   int card_holder,
                                   int card_uid,
                                   string card_name,
                                   string card_decribe,
                                   int crystal,
                                   int willpower,
                                   string states,
                                   string attr,
                                   Sprite pic

            )
        {
            _hero_card._card_holder = card_holder;
            _hero_card._card_uid = card_uid;
            _hero_card._name = card_name;
            _hero_card._decribe = card_decribe;
            _hero_card._crystal_cost = crystal;

            _hero_card._willpower = willpower;
            _hero_card._states = states;
            _hero_card._attr = attr;
            _hero_card._pic = pic;
        }
    }
}

