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
public class EventCardObject : ScriptableObject
{
    public List<string> card_uid = new List<string>();
    public List<string> card_use_object = new List<string>();
    public List<string> card_class = new List<string>();
    public List<string> card_name = new List<string>();
    public List<string> card_describe = new List<string>();   
    public List<string> card_crystal_cost = new List<string>();
    public List<string> card_material_cost = new List<string>();
}
