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
public class EventMysteriousA : ScriptableObject
{
    public List<string> mysterious_uid = new List<string>();

    public List<string> card_uid = new List<string>();

    public List<string> status_uid = new List<string>();

    public List<string> type = new List<string>();
}
