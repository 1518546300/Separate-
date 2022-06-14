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
   public class Model_LoginSceneInfo
   {
        //public int _player_own_star;
        public List<int> _stage_indexs = new List<int>();
        public List<int> _stage_req_star = new List<int>();
        public List<int> _stage_collect_star = new List<int>();

        public int _page_stage_num;
        public List<string> _stage_name = new List<string>();
        public List<string> _stage_pos = new List<string>();
        public string _page_name;
        public int _page_number;
        public string _page_describe;
        public int _page_total_num;

        public Model_LoginSceneInfo(int page_num,SceneConfigObject config) {
            //_player_own_star = Ctr_Main._Instance._game_save_control._game_save_file._star_num;
            _page_number = page_num; //测试
            Rewrite_Stage_Info(config);
            _page_total_num = config.page_name.Count;
        }

        public void Rewrite_Stage_Info(SceneConfigObject config) {
            _page_stage_num = int.Parse(config.page_stage_num[_page_number]);
            _page_name = config.page_name[_page_number];
            _page_describe = config.page_describe[_page_number];

            _stage_indexs.Clear();
            _stage_req_star.Clear();
            _stage_name.Clear();
            _stage_pos.Clear();
            _stage_collect_star.Clear();

            for (int i = _page_number * _page_stage_num; i < (_page_number+1)*_page_stage_num; i++)
            {
                _stage_indexs.Add(i);
                _stage_req_star.Add(int.Parse(config.star_req[i]));
                _stage_name.Add(config.stage_name[i]);
                _stage_pos.Add(config.stage_pos[i]);
                //_stage_collect_star.Add(Ctr_Main._Instance._game_save_control._game_save_file._stage_star_num[i]);
            }
        }
   }
}
