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
   public class BaseManager<T> where T:new()
   {
        private static T instance;

        public static T GetInstance() {
            if (instance == null) {
                instance = new T();
            }

            return instance;
        }
   }
}

