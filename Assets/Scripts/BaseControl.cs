/***
*
*   Title:  "Separate.1" 项目开发
*	
*		控制层：父类控制层
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
using UnityEngine.SceneManagement;

using Global;

namespace Control
{
   public class BaseControl:MonoBehaviour
   {
        protected void EnterNextScene(ScenesEnum sceneName) {
            SceneManager.LoadScene(Global.SceneInfo.ScenesName[sceneName]);
        }
   }
}

