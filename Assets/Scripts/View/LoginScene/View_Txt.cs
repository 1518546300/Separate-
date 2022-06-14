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

namespace View{
   public class View_Txt : MonoBehaviour
   {
        public float vel_x, vel_y, vel_z;//速度
        /// <summary>
        /// 最大、最小飞行界限
         /// </summary>
        public int maxPos = 190;
        public float maxSpeed = 1f;

        void Start()
        {
            int pos_x = Random.Range(-maxPos, maxPos);
            int pos_y = Random.Range(-maxPos, maxPos);
            transform.localPosition = new Vector3(pos_x, pos_y, 0);
            vel_x = Random.Range(-maxSpeed, maxSpeed);
            vel_y = Random.Range(-maxSpeed, maxSpeed);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(new Vector3(vel_x, vel_y, 0) * Time.deltaTime * 3f);
            Check();
        }

        public void Init(string name)
        {
            transform.GetComponent<Text>().text = name;
        }

        void ChangeDirection(float x, float y)
        {
            vel_x = x;
            vel_y = y;
        }

        void Check()
        {
            //如果到达预设的界限位置值，调换速度方向并让它当前的坐标位置等于这个临界边的位置值
            if (transform.localPosition.x > maxPos)
            {
                vel_x = -vel_x;
                transform.localPosition = new Vector3(maxPos, transform.localPosition.y, 0);
            }
            if (transform.localPosition.x < -maxPos)
            {
                vel_x = -vel_x;
                transform.localPosition = new Vector3(-maxPos, transform.localPosition.y, 0);
            }
            if (transform.localPosition.y > maxPos)
            {
                vel_y = -vel_y;
                transform.localPosition = new Vector3(transform.localPosition.x, maxPos, 0);
            }
            if (transform.localPosition.y < -maxPos)
            {
                vel_y = -vel_y;
                transform.localPosition = new Vector3(transform.localPosition.x, -maxPos, 0);
            }
        }
    }
}

