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

[SerializeField]
public class EventStatusA : ScriptableObject
{
    public List<string> status_uid = new List<string>();

    public List<string> status_name = new List<string>();

    public List<string> status_describe = new List<string>();

    public List<string> time = new List<string>();
}
