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
using UnityEditor;
using System.IO;
using System.Data;
using Excel;

public class ConvertTool : EditorWindow
{
    [MenuItem("ConvertTools/HeroCardTool", false, 1)]
    static void HeroCardTool()
    {
        HeroCardObject heros = ScriptableObject.CreateInstance<HeroCardObject>();
        string Path = "Assets/Resources/Assets/HeroCardA.asset";

        FileStream stream = File.Open("Assets/Resources/Data/data.xlsx", FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();

        string table_name = "hero_info";
        int columns = result.Tables[table_name].Columns.Count;
        int rows = result.Tables[table_name].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            heros.heros_name.Add(result.Tables[table_name].Rows[i][1].ToString());
            heros.heros_describe.Add(result.Tables[table_name].Rows[i][2].ToString());
        }

        AssetDatabase.CreateAsset(heros, Path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("ConvertTools/EventCardTool", false, 1)]
    static void EventCardTool()
    {
        EventCardObject cards = ScriptableObject.CreateInstance<EventCardObject>();
        string Path = "Assets/Resources/Assets/EventCardA.asset";

        FileStream stream = File.Open("Assets/Resources/Data/data.xlsx", FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();

        string table_name = "event_card";
        int columns = result.Tables[table_name].Columns.Count;
        int rows = result.Tables[table_name].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            cards.card_uid.Add(result.Tables[table_name].Rows[i][0].ToString());
            cards.card_use_object.Add(result.Tables[table_name].Rows[i][1].ToString());
            cards.card_class.Add(result.Tables[table_name].Rows[i][2].ToString());
            cards.card_name.Add(result.Tables[table_name].Rows[i][3].ToString());
            cards.card_describe.Add(result.Tables[table_name].Rows[i][4].ToString());
            cards.card_crystal_cost.Add(result.Tables[table_name].Rows[i][5].ToString());
            cards.card_material_cost.Add(result.Tables[table_name].Rows[i][6].ToString());
        }

        AssetDatabase.CreateAsset(cards, Path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EventLongTermA long_term_card = ScriptableObject.CreateInstance<EventLongTermA>();
        Path = "Assets/Resources/Assets/EventLongTermA.asset";

        stream = File.Open("Assets/Resources/Data/data.xlsx", FileMode.Open, FileAccess.Read);
        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        result = excelReader.AsDataSet();

        table_name = "event_card_long_term";
        columns = result.Tables[table_name].Columns.Count;
        rows = result.Tables[table_name].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            long_term_card.longterm_uid.Add(result.Tables[table_name].Rows[i][0].ToString());
            long_term_card.card_uid.Add(result.Tables[table_name].Rows[i][1].ToString());
            long_term_card.status_uid.Add(result.Tables[table_name].Rows[i][2].ToString());
            long_term_card.main_material_effect.Add(result.Tables[table_name].Rows[i][3].ToString());
            long_term_card.material_effect.Add(result.Tables[table_name].Rows[i][4].ToString());
            long_term_card.willpower_effect.Add(result.Tables[table_name].Rows[i][5].ToString());
            long_term_card.recombination_effect.Add(result.Tables[table_name].Rows[i][6].ToString());
        }

        AssetDatabase.CreateAsset(long_term_card, Path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EventNormalA normal_card = ScriptableObject.CreateInstance<EventNormalA>();
        Path = "Assets/Resources/Assets/EventNormalA.asset";

        stream = File.Open("Assets/Resources/Data/data.xlsx", FileMode.Open, FileAccess.Read);
        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        result = excelReader.AsDataSet();

        table_name = "event_normal_card";
        columns = result.Tables[table_name].Columns.Count;
        rows = result.Tables[table_name].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            normal_card.normal_uid.Add(result.Tables[table_name].Rows[i][0].ToString());
            normal_card.card_uid.Add(result.Tables[table_name].Rows[i][1].ToString());
            normal_card.main_material_effect.Add(result.Tables[table_name].Rows[i][2].ToString());
            normal_card.material_effect.Add(result.Tables[table_name].Rows[i][3].ToString());
            normal_card.willpower_effect.Add(result.Tables[table_name].Rows[i][4].ToString());
            normal_card.recombination_effect.Add(result.Tables[table_name].Rows[i][5].ToString());
        }

        AssetDatabase.CreateAsset(normal_card, Path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EventMysteriousA mysterious_card = ScriptableObject.CreateInstance<EventMysteriousA>();
        Path = "Assets/Resources/Assets/EventMysteriousA.asset";

        stream = File.Open("Assets/Resources/Data/data.xlsx", FileMode.Open, FileAccess.Read);
        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        result = excelReader.AsDataSet();

        table_name = "mysterious_card";
        columns = result.Tables[table_name].Columns.Count;
        rows = result.Tables[table_name].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            mysterious_card.mysterious_uid.Add(result.Tables[table_name].Rows[i][0].ToString());
            mysterious_card.card_uid.Add(result.Tables[table_name].Rows[i][1].ToString());
            mysterious_card.status_uid.Add(result.Tables[table_name].Rows[i][2].ToString());
            mysterious_card.type.Add(result.Tables[table_name].Rows[i][3].ToString());
        }

        AssetDatabase.CreateAsset(mysterious_card, Path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EventStatusA status = ScriptableObject.CreateInstance<EventStatusA>();
        Path = "Assets/Resources/Assets/EventStatusA.asset";

        stream = File.Open("Assets/Resources/Data/data.xlsx", FileMode.Open, FileAccess.Read);
        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        result = excelReader.AsDataSet();

        table_name = "event_card_status";
        columns = result.Tables[table_name].Columns.Count;
        rows = result.Tables[table_name].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            status.status_uid.Add(result.Tables[table_name].Rows[i][0].ToString());
            status.status_name.Add(result.Tables[table_name].Rows[i][1].ToString());
            status.status_describe.Add(result.Tables[table_name].Rows[i][2].ToString());
            status.time.Add(result.Tables[table_name].Rows[i][3].ToString());
        }

        AssetDatabase.CreateAsset(status, Path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("ConvertTools/GuidenceTool", false, 0)]
    static void GuidenceTool()
    {
        GuidenceAsset config = ScriptableObject.CreateInstance<GuidenceAsset>();
        string Path = "Assets/Resources/Assets/GuidenceAsset.asset";

        FileStream stream = File.Open("Assets/Resources/Data/dialogue.xlsx", FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();
        string tabel_name = "guidence";
        int rows = result.Tables[tabel_name].Rows.Count;

        for (int i = 1; i < rows-1; i++)
        {
            if (result.Tables[tabel_name].Rows[i][0].ToString() == "//") break;
            config.word.Add(result.Tables[tabel_name].Rows[i][0].ToString());
            config.config.Add(result.Tables[tabel_name].Rows[i][1].ToString());
        }

        AssetDatabase.CreateAsset(config, Path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        DialogueAsset dialogue = ScriptableObject.CreateInstance<DialogueAsset>();
        Path = "Assets/Resources/Assets/DialogueAsset.asset";
        tabel_name = "stage_dialogues";
        rows = result.Tables[tabel_name].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            dialogue._dialogue.Add(result.Tables[tabel_name].Rows[i][0].ToString());
        }

        tabel_name = "pics";

        rows = result.Tables[tabel_name].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            dialogue._dialogue_pic.Add(result.Tables[tabel_name].Rows[i][1].ToString());
        }

        AssetDatabase.CreateAsset(dialogue, Path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }

    [MenuItem("ConvertTools/SceneConfig", false, 0)]
    static void SceneConfigTool()
    {
        SceneConfigObject config = ScriptableObject.CreateInstance<SceneConfigObject>();
        string Path = "Assets/Resources/Assets/SceneConfigA.asset";

        FileStream stream = File.Open("Assets/Resources/Data/config.xlsx", FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);


        DataSet result = excelReader.AsDataSet();

        string tabel_name = "_grid_array";
        int columns = result.Tables[tabel_name].Columns.Count;
        int rows = result.Tables[tabel_name].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            string temp = "";
            for (int j = 0; j < columns; j++)
            {
                temp += result.Tables[tabel_name].Rows[i][j].ToString() + "/";
            }
            config.grid_array.Add(temp);
        }

        tabel_name = "_player_config";
        columns = result.Tables[tabel_name].Columns.Count;
        rows = result.Tables[tabel_name].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            config.uid.Add(result.Tables[tabel_name].Rows[i][0].ToString());
            config.player_config_crystal.Add(result.Tables[tabel_name].Rows[i][1].ToString());
            config.player_config_material.Add(result.Tables[tabel_name].Rows[i][2].ToString());
            config.card_cost.Add(result.Tables[tabel_name].Rows[i][3].ToString());
            config.gamestart_card_libary.Add(result.Tables[tabel_name].Rows[i][4].ToString());
            config.random_card_libary.Add(result.Tables[tabel_name].Rows[i][5].ToString());
            config.hero_card_config.Add(result.Tables[tabel_name].Rows[i][6].ToString());
        }

        tabel_name = "_opponent_config";
        columns = result.Tables[tabel_name].Columns.Count;
        rows = result.Tables[tabel_name].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            config.opponent_card_number.Add(result.Tables[tabel_name].Rows[i][0].ToString());
            config.opponent_material.Add(result.Tables[tabel_name].Rows[i][1].ToString());
            config.opponent_card_libary.Add(result.Tables[tabel_name].Rows[i][2].ToString());
        }

        tabel_name = "_dialogue";
        columns = result.Tables[tabel_name].Columns.Count;
        rows = result.Tables[tabel_name].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            config.player_name.Add(result.Tables[tabel_name].Rows[i][0].ToString());
            config.player_dialogue.Add(result.Tables[tabel_name].Rows[i][1].ToString());
            config.opponent_name.Add(result.Tables[tabel_name].Rows[i][2].ToString());
            config.opponent_dialogue.Add(result.Tables[tabel_name].Rows[i][3].ToString());
            config.avatar.Add(result.Tables[tabel_name].Rows[i][4].ToString());
            config.saying.Add(result.Tables[tabel_name].Rows[i][5].ToString());
            config.saying_cite.Add(result.Tables[tabel_name].Rows[i][6].ToString());
        }

