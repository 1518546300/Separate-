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
   public class View_EventCardBt : MonoBehaviour
   {
        public int _card_type;
        public int _current_index;
        public int _card_index;

        public GameObject _frame;
        public Text _name_text;
        public Image pic;

        public GameObject _table;

        public void Init(int card_type,int index,int num,string name,Sprite _sprite,GameObject table) {
            _frame.SetActive(false);
            _card_type = card_type;
            _current_index = index;
            _card_index = num;
            _name_text.text = name;
            pic.overrideSprite = _sprite;
            _table = table;
        }

        public void Bt_Clicked() {
            if (_frame.activeSelf == true)
            {
                _frame.SetActive(!_frame.activeSelf);
                _table.transform.GetComponent<View_EventCardCollect>()._collect_card_count++;
                _table.transform.GetComponent<View_EventCardCollect>().Rewrite_Collect_Count();
            }
            else
            {
                if (_table.transform.GetComponent<View_EventCardCollect>()._collect_card_count > 0)
                {
                    _frame.SetActive(!_frame.activeSelf);
                    _table.transform.GetComponent<View_EventCardCollect>()._collect_card_count--;
                    _table.transform.GetComponent<View_EventCardCollect>().Rewrite_Collect_Count();
                }
            }
        }
   }
}
