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

namespace View{
   public class View_YearBox : MonoBehaviour
   {
        public Text _year_text;
        public Image _year_bg;
        public Sprite[] _pics;

        public void Rewrite_Year_Num(int year,int flag)
        {
            _year_text.text = "第 " + year.ToString() + " 年";
            _year_bg.overrideSprite = _pics[flag];
        }
    }
}
