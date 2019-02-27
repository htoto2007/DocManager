using VitFTP;

namespace VitAccess
{
    /// <summary>
    /// Класс по работе с доступом к файловой системе
    /// </summary>
    public class ClassAccess
    {
        

        /// <summary>
        /// Установка прав доступа к папке на диске для заданного пользователя
        /// </summary>
        /// <param name="Dir"></param>
        public bool ChangeAccess(string DirectoryName, string userName)
        {
            ClassFTP classFTP = new ClassFTP("SYSTEM", "");

            return true;
        }

        
    }
}