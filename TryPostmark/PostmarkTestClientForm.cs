using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace TryPostmark
{
    public partial class PostmarkTestClientForm : Form
    {
        public PostmarkTestClientForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SendEmail() {
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                wc.Headers["X-Postmark-Server-Token"] = "72a4ecea-d586-418e-8ec1-79b6ba6a11c5";

                string fromAddr = "nithin.reddy@intertecsys.com";
                string toAddr = "nithin.reddy@intertecsys.com";
                string subject = "Test subject";
                string body = "Test mail body";
                string Data = "{" + string.Format("From: '{0}', To: '{1}', Subject: '{2}', TextBody: '{3}'", fromAddr, toAddr, subject, body) + "}";
                string url = "https://api.postmarkapp.com/email";

                wc.UploadString(url, Data);

                MessageBox.Show("Sent");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    //This gets the default proxy for the system along with the default credentials
                    IWebProxy defaultWebProxy = WebRequest.DefaultWebProxy;
                    defaultWebProxy.Credentials = CredentialCache.DefaultCredentials;
                    wc.Proxy = defaultWebProxy;

                    if (!string.IsNullOrEmpty(txtProxy.Text))
                    {
                        WebProxy webProxy = new WebProxy(txtProxy.Text);
                        if (ckDefaultCred.Checked)
                        {
                            webProxy.UseDefaultCredentials = true;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(txtUsername.Text))
                            {
                                webProxy.Credentials = new NetworkCredential(txtUsername.Text, txtPassword.Text);
                                webProxy.UseDefaultCredentials = false;
                            }
                        }
                        wc.Proxy = webProxy;
                    }

                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    wc.Headers["X-Postmark-Server-Token"] = "72a4ecea-d586-418e-8ec1-79b6ba6a11c5";

                    string fromAddr = txtFrom.Text;
                    string toAddr = txtTo.Text;
                    string subject = txtSubject.Text;
                    string body = txtBody.Text;
                    string Data = "{" + string.Format("From: '{0}', To: '{1}', Subject: '{2}', TextBody: '{3}'", fromAddr, toAddr, subject, body) + "}";
                    string url = "https://api.postmarkapp.com/email";

                    wc.UploadString(url, Data);

                    MessageBox.Show("Sent");
                }
            }
            catch (WebException ex)
            {
                using (var sr = new StreamReader(ex.Response.GetResponseStream()))
                {
                    var data = sr.ReadToEnd();
                    //throw new ApplicationException(data);
                    MessageBox.Show(data);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
