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

using Presistent;

namespace View{
   public class View_PromptionScript:MonoBehaviour
   {
        private bool prompt_status = false;
        public Animator anim;

        public void Prompt_Show()
        {
            anim.SetTrigger(prompt_status == true ? "hidden" : "show");
            prompt_status = !prompt_status;
            TouchControl._Instance.Game_Canvas_Shield(prompt_status);
        }
    }
}

