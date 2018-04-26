﻿using System.Collections.Generic;
using System.Linq;

namespace BulletRay.Web.Models.Common
{
    public class DataTableParametersModel
    {
        /// <summary>
        /// 请求次数计数器
        /// </summary>
        public int Draw { get; set; }

        /// <summary>
        /// 第一条数据的起始位置
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// 每页显示的数据条数
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 数据列
        /// </summary>
        public List<DataTablesColumns> Columns { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public List<DataTablesOrder> Order { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy => Columns != null && Columns.Any() && Order != null && Order.Any()
            ? Columns[Order[0].Column].Data
            : string.Empty;

        /// <summary>
        /// 排序模式
        /// </summary>
        public DataTablesOrderDir OrderDir => Order != null && Order.Any()
            ? Order[0].Dir
            : DataTablesOrderDir.Desc;
    }

    public class DataTablesOrder
    {
        /// <summary>
        /// 排序的列的索引
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// 排序模式
        /// </summary>
        public DataTablesOrderDir Dir { get; set; }
    }

    /// <summary>
    /// 排序模式
    /// </summary>
    public enum DataTablesOrderDir
    {
        /// <summary>
        /// 正序
        /// </summary>
        Asc,

        /// <summary>
        /// 倒序
        /// </summary>
        Desc
    }

    /// <summary>
    /// 数据列
    /// </summary>
    public class DataTablesColumns
    {
        /// <summary>
        /// 数据源
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否可以被搜索
        /// </summary>
        public bool Searchable { get; set; }

        /// <summary>
        /// 是否可以排序
        /// </summary>
        public bool Orderable { get; set; }
    }
}
