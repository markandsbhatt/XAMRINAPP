using System.Net.Http;
using F4HApp.models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System;

namespace F4HApp.dataservice
{

    public class DSLogin
    {
        //HttpClient client = new HttpClient();
        public DSLogin()
        {
        }

        public async Task<string> ChkLogin(string MobileNo, string Password)
        {
            string returnResult = "";

            try
            {
                //var content = new StringContent(JsonConvert.SerializeObject(new { UserName = MobileNo, Password = Password }));

                //var response = await client.PostAsync("http://www.khubisolutions.com/f4hrestapi/api/home/login", content);

                //var todoItems = JsonConvert.DeserializeObject<string>(response);
                //return response.ToString();

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"UserName\" : \"" + MobileNo + "\", \"Password\" : \""+ Password + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/home/login", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch(Exception ex)
            {
                returnResult = ex.Message;
            }

            return returnResult;
        }

        public async Task<string> ChkMobileExists(string MobileNo)
        {
            string returnResult = "";

            try
            {
              
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MobileNo\" : \"" + MobileNo + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/home/CheckMobile", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = ex.Message;
            }

            return returnResult;
        }

        public async Task<string> Registration(string Title, string FName, string LName, string YourDOB, string MobileNo, string EmailID, string Password)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"Title\" : \"" + Title + "\", \"FName\" : \"" + FName + "\", \"LName\" : \"" + LName + "\", \"YourDOB\" : \"" + YourDOB + "\", \"MobileNo\" : \"" + MobileNo + "\", \"EmailID\" : \"" + EmailID + "\", \"UserName\" : \"" + MobileNo + "\", \"UserPass\" : \"" + Password + "\", \"Source\" : \"Mobile App\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/home/Register", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Tecnical Error\"}";
            }

            return returnResult;

        }

        public async Task<string> GetMemberDetails(string MemberID)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberID\" : \"" + MemberID + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/home/GetMemberDetails", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Technical Error\"}";
            }

            return returnResult;

        }

        public async Task<string> UpdateMemberDetails(string MemberID, string AddLine1, string AddLine2, string AddLine3, string CityTown, string StateArea, string PostCode, string Country)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberID\" : \"" + MemberID + "\", \"AddLine1\" : \"" + AddLine1 + "\", \"AddLine2\" : \"" + AddLine2 + "\", \"AddLine3\" : \"" + AddLine3 + "\", \"CityTown\" : \"" + CityTown + "\", \"StateArea\" : \"" + StateArea + "\", \"PostCode\" : \"" + PostCode + "\", \"Country\" : \"" + Country + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/home/UpdateAddress", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Technical Error\"}";
            }

            return returnResult;

        }

        public async Task<string> UpdateMemberPassword(string MemberID, string OldPassword, string NewPassword)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberID\" : \"" + MemberID + "\", \"OldPassword\" : \"" + OldPassword + "\", \"NewPassword\" : \"" + NewPassword + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/home/UpdatePassword", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Technical Error\"}";
            }

            return returnResult;

        }

        public async Task<string> ResetMemberPassword(string MobileNo, string NewPassword)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"UserName\" : \"" + MobileNo + "\", \"Password\" : \"" + NewPassword + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/home/ResetPass", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Technical Error\"}";
            }

            return returnResult;

        }

        public async Task<string> MemberFF(string MemberID, string FName, string LName, string MobileNo, string FFCategory)
        {
            string returnResult = "";

            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberID\" : \"" + MemberID + "\", \"FName\" : \"" + FName + "\", \"LName\" : \"" + LName + "\", \"MobileNo\" : \"" + MobileNo + "\", \"FFCategory\" : \"" + FFCategory + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/home/MemberFF", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = "{\"Status\" : \"Fail\", \"Msg\" : \"Tecnical Error\"}";
            }

            return returnResult;

        }

        //For About US
        public async Task<string> GetAboutUS(string Status)
        {
            string returnResult = "";
             Status = "AboutUs";
            try
            {
                //var content = new StringContent(JsonConvert.SerializeObject(new { UserName = MobileNo, Password = Password }));

                //var response = await client.PostAsync("http://www.khubisolutions.com/f4hrestapi/api/home/login", content);

                //var todoItems = JsonConvert.DeserializeObject<string>(response);
                //return response.ToString();

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"Status\" : \"" + Status + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/transaction/AboutUS", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = ex.Message;
            }

            return returnResult;
        }


        public async Task<string> GetAboutUSPatners(string Status)
        {
            string returnResult = "";
            Status = "AboutUs";
            try
            {
                //var content = new StringContent(JsonConvert.SerializeObject(new { UserName = MobileNo, Password = Password }));

                //var response = await client.PostAsync("http://www.khubisolutions.com/f4hrestapi/api/home/login", content);

                //var todoItems = JsonConvert.DeserializeObject<string>(response);
                //return response.ToString();

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"Status\" : \"" + Status + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/transaction/AboutPatners", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = ex.Message;
            }

            return returnResult;
        }

        public async Task<string> GetAboutUSPlans(string Status)
        {
            string returnResult = "";
            Status = "AboutUs";
            try
            {
                //var content = new StringContent(JsonConvert.SerializeObject(new { UserName = MobileNo, Password = Password }));

                //var response = await client.PostAsync("http://www.khubisolutions.com/f4hrestapi/api/home/login", content);

                //var todoItems = JsonConvert.DeserializeObject<string>(response);
                //return response.ToString();

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"Status\" : \"" + Status + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/transaction/AboutHelathPlan", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
                var result = await response.Content.ReadAsStringAsync();
                returnResult = result;

            }
            catch (Exception ex)
            {
                returnResult = ex.Message;
            }

            return returnResult;
        }


        public async Task<string> SendSms(string MobileNo,string Message)
        {
            string returnResult = "";

            try
            {

                var client = new HttpClient();
                client.BaseAddress = new Uri("http://www.khubisolutions.com");

                string jsonData = "{\"MemberNumber\" : \"" + MobileNo + "\", \"Message\" : \"" + Message + "\"}";

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("/f4hrestapi/api/transaction/SendSMS", content);

                // this result string should be something like: "{"token":"rgh2ghgdsfds"}"
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
