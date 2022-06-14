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

namespace Control{
   public class ConfigManager : MonoBehaviour
   {
        public GameSave _game_save_control;
        public static ConfigManager _instance;

        void Awake()
        {
            if (_instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                _instance = this;
                _game_save_control.Init_JsonData(0, GlobalParameter.GAMECONFIGJSONPATH);
            }
        }

        public bool resolutionGet() {
            return _game_save_control._game_save_file._resolution_flag;
        }

        public bool changeResolution(bool flag) {
            return _game_save_control.changeResolution(flag);
        }

        public void reloadingJson() { 
            _game_save_control.Init_JsonData(1, GlobalParameter.GAMECONFIGJSONPATH);
        }

        public void readJson() {
            _game_save_control.Read_Json();
        }

        public void saveJson() {
            _game_save_control.Save_Json();
        }
    }
}