        tabel_name = "_stage_config";
        columns = result.Tables[tabel_name].Columns.Count;
        rows = result.Tables[tabel_name].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            config.star_req.Add(result.Tables[tabel_name].Rows[i][1].ToString());
            config.stage_name.Add(result.Tables[tabel_name].Rows[i][2].ToString());
            config.stage_pos.Add(result.Tables[tabel_name].Rows[i][3].ToString());
            config.target_grid.Add(result.Tables[tabel_name].Rows[i][4].ToString());
            config.game_class.Add(result.Tables[tabel_name].Rows[i][5].ToString());
        }

        tabel_name = "_page_config";
        columns = result.Tables[tabel_name].Columns.Count;
        rows = result.Tables[tabel_name].Rows.Count;

        for (int i = 1; i < rows; i++)
        {
            config.page_uid.Add(result.Tables[tabel_name].Rows[i][0].ToString());
            config.page_stage_num.Add(result.Tables[tabel_name].Rows[i][1].ToString());
            config.page_name.Add(result.Tables[tabel_name].Rows[i][2].ToString());
            config.page_describe.Add(result.Tables[tabel_name].Rows[i][3].ToString());
        }

        AssetDatabase.CreateAsset(config, Path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("UpdateResources/update_sprites", false, 0)]
    static void EventCard()
    {
        SpriteAsset sprites = ScriptableObject.CreateInstance<SpriteAsset>();
        string Path = "Assets/Resources/Assets/SpritesAsset.asset";

        Sprite temp = null;
        int i = 0;
        while ((temp = Resources.Load("Texture/Event/event_" + i, typeof(Sprite)) as Sprite) != null) {
            sprites.event_sprites.Add(temp);
            i++;
        }

        i = 0;
        while ((temp = Resources.Load("Texture/Status/status_" + i, typeof(Sprite)) as Sprite) != null)
        {
            sprites.event_status_sprites.Add(temp);
            i++;
        }

        i = 0;
        while ((temp = Resources.Load("Texture/Hero/hero_" + i, typeof(Sprite)) as Sprite) != null)
        {
            sprites.hero_sprites.Add(temp);
            i++;
        }

        i = 0;
        while ((temp = Resources.Load("Texture/avatar/" + i, typeof(Sprite)) as Sprite) != null)
        {
            sprites.player_avatar.Add(temp);
            i++;
        }

        i = 1;
        while ((temp = Resources.Load("Texture/Scene1/components/triangle_" + i, typeof(Sprite)) as Sprite) != null)
        {
            sprites.hero_state_triangle.Add(temp);
            i++;
        }

        i = 0;
        while ((temp = Resources.Load("Texture/StagesScene/bg/page_" + i, typeof(Sprite)) as Sprite) != null)
        {
            sprites.page_image.Add(temp);
            i++;
        }

        i = 0;
        while ((temp = Resources.Load("Texture/Stage_Scene/Pics/" + i, typeof(Sprite)) as Sprite) != null)
        {
            sprites.dialogue_image.Add(temp);
            i++;
        }

        AssetDatabase.CreateAsset(sprites, Path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("UpdateResources/update_audio", false, 0)]
    static void Audio()
    {
        AudioAsset audios = ScriptableObject.CreateInstance<AudioAsset>();
        string Path = "Assets/Resources/Assets/AudioAsset.asset";

        AudioClip temp = null;
        int i = 1;
        if ((temp = Resources.Load("Audio/Scene1/buy_herocard_error", typeof(AudioClip)) as AudioClip) != null)
        {
            audios._scene1_audio.Add(temp);
            i++;
        }

        if ((temp = Resources.Load("Audio/Scene1/buy_herocard_clip", typeof(AudioClip)) as AudioClip) != null)
        {
            audios._scene1_audio.Add(temp);
            i++;
        }

        temp = null;
        i = 0;
        while ((temp = Resources.Load("Audio/Common/bt_" + i, typeof(AudioClip)) as AudioClip) != null)
        {
            audios._common_audio_bt.Add(temp);
            i++;
        }

        AssetDatabase.CreateAsset(audios, Path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
