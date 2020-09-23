using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Model
{
    public static class Extend
    {

        public static void AppendLineText(this TextBox txtBox,string text)
        {
            txtBox.AppendText(text + Environment.NewLine);
        }

    }
}
