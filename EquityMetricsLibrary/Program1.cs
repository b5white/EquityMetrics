using System;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using System.Data;

namespace EtradeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string myConsumerKey = "fc30012e9ff37fd786e935e84f1e413e";
            string myConsumerSecret = "3d310363092bae3e031d6f7b40a77aad";

            TokenBase token = new TokenBase { ConsumerKey = myConsumerKey };
            BaseOAuthRepository rep = new BaseOAuthRepository(token, myConsumerSecret);
            OAuthSession session = rep.CreateSession();
            IToken accessToken = rep.GetAccessToken(session);

            ETradeModel etm = new ETradeModel(session, accessToken);

            etm.SetProduction(false);
            // make a request for data from ETrade
            string responseText = etm.GetQuote("ESPN", "ALL");

            Console.WriteLine("Response Text: " + responseText + ";");

            Console.ReadKey();
        }
    }
}
