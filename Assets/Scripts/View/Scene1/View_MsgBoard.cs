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
   public class View_MsgBoard : MonoBehaviour
   {
        public GameObject _content;
        public GameObject _msg_temp;

        public List<GameObject> bar_array = new List<GameObject>();

        public Dictionary<string, List<GameObject>> bar_pool = new Dictionary<string, List<GameObject>>();

        public void Add_MsgBar(string title,string info,int year,Color color) {
            if (bar_array.Count >= 4) {
                PushCardToPool(bar_array[0]);
            }
            GameObject msg_bar = Get_Bar_From_Pool();
            msg_bar.transform.GetComponent<View_Msgbar>().Init(title,info,year,color);
            msg_bar.transform.SetParent(_content.transform);
            msg_bar.transform.localScale = new Vector2(1, 1);
            bar_array.Add(msg_bar);
            _content.transform.localPosition = new Vector2(0,-132);
            _content.transform.GetComponent<RectTransform>().sizeDelta = new Vector2(
                                                                            0,
                                                                            _content.transform.childCount*53
            );
        }

        public GameObject Get_Bar_From_Pool()
        {
            GameObject bar = null;

            if (bar_pool.ContainsKey("card") && bar_pool["card"].Count > 0)
            {
                bar = bar_pool["card"][0];
                bar_pool["card"].RemoveAt(0);
            }
            else
            {
                bar = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Scene/msg_bar"));
            }

            bar.transform.SetParent(_content.transform);
            bar.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            bar.SetActive(true);

            return bar;
        }

        public void PushCardToPool(GameObject bar)
        {
            bar.transform.SetParent(_msg_temp.transform);
            bar.SetActive(false);
            bar_array.Remove(bar);

            if (bar_pool.ContainsKey("card"))
            {
                bar_pool["card"].Add(bar);
            }
            else
            {
                bar_pool.Add("card", new List<GameObject>() { bar });
            }
        }
    }
}
