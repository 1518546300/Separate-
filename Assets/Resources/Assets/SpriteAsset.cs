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
public class SpriteAsset : ScriptableObject
{
    public List<Sprite> player_avatar = new List<Sprite>();
    public List<Sprite> hero_sprites = new List<Sprite>();
    public List<Sprite> event_sprites = new List<Sprite>();
    public List<Sprite> event_status_sprites = new List<Sprite>();
    public List<Sprite> hero_state_triangle = new List<Sprite>();
    public List<Sprite> page_image = new List<Sprite>();
    public List<Sprite> dialogue_image = new List<Sprite>();
}
