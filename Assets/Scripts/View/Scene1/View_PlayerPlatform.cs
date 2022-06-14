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
using UnityEngine.UI;

using Global;
using Model;

namespace View{
   public class View_PlayerPlatform : MonoBehaviour
   {
        public Model_PlayerInfo _player;
        public Model_OpponentInfo _opponent;

        public string _player_name;
        public string _opponent_name;
        public string _player_dialogue;
        public string _opponent_dialogue;

        public GameObject _player_dialogue_obj;
        public GameObject _opponent_dialogue_obj;

        public Image _player_frame;
        public Image _opponent_frame;

        public Image _player_avatar;
        public Image _opponent_avatar;

        public Text _player_name_text;
        public Text _opponent_name_text;

        public Text _player_dialogue_text;
        public Text _opponent_dialogue_text;

        public Text _opponent_material_text;
        public Text _player_material_text;

        public int _player_dialogue_index;
        public int _opponent_dialogue_index;

        public void Init(SpriteAsset res,Model_PlayerInfo player,Model_OpponentInfo opponent) {
            _player = player;
            _opponent = opponent;

            _player_name = player._player_name;
            _opponent_name = opponent._opponent_name;
            _player_dialogue = player._player_dialogue;
            _opponent_dialogue = opponent._opponent_dialogue;

            _player_name_text.text = _player_name;
            _opponent_name_text.text = _opponent_name;

            _player_frame.color = Global.GlobalParameter.GRIDCOLOR[1];
            _opponent_frame.color = Global.GlobalParameter.GRIDCOLOR[0];

            _player_avatar.overrideSprite = res.player_avatar[player._player_avatar_num];
            _opponent_avatar.overrideSprite = res.player_avatar[opponent._opponent_avatar_num];

            //Player_Dialogue_Bt();
            //Opponent_Dialogue_Bt();
        }

        public void Player_Dialogue_Bt() {
            _player_dialogue_obj.SetActive(!_player_dialogue_obj.activeSelf);
            if (_player_dialogue_obj.activeSelf)
            {
                _player_dialogue_index = 0;
                string[] str = _player_dialogue.Split('/');
                _player_dialogue_text.text = '['+ (_player_dialogue_index+1).ToString()+ ']'+str[_player_dialogue_index];
            }
        }

        public void Opponent_Dialogue_Bt()
        {
            _opponent_dialogue_obj.SetActive(!_opponent_dialogue_obj.activeSelf);
            if (_opponent_dialogue_obj.activeSelf)
            {
                _opponent_dialogue_index = 0;
                string[] str = _opponent_dialogue.Split('/');
                _opponent_dialogue_text.text = '[' + (_opponent_dialogue_index+1).ToString() + ']'+str[_opponent_dialogue_index];
            }
        }

        public void Player_Dialogue_Next_Bt()
        {
            string[] str = _player_dialogue.Split('/');
            if (_player_dialogue_index+1 >= str.Length)
            {
                _player_dialogue_index = 0;
            }
            else
            {
                _player_dialogue_index++;
            }
            _player_dialogue_text.text = '[' + (_player_dialogue_index + 1).ToString() + ']' + str[_player_dialogue_index];
        }

        public void Opponent_Dialogue_Next_Bt()
        {
            string[] str = _opponent_dialogue.Split('/');
            if (_opponent_dialogue_index + 1 >= str.Length)
            {
                _opponent_dialogue_index = 0;
            }
            else
            {
                _opponent_dialogue_index++;
            }
            _opponent_dialogue_text.text = '[' + (_opponent_dialogue_index + 1).ToString() + ']' + str[_opponent_dialogue_index];
        }

        public void Opponent_Material_Display() {
            _opponent_material_text.text = _opponent._opponent_material.ToString();
        }

        public void Player_Material_Display() {
            _player_material_text.text = _player.material_num.ToString();
        }
    }
}
