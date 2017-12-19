using System.Net.Http;
using F4HApp.models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System;

namespace F4HApp.dataservice
{
    public class DSMaster
    {
        public async Task<string> GetCountryList()
        {
            string returnResult = "";

            try
            {
               
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                HttpResponseMessage response = await client.GetAsync("/f4hrestapi/api/Master/CountryList");
                
                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = ex.Message;
            }

            return returnResult;
        }

        public async Task<string> GetNotificationList()
        {
            string returnResult = "";

            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"LastLoginDate\" : \"" + App.LastLoginDate + "\", \"MemberID\" : \"" + App.MemberID + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/Master/NotificationList", content);

                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = ex.Message;
            }

            return returnResult;
        }

        public async Task<string> GetNotificationListForMember()
        {
            string returnResult = "";

            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"LastLoginDate\" : \"" + App.LastLoginDate + "\", \"MemberID\" : \"" + App.MemberID + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/Master/NotificationListForMembers", content);

                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = ex.Message;
            }

            return returnResult;
        }

        public async Task<string> GetMemberFFList(string FFCategory)
        {
            string returnResult = "";

            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberID\" : \"" + App.MemberID + "\", \"FFCategory\" : \"" + FFCategory + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/Master/MemberFFList", content);

                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = ex.Message;
            }

            return returnResult;
        }

        public async Task<string> SupportQuery(string MemberID, string CustomerQuery)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberID\" : \"" + MemberID + "\", \"CustomerQuery\" : \"" + CustomerQuery + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/master/NotificationAdd", content);

                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Tecnical Error\"}";
            }

            return returnResult;

        }

        public async Task<string> GetRedeemSchemeList()
        {
            string returnResult = "";

            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                HttpResponseMessage response = await client.GetAsync("/f4hrestapi/api/Master/RedeemSchemeList");

                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = ex.Message;
            }

            return returnResult;
        }
    }

    
}
