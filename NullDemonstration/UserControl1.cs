using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NullDemonstration
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            //this.ParentForm.Focus();
        }
        //public override bool PreProcessMessage(ref Message msg)
        //{
        //    MessageBox.Show(msg.Msg.ToString());
        //    {
        //        //return false;
        //    }
        //    return base.PreProcessMessage(ref msg);
        //}
    }
}