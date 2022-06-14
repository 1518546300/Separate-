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


namespace View
{
   public class View_PageInfoBoard : MonoBehaviour
   {
        public Text _page_board_name;
        public Text _page_board_artist;
        public Text _page_board_year;

        public Text _page_info_describe;

        public void Init_Page_Board_Info(string page_name,string page_artist_name,string page_year, string page_describe) {
            _page_board_name.text = page_name;
            _page_board_artist.text = page_artist_name;
            _page_board_year.text = page_year.ToString();

            _page_info_describe.text = page_describe;
        }
   }
}
