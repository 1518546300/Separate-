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

using Control;

namespace Model{
   public class Model_Grid
   {
        public string _hero_name;    //英雄名字
        public int _class_num;    //英雄阵营 ,1为我方阵营，2为敌方阵营,0为空,3.阵亡
        public int _hero_uid = -1;
        public string _hero_decribe;   //英雄描述
        public int _hero_willpower_constant;
        public int _hero_willpower_own;

        public int _hero_level = 1;

        public Sprite _hero_pic;

        public float _x;
        public float _y;

        public int _event_card_index;
        public int _event_card_collect_time;

        public string _cost_string;
        public string _cost_arr;

        public int _material_own_num = 0;

        public int _current_msgstack_index;

        public int _material_cost;

        public int _hero_state; //_hero_state = 3健康，= 2良好，= 1糟糕；

        //public List<int> _update_level_need = new List<int>();
        public int _update_level_need;
        public bool _updata_hero_flag;

        public List<Model_GridAttribute> _grid_attribute_list = new List<Model_GridAttribute>();
        public List<Model_GridStatus> _grid_status_list = new List<Model_GridStatus>();
        public List<Model_PlayerCard> _collect_card_list = new List<Model_PlayerCard>();

        public List<Model_GridMessage>[] _message_stack = { new List<Model_GridMessage>() , new List<Model_GridMessage>() };
        public List<Model_GridMessage> _message_settle_stack = new List<Model_GridMessage>();

        public Model_Grid(){
            //_hero_index = 0;
        }

        public bool Updata_Hero_Flag() {
            bool flag = false;
            if(_update_level_need != 0 && _material_own_num >= _update_level_need)
                flag = true;
            _updata_hero_flag = flag;
            return flag;
        }

        public int Get_Hero_State()
        {
            float indicator = 1-((((float)_hero_willpower_constant - _hero_willpower_own) / _hero_willpower_constant));
            //Debug.Log(_hero_willpower_constant + ":"+ _hero_willpower_own);
            if (indicator >= 0.7)
            {
                _hero_state = 3;
            }
            else if (indicator < 0.7 && indicator >= 0.3)
            {
                _hero_state = 2;
            }
            else
            {
                _hero_state = 1;
            }
            return _hero_state;
        }

        public void Rewrite_Grid_Material(int num) {
            if (num > 0) {
                _material_own_num += num;
            }
            else{
                if (_material_own_num < -num)
                {
                    Debug.Log("物质不够啦！");
                    _material_own_num = 0;
                }
                else
                {
                    _material_own_num += num;
                }
            }
        }

        public void Rewrite_Grid_Willpower(int num)
        {
            _hero_willpower_own += num;
            if (_hero_willpower_own < 0)
            {
                _hero_willpower_own = 0;
            }
            else if (_hero_willpower_own > _hero_willpower_constant)
            {
                _hero_willpower_own = _hero_willpower_constant;
            }
            Get_Hero_State();
        }

        public int Ret_Material_Num(int num) {
            int ret_num = num;
            if (_material_own_num < num)
            {
                ret_num = _material_own_num;
            }
            return ret_num;
        }

        public bool Find_Status(int num) {
            for (int i = 0; i < _grid_status_list.Count; i++) {
                if (_grid_status_list[i]._status_index == num) {
                    return true;
                }
            }
            return false;
        }

        public void Remove_Status(int num) {
            for (int i = 0; i < _grid_status_list.Count; i++)
            {
                if (_grid_status_list[i]._status_index == num)
                {
                    Status_Pop(i);
                    break;
                }
            }
        }

        //public void Init(int class_num,int hero_index,int willpower,Model_ResourcesManager res)
        public void Init_Player_Grid(int class_num,int material ,Model_HeroCard card, Model_ResourcesManager res)
        {
            _material_own_num = material;
            _class_num = class_num;
            _hero_uid = card._card_uid;
            _hero_name = card._name;
            _hero_decribe = card._decribe;
            
            _hero_willpower_own = card._willpower;
            _hero_willpower_constant = card._willpower;
            _cost_string = card._states;
            _cost_arr = card._attr;
            _hero_pic = card._pic;

            Get_Hero_State();
            Write_Hero_Attr(res);
        }

