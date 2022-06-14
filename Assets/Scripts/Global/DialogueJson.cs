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
using System;
using System.IO;

[Serializable]
public class DialogueJson
{
    public List<List<string>> main;
    public List<List<string>> debeated_dialogue;
    public List<List<string>> pics;
}

//[Serializable]
//public class main {
//    public List<List<string>> _main;
//}

//[Serializable]
//public class debeated_dialogue
//{
//    public List<List<string>> _debeated_dialogue;
//}

//[Serializable]
//public class pics {
//    public List<List<string>> _pics;
//}

//namespace Global{
//   public class DialogueJson
//   {
//        public stage_main_dialogue _stage_main_dialogue; 
//   }
//}
