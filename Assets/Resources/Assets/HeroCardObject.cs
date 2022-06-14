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
public class HeroCardObject : ScriptableObject
{
    public List<string> heros_name = new List<string>();

    public List<string> heros_describe = new List<string>();

    //public List<string> heros_crystal_cost = new List<string>();

    //public List<string> heros_material_cost = new List<string>();

    //public List<string> hero_willpower = new List<string>();

    //public List<string> hero_states = new List<string>();

    //public List<string> hero_attr = new List<string>();
}
