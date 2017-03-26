using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Consumer;

namespace EquityMetrics.Retrieve {
   public class ETradeModel {

      OAuthRepository authRepository;
      IToken accessToken;
      OAuthSession session;

      string myConsumerKeyDev = "fc30012e9ff37fd786e935e84f1e413e";
      string myConsumerKeyProd = "c6154d11e1a0a23901d75e7aa1993085";
      string myConsumerKey;

      string myConsumerSecretDev = "3d310363092bae3e031d6f7b40a77aad";
      string myConsumerSecretProd = "f21344ab9d4837f17ef44befe889ca82";
      string myConsumerSecret;

      string accountListLinkProd = "https://etws.etrade.com/accounts/rest/accountlist";
      string accountListLinkDev = "https://etwssandbox.etrade.com/accounts/sandbox/rest/accountlist";
      string accountListLink;

      string accountBalanceLinkProd = "https://etws.etrade.com/accounts/rest/accountbalance/{0}";
      string accountBalanceLinkDev = "https://etwssandbox.etrade.com/accounts/sandbox/rest/accountbalance/{0}";
      string accountBalanceLink;

      string accountPositionsLinkProd = "https://etws.etrade.com/accounts/rest/accountpositions/{0}";
      string accountPositionsLinkDev = "https://etwssandbox.etrade.com/accounts/sandbox/rest/accountpositions/{0}";
      string accountPositionsLink;

      string accountAlertsLinkProd = "https://etws.etrade.com/accounts/rest/alerts";
      string accountAlertsLinkDev = "https://etwssandbox.etrade.com/accounts/sandbox/rest/alerts";
      string accountAlertsLink;

      string accountAlertDetailsLinkProd = "https://etws.etrade.com/accounts/rest/alerts/{0}";
      string accountAlertDetailsLinkDev = "https://etwssandbox.etrade.com/accounts/sandbox/rest/alerts/{0}";
      string accountAlertDetailsLink;

      string optionChainsLinkProd = "https://etws.etrade.com/market/rest/optionchains?expirationMonth={0}&expirationYear={1}&chainType={2}&skipAdjusted={3}&underlier={4}";
      string optionChainsLinkDev = "https://etwssandbox.etrade.com/market/sandbox/rest/optionchains?expirationMonth={0}&expirationYear={1}&chainType={2}&skipAdjusted={3}&underlier={4}";
      string optionChainsLink;

      string optionExpireDatesLinkProd = "https://etws.etrade.com/market/rest/optionexpiredate?underlier={0}";
      string optionExpireDatesLinkDev = "https://etwssandbox.etrade.com/market/sandbox/rest/optionexpiredate?underlier={0}";
      string optionExpireDatesLink;

      string productLookupLinkProd = "https://etws.etrade.com/market/rest/productlookup?company={0}&type={1}";
      string productLookupLinkDev = "https://etwssandbox.etrade.com/market/sandbox/rest/productlookup?company={0}&type={1}";
      string productLookupLink;

      string quoteLinkProd = "https://etws.etrade.com/market/rest/quote/{0}?detailFlag={1}";
      string quoteLinkDev = "https://etwssandbox.etrade.com/market/sandbox/rest/quote/{0}?detailFlag={1}";
      string quoteLink;

      public ETradeModel(bool Production) {
         SetProduction(Production);
         authRepository = initializeAuth(myConsumerKey, myConsumerSecret);
         session = initializeSession(authRepository);
         accessToken = initializeAccessToken(authRepository, session);
      }

      public string GetResponse(OAuthSession session, string url) {
         try {
            string response;
            try {
               response = session.Request(accessToken).Get().ForUrl(url).ToString();
            } catch (Exception E) {
               accessToken = renewAccessToken(authRepository, session);
               response = session.Request(accessToken).Get().ForUrl(url).ToString();
            }
            return response;
         } catch {
            return null;
         }
      }

      public XDocument GetWebResponseAsXml(HttpWebResponse response) {
         XmlReader xmlReader = XmlReader.Create(response.GetResponseStream());
         XDocument xdoc = XDocument.Load(xmlReader);
         xmlReader.Close();
         return xdoc;
      }

