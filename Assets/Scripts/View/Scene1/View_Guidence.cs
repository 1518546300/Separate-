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
   public class View_Guidence : MonoBehaviour
   {
        public Text _guidence_word;
        public int _guidence_num = 0;
        public GameObject _arrow;

        public void Init()
        {
            _guidence_num = 0;
            Next_Guidence();
        }

        public void Next_Guidence() {
            if (_guidence_num < Ctrl_Scene1._Instance._res._guidence.word.Count) {
                string[] config = Ctrl_Scene1._Instance._res._guidence.config[_guidence_num].Split('/');
                _guidence_word.text = Ctrl_Scene1._Instance._res._guidence.word[_guidence_num];
                if (int.Parse(config[0]) == 1)
                {
                    _arrow.SetActive(true);
                    string[] pos = config[1].Split(',');
                    string[] rotation = config[2].Split(',');
                    _arrow.transform.localPosition = new Vector2(int.Parse(pos[0]), int.Parse(pos[1]));
                    _arrow.transform.localRotation = Quaternion.Euler(new Vector3(int.Parse(rotation[0]), int.Parse(rotation[1]), int.Parse(rotation[2])));
                }
                else {
                    _arrow.SetActive(false);
                }
                _guidence_num++;
            }
            else {
                Ctrl_Scene1._Instance.guidenceOver();
            }
        }
   }
}
