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
using Model;
using Global;

namespace View{
   public class View_GridScript:MonoBehaviour
   {
        //private float grid_range;
        public Image bg;
        public Sprite[] _bg_color;
        public Image _pic;
        
        public Animator hero_placed;

        public Sprite[] _life_bar_color;
        public Image _hero_life_line;

        public Animator _smog;

        public int _grid_index;

        public GameObject _fluorescence;

        public int _current_action_index;
        public int _current_message_stack;
        public int _run_action_flag = 0;

        public Text _cost_num;
        public Text _cost_title;
        public GameObject _cost_info;
        public GameObject _tag;
        public GameObject _particle;
        public GameObject _die_status;

        public GameObject _arrow;

        /// <summary>
        /// 英雄放置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="describe"></param>
        /// <param name="color"></param>
        public void Hero_Set(int index,int grid_group)
        {
            Debug.Log(grid_group);
            hero_placed.SetTrigger("hero_placed");
            _pic.overrideSprite = Ctrl_Scene1._Instance._res._sprites.hero_sprites[index];
            bg.overrideSprite = _bg_color[grid_group];
        }

        public void Grid_Init(int grid_num,Model_Grid grid)
        {
            _grid_index = grid_num;
            bg.overrideSprite = _bg_color[grid._class_num];
            Change_HeroStatus(grid);
        }

        public void Change_HeroStatus(Model_Grid grid)
        {
            if (grid._hero_state == 1)
            {
                Smog_Func("smog");
            }
            else
            {
                Smog_Func("idel");
            }
            float scale = 1 - ((float)(grid._hero_willpower_constant - grid._hero_willpower_own) / grid._hero_willpower_constant);
            float width = (Scene1Parameter.GRIDWILLPOWERLEN[1] - Scene1Parameter.GRIDWILLPOWERLEN[0]) * scale + Scene1Parameter.GRIDWILLPOWERLEN[0];
            _hero_life_line.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 25F);
            _hero_life_line.overrideSprite = _life_bar_color[grid._hero_state-1];
        }

        public void Smog_Func(string instruction)
        {
            _smog.ResetTrigger("smog");
            _smog.ResetTrigger("idel");
            _smog.SetTrigger(instruction);
        }

        public void ClearStatus() {
            bg.overrideSprite = _bg_color[0];
            transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }

        public void Clicked()
        {
            Ctrl_Scene1._Instance.Info_Box_Show(_grid_index);
        }

        public void Run_Grid_ImmAction()
        {
            if (_run_action_flag == 0) {
                _run_action_flag = 1;
                _current_action_index = 0;
                Model_Grid grid = Ctrl_Scene1._Instance._scene_info._grids[_grid_index];
                StartCoroutine(Immediate_Grid_Action(grid));
            }
        }

        public void Run_GridOver_Action()
        {
            if (_run_action_flag == 0)
            {
                _run_action_flag = 1;
                _current_action_index = 0;
                Model_Grid grid = Ctrl_Scene1._Instance._scene_info._grids[_grid_index];
                StartCoroutine(GridOver_Action(grid));
            }
           
        }

        public void Willpower_Particle_Action(int type,int cost,string title,string cost_name)
        {
            _cost_info.SetActive(true);
            _cost_title.text = cost_name;
            _cost_num.color = GlobalParameter.TEXTCOLOR[type - 1];
            _cost_num.text = title + (type == 1 ? "-" : "+") + cost.ToString();
            ParticleSystem pc = _particle.transform.GetChild(type-1).gameObject.GetComponent<ParticleSystem>();
            pc.Play();
            Invoke("Cost_Tip_Hidden", 1);
        }

        public void Material_Particle_Action(int type, int cost, string title,string cost_name)
        {
            _cost_info.SetActive(true);
            _cost_title.text = cost_name;
            _cost_num.color = GlobalParameter.TEXTCOLOR[type - 1];
            _cost_num.text = title + (type == 1 ? "-" : "+") + cost.ToString();
            ParticleSystem pc = _particle.transform.GetChild(1+type).gameObject.GetComponent<ParticleSystem>();
            pc.Play();
            Invoke("Cost_Tip_Hidden", 1);
        }

        public void Cost_Tip_Hidden()
        {
            _cost_info.SetActive(false);
        }

