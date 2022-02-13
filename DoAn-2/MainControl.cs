using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.Runtime.InteropServices;
using DoAn_2.MenuTab;
using System.Data.SqlClient;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace DoAn_2
{
    public partial class MainControl : Form
    {
        private IconButton currentbtn;
        private Panel lefborderbtn;
        private Form currentchildform;
        public MainControl()
        {
            InitializeComponent();
            lefborderbtn = new Panel();
            lefborderbtn.Size = new Size(7, 50);
            PanelMenu.Controls.Add(lefborderbtn);
            timer1.Start();//dong ho
        }

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172,126,241);
            public static Color color2 = Color.FromArgb(249,118,176);
            public static Color color3 = Color.FromArgb(253,138,114);
            public static Color color4 = Color.FromArgb(95,77,221);
            public static Color color5 = Color.FromArgb(249,88,155);
            public static Color color6 = Color.FromArgb(24,161,251);
            public static Color color7 = Color.FromArgb(145,151,154);

        }

        private void activebtn(object senderbtn, Color color)
        {
            if(senderbtn != null)
            {
                disablebtn();
                //button
                currentbtn = (IconButton)senderbtn;
                currentbtn.BackColor = Color.FromArgb(37,36,81);
                currentbtn.ForeColor = color;
                currentbtn.TextAlign = ContentAlignment.MiddleCenter;
                currentbtn.IconColor = color;
                currentbtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentbtn.ImageAlign = ContentAlignment.MiddleRight;

                //left border btn
                lefborderbtn.BackColor = color;
                lefborderbtn.Location = new Point(0,currentbtn.Location.Y);
                lefborderbtn.Visible = true;
                lefborderbtn.BringToFront();

                //icon small          
                iconmenusmall.IconColor = color;

            }
        }

        private void disablebtn()
        {
            if(currentbtn != null)
            {
                //button
                currentbtn.BackColor = Color.FromArgb(34, 36, 49);
                currentbtn.ForeColor = Color.Gainsboro;
                currentbtn.TextAlign = ContentAlignment.MiddleLeft;
                currentbtn.IconColor = Color.Gainsboro;
                currentbtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentbtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void motrangcon(Form trangcon)
        {
            if (currentchildform != null)
            {
                currentchildform.Close();
            }
            currentchildform = trangcon;
            trangcon.TopLevel = false;
            trangcon.FormBorderStyle = FormBorderStyle.None;
            trangcon.Dock = DockStyle.Fill;
            paneltrangcon.Controls.Add(trangcon);
            paneltrangcon.Tag = trangcon;
            trangcon.BringToFront();
            trangcon.Show();
        }
        private void btnhome_Click(object sender, EventArgs e)
        {
            activebtn(sender, RGBColors.color1);
            motrangcon(new BanHang());
            labelcon.Text = "Bảng điểm";
            iconmenusmall.IconChar = IconChar.Trophy;
        }

        private void btnorders_Click(object sender, EventArgs e)
        {
            activebtn(sender, RGBColors.color2);
            motrangcon(new donhang());
            labelcon.Text = "Phúc tra";
            iconmenusmall.IconChar = IconChar.Check;
        }

        private void btnsetting_Click(object sender, EventArgs e)
        {
            activebtn(sender, RGBColors.color3);
            motrangcon(new Setting());
            labelcon.Text = "Đăng kí TN";
            iconmenusmall.IconChar = IconChar.Rocket;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.lbDateTime.Text = datetime.ToString("dd/MM/yyyy HH:mm:ss");
            this.labelGioBig.Text = datetime.ToString("HH:mm:ss");
            this.lbNgayThangBig.Text = datetime.ToString("dd/MM/yyyy");
            this.lbDateBig.Text = datetime.ToString("dddd");
        }
        private void Reset()
        {
            disablebtn();
            lefborderbtn.Visible = false;
            iconmenusmall.IconChar = IconChar.Home;
            iconmenusmall.IconColor = Color.Gainsboro;
            labelcon.Text = "Home";
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam );

        private void panelTitlebar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void picLogo_Click(object sender, EventArgs e)
        {
            currentchildform.Close();
            Reset();
        }

        private void iconExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void iconMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void iconZoom_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }

        }

        private void MainControl_Load(object sender, EventArgs e)
        {
        }
       
        private void paneltrangcon_Paint(object sender, PaintEventArgs e)
        {
        }
      
        private void PanelMenu_Paint(object sender, PaintEventArgs e)
        {
        }

        private void lbDateBig_Click(object sender, EventArgs e)
        {
        }
    }
}
