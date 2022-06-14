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

namespace Control{
   public class Ctr_Game
   {
        public void Game_Prepare() {
            Ctrl_Scene1._Instance.Scene_Init();
        }

        public Model_GameMsg Game_Judge() {
            Model_GameMsg _msg = new Model_GameMsg();
            string config = Ctrl_Scene1._Instance._scene_info._game_config;

            _msg._game_result = Class_Func(config);
            return _msg;
        }

        /// <summary>
        /// 实体意志满足一定条件则胜利
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public int[] Class_Func(string config) {
            int[] ret = { 0, 0 };

            string[] year_condition = config.Split('/')[0].Split(',');

            int pre_year = int.Parse(year_condition[0]);
            int last_year = int.Parse(year_condition[1]);

            string[] grid_index = config.Split('/')[1].Split(',');
            string[] willpower_condition = config.Split('/')[2].Split(',');

            if (Ctrl_Scene1._Instance._scene_info._scene_year < last_year)
            {
                Ctrl_Scene1._Instance._opponent_info.Opponent_Control_Grid_Summarize();

                if (Ctrl_Scene1._Instance._opponent_info._grid_occupy.Count > 0)
                {
                    return ret;
                }

                ret[0] = 1;
                if (Ctrl_Scene1._Instance._scene_info._scene_year < pre_year) {
                    ret[1] = 3;
                }
                else if (Ctrl_Scene1._Instance._scene_info._scene_year >= pre_year && Ctrl_Scene1._Instance._scene_info._scene_year < last_year) {
                    ret[1] = 2;
                }
                else {
                    ret[1] = 1;
                }
            }
            else {
                ret[0] = 2;
                ret[1] = 0;
            }
            return ret;
        }

        //public int[] Class1_Func(string config)
        //{
        //    int[] ret = { 0, 0 };
        //    string[] year_condition = config.Split(',')[0].Substring(1, config.Split(',')[0].Length - 2).Split('|');

        //    int grid_index = int.Parse(config.Split(',')[1]);
        //    int year_limit = int.Parse(config.Split(',')[2]);

        //    Debug.Log(Ctrl_Scene1._Instance._scene_info._grids[grid_index]._hero_level);

        //    if (Ctrl_Scene1._Instance._scene_info._scene_year < year_limit)
        //    {
        //        if (Ctrl_Scene1._Instance._scene_info._grids[grid_index]._hero_level != 2)
        //        {
        //            return ret;
        //        }
        //    }

        //    int year = Ctrl_Scene1._Instance._scene_info._scene_year;
        //    if (Ctrl_Scene1._Instance._scene_info._scene_year == year_limit && Ctrl_Scene1._Instance._scene_info._grids[grid_index]._hero_level != 2)
        //    {
        //        ret[0] = 2;
        //        ret[1] = 0;
        //    }
        //    else
        //    {
        //        ret[0] = 1;
        //        if (year < int.Parse(year_condition[0]))
        //        {
        //            ret[1] = 3;
        //        }
        //        else if (year >= int.Parse(year_condition[0]) && year < int.Parse(year_condition[1]))
        //        {
        //            ret[1] = 2;
        //        }
        //        else if (year >= int.Parse(year_condition[1]))
        //        {
        //            ret[1] = 1;
        //        }
        //        else
        //        {
        //            ret[0] = 2;
        //            ret[1] = 0;
        //        }
        //    }
        //    return ret;
        //}
    }
}
