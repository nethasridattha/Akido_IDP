//using DTPortal.IDP.ViewModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DTPortal.IDP.Utilities
//{
//    public class ValidateSession
//    {
//        public static APIResponse Validate(string MethodName)
//        {
//            var response = new APIResponse();
//            using (var client = new HttpClient())
//            {
//                client.BaseAddress = new Uri("https://localhost:44395/api/Authentication/");
//                //HTTP POST
//                var postTask = client.PostAsJsonAsync<T>(MethodName, data);
//                postTask.Wait();

//                var result = postTask.Result;
//                if (result.IsSuccessStatusCode)
//                {
//                    var readTask = result.Content.ReadAsStringAsync();
//                    readTask.Wait();

//                    response = JsonConvert.DeserializeObject<APIResponse>(readTask.Result);
//                    return response;
//                }
//            }
//            return response;
//        }
//    }

//}
