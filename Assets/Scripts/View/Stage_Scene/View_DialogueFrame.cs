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
using Global;

namespace View{
   public class View_DialogueFrame : MonoBehaviour
   {
        public GameObject[] _choice_bar;
        public GameObject _dialogue;
        public GameObject _choice_frame;
        public GameObject _next_bt;
        public GameObject _enter_scene_bt;

        public void Dialogue_Frame_Init() {
            //_dialogue.SetActive(false);
        }

        public void Open_Choice_Frame() {
            _next_bt.SetActive(false);
            _choice_frame.SetActive(true);
        }

        public void Clear_Bt_Status() {
            _next_bt.SetActive(false);
            _choice_frame.SetActive(false);
            _enter_scene_bt.SetActive(false);
        }

        /// <summary>
        /// num = 0正常对话，= 1选择对话， = 3进入场景
        /// </summary>
        /// <param name="num"></param>
        public void Bt_Status(int num) {
            Clear_Bt_Status();
            switch (num) {
                case 0:
                    _next_bt.SetActive(true);
                    break;
                case 1:
                    _choice_frame.SetActive(true);
                    break;
                case 2:
                    break;
                case 3:
                    _enter_scene_bt.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        public void Change_Speaker_Name(int num,string str)
        {
            _dialogue.transform.Find("name").GetComponent<Text>().color = StageParameter.ACTORTXTCOLOR[num];
            _dialogue.transform.Find("name").GetComponent<Text>().text = str + ":";
        }

        public void Change_Speaker_Dialogue(int interval, string str)
        {
            string str_padding = "";
            for (int i = 0; i < interval+1; i++)
            {
                str_padding = str_padding + "   ";
            }
            _dialogue.transform.Find("dialogue_txt").GetComponent<Text>().text = str_padding + str;
        }

        public void Write_Choice_Frame(Model_DialogueFrame model) {
            Choice_Bar_Clear();
            if (model._choice_frame_num == 2)
            {
                for (int i = 2; i < 4; i++)
                {
                    _choice_bar[i].SetActive(true);
                    _choice_bar[i].transform.name = (i-2).ToString();
                    _choice_bar[i].transform.Find("Text").GetComponent<Text>().text = model._choice_frame_next_dialogue_str[i-2];

                }
            }
            else {
                for (int i = 0; i < model._choice_frame_num; i++)
                {
                    _choice_bar[i].SetActive(true);
                    _choice_bar[i].transform.name = i.ToString();
                    _choice_bar[i].transform.Find("Text").GetComponent<Text>().text = model._choice_frame_next_dialogue_str[i];
                }
            }
        }

        public void Choice_Bar_Clear() {
            for (int i = 0; i < _choice_bar.Length; i++)
            {
                _choice_bar[i].SetActive(false);
            }
        }

        public void Dialogue_Clear()
        {
            _dialogue.transform.Find("name").GetComponent<Text>().text = "";
            _dialogue.transform.Find("dialogue_txt").GetComponent<Text>().text = "";
        }


    }
}
