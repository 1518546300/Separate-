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
public class SceneConfigObject : ScriptableObject
{
    public List<string> grid_array = new List<string>();

    public List<string> uid = new List<string>();

    public List<string> player_config_crystal = new List<string>();

    public List<string> player_config_material = new List<string>();

    public List<string> card_cost = new List<string>();

    public List<string> gamestart_card_libary = new List<string>();

    public List<string> random_card_libary = new List<string>();

    public List<string> hero_card_config = new List<string>();

    public List<string> opponent_card_number = new List<string>();

    public List<string> opponent_material = new List<string>();

    public List<string> opponent_card_libary = new List<string>();

    public List<string> player_name = new List<string>();

    public List<string> player_dialogue = new List<string>();

    public List<string> opponent_name = new List<string>();

    public List<string> opponent_dialogue = new List<string>();

    public List<string> avatar = new List<string>();

    public List<string> saying = new List<string>();

    public List<string> saying_cite = new List<string>();

    public List<string> game_status = new List<string>();

    public List<string> star_req = new List<string>();

    public List<string> stage_name = new List<string>();

    public List<string> stage_pos = new List<string>();

    public List<string> target_grid = new List<string>();

    public List<string> game_class = new List<string>();

    public List<string> page_uid = new List<string>();

    public List<string> page_stage_num = new List<string>();

    public List<string> page_name = new List<string>();

    public List<string> page_describe = new List<string>();
}

