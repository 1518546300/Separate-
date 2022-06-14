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
using Model;

namespace View{
   public class View_GridAttributeBt : MonoBehaviour
   {
        // Start is called before the first frame update

        public GameObject _image;

        public Model_GridAttribute attr_model = new Model_GridAttribute();

        public void Init_Attribute(Model_GridAttribute attr_info)
        {
            attr_model = attr_info;
            _image.GetComponent<Image>().overrideSprite = attr_info._pic;
        }

        public void Point_Enter()
        {
            //Debug.Log(this.transform.position);
            View_Scene1._Instance.Grid_Attribute_Show(this.transform.position, attr_model);
        }

        public void Point_Exit()
        {
            View_Scene1._Instance.Grid_Attribute_Hidden();
        }
    }
}
