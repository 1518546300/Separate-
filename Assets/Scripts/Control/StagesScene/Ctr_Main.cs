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
   public class Ctr_Main : BaseControl
   {
        public static Ctr_Main _Instance;
        public Model_ResourcesManager _res;
        public Model_LoginSceneInfo _info;

        public ConfigManager _game_save_controllor;

        public AudioClip _bgMusic;


        void Awake()
        {
            _Instance = this;
            //_game_save_control.Read_Json();
        }

        void Start()
        {
            _game_save_controllor = ConfigManager._instance;
            _game_save_controllor.readJson();
            
            _res = new Model_ResourcesManager();
            _info = new Model_LoginSceneInfo(GlobalParameter.PAGENUM,_res._scene_config);
            AudioManager._Instance.AudioPlay(_bgMusic);
            View_StageScene._Instance.Init_Scene(_info);
        }

        public void Enter_Next_Scene(int num) {
            GlobalParameter.SCENENUM = num;
            Remember_Page();
            EnterNextScene(ScenesEnum.Scene1);
        }

        public void Remember_Page(){
            GlobalParameter.PAGENUM = _info._page_number;
        }

        public void Back_Menu()
        {
            EnterNextScene(ScenesEnum.FirstScene);
        }

        public void Next_Stage_Bt_Clicked() {
            if (_info._page_number < _info._page_total_num) {
                _info._page_number++;
                _info.Rewrite_Stage_Info(_res._scene_config);
                View_StageScene._Instance.Enter_Next_Page();
            }
        }

        public void Last_Stage_Bt_Clicked() {
            if (_info._page_number > 0)
            {
                _info._page_number--;
                _info.Rewrite_Stage_Info(_res._scene_config);
                View_StageScene._Instance.Enter_Next_Page();
            }
        }
    }
}