        public void Init_Opponent_Grid(int class_num, int card_index, int willpower, int material,Model_ResourcesManager res)
        {
            _material_own_num = 0;
            _class_num = class_num;
            _hero_uid = card_index;
            _hero_name = res._hero_card.heros_name[card_index];
            _hero_decribe = res._hero_card.heros_describe[card_index];

            _hero_willpower_own = willpower;
            _hero_willpower_constant = willpower;

            _material_own_num = material;

            _cost_string = "";
            _cost_arr = "";
            _hero_pic = res._sprites.hero_sprites[card_index];

            Get_Hero_State();

            //_material_cost = card._material_cost;
            //if (_class_num == 1)
            //{
            //    Write_Hero_Attr(res);
            //}
        }

        public void Write_Hero_Attr(Model_ResourcesManager res) {
            _grid_attribute_list.Clear();
            string[] attr_arr = _cost_arr.Split('|');
            string[] info = attr_arr[_hero_level - 1].Split(',');
            int index = int.Parse(info[0]);
            int time = int.Parse(info[1]);

            //foreach (string str in attr_arr) {
            //    string[] arrs = str.Split(',');
            //    _update_level_need.Add(int.Parse(arrs[2]));
            //}

            _update_level_need = int.Parse(info[2]);
            _material_cost = int.Parse(info[3]);

            Add_Card_Production(index, time, res._event_card.card_name[index],res._sprites.event_sprites[index]);
        }

        public void Update_Attr_State() {
            foreach (Model_GridAttribute attr in _grid_attribute_list)
            {
                if (_hero_state == 1)
                {
                    attr._attr_state = 0;
                }
                else
                {
                    attr._attr_state = 1;
                }
            }
        }

        public void Attr_Deal() {
            foreach (Model_GridAttribute attr in _grid_attribute_list)
            {
                if (_hero_state == 1)
                {
                    attr._attr_state = 0;
                }
                else {
                    attr._attr_state = 1;
                    attr.Rewrite_Time();
                }
            }
        }

        public void Add_Card_Production(int index,int time,string name,Sprite pic) {
            Model_GridAttribute attr = new Model_GridAttribute();
            attr.Init(index,name,time,pic);
            _grid_attribute_list.Add(attr);
        }

        //public void Init_Attribute(Sprite pic)
        //{

        //    string[] str = _cost_string.Split('-');
        //    string[] attr_arr = _cost_arr.Split(',');
        //    for (int i = 0; i < attr_arr.Length; i++)
        //    {
        //        //Debug.Log(int.Parse(attr_arr[i]));
        //        string[] info = str[int.Parse(attr_arr[i])-1].Split('/');
        //        Model_GridAttribute attr = new Model_GridAttribute();
        //        attr.Init(info[0],int.Parse(info[2])-1, int.Parse(info[3]),info[1],pic);
        //        _grid_attribute_list.Add(attr);
        //    }
        //}

        public void Hero_Level_Update(Model_ResourcesManager res) {
            if (_hero_level < 3) {
                _hero_level++;
                Write_Hero_Attr(res);
            }
        }

        public void Init_Pos(float x, float y) {
            _x = x;
            _y = y;
        }

        public Model_PlayerCard Event_Pop(int grid_index, int event_bt_index)
        {
            Model_PlayerCard temp = _collect_card_list[event_bt_index];
            _collect_card_list.RemoveAt(event_bt_index);
            return temp;
        }

        public void Status_Pop(int status_bt_index)
        {
            _grid_status_list.RemoveAt(status_bt_index);
        }

        public void Clear_Attribute()
        {
            _grid_attribute_list.Clear();
        }

        public void Clear_Status()
        {
            _grid_status_list.Clear();
        }

        //public void Clear_Msg()
        //{
        //    _message_stack[_current_msgstack_index].Clear();
        //    _message_settle_stack.Clear();
        //}

        public void Add_Status(int status_index,int status_class,string status_name, string describe, string cost, int time, Sprite pic)
        {
            Model_GridStatus status = new Model_GridStatus();
            status.Status_Init(status_index,status_class, status_name, describe, cost,time,pic);
            _grid_status_list.Add(status);
        }

        public void Loading_Msg()
        {
            foreach (Model_GridStatus status in _grid_status_list)
            {
                if (status._status_class == 1)
                {
                    Add_Message(status);
                }
            }

            if (_class_num == 1) {
                _message_settle_stack.Add(Add_GridAction_Prepare_Msg(1));
                if (_material_cost <= _material_own_num)
                {
                    _message_settle_stack.Add(Add_Hero_Material_Msg(1, "每年花费", _material_cost));

                }
                else
                {
                    _message_settle_stack.Add(Add_Hero_Material_Msg(1, "物质不够", _material_own_num));
                    _message_settle_stack.Add(Add_Hero_Damage_Msg(1, "惩罚伤害", 5));
                }
            }
        }

