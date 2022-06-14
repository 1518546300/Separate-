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
   public class View_MaterialExchange : MonoBehaviour
   {
        public int _num;
        public Text _num_text; 

        public void Init() {
            _num = 0;
            Change_Text();
        }

        public void Add_Num() {
            int num = Ctrl_Scene1._Instance.Get_Material_Num();
            if (_num + 10 > num)
            {
                _num = num;
            }
            else
            {
                _num += 10;
            }
            Change_Text();
        }

        public void Subtract_Num() {
            if (_num - 10 > 0)
            {
                _num -= 10;
            }
            else {
                _num = 0;
            }
            Change_Text();
        }

        public void Change_Text() {
            _num_text.text = _num.ToString();
        }

        //public void Exchange_Material_Clicked() {
        //    Ctrl_Scene1._Instance.Pass_Material_To_Grid(_num);
        //}
   }
}
