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

using View;
using Model;

public class View_GridStatusBt : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject _image;

    public Model_GridStatus _status_model = new Model_GridStatus();

    public void Init_Status(Model_GridStatus status) {
        _status_model = status;
        _image.GetComponent<Image>().overrideSprite = status._pic;
    }

    public void MouseEnter()
    {
        Debug.Log(this.transform.position);
        View_Scene1._Instance.Grid_Status_Show(this.transform.position, _status_model);
    }

    public void MouseExit()
    {
        View_Scene1._Instance.Grid_Status_Hidden();
    }
}