        public void Add_Message(Model_GridStatus status)
        {
            int k = 0;
            int flag = 0;
            foreach (string str in status._damage.Split('/'))
            {
                flag = int.Parse(str.Split(',')[0]);
                if (flag != 0)
                {
                    Cost_Decode(status._name,k, flag, int.Parse(str.Split(',')[1]));
                }
                k++;
            }
        }

        public void Add_Choice_Message() {
            _message_stack[_current_msgstack_index].Add(Add_Grid_Choice_Msg());
        }

        public void Cost_Decode(string card_name,int damage_flag, int damage_class_flag, int cost)
        {
            switch (damage_flag)
            {
                case 0:
                    _message_settle_stack.Add(Add_Player_Matreial_Msg(_class_num,damage_class_flag, cost));
                    break;
                case 1:
                    _message_settle_stack.Add(Add_Hero_Material_Msg(damage_class_flag,card_name, cost));
                    break;
                case 2:
                    _message_settle_stack.Add(Add_Hero_Damage_Msg(damage_class_flag,card_name, cost));
                    break;
                default:
                    Debug.Log("error!");
                    break;
            }
        }

        public float Grid_Action_Time()
        {
            float time = 0;
            foreach (Model_GridMessage msg in _message_stack[_current_msgstack_index])
            {
                time += msg._msg_time;
            }
            return time;
        }

        public float Grid_Round_Result_Time()
        {
            float time = 0;
            foreach (Model_GridMessage msg in _message_settle_stack)
            {
                time += msg._msg_time;
            }
            return time;
        }

        public void Add_IntimeMsg(Model_GridMessage msg) {
            _message_stack[_current_msgstack_index].Add(msg);
        }

        public Color Grid_Status() {
            return Ctrl_Scene1._Instance.Get_Grid_Status(_hero_willpower_own,_hero_willpower_constant);
        }

        public Model_GridMessage Add_Set_Hero_Miss_Msg()
        {
            Model_GridMessage msg = new Model_GridMessage();
            msg.Init_Miss_Msg("放置失败",1);
            return msg;
            //_message_stack[0].Add(msg);
        }

        public Model_GridMessage Add_Set_Hero_Msg(int hero_index) {
            Model_GridMessage msg = new Model_GridMessage();
            msg.Init_Set_Hero_Msg("放置成功",hero_index,1);
            return msg;
        }

        public Model_GridMessage Add_Update_Hero_Miss_Msg()
        {
            Model_GridMessage msg = new Model_GridMessage();
            msg.Init_Miss_Msg("升级失败", 1);
            return msg;
        }

        public Model_GridMessage Add_Update_Hero_Msg()
        {
            Model_GridMessage msg = new Model_GridMessage();
            msg.Init_Update_Hero_Msg(_hero_uid,"升级成功", 1);
            return msg;
        }

        /// <summary>
        /// flag = 1伤害，flag = 2治疗
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="damage"></param>
        public Model_GridMessage Add_Hero_Damage_Msg(int flag,string name,int damage)
        {
            Model_GridMessage msg = new Model_GridMessage();
            msg.Init_Hero_Damage_Msg(flag,name,damage, 1);
            return msg;
        }

        /// <summary>
        /// flag = 1损失物质，flag = 2获得物质
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="num"></param>
        public Model_GridMessage Add_Hero_Material_Msg(int flag,string name,int num)
        {
            Model_GridMessage msg = new Model_GridMessage();
            msg.Init_Hero_Material_Msg(flag,name,num, 1);
            return msg;
        }

        public Model_GridMessage Add_GridAction_Prepare_Msg(float time)
        {
            Model_GridMessage msg = new Model_GridMessage();
            msg.Init_GridAction_Prepare_Msg(time);
            return msg;
        }

        /// <summary>
        /// flag = 1失去物质，flag = 2获得物质
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="num"></param>
        public Model_GridMessage Add_Player_Matreial_Msg(int object_num,int flag,int num) {
            Model_GridMessage msg = new Model_GridMessage();
            msg.Init_Player_Material_Msg(object_num, flag,num,1);
            return msg;
        }

        public Model_GridMessage Add_Grid_Choice_Msg()
        {
            Model_GridMessage msg = new Model_GridMessage();
            msg.Init_Grid_Choice_Msg();
            return msg;
        }

        public Model_GridMessage Add_Hero_StatusChange_Msg(int grid_class)
        {
            Model_GridMessage msg = new Model_GridMessage();
            msg.Init_Hero_StatusChange_Msg(grid_class);
            return msg;
        }
    }
}
