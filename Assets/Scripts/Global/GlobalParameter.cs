/***
*
*   Title:  "Separate.1" 项目开发
*	
*		公共层：全局
*
*   Description:
*      [描述]
*   Date:2020
*
*   Version:0.1
*
*   Modify Recorder:
*
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global{
   public class GlobalParameter
   {
        public static int CHOICEGRIDINDEX = -1;

        public static int GAMESTATUS = 0;//0 玩家回合 ，1其他回合

        public static int SCENENUM = 0;
        public static int PAGENUM = 0;

        public static string GAMECONFIGJSONPATH = Application.persistentDataPath + "/SaveConfig.json";
        public static string DIALOGUEFILEPATH = "Json/dialogue";

        public static int STAGENUM = 30;

        /// <summary>常用颜色
        /// 0红色，1绿色，2为荧光,3为绿色，4为黑色，5为黄色
        /// </summary>
        public static Color[] COLORLIB = {
            new Color32(255, 14, 14, 255),
            new Color32(85, 255, 73, 255),
            new Color32(103, 255, 214, 255),
            new Color32(152, 47, 255, 255),
            new Color32(205, 0, 27, 255),
            new Color32(241, 217, 0, 255)
        };

        /// <summary>常用颜色
        /// 0红色，1绿色，2为荧光,3为绿色，4为黑色，5为黄色
        /// </summary>
        public static Color[] BTCOLOR = {
            new Color32(20, 161, 14, 255),
            new Color32(255, 0, 8, 255)
        };

        /// <summary>文本颜色
        /// 0红色，1绿色,2为蓝色，3为紫色
        /// </summary>
        public static Color[] TEXTCOLOR = {
            new Color32(231, 64, 67, 255),
            new Color32(37, 179, 21, 255),
            new Color32(0, 218, 255, 255),
            new Color32(206, 0, 238, 255)
        };

        /// <summary>格子的荧光边
        /// 0为红色，1为绿色
        /// </summary>
        public static Color[] GRIDCOLOR = {
            new Color32(130, 255, 110, 255),
            new Color32(255, 73, 55, 255)
        };

        /// <summary>英雄状态
        /// 0为紫色，1为黄色，2为荧光绿
        /// </summary>
        public static Color[] GRIDSTATUSCOLORLIB = {
            new Color32(0, 218, 255, 255),
            new Color32(255, 205, 0, 255),
            new Color32(147, 255, 0, 255),
        };

        public static Color[] STARCOLOR = {
            new Color32(255, 255, 255, 255),
            new Color32(255, 239, 69, 255),
        };

        /// <summary>
        /// 0为事件卡，1为实体卡，2为升级卡
        /// </summary>
        public static Color[] NOTIFYMSGCOLOR = {
            new Color32(34, 137, 217, 255),
            new Color32(14, 188, 14, 255),
            new Color32(176, 34, 162, 255)
        };

        public static string[] ROUNDNAME = {
            "玩家回合",
            "弃牌",
            "敌方回合",
            "结算"
        };
    }

    public enum ScenesEnum
    {
        LogonScene,
        FirstScene,
        Scene1,
        StagesView,
    }

    public class SceneInfo {
        public static Dictionary<ScenesEnum, string> ScenesName = new Dictionary<ScenesEnum, string>() {
            {ScenesEnum.FirstScene,"FirstScene"},
            {ScenesEnum.Scene1, "Scene1"},
            {ScenesEnum.StagesView, "Stage_Scene"},
        };

    }

    public class StageParameter {
        /// <summary>
        /// 0 卡西博士，1 探险家
        /// </summary>
        public static Color[] ACTORTXTCOLOR = {
            new Color32(251, 65, 65, 255),
            new Color32(72, 250, 65, 255),
            new Color32(255, 205, 0, 255)
        };

        public static int MATERIALSTAGENUM = 5;
    }

    public class Scene1Parameter {
        public static int HEROCOSTNUM = 0;

        public static int GRIDNUM = 3;
        public static int GRIDITL = 95;
        public static float x = -95;
        public static float y = 95;

        //测试数据（卡牌）
        public static int CARDNUM = 1;
        public static int CARDITL = 70;
        public static int CARDINITPOS_X = -200;
        public static int CARDINITPOS_Y = -10;

        //水晶配置
        public static int CRYSTALPOS_X = 0;
        public static int CRYSTALPOS_Y = 0;
        public static int CRYSTALPOS_INT_Y = 33;
        public static int CRYSTALPOS_INT_X = 30;

        //收集卡牌框配置
        public static int COLLECT_INT_X = -140;
        public static int COLLECT_INT_Y = 60;
        public static int COLLECT_INTV = 70;

        //grid 状态信息
        public static int STATUSPOS_X = -110;
        public static int STATUSPOS_INT_X = 45;

        //grid 时间牌
        public static int EVENTPOS_X = -110;
        public static int EVENTPOS_INT_X = 50;

        public static float ACTION_INT = 0.8f;
        public static float GRIDTIPS_INT = 0.4f;

        public static int STATUSTIPS_INT = 60;

        public static int PARTICLE_HIGHET = 30;

        public static bool HURRYUPFLAG = false;

        public static float ROUNDINTTIME = 1f;

        public static float LOADINGCURTAINSHOWTIME = 1f;

        public static float[] WILLPOWERLEN = {
            0,
            317
        };

        public static float[] GRIDWILLPOWERLEN = {
            2,
            89
        };
    }
}

