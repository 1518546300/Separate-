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

namespace View{
   public class View_LoginScene : MonoBehaviour
   {
        public static View_LoginScene _Instance;
        public GameObject text_prefab;
        public GameObject show_box;

        public Text _start_bt_text;
        public Text _screen_bt_text;

        public AudioClip _bt_music;
        public GameObject _thanks_frame;
        public GameObject _shield;

        public string[] nouns = {
        "核桃" ,"杏仁","大米","小米","燕麦","黑麦","高粱","黄米","荞麦","大豆",
        "蚕豆","豌豆","绿豆","红小豆","芸豆","红薯","马铃薯","山药","芋","木薯" ,
        "薏仁米","甜菜","向日葵","辣椒","西红柿","可可","西洋参","腰果","南瓜","棉花",
        "烟草","花生","油棕","甘蔗","茶叶","椰子","咖啡","油菜","水稻","蓖麻"
        };

        private void Awake()
        {
            _Instance = this;
        }

        void Start()
        {
            for (int i = 0; i < nouns.Length; i++)
            {
                GameObject text = Instantiate(text_prefab);
                text.transform.SetParent(show_box.transform);
                text.transform.localScale = new Vector3(1, 1, 0);
                text.transform.GetComponent<View_Txt>().Init(nouns[i]);
            }

        }

        public void Change_Screen_Bt(bool flag)
        {
            if (!flag)
            {
                _screen_bt_text.text = "全屏";
            }
            else
            {
                _screen_bt_text.text = "窗口化";
            }
        }

        public void Change_Start_Bt(int stage_num)
        {
            if (stage_num > 0)
            {
                _start_bt_text.text = "继续游戏";
            }
            else
            {
                _start_bt_text.text = "开始游戏";
            }
        }

        public void Thanks_Frame_Show() {
            _shield.SetActive(true);
            _thanks_frame.transform.GetComponent<Animator>().SetTrigger("thanks_show");
        }

        public void Thanks_Frame_Hidden()
        {
            _shield.SetActive(false);
            _thanks_frame.transform.GetComponent<Animator>().SetTrigger("thanks_hidden");
        }

        public void Bt_Clicked(int type) {
            AudioManager._Instance.Bt_Audio_Play_Intime(_bt_music);
            switch (type) {
                case 0: //gamestart
                    Ctr_LoginScene._Instance.GameStart();
                    break;
                case 1: //gamerestart
                    Ctr_LoginScene._Instance.GameRestart();
                    break;
                case 2: //config
                    break;
                case 3: //option
                    break;
                case 4: //quit
                    Ctr_LoginScene._Instance.Exit_Game();
                    break;
                default:
                    break;
            }
        }
    }
}

