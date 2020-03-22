namespace db_listview
{
    partial class frm_confirm_delete
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_confirm = new System.Windows.Forms.Label();
            this.btn_confirm_ok = new System.Windows.Forms.Button();
            this.btn_confirm_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_confirm
            // 
            this.lbl_confirm.AutoSize = true;
            this.lbl_confirm.Location = new System.Drawing.Point(109, 51);
            this.lbl_confirm.Name = "lbl_confirm";
            this.lbl_confirm.Size = new System.Drawing.Size(95, 17);
            this.lbl_confirm.TabIndex = 0;
            this.lbl_confirm.Text = "Вы уверены?";
            // 
            // btn_confirm_ok
            // 
            this.btn_confirm_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_confirm_ok.Location = new System.Drawing.Point(48, 98);
            this.btn_confirm_ok.Name = "btn_confirm_ok";
            this.btn_confirm_ok.Size = new System.Drawing.Size(105, 51);
            this.btn_confirm_ok.TabIndex = 1;
            this.btn_confirm_ok.Text = "Да";
            this.btn_confirm_ok.UseVisualStyleBackColor = true;
            // 
            // btn_confirm_cancel
            // 
            this.btn_confirm_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_confirm_cancel.Location = new System.Drawing.Point(159, 98);
            this.btn_confirm_cancel.Name = "btn_confirm_cancel";
            this.btn_confirm_cancel.Size = new System.Drawing.Size(105, 51);
            this.btn_confirm_cancel.TabIndex = 2;
            this.btn_confirm_cancel.Text = "Нет";
            this.btn_confirm_cancel.UseVisualStyleBackColor = true;
            // 
            // frm_confirm_delete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 201);
            this.Controls.Add(this.btn_confirm_cancel);
            this.Controls.Add(this.btn_confirm_ok);
            this.Controls.Add(this.lbl_confirm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_confirm_delete";
            this.Text = "frm_confirm_delete";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_confirm;
        private System.Windows.Forms.Button btn_confirm_ok;
        private System.Windows.Forms.Button btn_confirm_cancel;
    }
}