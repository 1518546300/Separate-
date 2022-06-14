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

namespace View{
   public class View_GameResult : MonoBehaviour
   {
        public GameObject _stars;
        //public Image[] _star_three;
        //public Sprite[] _star;
        public GameObject _m_num;
        public Text _num;

        public Text _content;

        /// <summary>
        /// flag = 1 为战胜，flag = 0 为战败
        /// </summary>
        /// <param name="flag"></param>
        public void Win_Or_Debeat(int flag,string str, int materal_num, Color color)
        {
            //_stars.SetActive(true);
            //Debug.Log(materal_num);
            //for (int i = 0; i < star_num; i++)
            //{
            //    _star_three[i].sprite = _star[1];
            //}
            Debug.Log(flag);
            if (flag == 1) {
                _m_num.SetActive(true);
                _num.text = materal_num.ToString();
            }
            _content.text = str;
            _content.color = color;
        }

        public void ConfirmBt_Clicked()
        {
            Ctrl_Scene1._Instance.Give_Up_Game();
        }
    }
}
