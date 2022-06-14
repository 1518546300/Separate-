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
   public class View_RoundPrompt : MonoBehaviour
   {
        public Animator _animator_ctrl;
        public ParticleSystem _effect;
        public Text _prompt_text;
        public Text _show_box_text;

        public void Round_Control_Change(string name) {
            
            _prompt_text.text = name;
            Change_ShowBox(name);

            Prompt_Show();
            Invoke("Play_Effect", 0.5f);
        }

        public void Play_Effect() {
            _effect.Play();
        }

        public void Change_ShowBox(string str) {
            _show_box_text.text = str;
        }

        public void Prompt_Hidden() {
            _animator_ctrl.SetTrigger("hidden");
        }

        public void Prompt_Show() {
            _animator_ctrl.SetTrigger("show");
        }
   }
}
