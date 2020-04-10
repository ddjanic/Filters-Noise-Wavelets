using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
        /*  Application.StartupPath
         * 
         * Записываем значение в файл:
         * INI ini = new INI("Путь_к_файлу"); ini.IniWriteValue("Test_block","Key","Value");
         * 
         * Теперь в нашем файле есть значение Key, которое равно Value. Теперь считаем его:
         * string value = ini.IniReadValue("Test_block","Key");
         *  
         */

    class ini              
    {
        public string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public void INI(string INIPath)
        {
            path = INIPath;
        } 

        public void IniWriteValue(string Section, string Key, string Value)
        {
           if(!Directory.Exists(Path.GetDirectoryName(path)))
                 Directory.CreateDirectory(Path.GetDirectoryName(path));

           if(!File.Exists(path))
                  using (File.Create(path)) { };

           WritePrivateProfileString(Section, Key, Value, this.path);
        }

        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }

    }
}
