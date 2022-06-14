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
using Model;
using View;

namespace Control{
   public class Ctr_LoginScene : BaseControl
   {
        public static Ctr_LoginScene _Instance;

        public AudioClip _bgMusic;

        public ConfigManager _game_save_controllor;
        public Model_ResourcesManager _res;

        public bool _thanks_flag = false;

        void Awake()
        {
            _Instance = this;
            _res = new Model_ResourcesManager();
        }

        void Start()
        {
            _game_save_controllor = ConfigManager._instance;
            View_LoginScene._Instance.Change_Start_Bt(_game_save_controllor._game_save_control._game_save_file._stage_num);
            ChangeResolution(_game_save_controllor.resolutionGet());
            AudioManager._Instance.AudioPlay(_bgMusic);
        }

        public void GameRestart()
        {
            GlobalParameter.PAGENUM = 0;
            _game_save_controllor.reloadingJson();
            GameStart();
        }

        public void GameStart() {
            EnterNextScene(ScenesEnum.StagesView);
        }

        public void Thanks_Frame_Show() {
            string name = View_LoginScene._Instance._thanks_frame.transform.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name;
            if (name == "thanks_hidden_idle") {
                _thanks_flag = !_thanks_flag;
                View_LoginScene._Instance.Thanks_Frame_Show();
            }
        }

        public void Thanks_Frame_Hidden()
        {
            string name = View_LoginScene._Instance._thanks_frame.transform.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name;
            if (name == "thanks_idle")
            {
                View_LoginScene._Instance.Thanks_Frame_Hidden();
            }
        }

        public void ChangeResolution(bool flag)
        {
            View_LoginScene._Instance.Change_Screen_Bt(flag);
            if (flag)
            {
                Screen.SetResolution(1920, 1080, true);
                return;
            }
            
            Screen.SetResolution(1280, 720, false);
        }

        public void ChangeResolutionBtClicked() {
            _game_save_controllor.changeResolution(!_game_save_controllor.resolutionGet());
            ChangeResolution(_game_save_controllor.resolutionGet());
        }

        public void Exit_Game()
        {
            _game_save_controllor.saveJson();
            Application.Quit();
        }
    }
}