        IEnumerator Immediate_Grid_Action(Model_Grid grid)
        {
            Arrow_Show();
            while (_current_action_index < grid._message_stack[grid._current_msgstack_index].Count)
            {
                Model_GridMessage msg = grid._message_stack[grid._current_msgstack_index][_current_action_index];
                float time = msg._msg_time;
                switch (msg._msg_type)
                {
                    case 0:
                        Hero_Set(msg._msg_hero_index, grid._class_num);
                        Grid_Tips_Show(msg._msg_info, msg._msg_info_color);
                        Change_HeroStatus(grid);
                        break;
                    case 1://放置失败
                        Grid_Tips_Show(msg._msg_info, msg._msg_info_color);
                        break;
                    case 2:
                        Hero_Set(msg._msg_hero_index, grid._class_num);
                        Ctrl_Scene1._Instance.Hero_Update(_grid_index);
                        Change_HeroStatus(grid);
                        Grid_Tips_Show(msg._msg_info, msg._msg_info_color);
                        break;
                    case 3://意志
                        Ctrl_Scene1._Instance.Change_Hero_Willpower(msg._msg_flag,_grid_index, msg._msg_effect_num);
                        Willpower_Particle_Action(msg._msg_flag, msg._msg_effect_num,"意识" ,msg._msg_name);
                        break;
                    case 4://物质
                        Ctrl_Scene1._Instance.Grid_Material_Change(msg._msg_flag, _grid_index, msg._msg_effect_num);
                        Material_Particle_Action(msg._msg_flag, msg._msg_effect_num, "物质" ,msg._msg_name);
                        break;
                    case 5://

                        break;
                    case 6://
                        
                        break;
                    case 7:
                        //Ctrl_Scene1._Instance.Grid_Get_Material(_grid_index, msg._msg_effect_num);
                        //Particle_Action(msg._msg_effect_type, msg._msg_effect_num, msg._msg_name, msg._msg_info_color);
                        break;
                    case 8://总物质效果
                        Ctrl_Scene1._Instance.Change_Player_Material(msg._msg_object_num,msg._msg_flag, msg._msg_effect_num);
                        //Ctrl_Scene1._Instance._task.Add_Player_Material_Msg(msg._msg_flag,msg._msg_effect_num , 0);
                        //Ctrl_Scene1._Instance._task.Run_Player_Task();
                        break;
                    case 9:
                        Arrow_Show();
                        Invoke("Arrow_Hidden", 1);
                        break;
                    case 10:
                        Class_Change(msg._msg_hero_change_status);
                        break;
                    default:
                        break;
                }
                _current_action_index++;
                yield return new WaitForSeconds(time);
            }
            Arrow_Hidden();
            _current_action_index = 0;
            grid._message_stack[grid._current_msgstack_index].Clear();

            if (grid._current_msgstack_index == 0)
            {
                if (grid._message_stack[1].Count > 0)
                {
                    grid._current_msgstack_index = 1;
                    yield return new WaitForSeconds(0);
                }
                else
                {
                    grid._current_msgstack_index = 0;
                }
            }
            else
            {
                if (grid._message_stack[0].Count > 0)
                {
                    grid._current_msgstack_index = 0;
                    yield return new WaitForSeconds(0);
                }
                else
                {
                    grid._current_msgstack_index = 0;
                }
            }
            _run_action_flag = 0;
        }

        IEnumerator GridOver_Action(Model_Grid grid)
        {
            Arrow_Show();
            while (_current_action_index < grid._message_settle_stack.Count)
            {
                Model_GridMessage msg = grid._message_settle_stack[_current_action_index];
                float time = msg._msg_time;
                //if (Scene1Parameter.HURRYUPFLAG) {
                //    time = 0;
                //}
                switch (msg._msg_type)
                {
                    case 3://意志
                        Ctrl_Scene1._Instance.Change_Hero_Willpower(msg._msg_flag, _grid_index, msg._msg_effect_num);
                        Willpower_Particle_Action(msg._msg_flag, msg._msg_effect_num, "意识" ,msg._msg_name);
                        break;
                    case 4://物质
                        Ctrl_Scene1._Instance.Grid_Material_Change(msg._msg_flag, _grid_index, msg._msg_effect_num);
                        Material_Particle_Action(msg._msg_flag, msg._msg_effect_num,"物质" ,msg._msg_name);
                        break;
                    default:
                        break;
                }
                yield return new WaitForSeconds(time);
                _current_action_index++;
            }
            Arrow_Hidden();
            _current_action_index = 0;
            grid._message_settle_stack.Clear();
            _run_action_flag = 0;
        }

        public void Arrow_Show() {
            _arrow.SetActive(true);
        }

        public void Arrow_Hidden() {
            _arrow.SetActive(false);
        }

        public void Grid_Tips_Show(string str, Color color)
        {
            _tag.SetActive(true);
            _tag.transform.Find("bg").Find("text").GetComponent<Text>().text = str;
            _tag.transform.Find("bg").Find("text").GetComponent<Text>().color = color;
            Invoke("Grid_Tips_Hidden", 1);
        }

        public void Grid_Tips_Hidden()
        {
            _tag.SetActive(false);
        }

        public void Class_Change(int class_num) {
            if (class_num == 3) {
                Ctrl_Scene1._Instance._scene_info._grids[_grid_index]._class_num = class_num;
                _die_status.SetActive(true);
                bg.overrideSprite = _bg_color[0];
            }
        }
    }
}

