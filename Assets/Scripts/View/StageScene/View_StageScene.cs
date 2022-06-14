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

using Model;
using Control;

namespace View{
   public class View_StageScene : MonoBehaviour
   {
        public static View_StageScene _Instance;
        public GameObject _stage_table;

        public List<GameObject> _bts = new List<GameObject>();

        public Image _picture_bg;
        //public Text _page_name;

        public Text _star_num;

        public GameObject _right_arrow;
        public GameObject _left_arrow;

        public GameObject _page_info_board;

        public Animator _page_info;
        public AudioClip[] _bt_music;

        public void Awake()
        {
            _Instance = this;
        }

        public void Init_Scene(Model_LoginSceneInfo info)
        {
            //_star_num.text = info._player_own_star.ToString();
            for (int i = 0; i < info._page_stage_num; i++)
            {
                GameObject bt = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/StageScene/stage_bt"));
                bt.transform.SetParent(_stage_table.transform);
                _bts.Add(bt);
            }
            Enter_Next_Page();
        }

        public void Print_Pos()
        {
            string pos = "";
            foreach (Transform bt in _stage_table.transform)
            {
                pos += bt.localPosition.x + "," + bt.localPosition.y + "/";
            }
            Debug.Log(pos);
        }

        public void Rewrite_Info(Model_LoginSceneInfo info)
        {
            Rewrite_Arrow_Name(info._page_number);
            _picture_bg.sprite = Ctr_Main._Instance._res._sprites.page_image[info._page_number];
            //_page_name.text = info._page_name;
            _page_info_board.GetComponent<View_PageInfoBoard>().Init_Page_Board_Info(info._page_name, "作者:卡西", "年份:1998年", info._page_describe);
            for (int i = 0; i < info._page_stage_num; i++)
            {
                float x = float.Parse(info._stage_pos[i].Split(',')[0]);
                float y = float.Parse(info._stage_pos[i].Split(',')[1]);

                _bts[i].transform.localPosition = new Vector2(x, y);
                //_bts[i].transform.GetComponent<View_Bt>().Init(info._stage_indexs[i] + 1, info._player_own_star, info._stage_req_star[i], info._stage_name[i], info._stage_collect_star[i]);
            }
        }

        public void Enter_Next_Page()
        {
            Rewrite_Info(Ctr_Main._Instance._info);
        }

        public void Rewrite_Arrow_Name(int page_num)
        {
            _right_arrow.SetActive(true);
            _left_arrow.SetActive(true);
            if (page_num == 0)
            {
                _left_arrow.SetActive(false);
                _right_arrow.transform.Find("next_page_bt").Find("title").GetComponent<Text>().text = "第 " + (page_num + 2) + " 章";
            }
            else if (page_num == Ctr_Main._Instance._info._page_total_num-1) {
                _left_arrow.transform.Find("title").GetComponent<Text>().text = "第 " + page_num + " 章";
                _right_arrow.SetActive(false);
            }
            else
            {
                _left_arrow.transform.Find("title").GetComponent<Text>().text = "第 " + page_num + " 章";
                _right_arrow.transform.Find("next_page_bt").Find("title").GetComponent<Text>().text = "第 " + (page_num + 2) + " 章";
            }



            //if (Ctr_Main._Instance._info._player_own_star > Ctr_Main._Instance._info._stage_req_star[5])
            //{
            //    _right_arrow.transform.Find("lock").gameObject.SetActive(false);
            //}
            //else
            //{
            //    _right_arrow.transform.Find("lock").gameObject.SetActive(true);
            //}
        }

        public void Show_Page_Info()
        {
            _page_info.Play("page_info_idel");
            _page_info.Update(0f);
            _page_info.SetTrigger("show");
            _page_info.ResetTrigger("hidden");
        }

        public void Page_Info_Hidden()
        {
            _page_info.SetTrigger("hidden");
        }

        public void Bt_Clicked(int type) {

            switch (type)
            {
                case 0: //return_menu
                    AudioManager._Instance.Bt_Audio_Play_Intime(_bt_music[0]);
                    Ctr_Main._Instance.Back_Menu();
                    Ctr_Main._Instance.Remember_Page();
                    break;
                case 1: //last_page
                    AudioManager._Instance.Bt_Audio_Play_Intime(_bt_music[1]);
                    Ctr_Main._Instance.Last_Stage_Bt_Clicked();
                    break;
                case 2: //next_page
                    AudioManager._Instance.Bt_Audio_Play_Intime(_bt_music[1]);
                    Ctr_Main._Instance.Next_Stage_Bt_Clicked();
                    break;
                case 3: //Page_Info_Show
                    AudioManager._Instance.Bt_Audio_Play_Intime(_bt_music[1]);
                    break;
                default:
                    break;
            }
        }
    }
}
