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

using Control;

namespace Model{
   public class Model_PageInfo
   {
        public int _page_number;
        public int _stage_num;
        public int _dialogue_num;
        public int _dialogue_object;
        public int _dialogue_object_type;
        public int _status;
        public List<string> _dialogue;
        public string[] _actor_name = new string[] { "卡西博士", "探险家" };

        public Model_PageInfo(int status,int stage_num,int dialogue_num,DialogueJson dialogue) {
            _status = status;
            _page_number = stage_num/6;
            _stage_num = stage_num;
            _dialogue_num = dialogue_num;
            if (status == 0)
            {
                _dialogue = dialogue.main[stage_num];
            }
            else if(status == 1) {
                _dialogue = dialogue.debeated_dialogue[Random.Range(0,dialogue.debeated_dialogue.Count)];
            }
        }

        public void Init(List<string> dialogue) {
            _dialogue = dialogue;
        }
        
        public void Read_Dialogue() {
            Process_Dialogue(_dialogue_num);
        }

        public void Process_Dialogue(int dialogue_num) {
            string dialogue = _dialogue[dialogue_num];
            string[] main_str = dialogue.Split('/');
            int dialogue_flag = int.Parse(main_str[0]);
            Pics_Process(dialogue);
            switch (dialogue_flag)
            {
                case 0:
                    Simple_Dialogue(main_str[1]);
                    Ctrl_Main._Instance.Change_Bt_Status(0);
                    break;
                case 1:
                    Choice_Dialogue(dialogue);
                    Ctrl_Main._Instance.Change_Bt_Status(1);
                    break;
                case 2:
                    //Hero_Dialogue(dialogue);
                    //Ctrl_Main._Instance.Change_Bt_Status(0);
                    break;
                case 3:
                    Enter_Scene(dialogue);
                    Ctrl_Main._Instance.Change_Bt_Status(3);
                    break;
                case 4:
                    //Hero_Next_Scene(dialogue);
                    //Ctrl_Main._Instance.Change_Bt_Status(3);
                    break;
                default:
                    break;
            }
            //Dialogue_Num_Change(next_dialogue_num);
        }

        //public void Hero_Next_Scene(string dialogue) {
        //    string[] str = dialogue.Split('/');
        //    int hero_num = int.Parse(str[1].Split(':')[0]);
        //    string dialogue_main = str[1].Split(':')[1].Split('&')[0];
        //    Ctrl_Main._Instance.Hero_Next_Scene(hero_num, dialogue_main);


        //    int next_dialogue_num = int.Parse(dialogue.Split('&')[1]);
        //    Dialogue_Num_Change(next_dialogue_num);
        //}

        //public void Hero_Dialogue(string dialogue) {
        //    string[] str = dialogue.Split('/');
        //    int hero_num = int.Parse(str[1].Split(':')[0]);
        //    string dialogue_main = str[1].Split(':')[1].Split('&')[0];

        //    Ctrl_Main._Instance.Hero_Dialogue(hero_num,dialogue_main);


        //    int next_dialogue_num = int.Parse(dialogue.Split('&')[1]);
        //    Dialogue_Num_Change(next_dialogue_num);
        //}

        public void Pics_Process(string dialogue)
        {
            string[] configs = dialogue.Split('$');
            if (configs.Length > 1)
            {
                Ctrl_Main._Instance.Pics_Show(configs[1]);
            }
        }

        public void Enter_Scene(string dialogue) {
            string[] str = dialogue.Split('/');
            _dialogue_object = Object_name2_Object_num(str[1].Split(':')[0]);
            string dialogue_main = str[1].Split(':')[1].Split('&')[0];

            Ctrl_Main._Instance.Change_Dialogue(dialogue_main);

            int scene_num = int.Parse(str[2].Split(',')[0]);
            int page_num = int.Parse(str[2].Split(',')[1]);
            Dialogue_Num_Change(int.Parse(str[1].Split(':')[1].Split('&')[1]));
            Ctrl_Main._Instance.Enter_Scene(scene_num,page_num);
        }

        public void Choice_Dialogue(string dialogue) {
            string[] str = dialogue.Split('/');
            _dialogue_object = Object_name2_Object_num(str[1].Split(':')[0]);
            string dialogue_main = str[1].Split(':')[1];
            Ctrl_Main._Instance.Change_Dialogue(dialogue_main);
            Ctrl_Main._Instance.Write_Dialogue_Frame(str[2]);
        }

        //public void Answer_Dialogue(string dialogue) {
        //    string[] str = dialogue.Split('/');
        //    int object_num = Object_name2_Object_num(str[1].Split(':')[0]);
        //    string dialogue_main = str[1].Split(':')[1];
        //    Ctrl_Main._Instance.Change_Dialogue(object_num, dialogue_main);
        //    Ctrl_Main._Instance.Write_Dialogue_Frame(str[2]);
        //}

        public void Simple_Dialogue(string str) {
            int next_dialogue_num = int.Parse(str.Split('&')[1]);
            _dialogue_object = Object_name2_Object_num(str.Split(':')[0]);
            string dialogue = str.Split('&')[0].Split(':')[1];

            Ctrl_Main._Instance.Change_Dialogue(dialogue);
            Dialogue_Num_Change(next_dialogue_num);
        }

        public int Object_name2_Object_num(string object_name) {
            int object_num = 0;
            switch (object_name)
            {
                case "a":
                    object_num = 0;
                    _dialogue_object_type = 0;
                    break;
                case "b":
                    object_num = 1;
                    _dialogue_object_type = 0;
                    break;
                default:
                    object_num = int.Parse(object_name);
                    _dialogue_object_type = 1;
                    break;
            }
            return object_num;
        }

        public void Dialogue_Num_Change(int num) {
            _dialogue_num = num;
        }
   }
}
