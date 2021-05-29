using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace service_login_popup
{
    public partial class Service1 : ServiceBase
    {   
        public Service1()
        {
            InitializeComponent();
            this.CanHandleSessionChangeEvent = true;
        }
        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSSendMessage(
                 IntPtr hServer,
                 [MarshalAs(UnmanagedType.I4)] int SessionId,
                 String pTitle,
                 [MarshalAs(UnmanagedType.U4)] int TitleLength,
                 String pMessage,
                 [MarshalAs(UnmanagedType.U4)] int MessageLength,
                 [MarshalAs(UnmanagedType.U4)] int Style,
                 [MarshalAs(UnmanagedType.U4)] int Timeout,
                 [MarshalAs(UnmanagedType.U4)] out int pResponse,
                 bool bWait);
        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            if (changeDescription.Reason == SessionChangeReason.SessionUnlock)
            {
                bool result = false;
                String title = "Attacker";
                int tlen = title.Length;
                String msg = "18520609";
                int mlen = msg.Length;
                int resp = 7;
                var WTS_CURRENT_SERVER_HANDLE = IntPtr.Zero;
                result = WTSSendMessage(WTS_CURRENT_SERVER_HANDLE, changeDescription.SessionId, title, tlen, msg, mlen, 4, 3, out resp, true);
            }
        }
        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
