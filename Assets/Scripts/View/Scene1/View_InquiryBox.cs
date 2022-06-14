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

namespace View{
   public class View_InquiryBox : MonoBehaviour
   {
        public GameObject[] bts;
        public Text _content;

        public void Exit_Game() {

            bts[0].SetActive(false);
            bts[1].SetActive(true);

            string str = "您想退出游戏？";
            _content.text = str;
        }

        public void Discard_Card_Box() {

            bts[0].SetActive(true);
            bts[1].SetActive(false);

            string str = "不满足条件，请继续弃牌！";
            _content.text = str;
        }

        public void Bt1_Clicked() {
            if (Ctrl_Scene1._Instance._round_status == 1) {
                this.gameObject.SetActive(false);
            }
            else
            {
                Ctrl_Scene1._Instance.Give_Up_Game();
            }
        }

        public void Bt2_Clicked()
        {
            Ctrl_Scene1._Instance.Give_Up_Game();
        }

        public void Bt3_Clicked()
        {
            View_Scene1._Instance.InquiryBox_Hidden();
        }
    }
}
