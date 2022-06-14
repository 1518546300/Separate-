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

namespace Control
{
    public class AudioEffectMgr : MonoBehaviour
    {
        public static AudioEffectMgr _Instance;
        private AudioSource _audio_effect;

        void Awake()
        {
            if (_Instance != null)
            {
                DestroyImmediate(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
                _Instance = this;
                _audio_effect = GetComponent<AudioSource>();

            }
        }

        public void Audio_Play_Intime(AudioClip clip)
        {
            if (clip != null)
            {
                _audio_effect.clip = clip;
                _audio_effect.loop = false;
                _audio_effect.Play();
            }
        }

        public void Bt_Click_Audio(AudioClip clip) {
            if (clip != null)
            {
                _audio_effect.clip = clip;
                _audio_effect.loop = false;
                _audio_effect.Play();
            }
        }
    }
}
