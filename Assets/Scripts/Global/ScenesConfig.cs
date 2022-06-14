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

namespace Global{
    public class ScenesConfig
    {
        //下标0为水晶个数，下标1为物质数
        public static int[,] _scenes_config_data = {
            {
                5,
                100
            },
            {
                5,
                120
            },
        };

        public static int[,] _scenes_grid_fighting_line_num = {
            {
                10,20,30,
                40,50,60,
                70,80,90
            },
            {
                10,20,30,
                40,50,60,
                70,80,90
            },
        };

        public static int[,] _scenes_grid_array = {
            {
                0,0,0,
                0,0,0,
                0,0,0
            },
            {
                0,0,0,
                0,2,0,
                0,0,0
            },
        };

        public static int[,,] _scenes_grid_status = {
            
        };
    }
}
