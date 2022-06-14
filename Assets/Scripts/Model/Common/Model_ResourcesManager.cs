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
using System;
using System.IO;
using Global;
using Newtonsoft.Json;


namespace Model{
   public class Model_ResourcesManager
   {
        public SceneConfigObject _scene_config;

        public EventCardObject _event_card;
        public EventLongTermA _event_longterm;
        public EventMysteriousA _event_mysterious;
        public EventNormalA _event_normal;
        public EventStatusA _event_status;

        public HeroCardObject _hero_card;

        public SpriteAsset _sprites;
        public AudioAsset _audio;

        public GuidenceAsset _guidence;
        public DialogueJson _dialogue;

        public Model_ResourcesManager() {
            _scene_config = Resources.Load("Assets/SceneConfigA", typeof(SceneConfigObject)) as SceneConfigObject;
            _event_card = Resources.Load("Assets/EventCardA", typeof(EventCardObject)) as EventCardObject;
            _event_longterm = Resources.Load("Assets/EventLongTermA", typeof(EventLongTermA)) as EventLongTermA;
            _event_mysterious = Resources.Load("Assets/EventMysteriousA", typeof(EventMysteriousA)) as EventMysteriousA;
            _event_normal = Resources.Load("Assets/EventNormalA", typeof(EventNormalA)) as EventNormalA;
            _event_status = Resources.Load("Assets/EventStatusA", typeof(EventStatusA)) as EventStatusA;

            _hero_card = Resources.Load("Assets/HeroCardA", typeof(HeroCardObject)) as HeroCardObject;

            _sprites = Resources.Load("Assets/SpritesAsset", typeof(SpriteAsset)) as SpriteAsset;
            _audio = Resources.Load("Assets/AudioAsset", typeof(AudioAsset)) as AudioAsset;

            _guidence = Resources.Load("Assets/GuidenceAsset", typeof(GuidenceAsset)) as GuidenceAsset;
            Read_Json(GlobalParameter.DIALOGUEFILEPATH);

        }

        public void Read_Json(string JsonPath)
        {
            //if (!File.Exists(JsonPath))
            //{
            //    Debug.LogError(JsonPath);
            //    Debug.LogError("读取的文件不存在！");
            //    return ;
            //}
            TextAsset text = Resources.Load<TextAsset>(GlobalParameter.DIALOGUEFILEPATH);
            string json = text.text;
            _dialogue = JsonConvert.DeserializeObject<DialogueJson>(json);
        }
    }
}
