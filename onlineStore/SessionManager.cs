using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace onlineStore
{
    public static class SessionManager
    {
        public static int CurrentUserId { get; set; }
        public static string CurrentUserLogin { get; set; }
        public static string CurrentUserRole { get; set; }

        public static bool IsAuthenticated => CurrentUserId > 0;

        public static void ClearSession()
        {
            CurrentUserId = 0;
            CurrentUserLogin = null;
            CurrentUserRole = null;
        }


    }
}

