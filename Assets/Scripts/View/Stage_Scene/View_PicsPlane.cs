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

namespace View{
   public class View_PicsPlane : MonoBehaviour
   {
        public GameObject[] _pics;

        //public Image _hero_pic;
        //public Text _hero_name;
        //public GameObject _hero_dialogue_obj;
        
        public Image _avatar_image;
        public Sprite[] _avatar_sprite;
        public Text _actor_name;


        public void Clear_Pics()
        {
            for (int i = 0; i < _pics.Length; i++)
            {
                _pics[i].SetActive(false);
            }
        }

        public void Pics_Show(string config) {
            string[] str = config.Split('/');
            int pic_type = int.Parse(str[0]);

            _pics[pic_type+1].SetActive(true);

            switch (pic_type) {
                case 0:
                    Type0_Pic(str[1]);
                    break;
                case 1:
                    Type1_Pic(str[1]);
                    break;
                case 2:
                    //Type2_Pic(str[1]);
                    break;
                default:
                    break;
            }
        }

        public void Type0_Pic(string config) {
            string[] one_pic_config = config.Split('|')[0].Split(',');
            _pics[1].transform.Find("0").Find("pic").GetComponent<Image>().sprite = View_Main._Instance.Get_Dialogue_Pic(int.Parse(one_pic_config[0]));
            string temp_name = one_pic_config[1];
            if (name.Length > 6)
            {
                temp_name = one_pic_config[1].Substring(0, 5) + '\n' + one_pic_config[1].Substring(5, one_pic_config[1].Length - 5);
            }
            _pics[1].transform.Find("0").Find("name").Find("frame").Find("name").GetComponent<Text>().text = temp_name;
        }

        public void Type1_Pic(string config)
        {
            string[] pic_config = config.Split('|');
            for (int i = 0; i < pic_config.Length; i++)
            {
                string[] temp_config = pic_config[i].Split(',');
                _pics[2].transform.Find(i.ToString()).Find("pic").GetComponent<Image>().sprite = View_Main._Instance.Get_Dialogue_Pic(int.Parse(temp_config[0]));
                _pics[2].transform.Find(i.ToString()).Find("name").Find("frame").Find("name").GetComponent<Text>().text = temp_config[1];
            }
        }

        public void Hero_Avatar_Change(Sprite pic,string name,string dialogue) {
            _pics[0].SetActive(true);
            _avatar_image.sprite = pic;
            string temp_name = name;
            if (name.Length > 6) {
                temp_name = name.Substring(0, 5) + "\n" + name.Substring(5, name.Length - 5);
            }
            _actor_name.text = temp_name;
        }

        public void NPC_Avatar(int flag) {
            if (flag == 0)
            {
                _pics[0].SetActive(true);
                _actor_name.text = "卡西博士";
                _avatar_image.sprite = _avatar_sprite[0];
            }
            else if (flag == 1) {
                _pics[0].SetActive(true);
                _actor_name.text = "探险家";
                _avatar_image.sprite = _avatar_sprite[1];
            }
        }
    }
}
