using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace db_listview
{
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
            database_funcs.InitialiseLV(lv_main);
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm_insert = new frm_insertupdate(frm_insertupdate.ActionType.Insert);
            if (frm_insert.ShowDialog() != DialogResult.OK)
                return;
            (long, string, string) add_result = database_funcs.AddUser(
                frm_insert.Login, 
                frm_insert.Password, 
                frm_insert.Reg_date);
            var lvi = new ListViewItem(new[]
                    {
                        frm_insert.Login,
                        add_result.Item2,
                        add_result.Item3,
                        ((DateTime) frm_insert.Reg_date).ToLongDateString()
                    })
            {
                Tag = (add_result.Item1, frm_insert.Reg_date)
            };
            lv_main.Items.Add(lvi);
        }
    }
}
