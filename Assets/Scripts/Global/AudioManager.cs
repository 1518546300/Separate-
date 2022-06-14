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

namespace Control{
   public class AudioManager:MonoBehaviour
   {
        public static AudioManager _Instance;
        public AudioSource _audioS;
        public AudioSource _audio_effect;
        public AudioSource _audio_bt;

        private bool _bg_music_status = true;

        void Awake() {
            if (_Instance != null){
                DestroyImmediate(gameObject);
            }else {
                DontDestroyOnLoad(gameObject);
                _Instance = this;
            }       
        }

        public void Audio_Volume_Change(float volume_num) {
            _audioS.volume = volume_num;
            _audio_effect.volume = volume_num;
            _audio_bt.volume = volume_num;
        }

        public void Bt_Music_Control(int num) {
            switch (num)
            {
                case 0:
                    Bt_Audio_Play_Intime(Ctrl_Scene1._Instance._res._audio._common_audio_bt[num]);
                    break;
                case 1:
                    Bt_Audio_Play_Intime(Ctrl_Scene1._Instance._res._audio._common_audio_bt[num]);
                    break;
                default:
                    break;
            }
        }

        public void Scene_Music_Control(int num)
        {
            Scene_Audio_Play_Intime(Ctrl_Scene1._Instance._res._audio._scene1_audio[num]);
        }

        public void AudioPlay(AudioClip clip)
        {
            _audioS.clip = clip;
            _audioS.loop = true;
            _audioS.Play();
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

        public void Bt_Audio_Play_Intime(AudioClip clip)
        {
            if (clip != null)
            {
                _audio_bt.clip = clip;
                _audio_bt.loop = false;
                _audio_bt.Play();
            }
        }

        public void Scene_Audio_Play_Intime(AudioClip clip) {
            if (clip != null)
            {
                _audio_bt.clip = clip;
                _audio_bt.loop = false;
                _audio_bt.Play();
            }
        }

        public bool Change_Bg_Music_Status()
        {
            if (_bg_music_status)
            {
                _bg_music_status = false;
                Audio_Volume_Change(0f);
                //_audioS.Pause();
            }
            else
            {
                _bg_music_status = true;
                Audio_Volume_Change(0.5f);
                //_audioS.Play();
            }
            return _bg_music_status;
        }
    }
}