      public string GetWebResponseAsString(HttpWebResponse response) {
         Encoding enc = System.Text.Encoding.GetEncoding(1252);
         StreamReader loResponseStream = new
         StreamReader(response.GetResponseStream(), enc);         
         return loResponseStream.ReadToEnd();
      }

      // resource methods (not exhaustive)
      // each of these calls maps to an ETrade API resource

      //accounts

      public string GetAccountList() {
         return GetResponse(session, accountListLink);
      }

      public string GetAccountBalance(string AccountId) {
         return GetResponse(session, String.Format(accountBalanceLink, AccountId));
      }

      public string GetAccountPositions(string AccountId) {
         return GetResponse(session, String.Format(accountPositionsLink, AccountId));
      }

      public string GetAccountAlerts() {
         return GetResponse(session, accountAlertsLink);
      }

      public string GetAccountAlertDetails(string AlertId) {
         return GetResponse(session, String.Format(accountAlertDetailsLink, AlertId));
      }

      public string DeleteAccountAlert(string AlertId) {
         string response;
         try {
            response = session.Request(accessToken).Delete().ForUrl(String.Format(accountAlertDetailsLink, AlertId)).ToString();
         } catch {
            accessToken = renewAccessToken(authRepository, session);
            response = session.Request(accessToken).Delete().ForUrl(String.Format(accountAlertDetailsLink, AlertId)).ToString();
         }            //            return session.Request(accessToken).Delete().ForUrl(String.Format("https://etws.etrade.com/accounts/rest/alerts/{0}", AlertId)).ToString();
         return response;
      }

   //Markets

      public string GetOptionChains(string UnderLier, string ExpMonth, string ExpYear, string ChainType, string SkipAdjusted) {
         return GetResponse(session, String.Format(optionChainsLink, ExpMonth, ExpYear, ChainType, SkipAdjusted, UnderLier));
      }

      public string GetOptionExpireDates(string UnderLier) {
         return GetResponse(session, String.Format(optionExpireDatesLink, UnderLier));
      }

      public string GetProductLookup(string Company, string Type) {
         return GetResponse(session, String.Format(productLookupLink, Company, Type));
      }

      public string GetQuote(string SymbolList, string DetailFlag) {
         string URL = String.Format(quoteLink, SymbolList.Trim(), DetailFlag);
         Console.WriteLine(URL);
         return GetResponse(session, URL);
      }

      public void SetProduction(Boolean flag) {
         myConsumerKey = flag ? myConsumerKeyProd : myConsumerKeyDev;
         myConsumerSecret = flag ? myConsumerSecretProd : myConsumerSecretDev;         
         accountListLink = flag ? accountListLinkProd : accountListLinkDev;
         accountBalanceLink = flag ? accountBalanceLinkProd : accountBalanceLinkDev;
         accountPositionsLink = flag ? accountPositionsLinkProd : accountPositionsLinkDev;
         accountAlertsLink = flag ? accountAlertsLinkProd : accountAlertsLinkDev;
         accountAlertDetailsLink = flag ? accountAlertDetailsLinkProd : accountAlertDetailsLinkDev;
         optionChainsLink = flag ? optionChainsLinkProd : optionChainsLinkDev;
         optionExpireDatesLink = flag ? optionExpireDatesLinkProd : optionExpireDatesLinkDev;
         productLookupLink = flag ? productLookupLinkProd : productLookupLinkDev;
         quoteLink = flag ? quoteLinkProd : quoteLinkDev;
      }

      public OAuthSession initializeSession(OAuthRepository authRepository) {
         return authRepository.CreateSession();
      }

      public OAuthRepository initializeAuth(string consumerKey, string consumerSecret) {
         TokenBase token = new TokenBase { ConsumerKey = consumerKey };
         return new OAuthRepository(token, consumerSecret);
      }

      public IToken initializeAccessToken(OAuthRepository authRepository, OAuthSession session) {
         return authRepository.GetAccessTokenAuto(session);
      }

      public IToken renewAccessToken(OAuthRepository authRepository, OAuthSession session) {
         return authRepository.GetAccessTokenAuto(session);
      }
   }
}
