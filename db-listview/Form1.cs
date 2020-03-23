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
            try
            {
                database_funcs.InitialiseLV(lv_main);
            }
            catch
            {
                MessageBox.Show("Ой! Что-то пошло не так! Попробуйте снова! (｡╯3╰｡)");
                return;
            }
        }
        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm_insert = new frm_insertupdate(frm_insertupdate.ActionType.Insert);
            if (frm_insert.ShowDialog() != DialogResult.OK)
                return;
            (long, string, string) add_result = (0, "", "");
            try
            {
                add_result = database_funcs.AddUser(
                frm_insert.Login,
                frm_insert.Password,
                frm_insert.Reg_date);
            }
            catch
            {
                MessageBox.Show("Ой! Что-то пошло не так! Попробуйте снова! (｡╯3╰｡)");
                return;
            }
            var lvi = new ListViewItem(new[]
                    {
                        frm_insert.Login,
                        add_result.Item2,
                        add_result.Item3,
                        ((DateTime) frm_insert.Reg_date).ToLongDateString()
                    })
            {
                Tag = Tuple.Create(add_result.Item1, frm_insert.Reg_date)
            };
            lv_main.Items.Add(lvi);
        }
        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem curr_user in lv_main.SelectedItems)
            {
                var curr_tag = (Tuple<long, DateTime>)curr_user.Tag;
                long user_id = curr_tag.Item1;
                DateTime reg_date = curr_tag.Item2;
                var frm_insert = new frm_insertupdate(frm_insertupdate.ActionType.Update);
                frm_insert.Login = frm_insert.Old_login = curr_user.SubItems[0].Text;
                frm_insert.Reg_date = reg_date;
                if (frm_insert.ShowDialog() != DialogResult.OK)
                    continue;
                (string, string) hash_ahd_salt = ("", "");
                try
                {
                    hash_ahd_salt = database_funcs.UpdateUser(user_id,
                    frm_insert.Login,
                    frm_insert.Password,
                    frm_insert.Reg_date);
                }
                catch
                {
                    MessageBox.Show("Ой! Что-то пошло не так! Попробуйте снова! (｡╯3╰｡)");
                    return;
                }
                curr_user.SubItems[0].Text = frm_insert.Login;
                if (hash_ahd_salt.Item1 != "")
                {
                    curr_user.SubItems[1].Text = hash_ahd_salt.Item1;
                    curr_user.SubItems[2].Text = hash_ahd_salt.Item2;
                }
                curr_user.SubItems[3].Text = frm_insert.Reg_date.ToLongDateString();
                curr_user.Tag = Tuple.Create(user_id, frm_insert.Reg_date);
                }
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lv_main.SelectedItems.Count <= 0)
                return;
            var frm_delete = new frm_confirm_delete();
            if (frm_delete.ShowDialog() != DialogResult.OK)
                return;
            long[] id_for_delete = lv_main.SelectedItems.Cast<ListViewItem>()
                .Select(x => ((Tuple<long, DateTime>)x.Tag).Item1).ToArray();
            try
            {
                database_funcs.DeleteUsers(id_for_delete);
            }
            catch
            {
                MessageBox.Show("Ой! Что-то пошло не так! Попробуйте снова! (｡╯3╰｡)");
                return;
            }
            foreach (ListViewItem curr_user in lv_main.SelectedItems)
                lv_main.Items.Remove(curr_user);
        }
    }
}
