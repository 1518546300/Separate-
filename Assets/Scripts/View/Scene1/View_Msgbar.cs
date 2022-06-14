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
   public class View_Msgbar : MonoBehaviour
   {
        public Text _title;
        public Text _info;
        public Text _year;

        public void Init(string title,string info,int year,Color color)
        {
            _title.text = title;
            _year.text = "第" + year + "年";
            _info.text = info;
            _info.color = color;
        }
    }
}
