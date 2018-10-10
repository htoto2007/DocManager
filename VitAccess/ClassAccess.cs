using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Management;
using System.Security.AccessControl;
using System.DirectoryServices;
using System.Collections;

namespace VitAccess
{
    public class ClassAccess
    {
        string[] usersName;
        struct StaticUsersName
        {
            public const string SYSTEM = "SYSTEM";
            public const string AUTHENTICATED_USERS = "Authtenticated Users";
        }

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
                var ds = Directory.GetAccessControl(DirectoryName);
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
    }
}
