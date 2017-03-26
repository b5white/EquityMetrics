using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquityMetrics.Utilities;
using EquityMetrics.Model;

namespace EquityMetrics.Retrieve
{
    class Program
    {
        static Messages messages;

        [STAThread]
        static void Main(string[] args)
        {
            ETradeController controller = new ETradeController();
            messages = Messages.Instance; // Singleton reference to the Messages class. This contains the shared event
                                          // and messages Queue.

            // Subscribe to the message event. This will allow the form to be notified whenever there's a new message.
            //
            messages.HandleMessage += new EventHandler(OnHandleMessage);
            
            while (Console.KeyAvailable == false) {
                System.Threading.Thread.Sleep(1000);  // Loop until input is entered.
                controller.RetrieveQuotes(5);              
            }
        }

        /// <summary>
        /// Called when [handle message].
        /// This is called whenever a new message has been added to the "central" Queue.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        static public void OnHandleMessage(object sender, EventArgs args)
        {
            string msg;
            msg = messages.GetMessage();
            while ((msg != null) && (msg != "")) {
                Console.WriteLine(DateTime.Now.ToString() + " -  " + msg);
                msg = messages.GetMessage();
            }
        }
    }
}
