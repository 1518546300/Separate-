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

using Global;
using View;
using Model;

namespace Control{
   public class Ctrl_Main : BaseControl
   {
        public static Ctrl_Main _Instance;
        public Model_ResourcesManager _res;
        public Model_PageInfo _page_model;
        public Model_DialogueFrame _dialogue_frame_model;
        public AudioClip bg_music;
        public ConfigManager _game_save_controllor;

        void Awake()
        {
            _Instance = this;
            _res = new Model_ResourcesManager();
        }

        void Start()
        {
            _game_save_controllor = ConfigManager._instance;
            _game_save_controllor.readJson();
            _page_model = new Model_PageInfo(_game_save_controllor._game_save_control._game_save_file._talking_status, 
                                             _game_save_controllor._game_save_control._game_save_file._stage_num,
                                             _game_save_controllor._game_save_control._game_save_file._dialogue_num, _res._dialogue
                                             );
            _dialogue_frame_model = new Model_DialogueFrame();
            Start_Dialogue();
            if (_game_save_controllor._game_save_control._game_save_file._stage_num > StageParameter.MATERIALSTAGENUM)
            {
                View_Main._Instance.Material_Show();
            }
            else {
                View_Main._Instance.Material_Hidden();
            }
            AudioManager._Instance.AudioPlay(bg_music);
        }

        public void Start_Dialogue() {
            _page_model.Read_Dialogue();
        }

        public void Change_Bt_Status(int num)
        {
            View_Main._Instance.Change_Bt_Status(num);
        }

        public void Enter_Next_Scene()
        {
            _game_save_controllor._game_save_control._game_save_file._dialogue_num = _page_model._dialogue_num;
            _game_save_controllor._game_save_control._game_save_file._talking_status = 1;
            _game_save_controllor.saveJson();
            EnterNextScene(ScenesEnum.Scene1);
        }

        public void Next_Dialogue() {
            View_Main._Instance.Clear_Pics();
            View_Main._Instance.Main_Dialogue_Clear();
            _page_model.Read_Dialogue();
        }

        public void Change_Dialogue(string dialogue) {
            int object_num = _page_model._dialogue_object;
            if (_page_model._dialogue_object_type == 1)
            {
                int num = _page_model._dialogue_object;
                View_Main._Instance.Hero_Avatar_Change(_res._sprites.hero_sprites[num], _res._hero_card.heros_name[num], dialogue);
                View_Main._Instance.Change_Dialogue(2, _res._hero_card.heros_name[num], dialogue);
            }
            else
            {
                View_Main._Instance.Avatar_Change(object_num);
                View_Main._Instance.Change_Dialogue(object_num, _page_model._actor_name[object_num], dialogue);
            }
        }

        public void Write_Dialogue_Frame(string raw_dialogue) {
            _dialogue_frame_model.Write_Choice_Frame_Dialogue(raw_dialogue);
            View_Main._Instance.Dialogue_Frame_Write(_dialogue_frame_model);
        }

        public void Dialogue_Bar_Choice(int index) {
            int next_dialogue_num = _dialogue_frame_model._choice_frame_next_dialogue_num[index];
            _page_model.Dialogue_Num_Change(next_dialogue_num);
            Next_Dialogue();
        }

        public void Enter_Scene(int scene_num,int page_num) {
            if (_page_model._status == 0)
            {
                GlobalParameter.SCENENUM = scene_num;
            }
            else
            {
                GlobalParameter.SCENENUM = _game_save_controllor._game_save_control._game_save_file._stage_num;
            }
            
            GlobalParameter.PAGENUM = page_num;
            _page_model._page_number = page_num;
        }

        public void Pics_Show(string config)
        {
            int num = int.Parse(config);
            View_Main._Instance.Pic_Show(_res._dialogue.pics[1][num]);
        }

        //public void Hero_Dialogue(int num,string dialogue) {
        //    View_Main._Instance.Hero_Dialogue(_res._sprites.hero_sprites[num],_res._hero_card.heros_name[num],dialogue);
        //}

        //public void Hero_Next_Scene(int num, string dialogue) {
        //    View_Main._Instance.Change_Bt_Status(3);
        //    View_Main._Instance.Hero_Dialogue(_res._sprites.hero_sprites[num], _res._hero_card.heros_name[num], dialogue);
        //}

        public void Back_Bt_Clicked() {
            EnterNextScene(ScenesEnum.FirstScene);
        }
    }
}