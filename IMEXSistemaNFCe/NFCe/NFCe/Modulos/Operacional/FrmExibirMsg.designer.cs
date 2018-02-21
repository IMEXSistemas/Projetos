namespace BmsSoftware.Modulos.Operacional
{
    partial class FrmExibirMsg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExibirMsg));
            this.lblmsg = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pictureBox1_Salvo = new System.Windows.Forms.PictureBox();
            this.pictureBox2_erro = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1_Salvo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2_erro)).BeginInit();
            this.SuspendLayout();
            // 
            // lblmsg
            // 
            this.lblmsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblmsg.AutoSize = true;
            this.lblmsg.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsg.ForeColor = System.Drawing.Color.White;
            this.lblmsg.Location = new System.Drawing.Point(26, 40);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(85, 16);
            this.lblmsg.TabIndex = 0;
            this.lblmsg.Text = "Mensagem";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(24, 5);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(101, 26);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Sucesso";
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // pictureBox1_Salvo
            // 
            this.pictureBox1_Salvo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1_Salvo.Image")));
            this.pictureBox1_Salvo.Location = new System.Drawing.Point(625, 5);
            this.pictureBox1_Salvo.Name = "pictureBox1_Salvo";
            this.pictureBox1_Salvo.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1_Salvo.TabIndex = 2;
            this.pictureBox1_Salvo.TabStop = false;
            this.pictureBox1_Salvo.Visible = false;
            this.pictureBox1_Salvo.Click += new System.EventHandler(this.pictureBox1_Salvo_Click);
            // 
            // pictureBox2_erro
            // 
            this.pictureBox2_erro.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2_erro.Image")));
            this.pictureBox2_erro.Location = new System.Drawing.Point(603, 5);
            this.pictureBox2_erro.Name = "pictureBox2_erro";
            this.pictureBox2_erro.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2_erro.TabIndex = 3;
            this.pictureBox2_erro.TabStop = false;
            this.pictureBox2_erro.Visible = false;
            this.pictureBox2_erro.Click += new System.EventHandler(this.pictureBox2_erro_Click);
            // 
            // FrmExibirMsg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(653, 73);
            this.Controls.Add(this.lblmsg);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pictureBox2_erro);
            this.Controls.Add(this.pictureBox1_Salvo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmExibirMsg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmExibirMsg_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmExibirMsg_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1_Salvo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2_erro)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.PictureBox pictureBox1_Salvo;
        private System.Windows.Forms.PictureBox pictureBox2_erro;

    }
}