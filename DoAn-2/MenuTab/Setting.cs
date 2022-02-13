using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Net.Mail;
using System.Net;

namespace DoAn_2.MenuTab
{
    public partial class Setting : Form
    {
      
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "euXIwSf1qoXdcqULpMr2sGekOD3hC6UzPLUhUnbi",
            BasePath = "https://tsdv-oop-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public Setting()
        {
            InitializeComponent();
            string[] School = { "SET", "SoICT", "SME", "SEE", "SOFT", "TEX", "SAMI", "SEP" };
            comboBoxSchool.Items.AddRange(School);
        }
      
        
        private void Setting_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

        }

        private async void buttonapply_Click(object sender, EventArgs e)
        {
            DataFB obj = new DataFB();
            try
            {
                FirebaseResponse getresponse = await client.GetTaskAsync("RegisExpClass/" + textBoxID.Text);
                obj = getresponse.ResultAs<DataFB>();
            }
            catch (Exception exception) { }


            if (string.Equals(obj.PayState.ToString(), "OK"))
            {
                if (textBoxName.Text.Length != 0 && textBoxClass.Text.Length != 0 && textBoxID.Text.Length != 0
              && textBoxExpClassCode.Text.Length != 0 && textBoxSubCode.Text.Length != 0 && textBoxCurClassCode.Text.Length != 0
              && textBoxEmail.Text.Length != 0 && textBoxTel.Text.Length != 0 && comboBoxSchool.Text.Length != 0)
                {
                    SetResponse res1 = client.Set(@"RegisExpClass/" + textBoxID.Text + "/ID", textBoxID.Text);
                    SetResponse res2 = client.Set(@"RegisExpClass/" + textBoxID.Text + "/name", textBoxName.Text);
                    SetResponse res3 = client.Set(@"RegisExpClass/" + textBoxID.Text + "/SchoolClass", textBoxClass.Text);
                    SetResponse res4 = client.Set(@"RegisExpClass/" + textBoxID.Text + "/ExpClass", textBoxExpClassCode.Text);
                    SetResponse res5 = client.Set(@"RegisExpClass/" + textBoxID.Text + "/Subject", textBoxSubCode.Text);
                    SetResponse res6 = client.Set(@"RegisExpClass/" + textBoxID.Text + "/CurClass", textBoxCurClassCode.Text);
                    SetResponse res7 = client.Set(@"RegisExpClass/" + textBoxID.Text + "/Email", textBoxEmail.Text);
                    SetResponse res8 = client.Set(@"RegisExpClass/" + textBoxID.Text + "/Telephone", textBoxTel.Text);
                    SetResponse res9 = client.Set(@"RegisExpClass/" + textBoxID.Text + "/School", comboBoxSchool.Text);
                    MessageBox.Show("Successful request");
                    MailMessage message = new MailMessage();
                    message.To.Add(textBoxEmail.Text);
                    message.From = new MailAddress("phamphung169@gmail.com");
                    message.Body = "Your request was successfull!";
                    message.Subject = "HUST SmartPay";
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                    smtp.Port = 587;
                    smtp.Credentials = new NetworkCredential("phamphung169@gmail.com", "Phung1692K");
                    smtp.EnableSsl = true;
                    try
                    {
                        smtp.Send(message);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Failed");
                    }
                }
                else
                {
                    MessageBox.Show("Fill in all fields!");
                }
            }
        }

      
    }
}
