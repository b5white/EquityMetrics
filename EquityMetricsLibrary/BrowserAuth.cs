using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WatiN.Core; // IE Automation

namespace EquityMetrics.Retrieve {
   class StaticBrowserInstanceHelper<T> where T : Browser {
      private Browser _browser;
      private int _browserThread;
      private string _browserHwnd;

      public Browser Browser {
         get {
            int currentThreadId = GetCurrentThreadId();
            if (currentThreadId != _browserThread) {
               _browser = Browser.AttachTo<T>(Find.By("hwnd", _browserHwnd));
               _browserThread = currentThreadId;
            }
            return _browser;
         }
         set {
            _browser = value;
            _browserHwnd = _browser.hWnd.ToString();
            _browserThread = GetCurrentThreadId();
         }
      }

      private int GetCurrentThreadId() {
         return Thread.CurrentThread.GetHashCode();
      }
   }

   static class BrowserAuth {

      static public string GetPin(string username, string password, string logonLink, string authLink) {
         // Settings.Instance.MakeNewIeInstanceVisible = false;

         var StaticInstanceHelper = new StaticBrowserInstanceHelper<IE>();
         Settings.Instance.AutoStartDialogWatcher = false;

         // This code doesn't always handle it well when IE is already running, but it won't be in my case. You may need to attach to existing, depending on your context. 
         try {
            StaticInstanceHelper.Browser = new IE(logonLink);
         } catch (WatiN.Core.Exceptions.TimeoutException) {
            StaticInstanceHelper.Browser = new IE(logonLink); // try again
         }
         string authCode = "";

         // Browser reference was failing because IE hadn't started up yet.
         // I'm in the background, so I don't care how long it takes.
         // You may want to do a WaitFor to make it snappier.
         Thread.Sleep(5000);
         if (StaticInstanceHelper.Browser.ContainsText("Scheduled System Maintenance")) {
            throw new ApplicationException("eTrade down for maintenance.");
         }

         TextField user = StaticInstanceHelper.Browser.TextField(Find.ByName("USER"));

         TextField pass2 = StaticInstanceHelper.Browser.TextField(Find.ByName("PASSWORD"));

         // Class names of the Logon and Logoff buttons vary by page, so I find by text. Seems likely to be more stable.
         Button btnLogOn = StaticInstanceHelper.Browser.Button(Find.ByText("Log On"));
         Element btnLogOff = StaticInstanceHelper.Browser.Element(Find.ByText("Log Off"));
         Button btnAccept = StaticInstanceHelper.Browser.Button(Find.ByValue("Accept"));

         TextField authCodeBox = StaticInstanceHelper.Browser.TextField(Find.First());

         if (user != null && btnLogOn != null &&
             user.Exists && pass2.Exists && btnLogOn.Exists) {
            user.Value = username;
            pass2.Value = password;
            btnLogOn.Click();
         }

         Thread.Sleep(1000);
         if (StaticInstanceHelper.Browser.ContainsText("Scheduled System Maintenance")) {
            Element btnContinue = StaticInstanceHelper.Browser.Element(Find.ByName("continueButton"));
            if (btnContinue.Exists)
               btnContinue.Click();
         }
         btnLogOff.WaitUntilExists(30);

         // Here we go, finally.
         StaticInstanceHelper.Browser.GoTo(authLink);
         btnAccept.WaitUntilExists(30);
         btnAccept.Click();

         authCodeBox.WaitUntilExists(30);
         authCode = authCodeBox.Value;
         StaticInstanceHelper.Browser.Close();

         return authCode;
      }
   }
}
