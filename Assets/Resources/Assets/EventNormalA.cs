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
public class EventNormalA : ScriptableObject
{
    public List<string> normal_uid = new List<string>();

    public List<string> card_uid = new List<string>();

    public List<string> main_material_effect = new List<string>();

    public List<string> material_effect = new List<string>();

    public List<string> willpower_effect = new List<string>();

    public List<string> recombination_effect = new List<string>();
}
