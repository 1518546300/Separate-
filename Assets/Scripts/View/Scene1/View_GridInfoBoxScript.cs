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
   public class View_GridInfoBoxScript:MonoBehaviour
   {
        public Animator _info_box_anim;
        public Text title;
        public Text content;
        public Text _willpower_text;
        public Text _next_level_num;
        public Text _material_cost;

        public Image _hero_state;

        public GameObject avatar;
        public GameObject _updata_hero_bt;
        public Text _grid_status;

        public List<GameObject> _status_object = new List<GameObject>();
        public List<GameObject> _attribute_object = new List<GameObject>();

        //public Slider fighting_slider;

        public GameObject _status_info_frame;
        public GameObject _status_table;
        public GameObject _attribute_table;

        public GameObject _attribute_text_obj;
        public GameObject _status_text_obj;

        public GameObject _material_collect_num;

        public int _grid_index;

        public GameObject[] stars;

        public GameObject _willpower_slip;

        //当前状态 
        private static int status = 0;

        public GameObject _attr_board;
        public GameObject _cost_board;
        public GameObject _info_board;

        public Sprite[] _updata_bt_sprite;

        public bool _updata_flag;

        public void Info_Show(bool flag) {
            _attr_board.SetActive(flag);
            _cost_board.SetActive(flag);
            _info_board.SetActive(flag);
        }

        public void Status_Init(Model_Grid hero_info) {
            foreach (GameObject status in _status_object)
            {
                Destroy(status);
            }

            _status_object.Clear();

            for (int i = 0; i < hero_info._grid_status_list.Count; i++)
            {
                GameObject status = null;
                status = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Scene/status"));
                status.transform.SetParent(_status_table.transform);
                status.transform.localScale = new Vector3(1f, 1f, 1f);
                status.transform.localPosition = new Vector3(Global.Scene1Parameter.STATUSPOS_X + i * Global.Scene1Parameter.STATUSPOS_INT_X,0,0);
                status.transform.GetComponent<View_GridStatusBt>().Init_Status(hero_info._grid_status_list[i]);
                _status_object.Add(status);
            }
        }

        public void Attribute_Init(Model_Grid hero_info)
        {
            foreach (GameObject attribute in _attribute_object)
            {
                Destroy(attribute);
            }

            _attribute_object.Clear();

            for (int i = 0; i < hero_info._grid_attribute_list.Count; i++)
            {
                GameObject attr = null;
                attr = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Scene/attribute"));
                attr.transform.SetParent(_attribute_table.transform);
                attr.transform.localScale = new Vector3(1f, 1f, 1f);
                attr.transform.localPosition = new Vector3(Global.Scene1Parameter.STATUSPOS_X + i * Global.Scene1Parameter.STATUSPOS_INT_X, 0, 0);
                attr.transform.GetComponent<View_GridAttributeBt>().Init_Attribute(hero_info._grid_attribute_list[i]);
                _attribute_object.Add(attr);
            }
            
        }

        public void Info_Box_Show(int grid_num,Model_Grid grid)
        {
            if (status == 0) {
                _info_box_anim.SetTrigger("show");

                string temp_name = grid._hero_name;
                if (temp_name.Length > 6)
                {
                    temp_name = grid._hero_name.Substring(0, 5) + '\n' + grid._hero_name.Substring(5, grid._hero_name.Length - 5);
                }
                title.text = temp_name;

                content.text = grid._hero_decribe;
                _material_cost.text = grid._material_cost.ToString();

                _grid_index = grid_num;

                avatar.transform.GetComponent<Image>().overrideSprite = grid._hero_pic;
                status = 1;

                //fighting_slider.value = 1-;
                float scale = 1-((float)(grid._hero_willpower_constant - grid._hero_willpower_own) / grid._hero_willpower_constant);
                float width = (Scene1Parameter.WILLPOWERLEN[1] - Scene1Parameter.WILLPOWERLEN[0]) * scale + Scene1Parameter.WILLPOWERLEN[0];
                _willpower_slip.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(width, 102F);


                if (grid._hero_willpower_own < 30)
                {
                    _willpower_text.color = Global.GlobalParameter.TEXTCOLOR[0]; 
                }
                else
                {
                    _willpower_text.color = Global.GlobalParameter.TEXTCOLOR[1];
                }

                for (int i = 0; i < 3; i++) {
                    stars[i].SetActive(true);
                }

                for (int i = grid._hero_level; i < 3; i++) {
                    stars[i].SetActive(false);
                }

                _willpower_text.text = grid._hero_willpower_own.ToString() + "/" + grid._hero_willpower_constant.ToString();
                _material_collect_num.transform.Find("num").GetComponent<Text>().text = grid._material_own_num.ToString();

                if (grid._update_level_need == 0)
                {
                    _next_level_num.text = "顶级";
                }
                else
                {
                    _next_level_num.text = grid._update_level_need.ToString();
                }

                //Debug.Log(grid._hero_state);
                _hero_state.sprite = Ctrl_Scene1._Instance._res._sprites.hero_state_triangle[grid._hero_state-1];

                Attribute_Init(grid);
                Status_Init(grid);

                if (grid._class_num == 1)
                {
                    Info_Show(true);
                    _grid_status.color = GlobalParameter.TEXTCOLOR[1];
                    _grid_status.text = "我方";
                }
                else if(grid._class_num == 2) {
                    _material_collect_num.transform.Find("num").GetComponent<Text>().text = "???";
                    Info_Show(false);
                    _grid_status.color = GlobalParameter.TEXTCOLOR[0];
                    _grid_status.text = "敌方";
                }
                else if (grid._class_num == 3)
                {
                    _material_collect_num.transform.Find("num").GetComponent<Text>().text = "???";
                    Info_Show(false);
                    _grid_status.color = GlobalParameter.TEXTCOLOR[3];
                    _grid_status.text = "阵亡";
                }

                _updata_flag = grid.Updata_Hero_Flag();
                if (_updata_flag)
                {
                    _updata_hero_bt.transform.GetComponent<Image>().overrideSprite = _updata_bt_sprite[1];
                }
                else {
                    _updata_hero_bt.transform.GetComponent<Image>().overrideSprite = _updata_bt_sprite[0];
                }
            }
        }

        public void Update_Bt_Clicked() {
            Ctrl_Scene1._Instance.Updata_Grid_Clicked(_grid_index);
        }

        public void InfoBoxHidden()
        {
            if (status == 1){
                _info_box_anim.SetTrigger("hidden");
                Ctrl_Scene1._Instance.Info_Box_Hidden();
                status = 0;
            }
        }

        public void Status_Info_Frame_Show(Vector3 pos,Model_GridStatus status) {
            _status_info_frame.SetActive(true);
            _attribute_text_obj.SetActive(false);
            _status_text_obj.SetActive(true);

            _status_info_frame.transform.Find("status").gameObject.SetActive(true);
            _status_info_frame.transform.Find("decribe").Find("name").GetComponent<Text>().text = status._name;
            _status_info_frame.transform.Find("decribe").Find("info").gameObject.SetActive(true);
            _status_info_frame.transform.Find("decribe").Find("info").GetComponent<Text>().text = status._decribe;
            Text result_text = _status_info_frame.transform.Find("status").Find("text").GetComponent<Text>();
            result_text.color = Global.GlobalParameter.TEXTCOLOR[0];
            result_text.text = status._time.ToString();
            //switch (status._status_class)
            //{
            //    case 0:
            //        result_text.text = '-' + status._damage.ToString();
            //        result_text.color = Global.GlobalParameter.TEXTCOLOR[0];
            //        break;
            //    case 1:
            //        result_text.text = '-' + status._damage.ToString();
            //        result_text.color = Global.GlobalParameter.TEXTCOLOR[0];
            //        break;
            //    case 2:
            //        result_text.text = '+' + status._damage.ToString();
            //        result_text.color = Global.GlobalParameter.TEXTCOLOR[1];
            //        break;
            //    case 3:
            //        result_text.text = '+' + status._damage.ToString();
            //        result_text.color = Global.GlobalParameter.TEXTCOLOR[1];
            //        break;
            //    case 4:
            //        _status_info_frame.transform.Find("result").gameObject.SetActive(false);
            //        break;
            //    default:
            //        Debug.Log("error!!!");
            //        break;
            //}

            _status_info_frame.transform.position = new Vector3(pos.x + Scene1Parameter.STATUSTIPS_INT, pos.y + Scene1Parameter.STATUSTIPS_INT, 0);
        }

        public void Attr_Info_Frame_Show(Vector3 pos, Model_GridAttribute attr)
        {
            _status_info_frame.SetActive(true);
            _attribute_text_obj.SetActive(true);
            _status_text_obj.SetActive(false);

            _status_info_frame.transform.Find("decribe").Find("name").GetComponent<Text>().text = attr._name;
            _status_info_frame.transform.Find("decribe").Find("info").gameObject.SetActive(false);
            _status_info_frame.transform.Find("attr").Find("time_num").GetComponent<Text>().text = attr._rest_time.ToString();
            _status_info_frame.transform.position = new Vector3(pos.x + Scene1Parameter.STATUSTIPS_INT, pos.y + Scene1Parameter.STATUSTIPS_INT, 0);
        }

        public void Info_Frame_Hidden()
        {
            _status_info_frame.SetActive(false);
        }
    }
}

