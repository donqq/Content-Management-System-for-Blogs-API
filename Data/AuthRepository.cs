/* Copyright (C) 2021/January Badde Liyanage Don Dilanga ( github@dilanga.com ) - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the Simple non code license (SNCL)
 * https://tldrlegal.com/license/simple-non-code-license-(sncl)
 * You should have received a copy of the Simple non code license (SNCL) with
 * this file. If not, please write to: github@dilanga.com , or visit : https://github.com/donqq/Content-Management-System-for-Blogs
 */

using System.Net.Http;
using System.Threading.Tasks;

namespace contentMngApp.api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IHttpClientFactory _clientfactory;

        private readonly string _url = "https://Firebase_Project_ID.firebaseio.com/users/UID/userid.json?auth=TOKEN";

        public AuthRepository(IHttpClientFactory clientfactory) {
            _clientfactory = clientfactory;
        }


        public async Task<bool> UserExist(string uid, string token) {
            
            if (uid.Length <= 0 || token.Length <= 0) {
                return false;
            }

            try {
                using (HttpClient client = _clientfactory.CreateClient()) {
                    string url = _url.Replace("UID", uid).Replace("TOKEN", token);

                    using (HttpResponseMessage response = await client.GetAsync(url)) {
                        response.EnsureSuccessStatusCode();
                        using (HttpContent content = response.Content) {
                            string value = await content.ReadAsStringAsync(); // it reads as "\"UID\""
                            return true;
                        }
                    }
                }
            } catch (HttpRequestException error) {
                return false;
            }
        }
    }
}