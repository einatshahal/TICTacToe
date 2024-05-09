using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public static class Program
    {
        public static void Main()
        {
            LoginForm newLoginForm = new LoginForm();
            newLoginForm.ShowDialog();
        }
    }
}
