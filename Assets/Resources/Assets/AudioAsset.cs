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
public class AudioAsset : ScriptableObject
{
    public List<AudioClip> _scene1_audio = new List<AudioClip>();

    public List<AudioClip> _common_audio_bt = new List<AudioClip>();
}
