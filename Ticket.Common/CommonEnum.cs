/** 1. 功能：用于公共操作类的枚举
 *  2. 作者：何平 
 *  3. 创建日期：2008-4-18
 *  4. 最后修改日期：2008-9-19
**/
#region 引用命名空间
using System;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Ticket.Common
{
    #region 当前应用程序的类型
    /// <summary>
    /// 表示当前应用程序的类型
    /// </summary>
    public enum AppType
    {
        /// <summary>
        /// 当前是BS类型的应用程序
        /// </summary>
        BS = 0,
        /// <summary>
        /// 当前是CS类型的应用程序
        /// </summary>
        CS = 1
    }
    #endregion

    #region 跟踪日志文件的跟踪等级
    /// <summary>
    /// 跟踪日志文件的跟踪等级
    /// </summary>
    public enum TraceLogLevel
    {
        /// <summary>
        /// 不使用跟踪日志
        /// </summary>
        None = 0,
        /// <summary>
        /// 仅把错误信息写入跟踪日志
        /// </summary>
        Error = 1,
        /// <summary>
        /// 把错误信息和警告信息写入跟踪日志
        /// </summary>
        Warning = 2,
        /// <summary>
        /// 把所有的信息都写入跟踪日志
        /// </summary>
        Information = 3
    }
    #endregion

    #region 日志字段
    /// <summary>
    /// 日志字段
    /// </summary>
    public enum LogField
    {
        /// <summary>
        /// 日志编号
        /// </summary>
        LogID = 0,
        /// <summary>
        /// 日志记录时间
        /// </summary>
        Time = 1,
        /// <summary>
        /// 操作人
        /// </summary>
        Operator = 2,
        /// <summary>
        /// 操作
        /// </summary>
        Operation = 3,
        /// <summary>
        /// 日志内容
        /// </summary>
        Message = 4
    }
    #endregion

    #region 基础控件的类型
    /// <summary>
    /// 基础控件的类型
    /// </summary>
    public enum ControlType
    {
        /// <summary>
        /// 增强的文本框
        /// </summary>
        TextBoxControl = 0,
        /// <summary>
        /// 增强的下拉列表框
        /// </summary>
        DropDownListControl = 1
    }
    #endregion

    #region 列表控件的类型
    /// <summary>
    /// 列表控件的类型
    /// </summary>
    public enum ListControlType
    {
        /// <summary>
        /// 表格控件
        /// </summary>
        GridView = 0,
        /// <summary>
        /// 列表控件
        /// </summary>
        DataList = 1,
        /// <summary>
        /// 轻量级模板控件
        /// </summary>
        Repeater = 2,
        /// <summary>
        /// 列表框
        /// </summary>
        ListBox = 3
    }
    #endregion

    #region 查询运算符
    /// <summary>
    /// 查询运算符
    /// </summary>
    internal enum QueryOperator
    {
        /// <summary>
        /// 等于( = )
        /// </summary>
        Equal = 0,
        /// <summary>
        /// 不等于( != )
        /// </summary>
        NotEqual = 1,
        /// <summary>
        /// 大于( > )
        /// </summary>
        Greater = 2,
        /// <summary>
        /// 大于等于( >= )
        /// </summary>
        GreaterEqual = 3,
        /// <summary>
        /// 小于
        /// </summary>
        Less = 4,
        /// <summary>
        /// 小于等于
        /// </summary>
        LessEqual = 5,
        /// <summary>
        /// 模糊查询
        /// </summary>
        Like = 6
    }
    #endregion

    #region 数据操作类型
    /// <summary>
    /// 数据操作类型
    /// </summary>
    public enum DataOperatorType
    {
        /// <summary>
        /// 数据库操作
        /// </summary>
        DataBase = 0,
        /// <summary>
        /// 对象操作
        /// </summary>
        Object = 1
    }
    #endregion

    #region 数据库操作类型
    /// <summary>
    /// 数据库操作类型
    /// </summary>
    public enum DataBaseOperatorType
    {
        /// <summary>
        /// 编辑操作，即新增和修改
        /// </summary>
        Edit = 0,
        /// <summary>
        /// 查询操作
        /// </summary>
        Query = 1
    }
    #endregion

    #region 数据库类型
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DataBaseType
    {
        /// <summary>
        /// SQL Server 数据库
        /// </summary>
        SQLServer = 0,
        /// <summary>
        /// Oracle 数据库
        /// </summary>
        Oracle = 1,
        /// <summary>
        /// DB2 数据库
        /// </summary>
        DB2 = 2,
        /// <summary>
        /// MySql 数据库
        /// </summary>
        MySql = 3,
        /// <summary>
        /// Access 数据库
        /// </summary>
        Access = 4
    }
    #endregion

    #region 数据类型
    /// <summary>
    /// 数据类型
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// 字符串
        /// </summary>
        String = 0,
        /// <summary>
        /// 数字
        /// </summary>
        Number = 1,
        /// <summary>
        /// 日期
        /// </summary>
        Date = 2,
        /// <summary>
        /// 布尔值
        /// </summary>
        Boolean = 3,
        /// <summary>
        /// 二进制流
        /// </summary>
        Binary = 4
    }
    #endregion

    #region 文件上传的方式
    /// <summary>
    /// 文件上传的方式
    /// </summary>
    public enum UploadFileType
    {
        /// <summary>
        /// 上传到文件目录
        /// </summary>
        Directory = 0,
        /// <summary>
        /// 上传到数据库
        /// </summary>
        DataBase = 1
    }
    #endregion

    #region FusionChart图表控件的类型
    /// <summary>
    /// FusionChart图表控件的类型
    /// </summary>
    public enum FusionChartType
    {
        /// <summary>
        /// 2D区域图
        /// </summary>
        Area2D,
        /// <summary>
        /// 2D横向柱状图
        /// </summary>
        Bar2D,
        /// <summary>
        /// 泡状图
        /// </summary>
        Bubble,
        /// <summary>
        /// 2D柱状图
        /// </summary>
        Column2D,
        /// <summary>
        /// 3D柱状图
        /// </summary>
        Column3D,
        /// <summary>
        /// 2D环状图
        /// </summary>
        Doughnut2D,
        /// <summary>
        /// 3D环状图
        /// </summary>
        Doughnut3D,
        /// <summary>
        /// 2D曲线图
        /// </summary>
        Line,
        /// <summary>
        /// 2D区域图
        /// </summary>
        MSArea,
        /// <summary>
        /// 2D横向柱状图
        /// </summary>
        MSBar2D,
        /// <summary>
        /// 3D横向柱状图
        /// </summary>
        MSBar3D,
        /// <summary>
        /// 2D柱状图
        /// </summary>
        MSColumn2D,
        /// <summary>
        /// 3D柱状图
        /// </summary>
        MSColumn3D,
        /// <summary>
        /// 3D ( 柱状 + 曲线 ) 图
        /// </summary>
        MSColumn3DLineDY,
        /// <summary>
        /// 3D ( 柱状 + 曲线 ) 图
        /// </summary>
        MSColumnLine3D,
        /// <summary>
        /// 由各种形状组合而成的2D图
        /// </summary>
        MSCombi2D,
        /// <summary>
        /// 由各种形状组合而成的2D图
        /// </summary>
        MSCombiDY2D,
        /// <summary>
        /// 2D曲线图
        /// </summary>
        MSLine,
        /// <summary>
        /// 2D柱状图
        /// </summary>
        MSStackedColumn2D,
        /// <summary>
        /// 2D ( 柱状 + 曲线 ) 图
        /// </summary>
        MSStackedColumn2DLineDY,
        /// <summary>
        /// 2D饼图
        /// </summary>
        Pie2D,
        /// <summary>
        /// 3D饼图
        /// </summary>
        Pie3D,
        /// <summary>
        /// 点状图
        /// </summary>
        Scatter,
        /// <summary>
        /// 2D区域图( 带滚动条 )
        /// </summary>
        ScrollArea2D,
        /// <summary>
        /// 2D柱状图( 带滚动条 )
        /// </summary>
        ScrollColumn2D,
        /// <summary>
        /// 由各种形状组合而成的图形( 带滚动条 )
        /// </summary>
        ScrollCombi2D,
        /// <summary>
        /// 由各种形状组合而成的图形( 带滚动条 )
        /// </summary>
        ScrollCombiDY2D,
        /// <summary>
        /// 2D曲线图( 带滚动条 )
        /// </summary>
        ScrollLine2D,
        /// <summary>
        /// 2D柱状图( 带滚动条 )
        /// </summary>
        ScrollStackedColumn2D,
        /// <summary>
        /// SSGrid
        /// </summary>
        SSGrid,
        /// <summary>
        /// 2D区域图
        /// </summary>
        StackedArea2D,
        /// <summary>
        /// 2D横向柱状图
        /// </summary>
        StackedBar2D,
        /// <summary>
        /// 3D横向柱状图
        /// </summary>
        StackedBar3D,
        /// <summary>
        /// 2D柱状图
        /// </summary>
        StackedColumn2D,
        /// <summary>
        /// 3D柱状图
        /// </summary>
        StackedColumn3D,
        /// <summary>
        /// 3D ( 柱状 + 曲线 ) 图
        /// </summary>
        StackedColumn3DLineDY
    }
    #endregion

    #region FusionChart图表控件的颜色风格
    /// <summary>
    /// FusionChart图表控件的颜色风格
    /// </summary>
    public enum FusionChartPalette
    {
        /// <summary>
        /// 浅灰色风格
        /// </summary>
        Style1 = 1,
        /// <summary>
        /// 深灰色风格
        /// </summary>
        Style2 = 2,
        /// <summary>
        /// 浅蓝色风格
        /// </summary>
        Style3 = 3,
        /// <summary>
        /// 浅棕色风格
        /// </summary>
        Style4 = 4,
        /// <summary>
        /// 浅红色风格
        /// </summary>
        Style5 = 5
    }
    #endregion
}
