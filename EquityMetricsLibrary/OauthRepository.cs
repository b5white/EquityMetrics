using System;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using System.Configuration;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Consumer;

namespace EquityMetrics.Retrieve {
   public class OAuthRepository {

      private static string REQUEST_URL = "https://etws.etrade.com/oauth/request_token";
      private static string AUTHORIZE_URL = "https://us.etrade.com/e/t/etws/authorize";
      private static string LOGON_URL = "https://us.etrade.com/home";
      private static string ACCESS_URL = "https://etws.etrade.com/oauth/access_token";
      private static string USERNAME = ite";
      private static string PASSWORD = "1q2w#E$R";

      private readonly TokenBase _tokenBase;
      private readonly string _consumerSecret;

      public OAuthRepository(TokenBase tokenBase,
                                    string consumerSecret) {
         _tokenBase = tokenBase;
         _consumerSecret = consumerSecret;
      }

      public TokenBase MyTokenBase {
         get { return _tokenBase; }
      }

      public string MyConsumerSecret {
         get { return _consumerSecret; }
      }

      public OAuthSession CreateSession() {
         var consumerContext = new OAuthConsumerContext {
            ConsumerKey = MyTokenBase.ConsumerKey,
            ConsumerSecret = MyConsumerSecret,
            SignatureMethod = SignatureMethod.HmacSha1,
            UseHeaderForOAuthParameters = true,
            //CallBack = "oob"
         };

         var session = new OAuthSession(consumerContext, REQUEST_URL, AUTHORIZE_URL, ACCESS_URL);
         return session;
      }

      public IToken GetAccessTokenAuto(OAuthSession session) {
         IToken requestToken = session.GetRequestToken();

         string authorizationLink = GetUserAuthorizationUrlForToken(requestToken, session.ConsumerContext.ConsumerKey);

         string pin = BrowserAuth.GetPin(USERNAME, PASSWORD, LOGON_URL, authorizationLink);

         IToken accessToken = session.ExchangeRequestTokenForAccessToken(requestToken, pin.ToUpper());

         return accessToken;
      }

      public IToken GetAccessTokenManual(OAuthSession session) {
         IToken requestToken = session.GetRequestToken();
         string authorizationLink = GetUserAuthorizationUrlForToken(requestToken, session.ConsumerContext.ConsumerKey);
         Console.WriteLine(authorizationLink);
         Process.Start(authorizationLink);

         Console.Write("Please enter pin from browser: ");
         string pin = Console.ReadLine();

         IToken accessToken = session.ExchangeRequestTokenForAccessToken(requestToken, pin.ToUpper());

         return accessToken;
      }

      private string GetUserAuthorizationUrlForToken(IToken token, string consumerKey) {
         string UserAuthorizeUri = "https://us.etrade.com/e/t/etws/authorize";
         return UserAuthorizeUri + "?key=" + consumerKey + "&token=" + token.Token;
      }

      private void AddUpdateAppSettings(string key, string value) {
         try {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null) {
               settings.Add(key, value);
            } else {
               settings[key].Value = value;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
         } catch (ConfigurationErrorsException) {
            Console.WriteLine("Error writing app settings");
         }
      }
   }
}
