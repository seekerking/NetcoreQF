using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DBContextEF.Extensions
{
    public static class DbContextExtension
    {
        public static IList<T> SqlQuery<T>(this Microsoft.EntityFrameworkCore.DbContext db, string sql, List<object> parameters = null, bool mapAllProperty = true)
            where T : new()
        {
            //注意：不要对GetDbConnection获取到的conn进行using或者调用Dispose，否则DbContext后续不能再进行使用了，会抛异常
            var conn = db.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }
                    var propts = typeof(T).GetProperties().ToList();
                    var rtnList = new List<T>();
                    T model;
                    object val;
                    using (var reader = command.ExecuteReader())
                    {                        //如果sql查询语句的列和转换实体的属性不一一对应，是否需要强关联，强关联不对应就会报错，反之不会报错
                        if (!mapAllProperty)
                        {
                            var temppropts = new List<PropertyInfo>();
                            if (reader.HasRows)
                            {
                                DataTable tableSchream = reader.GetSchemaTable();
                                foreach (var coloumn in propts)
                                {
                                    if (ColumnExists(tableSchream, coloumn.Name))
                                    {
                                        temppropts.Add(coloumn);

                                    }
                                }

                            }
                            propts = temppropts;
                        }
                        while (reader.Read())
                        {
                            model = new T();
                            foreach (var l in propts)
                            {
                                val = reader[l.Name];
                                if (val == DBNull.Value)
                                {
                                    l.SetValue(model, null);
                                }
                                else
                                {
                                    l.SetValue(model, val);
                                }
                            }
                            rtnList.Add(model);
                        }
                    }
                    return rtnList;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public static async Task<IList<T>> SqlQueryAsync<T>(this Microsoft.EntityFrameworkCore.DbContext db, string sql, List<object> parameters = null, bool mapAllProperty = true)
            where T : new()
        {
            //注意：不要对GetDbConnection获取到的conn进行using或者调用Dispose，否则DbContext后续不能再进行使用了，会抛异常
            var conn = db.Database.GetDbConnection();
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = sql;
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }
                    var propts = typeof(T).GetProperties().ToList();



                    var rtnList = new List<T>();
                    T model;
                    object val;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        //如果sql查询语句的列和转换实体的属性不一一对应，是否需要强关联，强关联不对应就会报错，反之不会报错
                        if (!mapAllProperty)
                        {
                            var temppropts = new List<PropertyInfo>();
                            if (reader.HasRows)
                            {
                                DataTable tableSchream = reader.GetSchemaTable();
                                foreach (var coloumn in propts)
                                {
                                    if (ColumnExists(tableSchream, coloumn.Name))
                                    {
                                        temppropts.Add(coloumn);

                                    }
                                }

                            }
                            propts = temppropts;
                        }


                        while (reader.Read())
                        {
                            model = new T();
                            foreach (var l in propts)
                            {
                                //如果没有不应该throw exception ，应该继续走下去
                                val = reader[l.Name];
                                if (val == DBNull.Value)
                                {
                                    l.SetValue(model, null);
                                }
                                else
                                {
                                    l.SetValue(model, val);
                                }
                            }
                            rtnList.Add(model);
                        }
                    }
                    command.Parameters.Clear();
                    return rtnList;
                }
            }
            finally
            {
                conn.Close();
            }
        }


        //调用该方法判断datareader中是否有指定列
        private static bool ColumnExists(DataTable table, string columnName)
        {

            table.DefaultView.RowFilter = "ColumnName= '" + columnName + "'";
            return (table.DefaultView.Count > 0);
        }
    }
}
