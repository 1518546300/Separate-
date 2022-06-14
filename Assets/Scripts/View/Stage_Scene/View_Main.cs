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
using Model;
using UnityEngine.UI;

//using Model;
using Control;

namespace View{
   public class View_Main : MonoBehaviour
   {
        public static View_Main _Instance;

        
        public GameObject _avatar;
        public GameObject _dialogue_frame;
        public GameObject _pics_frame;

        public GameObject _material_obj;
        public Text _material_text;

        void Start()
        {
            _Instance = this;
            Clear_Pics();
        }

        public void Pic_Show(string config)
        {
            _pics_frame.SetActive(true);
            _pics_frame.GetComponent<View_PicsPlane>().Pics_Show(config);
        }

        public void Material_Show()
        {
            _material_obj.SetActive(true);
            _material_text.text = Ctrl_Main._Instance._game_save_controllor._game_save_control._game_save_file._material_num.ToString();
        }

        public void Material_Hidden() {
            _material_obj.SetActive(false);
        }

        public void Clear_Pics() {
            _pics_frame.GetComponent<View_PicsPlane>().Clear_Pics();
        }

        public void Scene_Init() {
            
        }

        public void Choice_Bar_Clicked(int index) {
            Ctrl_Main._Instance.Dialogue_Bar_Choice(index);
        }

        public void Change_Dialogue(int num,string name,string dialogue) {
            Change_Dialogue_TXT(num,name,dialogue);
        }

        public void Change_Dialogue_TXT(int num,string name,string dialogue) {
            _dialogue_frame.GetComponent<View_DialogueFrame>().Change_Speaker_Name(num,name);
            _dialogue_frame.GetComponent<View_DialogueFrame>().Change_Speaker_Dialogue(name.Length,dialogue);
        }

        public void Dialogue_Frame_Write(Model_DialogueFrame model) {
            _dialogue_frame.GetComponent<View_DialogueFrame>().Write_Choice_Frame(model);
        }

        public void Change_Bt_Status(int num) {
            _dialogue_frame.GetComponent<View_DialogueFrame>().Bt_Status(num);
        }

        public Sprite Get_Dialogue_Pic(int num) {
            return Ctrl_Main._Instance._res._sprites.dialogue_image[num];
        }

        public void Hero_Avatar_Change(Sprite pic, string name, string dialogue) {
            //_pics_frame.SetActive(true);
            _pics_frame.GetComponent<View_PicsPlane>().Hero_Avatar_Change(pic,name,dialogue);
        }

        public void Avatar_Change(int num) {
            _pics_frame.GetComponent<View_PicsPlane>().NPC_Avatar(num);
        }

        public void Main_Dialogue_Clear() {
            _dialogue_frame.GetComponent<View_DialogueFrame>().Dialogue_Clear();
        }
    }
}
