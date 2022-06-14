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


[Serializable]
public class GAMECONFIGFILE
{
    public int _stage_num;
    public int _dialogue_num;
    public int _talking_status;
    public int _material_num;

    //游戏分辨率
    public bool _resolution_flag;
}


public class GameSave:MonoBehaviour
{
    private string JsonPath;
    public GAMECONFIGFILE _game_save_file;

    //void Awake()
    //{
    //    InitJsonData();
    //    SaveJson(GlobalParameter.GAMECONFIGJSONPATH);
    //    ReadJson(GlobalParameter.GAMECONFIGJSONPATH);
    //}


    /// <summary>
    /// flag == 0 正常启动，flag == 1 重新开始游戏
    /// </summary>
    /// <param name="flag"></param>
    /// <param name="JsonPath"></param>
    /// <returns></returns>
    public int Init_JsonData(int flag, string json_path)
    {
        int ret = 1;
        setJsonPath(json_path);

        if (flag == 0)
        {
            if (!File.Exists(JsonPath))
            {
                _game_save_file = new GAMECONFIGFILE();
                
                _game_save_file._stage_num = 0;
                _game_save_file._dialogue_num = 0;
                _game_save_file._talking_status = 0;
                _game_save_file._material_num = 0;

                _game_save_file._resolution_flag = false;

                //_game_save_file.stage_num = 0;
                //_game_save_file._stage_star_num = new List<int>();
                //for (int i = 0; i < GlobalParameter.STAGENUM; i++)
                //{
                //    _game_save_file._stage_star_num.Add(0);
                //}
                //_game_save_file._stage_star_num[0] = 2;
                Save_Json();
                ret = 0;
            }
            else
            {
                Read_Json();
                ret = 1;
            }
        }
        else {
            //_game_save_file = new GAMECONFIGFILE();

            _game_save_file._stage_num = 0;
            _game_save_file._dialogue_num = 0;
            _game_save_file._talking_status = 0;
            _game_save_file._material_num = 0;

            //_game_save_file._resolution_flag = false;

            //_game_save_file._star_num = 0;
            //_game_save_file._stage_star_num = new List<int>();
            //for (int i = 0; i < GlobalParameter.STAGENUM; i++)
            //{
            //    _game_save_file._stage_star_num.Add(0);
            //}
            //_game_save_file._stage_star_num[0] = 2;
            Save_Json();
            ret = 0;
        }
        return ret;
    }

    public void setJsonPath(string json_path) {
        JsonPath = json_path;
    }

    public void Save_Json()
    {
        if (!File.Exists(JsonPath))
        {
            File.Create(JsonPath).Dispose(); ;
        }
        //Debug.Log(_game_save_file._star_num);
        string json = JsonUtility.ToJson(_game_save_file, true);
        File.WriteAllText(JsonPath, json);
        //Debug.Log("保存成功");
    }


    public void Read_Json()
    {
        if (!File.Exists(JsonPath))
        {
            Debug.LogError("读取的文件不存在！");
            return;
        }
        string json = File.ReadAllText(JsonPath);
        _game_save_file = JsonUtility.FromJson<GAMECONFIGFILE>(json);

        //int star_num = _game_save_file._star_num;
        //Debug.LogError(star_num);
        //for (int j = 0; j < _game_save_file._stage_star_num.Count; j++)
        //{
        //    Debug.Log(_game_save_file._stage_star_num[j]);
        //}
    }

    public void Rewrite_Star_Num() {
        //int num = 0;
        //for (int i = 0; i < GlobalParameter.STAGENUM; i++)
        //{
        //    num = num + _game_save_file._stage_star_num[i];
        //}
        //_game_save_file._star_num = num;
        _game_save_file._stage_num = 0;
        _game_save_file._dialogue_num = 0;
    }

    public bool changeResolution(bool flag) {
        _game_save_file._resolution_flag = flag;
        Save_Json();
        return _game_save_file._resolution_flag;
    }
}
