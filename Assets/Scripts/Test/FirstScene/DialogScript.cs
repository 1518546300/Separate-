using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Root
{
    public List<string[]> PlayerDialog { get; set; }
    public List<string[]> DRCarshDialog { get; set; }
}

public class DialogScript : MonoBehaviour
{
    //public static DialogScript instance;

    //private int DrCarshDialogAccount = 0; //博士会话计数
    //private int DrCarshDialogAccountMax; //最大会话数目

    //private int PlayerDialogAccount = 0; //当前玩家会话栏

    //private int DrCashInitLineNum; //最大会话数目

    ////private Root r;

    //public Text playerConversationBar;

    //public Text drCarshConversationBar;

    //void Awake()
    //{
    //    instance = this;
    //    StreamReader streamreader = new StreamReader(Application.dataPath + "/Scripts/FirstScene/dialog.json");//读取数据，转换成数据流
    //    //JsonReader js = new JsonReader(streamreader);//再转换成json数据
    //    //r = JsonMapper.ToObject<Root>(js);//读取

    //    DrCashInitLineNum = r.DRCarshDialog[0].Length;
    //    DrCarshDialogAccountMax = r.DRCarshDialog[1].Length;
    //    PlayerDialogAccount = r.PlayerDialog[0].Length;

    //    this.InvokeRepeating("SetInterval", 0.1f, 0.1f);//1.5
    //}

    //private void SetInterval()
    //{
    //    if (DrCarshDialogAccount < DrCashInitLineNum)
    //    {
    //        DrCarshDialogChange(r.DRCarshDialog[0][DrCarshDialogAccount]);
    //        DrCarshDialogAccount++;
    //    }
    //    else {
    //        DrCarshDialogAccount = 0;
    //        PlayerDialogAccount = 0;
    //        PlayerDialogChange(r.PlayerDialog[0][PlayerDialogAccount]);
    //        PlayerDialogAccount++;
    //        CancelInvoke();
    //    }
    //}

    //public void PlayerDialogChange(string str)
    //{
    //    playerConversationBar.text = str;
    //}

    //public void DrCarshDialogChange(string str) {
    //    drCarshConversationBar.text = str;
    //}

    //public void OnClickedBtFunc(GameObject bt)
    //{
    //    if (DrCarshDialogAccount < DrCarshDialogAccountMax) {
    //        Debug.Log(bt.name);
    //        switch (bt.name)
    //        {
    //            case "NextBt":
    //                break;
    //            case "exitBt":
    //                break;
    //            default:
    //                break;
    //        }
    //        DrCarshDialogChange(r.DRCarshDialog[1][DrCarshDialogAccount]);
    //        DrCarshDialogAccount++;
    //        PlayerDialogChange(r.PlayerDialog[0][PlayerDialogAccount]);
    //        PlayerDialogAccount++;
    //    }
    //}
}
