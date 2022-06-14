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
using Global;

namespace Presistent{
   public class TouchControl : MonoBehaviour
   {
        public static TouchControl _Instance;
        public GameObject _buy_hero_bt;
        public GameObject _next_year_bt;
        public GameObject _card_table;
        public GameObject _grid_table;
        public GameObject _shield;

        public string _shield_layer_name = "Ignore Raycast";

        void Awake()
        {
            _Instance = this;
        }

        public void Game_Canvas_Shield(bool flag) {
            _shield.SetActive(flag);
            if (flag == true) {
                GlobalParameter.GAMESTATUS = 1;
            }
            else {
                GlobalParameter.GAMESTATUS = 0;
            }
        }

        public void Grid_Shield(string layer)
        {
            foreach (Transform grid in _grid_table.transform)
            {
                grid.gameObject.layer = LayerMask.NameToLayer(layer);
            }
        }

        public void Card_Shield(string layer)
        {
            foreach (Transform card in _card_table.transform)
            {
                card.gameObject.layer = LayerMask.NameToLayer(layer);
            }
        }

        public void Bts_Shield(string layer)
        {
            //_buy_hero_bt.layer = LayerMask.NameToLayer(layer);
            //_next_year_bt.layer = LayerMask.NameToLayer(layer);
        }

        public void Close_Shield() {
            Grid_Shield("Grid");
            Card_Shield("Card");
            Bts_Shield("UI");
        }

    }
}
