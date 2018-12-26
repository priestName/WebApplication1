using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication3
{
    //[ComSourceInterfaces(typeof(IComEvents))]
    [Guid("30A3C1B8-9A9A-417A-9E87-80D0EE827658")]
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }
        public bool Gets()
        {
            return textBox1.Text == "123456";
        }
        //[Guid("FDA0A081-3D3B-4EAB-AE01-6A40FDDA9A60")]
        //[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
        //public interface IComEvents
        //{
        //    [DispId(0x00000001)]
        //    void OnNotify(string pwd);
        //}
    }
}
