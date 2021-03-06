﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zx2642UtilsScripts
{
    /// <summary>
    /// 生成Moons.OPC.DA.Agent2642项目OpcConfigurationData.xml文件中的Opc变量名、中间变量名、2642变量名的对应关系
    /// </summary>
    public class OpcAgent2642Script : ScriptBase
    {
        //private string[,] opc_mid_2642varNames = new string[3, 3] {
        //    { "","","" },
        //    { "","","" },
        //    { "","","" },
        //};

        /// <summary>
        /// opc名、中间变量名、2642变量名对应的矩阵
        /// 需要修改
        /// </summary>
        private List<string[]> opc_mid_2642varNames = new List<string[]> {
            //new string[]{ "OpcItemName", "ItemName", "VarName"},
            //new string[]{ "OpcItemName2", "ItemName2", "VarName2"},

new string[]{ "6-1同步机负荷端温度转换输出值", "V3020201", "V3020201"},
new string[]{ "6-1同步机非负荷端温度转换输出值", "V3020202", "V3020202"},
new string[]{ "6-1球磨机副轴负荷端温度转换输出值", "V3020203", "V3020203"},
new string[]{ "6-1球磨机副轴非负荷端温度转换输出值", "V3020204", "V3020204"},
new string[]{ "6-1球磨机主轴负荷端温度1转换输出值", "V3020205", "V3020205"},
new string[]{ "6-1球磨机主轴负荷端温度2转换输出值", "V3020206", "V3020206"},
new string[]{ "6-1球磨机主轴负荷端温度3转换输出值", "V3020207", "V3020207"},
new string[]{ "6-1球磨机主轴非负荷端温度1转换输出值", "V3020208", "V3020208"},
new string[]{ "6-1球磨机主轴非负荷端温度2转换输出值", "V3020209", "V3020209"},
new string[]{ "6-1球磨机主轴非负荷端温度3转换输出值", "V3020210", "V3020210"},
new string[]{ "6-1球磨机主轴负荷端油流转换输出值", "V3022201", "V3022201"},
new string[]{ "6-1球磨机主轴非负荷端油流转换输出值", "V3022202", "V3022202"},
new string[]{ "6-1油箱油温转换输出值", "V3020211", "V3020211"},
new string[]{ "6-1油箱低压油泵压力转换输出值", "V3023202", "V3023202"},
new string[]{ "6-1给矿皮带机减速机高速端轴承温度转换输出值", "V3020212", "V3020212"},
new string[]{ "6-1给矿皮带机电机负荷端轴承温度转换输出值", "V3020213", "V3020213"},
new string[]{ "6-1给矿皮带机机头滚筒右侧轴承温度转换输出值", "V3020214", "V3020214"},
new string[]{ "6-1给矿皮带机机头滚筒左侧轴承温度转换输出值", "V3020215", "V3020215"},
new string[]{ "6-1给矿皮带机机尾滚筒右侧轴承温度转换输出值", "V3020216", "V3020216"},
new string[]{ "6-1给矿皮带机机尾滚筒左侧轴承温度转换输出值", "V3020217", "V3020217"},
new string[]{ "6#分级机减速机高速端轴承温度转换输出值", "V3020218", "V3020218"},
new string[]{ "6#分级机电机负荷端轴承温度转换输出值", "V3020219", "V3020219"},
new string[]{ "5-1同步机负荷端温度转换输出值", "V3020220", "V3020220"},
new string[]{ "5-1同步机非负荷端温度转换输出值", "V3020221", "V3020221"},
new string[]{ "5-1球磨机副轴负荷端温度转换输出值", "V3020222", "V3020222"},
new string[]{ "5-1球磨机副轴非负荷端温度转换输出值", "V3020223", "V3020223"},
new string[]{ "5-1球磨机主轴负荷端温度1转换输出值", "V3020224", "V3020224"},
new string[]{ "5-1球磨机主轴负荷端温度2转换输出值", "V3020225", "V3020225"},
new string[]{ "5-1球磨机主轴负荷端温度3转换输出值", "V3020226", "V3020226"},
new string[]{ "5-1球磨机主轴非负荷端温度1转换输出值", "V3020227", "V3020227"},
new string[]{ "5-1球磨机主轴非负荷端温度2转换输出值", "V3020228", "V3020228"},
new string[]{ "5-1球磨机主轴非负荷端温度3转换输出值", "V3020229", "V3020229"},
new string[]{ "5-1球磨机主轴负荷端油流转换输出值", "V3022203", "V3022203"},
new string[]{ "5-1球磨机主轴非负荷端油流转换输出值", "V3022204", "V3022204"},
new string[]{ "5-1油箱油温转换输出值", "V3020230", "V3020230"},
new string[]{ "5-1油箱低压油泵压力转换输出值", "V3023204", "V3023204"},
new string[]{ "5-1给矿皮带机减速机高速端轴承温度转换输出值", "V3020231", "V3020231"},
new string[]{ "5-1给矿皮带机电机负荷端轴承温度转换输出值", "V3020232", "V3020232"},
new string[]{ "5-1给矿皮带机机头滚筒右侧轴承温度转换输出值", "V3020233", "V3020233"},
new string[]{ "5-1给矿皮带机机头滚筒左侧轴承温度转换输出值", "V3020234", "V3020234"},
new string[]{ "5-1给矿皮带机机尾滚筒右侧轴承温度转换输出值", "V3020235", "V3020235"},
new string[]{ "5-1给矿皮带机机尾滚筒左侧轴承温度转换输出值", "V3020236", "V3020236"},
new string[]{ "5#分级机减速机高速端轴承温度转换输出值", "V3020237", "V3020237"},
new string[]{ "5#分级机电机负荷端轴承温度转换输出值", "V3020238", "V3020238"},
new string[]{ "4-1同步机负荷端温度转换输出值", "V3020239", "V3020239"},
new string[]{ "4-1同步机非负荷端温度转换输出值", "V3020240", "V3020240"},
new string[]{ "4-1球磨机副轴负荷端温度转换输出值", "V3020241", "V3020241"},
new string[]{ "4-1球磨机副轴非负荷端温度转换输出值", "V3020242", "V3020242"},
new string[]{ "4-1球磨机主轴负荷端温度1转换输出值", "V3020243", "V3020243"},
new string[]{ "4-1球磨机主轴负荷端温度2转换输出值", "V3020244", "V3020244"},
new string[]{ "4-1球磨机主轴负荷端温度3转换输出值", "V3020245", "V3020245"},
new string[]{ "4-1球磨机主轴非负荷端温度1转换输出值", "V3020246", "V3020246"},
new string[]{ "4-1球磨机主轴非负荷端温度2转换输出值", "V3020247", "V3020247"},
new string[]{ "4-1球磨机主轴非负荷端温度3转换输出值", "V3020248", "V3020248"},
new string[]{ "4-1球磨机主轴负荷端油流转换输出值", "V3022205", "V3022205"},
new string[]{ "4-1球磨机主轴非负荷端油流转换输出值", "V3022206", "V3022206"},
new string[]{ "4-1油箱油温转换输出值", "V3020249", "V3020249"},
new string[]{ "4-1油箱低压油泵压力转换输出值", "V3023206", "V3023206"},
new string[]{ "4-1给矿皮带机减速机高速端轴承温度转换输出值", "V3020250", "V3020250"},
new string[]{ "4-1给矿皮带机电机负荷端轴承温度转换输出值", "V3020251", "V3020251"},
new string[]{ "4-1给矿皮带机机头滚筒右侧轴承温度转换输出值", "V3020252", "V3020252"},
new string[]{ "4-1给矿皮带机机头滚筒左侧轴承温度转换输出值", "V3020253", "V3020253"},
new string[]{ "4-1给矿皮带机机尾滚筒右侧轴承温度转换输出值", "V3020254", "V3020254"},
new string[]{ "4-1给矿皮带机机尾滚筒左侧轴承温度转换输出值", "V3020255", "V3020255"},
new string[]{ "4#分级机减速机高速端轴承温度转换输出值", "V3020256", "V3020256"},
new string[]{ "4#分级机电机负荷端轴承温度转换输出值", "V3020257", "V3020257"},

new string[]{ "3-1同步机负荷端温度转换输出值", "V3020258", "V3020258"},
new string[]{ "3-1同步机非负荷端温度转换输出值", "V3020259", "V3020259"},
new string[]{ "3-1球磨机副轴负荷端温度转换输出值", "V3020260", "V3020260"},
new string[]{ "3-1球磨机副轴非负荷端温度转换输出值", "V3020261", "V3020261"},
new string[]{ "3-1球磨机主轴负荷端温度1转换输出值", "V3020262", "V3020262"},
new string[]{ "3-1球磨机主轴负荷端温度2转换输出值", "V3020263", "V3020263"},
new string[]{ "3-1球磨机主轴负荷端温度3转换输出值", "V3020264", "V3020264"},
new string[]{ "3-1球磨机主轴非负荷端温度1转换输出值", "V3020265", "V3020265"},
new string[]{ "3-1球磨机主轴非负荷端温度2转换输出值", "V3020266", "V3020266"},
new string[]{ "3-1球磨机主轴非负荷端温度3转换输出值", "V3020267", "V3020267"},
new string[]{ "3-1球磨机主轴负荷端油流转换输出值", "V3022207", "V3022207"},
new string[]{ "3-1球磨机主轴非负荷端油流转换输出值", "V3022208", "V3022208"},
new string[]{ "3-1油箱油温转换输出值", "V3020268", "V3020268"},
new string[]{ "3-1油箱低压油泵压力转换输出值", "V3023208", "V3023208"},
new string[]{ "3-1给矿皮带机减速机高速端轴承温度转换输出值", "V3020269", "V3020269"},
new string[]{ "3-1给矿皮带机电机负荷端轴承温度转换输出值", "V3020270", "V3020270"},
new string[]{ "3-1给矿皮带机机头滚筒右侧轴承温度转换输出值", "V3020271", "V3020271"},
new string[]{ "3-1给矿皮带机机头滚筒左侧轴承温度转换输出值", "V3020272", "V3020272"},
new string[]{ "3-1给矿皮带机机尾滚筒右侧轴承温度转换输出值", "V3020273", "V3020273"},
new string[]{ "3-1给矿皮带机机尾滚筒左侧轴承温度转换输出值", "V3020274", "V3020274"},
new string[]{ "3#分级机减速机高速端轴承温度转换输出值", "V3020275", "V3020275"},
new string[]{ "3#分级机电机负荷端轴承温度转换输出值", "V3020276", "V3020276"},
new string[]{ "2-1同步机负荷端温度转换输出值", "V3020277", "V3020277"},
new string[]{ "2-1同步机非负荷端温度转换输出值", "V3020278", "V3020278"},
new string[]{ "2-1球磨机副轴负荷端温度转换输出值", "V3020279", "V3020279"},
new string[]{ "2-1球磨机副轴非负荷端温度转换输出值", "V3020280", "V3020280"},
new string[]{ "2-1球磨机主轴负荷端温度1转换输出值", "V3020281", "V3020281"},
new string[]{ "2-1球磨机主轴负荷端温度2转换输出值", "V3020282", "V3020282"},
new string[]{ "2-1球磨机主轴负荷端温度3转换输出值", "V3020283", "V3020283"},
new string[]{ "2-1球磨机主轴非负荷端温度1转换输出值", "V3020284", "V3020284"},
new string[]{ "2-1球磨机主轴非负荷端温度2转换输出值", "V3020285", "V3020285"},
new string[]{ "2-1球磨机主轴非负荷端温度3转换输出值", "V3020286", "V3020286"},
new string[]{ "2-1球磨机主轴负荷端油流转换输出值", "V3022209", "V3022209"},
new string[]{ "2-1球磨机主轴非负荷端油流转换输出值", "V3022210", "V3022210"},
new string[]{ "2-1油箱油温转换输出值", "V3020287", "V3020287"},
new string[]{ "2-1油箱低压油泵压力转换输出值", "V3023210", "V3023210"},
new string[]{ "2-1给矿皮带机减速机高速端轴承温度转换输出值", "V3020288", "V3020288"},
new string[]{ "2-1给矿皮带机电机负荷端轴承温度转换输出值", "V3020289", "V3020289"},
new string[]{ "2-1给矿皮带机机头滚筒右侧轴承温度转换输出值", "V3020290", "V3020290"},
new string[]{ "2-1给矿皮带机机头滚筒左侧轴承温度转换输出值", "V3020291", "V3020291"},
new string[]{ "2-1给矿皮带机机尾滚筒右侧轴承温度转换输出值", "V3020292", "V3020292"},
new string[]{ "2-1给矿皮带机机尾滚筒左侧轴承温度转换输出值", "V3020293", "V3020293"},
new string[]{ "2#分级机减速机高速端轴承温度转换输出值", "V3020294", "V3020294"},
new string[]{ "2#分级机电机负荷端轴承温度转换输出值", "V3020295", "V3020295"},
new string[]{ "2-2同步机负荷端温度转换输出值", "V3020296", "V3020296"},
new string[]{ "2-2同步机非负荷端温度转换输出值", "V3020297", "V3020297"},
new string[]{ "2-2球磨机副轴负荷端温度转换输出值", "V3020298", "V3020298"},
new string[]{ "2-2球磨机副轴非负荷端温度转换输出值", "V3020299", "V3020299"},
new string[]{ "2-2球磨机主轴负荷端温度1转换输出值", "V3020300", "V3020300"},
new string[]{ "2-2球磨机主轴负荷端温度2转换输出值", "V3020301", "V3020301"},
new string[]{ "2-2球磨机主轴负荷端温度3转换输出值", "V3020302", "V3020302"},
new string[]{ "2-2球磨机主轴非负荷端温度1转换输出值", "V3020303", "V3020303"},
new string[]{ "2-2球磨机主轴非负荷端温度2转换输出值", "V3020304", "V3020304"},
new string[]{ "2-2球磨机主轴非负荷端温度3转换输出值", "V3020305", "V3020305"},
new string[]{ "2-2球磨机主轴负荷端油流转换输出值", "V3022211", "V3022211"},
new string[]{ "2-2球磨机主轴非负荷端油流转换输出值", "V3022212", "V3022212"},
new string[]{ "2-2油箱油温转换输出值", "V3020306", "V3020306"},
new string[]{ "2-2油箱低压油泵压力转换输出值", "V3023212", "V3023212"},
new string[]{ "2-2给矿皮带机减速机高速端轴承温度转换输出值", "V3020307", "V3020307"},
new string[]{ "2-2给矿皮带机电机负荷端轴承温度转换输出值", "V3020308", "V3020308"},
new string[]{ "2-2给矿皮带机机头滚筒右侧轴承温度转换输出值", "V3020309", "V3020309"},
new string[]{ "2-2给矿皮带机机头滚筒左侧轴承温度转换输出值", "V3020310", "V3020310"},
new string[]{ "2-2给矿皮带机机尾滚筒右侧轴承温度转换输出值", "V3020311", "V3020311"},
new string[]{ "2-2给矿皮带机机尾滚筒左侧轴承温度转换输出值", "V3020312", "V3020312"},
new string[]{ "2-2中间给矿皮带机减速机高速端轴承温度转换输出值", "V3020313", "V3020313"},
new string[]{ "2-2中间给矿皮带机电机负荷端轴承温度转换输出值", "V3020314", "V3020314"},
new string[]{ "2-2中间给矿皮带机机头滚筒左侧轴承温度转换输出值", "V3020315", "V3020315"},
new string[]{ "2-2中间给矿皮带机机头滚筒右侧轴承温度转换输出值", "V3020316", "V3020316"},
new string[]{ "2-2中间给矿皮带机机尾滚筒左侧轴承温度转换输出值", "V3020317", "V3020317"},
new string[]{ "2-2中间给矿皮带机机尾滚筒右侧轴承温度转换输出值", "V3020318", "V3020318"},
new string[]{ "1-1同步机负荷端温度转换输出值", "V3020319", "V3020319"},
new string[]{ "1-1同步机非负荷端温度转换输出值", "V3020320", "V3020320"},
new string[]{ "1-1球磨机副轴负荷端温度转换输出值", "V3020321", "V3020321"},
new string[]{ "1-1球磨机副轴非负荷端温度转换输出值", "V3020322", "V3020322"},
new string[]{ "1-1球磨机主轴负荷端温度1转换输出值", "V3020323", "V3020323"},
new string[]{ "1-1球磨机主轴负荷端温度2转换输出值", "V3020324", "V3020324"},
new string[]{ "1-1球磨机主轴负荷端温度3转换输出值", "V3020325", "V3020325"},
new string[]{ "1-1球磨机主轴非负荷端温度1转换输出值", "V3020326", "V3020326"},
new string[]{ "1-1球磨机主轴非负荷端温度2转换输出值", "V3020327", "V3020327"},
new string[]{ "1-1球磨机主轴非负荷端温度3转换输出值", "V3020328", "V3020328"},
new string[]{ "1-1球磨机主轴负荷端油流转换输出值", "V3022213", "V3022213"},
new string[]{ "1-1球磨机主轴非负荷端油流转换输出值", "V3022214", "V3022214"},
new string[]{ "1-1油箱油温转换输出值", "V3020329", "V3020329"},
new string[]{ "1-1油箱低压油泵压力转换输出值", "V3023214", "V3023214"},
new string[]{ "1-1给矿皮带机减速机高速端轴承温度转换输出值", "V3020330", "V3020330"},
new string[]{ "1-1给矿皮带机电机负荷端轴承温度转换输出值", "V3020331", "V3020331"},
new string[]{ "1-1给矿皮带机机头滚筒右侧轴承温度转换输出值", "V3020332", "V3020332"},
new string[]{ "1-1给矿皮带机机头滚筒左侧轴承温度转换输出值", "V3020333", "V3020333"},
new string[]{ "1-1给矿皮带机机尾滚筒右侧轴承温度转换输出值", "V3020334", "V3020334"},
new string[]{ "1-1给矿皮带机机尾滚筒左侧轴承温度转换输出值", "V3020335", "V3020335"},

new string[]{ "7-1同步机负荷端温度转换输出值", "V3020336", "V3020336"},
new string[]{ "7-1同步机非负荷端温度转换输出值", "V3020337", "V3020337"},
new string[]{ "7-1球磨机副轴负荷端温度转换输出值", "V3020338", "V3020338"},
new string[]{ "7-1球磨机副轴非负荷端温度转换输出值", "V3020339", "V3020339"},
new string[]{ "7-1球磨机主轴负荷端温度1转换输出值", "V3020340", "V3020340"},
new string[]{ "7-1球磨机主轴负荷端温度2转换输出值", "V3020341", "V3020341"},
new string[]{ "7-1球磨机主轴负荷端温度3转换输出值", "V3020342", "V3020342"},
new string[]{ "7-1球磨机主轴非负荷端温度1转换输出值", "V3020343", "V3020343"},
new string[]{ "7-1球磨机主轴非负荷端温度2转换输出值", "V3020344", "V3020344"},
new string[]{ "7-1球磨机主轴非负荷端温度3转换输出值", "V3020345", "V3020345"},
new string[]{ "7-1球磨机主轴负荷端油流转换输出值", "V3022215", "V3022215"},
new string[]{ "7-1球磨机主轴非负荷端油流转换输出值", "V3022216", "V3022216"},
new string[]{ "7-1油箱油温转换输出值", "V3020346", "V3020346"},
new string[]{ "7-1油箱低压油泵压力转换输出值", "V3023216", "V3023216"},
new string[]{ "7-1给矿皮带机减速机高速端轴承温度转换输出值", "V3020347", "V3020347"},
new string[]{ "7-1给矿皮带机电机负荷端轴承温度转换输出值", "V3020348", "V3020348"},
new string[]{ "7-1给矿皮带机机头滚筒右侧轴承温度转换输出值", "V3020349", "V3020349"},
new string[]{ "7-1给矿皮带机机头滚筒左侧轴承温度转换输出值", "V3020350", "V3020350"},
new string[]{ "7-1给矿皮带机机尾滚筒右侧轴承温度转换输出值", "V3020351", "V3020351"},
new string[]{ "7-1给矿皮带机机尾滚筒左侧轴承温度转换输出值", "V3020352", "V3020352"},
new string[]{ "8-1同步机负荷端温度转换输出值", "V3020353", "V3020353"},
new string[]{ "8-1同步机非负荷端温度转换输出值", "V3020354", "V3020354"},
new string[]{ "8-1球磨机副轴负荷端温度转换输出值", "V3020355", "V3020355"},
new string[]{ "8-1球磨机副轴非负荷端温度转换输出值", "V3020356", "V3020356"},
new string[]{ "8-1球磨机主轴负荷端温度1转换输出值", "V3020357", "V3020357"},
new string[]{ "8-1球磨机主轴负荷端温度2转换输出值", "V3020358", "V3020358"},
new string[]{ "8-1球磨机主轴负荷端温度3转换输出值", "V3020359", "V3020359"},
new string[]{ "8-1球磨机主轴非负荷端温度1转换输出值", "V3020360", "V3020360"},
new string[]{ "8-1球磨机主轴非负荷端温度2转换输出值", "V3020361", "V3020361"},
new string[]{ "8-1球磨机主轴非负荷端温度3转换输出值", "V3020362", "V3020362"},
new string[]{ "8-1球磨机主轴负荷端油流转换输出值", "V3022217", "V3022217"},
new string[]{ "8-1球磨机主轴非负荷端油流转换输出值", "V3022218", "V3022218"},
new string[]{ "8-1油箱油温转换输出值", "V3020363", "V3020363"},
new string[]{ "8-1油箱低压油泵压力转换输出值", "V3023218", "V3023218"},
new string[]{ "8-1给矿皮带机减速机高速端轴承温度转换输出值", "V3020364", "V3020364"},
new string[]{ "8-1给矿皮带机电机负荷端轴承温度转换输出值", "V3020365", "V3020365"},
new string[]{ "8-1给矿皮带机机头滚筒右侧轴承温度转换输出值", "V3020366", "V3020366"},
new string[]{ "8-1给矿皮带机机头滚筒左侧轴承温度转换输出值", "V3020367", "V3020367"},
new string[]{ "8-1给矿皮带机机尾滚筒右侧轴承温度转换输出值", "V3020368", "V3020368"},
new string[]{ "8-1给矿皮带机机尾滚筒左侧轴承温度转换输出值", "V3020369", "V3020369"},
new string[]{ "9-1同步机负荷端温度转换输出值", "V3020370", "V3020370"},
new string[]{ "9-1同步机非负荷端温度转换输出值", "V3020371", "V3020371"},
new string[]{ "9-1球磨机副轴负荷端温度转换输出值", "V3020372", "V3020372"},
new string[]{ "9-1球磨机副轴非负荷端温度转换输出值", "V3020373", "V3020373"},
new string[]{ "9-1球磨机主轴负荷端温度1转换输出值", "V3020374", "V3020374"},
new string[]{ "9-1球磨机主轴负荷端温度2转换输出值", "V3020375", "V3020375"},
new string[]{ "9-1球磨机主轴负荷端温度3转换输出值", "V3020376", "V3020376"},
new string[]{ "9-1球磨机主轴非负荷端温度1转换输出值", "V3020377", "V3020377"},
new string[]{ "9-1球磨机主轴非负荷端温度2转换输出值", "V3020378", "V3020378"},
new string[]{ "9-1球磨机主轴非负荷端温度3转换输出值", "V3020379", "V3020379"},
new string[]{ "9-1球磨机主轴负荷端油流转换输出值", "V3022219", "V3022219"},
new string[]{ "9-1球磨机主轴非负荷端油流转换输出值", "V3022220", "V3022220"},
new string[]{ "9-1油箱油温转换输出值", "V3020380", "V3020380"},
new string[]{ "9-1油箱低压油泵压力转换输出值", "V3023220", "V3023220"},
new string[]{ "9-1给矿皮带机减速机高速端轴承温度转换输出值", "V3020381", "V3020381"},
new string[]{ "9-1给矿皮带机电机负荷端轴承温度转换输出值", "V3020382", "V3020382"},
new string[]{ "9-1给矿皮带机机头滚筒右侧轴承温度转换输出值", "V3020383", "V3020383"},
new string[]{ "9-1给矿皮带机机头滚筒左侧轴承温度转换输出值", "V3020384", "V3020384"},
new string[]{ "9-1给矿皮带机机尾滚筒右侧轴承温度转换输出值", "V3020385", "V3020385"},
new string[]{ "9-1给矿皮带机机尾滚筒左侧轴承温度转换输出值", "V3020386", "V3020386"},
new string[]{ "10-1同步机负荷端温度转换输出值", "V3020387", "V3020387"},
new string[]{ "10-1同步机非负荷端温度转换输出值", "V3020388", "V3020388"},
new string[]{ "10-1球磨机副轴负荷端温度转换输出值", "V3020389", "V3020389"},
new string[]{ "10-1球磨机副轴非负荷端温度转换输出值", "V3020390", "V3020390"},
new string[]{ "10-1球磨机主轴负荷端温度1转换输出值", "V3020391", "V3020391"},
new string[]{ "10-1球磨机主轴负荷端温度2转换输出值", "V3020392", "V3020392"},
new string[]{ "10-1球磨机主轴负荷端温度3转换输出值", "V3020393", "V3020393"},
new string[]{ "10-1球磨机主轴非负荷端温度1转换输出值", "V3020394", "V3020394"},
new string[]{ "10-1球磨机主轴非负荷端温度2转换输出值", "V3020395", "V3020395"},
new string[]{ "10-1球磨机主轴非负荷端温度3转换输出值", "V3020396", "V3020396"},
new string[]{ "10-1球磨机主轴负荷端油流转换输出值", "V3022221", "V3022221"},
new string[]{ "10-1球磨机主轴非负荷端油流转换输出值", "V3022222", "V3022222"},
new string[]{ "10-1油箱油温转换输出值", "V3020397", "V3020397"},
new string[]{ "10-1油箱低压油泵压力转换输出值", "V3023222", "V3023222"},
new string[]{ "10-1给矿皮带机减速机高速端轴承温度转换输出值", "V3020398", "V3020398"},
new string[]{ "10-1给矿皮带机电机负荷端轴承温度转换输出值", "V3020399", "V3020399"},
new string[]{ "10-1给矿皮带机机头滚筒右侧轴承温度转换输出值", "V3020400", "V3020400"},
new string[]{ "10-1给矿皮带机机头滚筒左侧轴承温度转换输出值", "V3020401", "V3020401"},
new string[]{ "10-1给矿皮带机机尾滚筒右侧轴承温度转换输出值", "V3020402", "V3020402"},
new string[]{ "10-1给矿皮带机机尾滚筒左侧轴承温度转换输出值", "V3020403", "V3020403"},

new string[]{ "一段受电开关温度001转换输出值", "V3020801", "V3020801"},
new string[]{ "一段受电开关温度002转换输出值", "V3020802", "V3020802"},
new string[]{ "一段受电开关温度003转换输出值", "V3020803", "V3020803"},
new string[]{ "一段受电开关温度004转换输出值", "V3020804", "V3020804"},
new string[]{ "一段受电开关温度005转换输出值", "V3020805", "V3020805"},
new string[]{ "一段受电开关温度006转换输出值", "V3020806", "V3020806"},
new string[]{ "二段受电开关温度007转换输出值", "V3020807", "V3020807"},
new string[]{ "二段受电开关温度008转换输出值", "V3020808", "V3020808"},
new string[]{ "二段受电开关温度009转换输出值", "V3020809", "V3020809"},
new string[]{ "二段受电开关温度010转换输出值", "V3020810", "V3020810"},
new string[]{ "二段受电开关温度011转换输出值", "V3020811", "V3020811"},
new string[]{ "二段受电开关温度012转换输出值", "V3020812", "V3020812"},
new string[]{ "三段受电开关温度013转换输出值", "V3020813", "V3020813"},
new string[]{ "三段受电开关温度014转换输出值", "V3020814", "V3020814"},
new string[]{ "三段受电开关温度015转换输出值", "V3020815", "V3020815"},
new string[]{ "三段受电开关温度016转换输出值", "V3020816", "V3020816"},
new string[]{ "三段受电开关温度017转换输出值", "V3020817", "V3020817"},
new string[]{ "三段受电开关温度018转换输出值", "V3020818", "V3020818"},
new string[]{ "四段受电开关温度019转换输出值", "V3020819", "V3020819"},
new string[]{ "四段受电开关温度020转换输出值", "V3020820", "V3020820"},
new string[]{ "四段受电开关温度021转换输出值", "V3020821", "V3020821"},
new string[]{ "四段受电开关温度022转换输出值", "V3020822", "V3020822"},
new string[]{ "四段受电开关温度023转换输出值", "V3020823", "V3020823"},
new string[]{ "四段受电开关温度024转换输出值", "V3020824", "V3020824"},
new string[]{ "电磁站环境温度025转换输出值", "V3020825", "V3020825"},
new string[]{ "电磁站环境温度026转换输出值", "V3020826", "V3020826"},
new string[]{ "电缆沟环境温度001转换输出值", "V3020827", "V3020827"},
new string[]{ "电缆沟环境温度002转换输出值", "V3020828", "V3020828"},
new string[]{ "电缆层环境温度003转换输出值", "V3020829", "V3020829"},
new string[]{ "电缆层环境温度004转换输出值", "V3020830", "V3020830"},
new string[]{ "1#励磁柜温度001转换输出值", "V3020831", "V3020831"},
new string[]{ "2#励磁柜温度001转换输出值", "V3020832", "V3020832"},
new string[]{ "3#励磁柜温度001转换输出值", "V3020833", "V3020833"},
new string[]{ "4#励磁柜温度001转换输出值", "V3020834", "V3020834"},
new string[]{ "1#励磁柜湿度002转换输出值", "V3022601", "V3022601"},
new string[]{ "2#励磁柜湿度002转换输出值", "V3022602", "V3022602"},
new string[]{ "3#励磁柜湿度002转换输出值", "V3022603", "V3022603"},
new string[]{ "4#励磁柜湿度002转换输出值", "V3022604", "V3022604"},

            //new string[]{ "", "", ""},
            //new string[]{ "", "", ""},
        };

        /// <summary>
        /// 生成xml片段
        /// </summary>
        public void GenerateXmlSegments()
        {
            WriteLine($"<!-- 中间名称的组合(DataA)对2642变量名(DataB)的映射 -->");
            WriteLine($"<ItemName2VarName>");
            foreach( var names in opc_mid_2642varNames)
            {
                var opc = names[0];
                var mid = names[1];
                var var2642 = names[2];
                WriteLine($"  <Item2Var>");
                WriteLine($"    <DataA>{mid}</DataA>");
                WriteLine($"    <DataB>{var2642}</DataB>");
                WriteLine($"  </Item2Var>");
            }
            WriteLine($"</ItemName2VarName>");
            WriteLine($"");
            WriteLine($"");
            WriteLine($"<!-- OPC服务器数据项名称(DataA)对中间名称(DataB)的映射 -->");
            WriteLine($"<OpcItemName2ItemName>");
            foreach (var names in opc_mid_2642varNames)
            {
                var opc = names[0];
                var mid = names[1];
                var var2642 = names[2];
                WriteLine($"  <Opc2Item>");
                WriteLine($"    <DataA>{opc}</DataA>");
                WriteLine($"    <DataB>{mid}</DataB>");
                WriteLine($"  </Opc2Item>");
            }
            WriteLine($"</OpcItemName2ItemName>");
            WriteLine($"");
        }
    }
}
