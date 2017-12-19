using System.Net.Http;
using F4HApp.models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System;

namespace F4HApp.dataservice
{
    public class DSTransactions
    {

        public async Task<string> GetMemberPoints(string MemberID)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberID\" : \"" + MemberID + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/transaction/MemberPoints", content);
               
                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Tecnical Error\"}";
            }

            return returnResult;

        }

        public async Task<string> EarnPoints(string MemberID, string ScratchCode)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberID\" : \"" + MemberID + "\", \"ScratchCode\" : \"" + ScratchCode + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/transaction/EarnTransaction", content);

                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Tecnical Error\", \"Points\" : \"0\"}";
            }

            return returnResult;

        }

        public async Task<string> TransferPoints(string MemberID, string ToMemberID, int Points)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberID\" : \"" + MemberID + "\", \"ToMemberID\" : \"" + ToMemberID + "\", \"Points\" : \"" + Points + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/transaction/TransferTransaction", content);

                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Tecnical Error\", \"Points\" : \"0\"}";
            }

            return returnResult;

        }

        public async Task<string> RedeemPoints(string MemberID, string EPointID, int Points)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberID\" : \"" + MemberID + "\", \"EPointID\" : \"" + EPointID + "\", \"Points\" : \"" + Points + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/transaction/RedeemTransaction", content);

                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Tecnical Error\"}";
            }

            return returnResult;

        }

        public async Task<string> AllPointsList(string MemberID, string Mode)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberID\" : \"" + MemberID + "\",\"Mode\" : \"" + Mode + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/transaction/AllPointList", content);

                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Tecnical Error\", \"Points\" : \"0\"}";
            }

            return returnResult;

        }

        public async Task<string> EarnPointsList(string MemberID)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberID\" : \"" + MemberID + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/transaction/EarnPointList", content);

                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Tecnical Error\", \"Points\" : \"0\"}";
            }

            return returnResult;

        }

        public async Task<string> RedeemPointsList(string MemberID)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberID\" : \"" + MemberID + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/transaction/RedeemPointList", content);

                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Tecnical Error\", \"Points\" : \"0\"}";
            }

            return returnResult;

        }

        public async Task<string> TransferPointsList(string MemberID)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberID\" : \"" + MemberID + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/transaction/TransferedPointList", content);

                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Tecnical Error\", \"Points\" : \"0\"}";
            }

            return returnResult;

        }

    }
}
