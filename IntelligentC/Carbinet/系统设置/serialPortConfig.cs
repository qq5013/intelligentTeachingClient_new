using System;
using System.Collections.Generic;
using System.Text;
using Carbinet;
using System.Data;


namespace Config
{
    public class serialPortConfig
    {
        //static string configFilePath = "app.conf";
        public string configName = string.Empty;
        public string portName = string.Empty;
        public string baudRate = string.Empty;
        public string parity = string.Empty;
        public string dataBits = string.Empty;
        public string stopBits = string.Empty;
        public serialPortConfig(string port, string rate)
        {
            this.portName = port;
            this.baudRate = rate;
        }
        public serialPortConfig(string configName, string port, string rate, string parity, string dataBits, string stopBits)
        {
            this.configName = configName;
            this.portName = port;
            this.baudRate = rate;
            this.parity = parity;
            this.dataBits = dataBits;
            this.stopBits = stopBits;
        }
        public static void copy(serialPortConfig source, serialPortConfig dest)
        {
            dest.configName = source.configName;
            dest.portName = source.portName;
            dest.baudRate = source.baudRate;
            dest.parity = source.parity;
            dest.dataBits = source.dataBits;
            dest.stopBits = source.stopBits;
        }
        public static serialPortConfig getDefaultConfig()
        {
            serialPortConfig config = null;
            string port_name = string.Empty;
            string baut_rate = string.Empty;
            string sql = "select vvalue from sys_config where vkey = '{0}'";
            DataTable dt_temp = CsharpSQLiteHelper.ExecuteTable(sql, new object[1] { "port_name" });
            if (dt_temp.Rows.Count > 0)
            {
                port_name = dt_temp.Rows[0]["vvalue"].ToString();
            }
            sql = "select vvalue from sys_config where vkey = '{0}'";
            dt_temp = CsharpSQLiteHelper.ExecuteTable(sql, new object[1] { "baut_rate" });
            if (dt_temp.Rows.Count > 0)
            {
                baut_rate = dt_temp.Rows[0]["vvalue"].ToString();
            }
            config = new serialPortConfig(port_name, baut_rate);
            //IObjectContainer db = Db4oFactory.OpenFile(serialPortConfig.configFilePath);
            //try
            //{
            //    IList<serialPortConfig> list = db.Query<serialPortConfig>(delegate(serialPortConfig cf)
            //    {
            //        return cf.configName == "default";
            //    }
            //                                              );
            //    if (list.Count > 0)
            //    {
            //        config = list[0];
            //    }

            //}
            //finally
            //{
            //    db.Close();
            //}
            return config;
        }
        public static void saveConfig(serialPortConfig config)
        {
            string sql = "insert into sys_config(vkey,vvalue) values('{0}','{1}')";
            int rtn = 0;
            rtn = CsharpSQLiteHelper.ExecuteNonQuery(sql, new object[2] { "port_name", config.portName });
            if (rtn <= 0)
            {
                return;
            }
            rtn = 0;
            rtn = CsharpSQLiteHelper.ExecuteNonQuery(sql, new object[2] { "baut_rate", config.baudRate });
            //if (rtn <= 0)
            //{
            //    return;
            //}
            //// accessDb4o
            //IObjectContainer db = Db4oFactory.OpenFile(serialPortConfig.configFilePath);
            //try
            //{
            //    IList<serialPortConfig> list = db.Query<serialPortConfig>(delegate(serialPortConfig cf)
            //    {
            //        return cf.configName == config.configName;
            //    }
            //                                              );
            //    if (list.Count <= 0)
            //    {
            //        db.Store(config);
            //    }
            //    else
            //    {
            //        serialPortConfig.copy(config, list[0]);
            //        db.Store(list[0]);
            //    }

            //}
            //finally
            //{
            //    db.Close();
            //}
        }
    }
}
