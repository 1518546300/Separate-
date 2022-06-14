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
   public class View_LoadingCurtain : MonoBehaviour
   {
        public Text _saying_text;
        public Text _name_text;
        public Animator _anim;
        public GameObject _mask;

        public void Change_Saying(string saying,string name) {
            _name_text.text = name;
            _saying_text.text = saying;
        }

        public void Curtains_Finally_Closin() {
            _mask.SetActive(false);
            _anim.SetTrigger("close");
        }
   }
}
