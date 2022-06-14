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

namespace View{
   public class View_ChoiceBar : MonoBehaviour
   {
        public void Choice_Bar_Clicked() {
            View_Main._Instance.Choice_Bar_Clicked(int.Parse(this.name));
        }
   }
}
