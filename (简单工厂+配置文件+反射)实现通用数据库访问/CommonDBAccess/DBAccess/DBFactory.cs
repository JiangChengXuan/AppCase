using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Data.Common;
namespace DBAccess
{
   public class DBFactory
    {

        private static string assemblyName = ConfigurationManager.AppSettings["DBAssembly"];

        private static string DBClass = ConfigurationManager.AppSettings["DBClass"];

        public static DBHelper CreateDBHelper()
        {
            DBHelper dbHelper = null;
            Assembly ass = Assembly.Load(assemblyName);
            Type type = ass.GetType(assemblyName+"."+DBClass);

            object obj = Activator.CreateInstance(type);

            if (obj is DBHelper )
            {
                dbHelper = obj as DBHelper;
            }

            return dbHelper;
        } //END CreateDBHelper（）

       


    }
}
