using System;
using System.IO;
using System.Security.AccessControl;

namespace VitAccess
{
    /// <summary>
    /// Класс по работе с доступом к файловой системе
    /// </summary>
    public class ClassAccess
    {
        private readonly string[] usersName;

        /// <summary>
        /// Установка прав доступа к папке на диске для заданного пользователя
        /// </summary>
        /// <param name="Dir"></param>
        public bool ChangeAccess(string DirectoryName, string userName)
        {
            if (!Directory.Exists(DirectoryName))
            {
                Console.WriteLine("директория " + DirectoryName + " отсутствует!");
                return false;
            }
            try
            {
                DirectorySecurity ds = Directory.GetAccessControl(DirectoryName);
                ds.AddAccessRule(
                    new System.Security.AccessControl.FileSystemAccessRule(
                        userName,
                        System.Security.AccessControl.FileSystemRights.FullControl,
                        System.Security.AccessControl.InheritanceFlags.ContainerInherit,
                        System.Security.AccessControl.PropagationFlags.NoPropagateInherit,
                        System.Security.AccessControl.AccessControlType.Allow)
                        );

                Directory.SetAccessControl(DirectoryName, ds);
                return true;
            }
            catch { return false; }
        }

        private struct StaticUsersName
        {
            public const string AUTHENTICATED_USERS = "Authtenticated Users";
            public const string SYSTEM = "SYSTEM";
        }
    }
}