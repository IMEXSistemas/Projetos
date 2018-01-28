using System.Windows.Forms;
using System.Drawing;

namespace BMSworks.UI.BMSworks.UI
{
    public partial class passwordBox
    {

     private Form frm;
    public string Show(string prompt, string title)
    {
        frm = new Form();
        FlowLayoutPanel FL = new FlowLayoutPanel();
        Label lbl = new Label();
        TextBox txt = new TextBox();
        Button ok = new Button();

        frm.Font = new Font("Calibri", 9, FontStyle.Bold);
        frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        frm.StartPosition = FormStartPosition.CenterScreen;
        frm.Width = 200;
        frm.Height = 150;

        frm.Text = title;
        lbl.Text = prompt;
        ok.Text = "Ok";
        txt.PasswordChar = '*';

        ok.FlatStyle = FlatStyle.Flat;
        ok.BackColor = SystemColors.ButtonShadow;
        ok.ForeColor = SystemColors.ButtonHighlight;
        ok.Cursor = Cursors.Hand;      

        FL.Left = 0;
        FL.Top = 0;
        FL.Width = frm.Width;
        FL.Height = frm.Height;
        FL.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        FL.Padding = new Padding(10);
        FL.FlowDirection = FlowDirection.TopDown;

        ok.Width = FL.Width - 35;
        txt.Width = ok.Width;
       
        lbl.Width = ok.Width;

        ok.Click += new System.EventHandler(okClick);      
        txt.KeyPress += new KeyPressEventHandler(txtEnter);

        FL.Controls.Add(lbl);
        FL.Controls.Add(txt);
        FL.Controls.Add(ok);      
        frm.Controls.Add(FL);

        frm.ShowDialog();
        DialogResult DR = frm.DialogResult;
        frm.Dispose();
        frm = null;
        if (DR == DialogResult.OK)
        {
            return txt.Text;
        }
        else
        {
            return "";
        }
    }

    private void okClick(object sender, System.EventArgs e)
    {
        frm.DialogResult = DialogResult.OK;
        frm.Close();
    }
   
    private void txtEnter(object sender, KeyPressEventArgs e)
    {
        if (e.KeyChar == 13) { okClick(null, null); }
    }
}



}
