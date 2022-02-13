using System;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace DoAn_2.MenuTab
{
    public partial class BanHang : Form
    {
        IFirebaseConfig config = new FirebaseConfig {
            AuthSecret = "euXIwSf1qoXdcqULpMr2sGekOD3hC6UzPLUhUnbi",
            BasePath = "https://tsdv-oop-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public BanHang()
        {
            InitializeComponent();
        }


        private void BanHang_Load(object sender, EventArgs e) 
        {
            client = new FireSharp.FirebaseClient(config);
        }
      

        private async void buttonapply_Click_1(object sender, EventArgs e)
        {
            DataFB obj = new DataFB();
            try
            {
                FirebaseResponse getresponse = await client.GetTaskAsync("PointTable/" + textBoxID.Text);
                obj = getresponse.ResultAs<DataFB>();
            }
            catch(Exception exception){  }
            
                
            if (string.Equals(obj.PayState.ToString(), "OK"))
            {
                if (textBoxName.Text.Length != 0 && textBoxClass.Text.Length != 0 && textBoxID.Text.Length != 0
                && textBoxTel.Text.Length != 0 && textBoxEmail.Text.Length != 0)
                {
                    SetResponse res = client.Set(@"PointTable/" + textBoxID.Text + "/ID", textBoxID.Text);
                    SetResponse res2 = client.Set(@"PointTable/" + textBoxID.Text + "/name", textBoxName.Text);
                    SetResponse res3 = client.Set(@"PointTable/" + textBoxID.Text + "/SchoolClass", textBoxClass.Text);
                    SetResponse res4 = client.Set(@"PointTable/" + textBoxID.Text + "/Telephone", textBoxTel.Text);
                    SetResponse res5 = client.Set(@"PointTable/" + textBoxID.Text + "/Email", textBoxEmail.Text);
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
                    catch(Exception exception) {
                        MessageBox.Show("Failed");
                    }
                }
                else
                {
                    MessageBox.Show("Fill in all fields!");
                }
            }
            else
            {
                MessageBox.Show("Please pay the fee!");
            }
            
        }
    }
}
