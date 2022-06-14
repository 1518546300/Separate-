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

using Global;

namespace View{
   public class View_CrystalPlatform : MonoBehaviour
   {

        public Sprite[] _crystal_pic;

        public List<GameObject> _crystal_array = new List<GameObject>();
        public int crystal_num = 0;
        public int crystal_own = 0;

        public void Crystal_Init(int num) {
            int j = 0;
            for (int i = 0; i < num; i++) {
                if (i != 0 && i % 5 == 0) {
                    j++;
                }
                GameObject crystal = null;
                crystal = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Scene/crystal"));
                crystal.transform.SetParent(this.transform);
                crystal.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                crystal.transform.localPosition = new Vector3(Global.Scene1Parameter.CRYSTALPOS_X + Global.Scene1Parameter.CRYSTALPOS_INT_X * j, Global.Scene1Parameter.CRYSTALPOS_Y + Global.Scene1Parameter.CRYSTALPOS_INT_Y * (i - 5 * j), 0);
                _crystal_array.Add(crystal);
            }
            Crystal_Recovery(num);
        }

        public void Crystal_Rewrite(int num)
        {
            for (int i = crystal_own-1; i >= num; i--)
            {
                _crystal_array[i].transform.Find("bg").GetComponent<Image>().overrideSprite = _crystal_pic[0];
            }
            crystal_own = num;
        }

        public void Crystal_Recovery(int num)
        {
            crystal_num = num;
            crystal_own = num;
            for (int i = 0; i < crystal_num; i++)
            {
                _crystal_array[i].transform.Find("bg").GetComponent<Image>().overrideSprite = _crystal_pic[1];
            }

        }
    }
}
