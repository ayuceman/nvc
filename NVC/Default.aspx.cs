using ActiveUp.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        public static Imap4Client imap;
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (imap == null)
                {
                    imap = new Imap4Client();
                    imap.ConnectSsl("imap.gmail.com", 993);
                    imap.Login(@"dhammashringaxml@gmail.com", @"dhamma@xml");

                    imap.Command("capability");
                    imap.LingerState.Enabled = true;
                    imap.LingerState.LingerTime = 2000;


                }
                Mailbox inbox = imap.SelectMailbox("Applications");
                int[] ids = inbox.Search("UNSEEN");
                lblNo.Text = ids.Count().ToString();
            }
            catch (Exception ex)
            {

            }

        }

        public void initNotify(string StrSplash)
        {
            // Only do this on the first call to the page
            if ((!IsCallback) && (!IsPostBack))
            {
                //Register loadingNotifier.js for showing the Progress Bar
                Response.Write(string.Format(@"<script type='text/javascript'  src='Scripts/loadingNotifier.js'></script><script language='javascript' type='text/javascript'>initLoader('{0}');</script>", StrSplash));
                // Send it to the client
                Response.Flush();

            }

        }

        public void Notify(string strPercent, string strMessage)
        {
            // Only do this on the first call to the page
            if ((!IsCallback) && (!IsPostBack))
            {
                //Update the Progress bar

                Response.Write(string.Format(@"<script language='javascript' type" + "='text/javascript'>setProgress({0},'{1}'); </script>", strPercent, strMessage));
                Response.Flush();
            }
        }
    }
}