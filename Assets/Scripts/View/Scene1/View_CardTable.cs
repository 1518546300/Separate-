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
using Model;

namespace View{
   public class View_CardTable:MonoBehaviour
   {
        public GameObject _card_temp_table;
        public List<GameObject> card_arry = new List<GameObject>();
        //public List<Model_PlayerCard> card_info_array = new List<Model_PlayerCard>();

        public Dictionary<string , List<GameObject>> card_pool = new Dictionary<string, List<GameObject>>();

        public void Card_Order() {
            int init_pos_x;
            if (transform.childCount % 2 == 1)
            {
                init_pos_x = -(int)(transform.childCount / 2) * Scene1Parameter.CARDITL;
            }
            else
            {
                init_pos_x = -(int)(transform.childCount / 2) * Scene1Parameter.CARDITL + Scene1Parameter.CARDITL / 2;
            }

            int length = card_arry.Count;
            for (int i = 0; i < length; i++) {
                card_arry[i].transform.SetParent(GameObject.Find("cards_temp").transform);
            }
            for (int i = 0; i < length; i++)
            {
                card_arry[i].transform.SetParent(this.transform);
                card_arry[i].transform.localPosition = new Vector3(i * Scene1Parameter.CARDITL + init_pos_x, Scene1Parameter.CARDINITPOS_Y, 0);
            }
        }

        public void Card_Order_Temp() {
            int init_pos_x;
            if (transform.childCount % 2 == 1)
            {
                init_pos_x = -(int)(transform.childCount / 2) * Scene1Parameter.CARDITL;
            }
            else
            {
                init_pos_x = -(int)(transform.childCount / 2) * Scene1Parameter.CARDITL + Scene1Parameter.CARDITL / 2;
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).localPosition = new Vector3(i * Scene1Parameter.CARDITL + init_pos_x, Scene1Parameter.CARDINITPOS_Y, 0);
            }
        }

        public void Change_Card_Layer (string layer) {
            foreach (GameObject card in card_arry) {
                card.layer = LayerMask.NameToLayer(layer);
            }
        }

        public void Card_Restore(GameObject card)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject != card) {
                    transform.GetChild(i).GetComponent<View_CardScript>().Card_Restore();
                } 
            }

        }

        public GameObject AddCard()
        {
            GameObject card = null;
            card = GetCardFromPool();
            card_arry.Add(card);
            Card_Order();
            return card;
        }

        public GameObject GetCardFromPool() {
            GameObject card = null;

            if (card_pool.ContainsKey("card") && card_pool["card"].Count > 0)
            {
                card = card_pool["card"][0];
                card_pool["card"].RemoveAt(0);
            }
            else
            {
                card = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Scene/card"));
            }

            card.transform.SetParent(transform);
            card.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            card.SetActive(true);

            return card;
        }

        public void PushCardToPool(GameObject card) {
            card.transform.SetParent(GameObject.Find("cards_temp").transform);
            card.SetActive(false);
            card_arry.Remove(card);

            if (card_pool.ContainsKey("card"))
            {
                card_pool["card"].Add(card);
            }
            else
            {
                card_pool.Add("card", new List<GameObject>() { card });
            }
        }
    }
}

