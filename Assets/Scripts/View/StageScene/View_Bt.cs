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
using Global;

namespace View{
   public class View_Bt : MonoBehaviour
   {
        public Text _title;

        public Text _stars_num1;
        public Text _stars_num2;

        public Text _name;
        public Image _ball;
        public Image[] _stars;
        public Sprite[] _star_sprite;

        public GameObject _lock;
        public int _stage_num;

        public void Init(int num,int own_num,int constant_num,string name,int star_num) {
            _stage_num = num;
            _title.text = "第" + num +"关";
            _stars_num1.text = own_num.ToString();
            _stars_num2.text = constant_num.ToString();
            if (own_num < constant_num)
            {
                _stars_num2.color = GlobalParameter.BTCOLOR[1];
            }
            else
            {
                _stars_num2.color = GlobalParameter.BTCOLOR[0];
            }
            _name.text = name;

            if (own_num >= constant_num)
            {
                _lock.SetActive(false);
            }
            else
            {
                _lock.SetActive(true);
            }

            Star_Init(star_num);
        }

        public void Star_Init(int num) {
            for (int i = 0; i < 3; i++)
            {
                _stars[i].overrideSprite = _star_sprite[0];
            }
            for (int i = 0; i < num; i++) {
                _stars[i].overrideSprite = _star_sprite[1];
            }
        }

        public void Enter_Game() {
            //Debug.Log(_stage_num);
            AudioManager._Instance.Bt_Audio_Play_Intime(View_StageScene._Instance._bt_music[2]);
            Ctr_Main._Instance.Enter_Next_Scene(_stage_num);
        }
   }
}
