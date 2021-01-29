/* Copyright (C) 2021/January Badde Liyanage Don Dilanga ( github@dilanga.com ) - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the Simple non code license (SNCL)
 * https://tldrlegal.com/license/simple-non-code-license-(sncl)
 * You should have received a copy of the Simple non code license (SNCL) with
 * this file. If not, please write to: github@dilanga.com , or visit : https://github.com/donqq/Content-Management-System-for-Blogs
 */

using System.Text.Json;
using contentMngApp.api.DataTransferObject;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace contentMngApp.api.Data
{
    public class PlagiarismChecking : IPlagiarismChecking
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string url = "https://www.copyscape.com/api/";

        public PlagiarismChecking(IHttpClientFactory clientFactory) {
            _clientFactory = clientFactory;
        }

        public async Task<string> plagiarismResult(string content, string depth) {

            if (content.Length <= 0 || depth.Length <= 0) {
                return "Neither content nor depth can be blank";
            }

            try {
                PlagiarismCheckingMeta jsonContent = new PlagiarismCheckingMeta {
                    t = content,
                    c = depth
                };

                List<KeyValuePair<string, string>> Postbody = new List<KeyValuePair<string, string>>();
                Postbody.Add(new KeyValuePair<string, string>("e", jsonContent.e));
                Postbody.Add(new KeyValuePair<string, string>("f" ,jsonContent.f));
                Postbody.Add(new KeyValuePair<string, string>("k" ,jsonContent.k));
                Postbody.Add(new KeyValuePair<string, string>("o" ,jsonContent.o));
                Postbody.Add(new KeyValuePair<string, string>("t" ,jsonContent.t));
                Postbody.Add(new KeyValuePair<string, string>("u" ,jsonContent.u));
                Postbody.Add(new KeyValuePair<string, string>("c" ,jsonContent.c));

                FormUrlEncodedContent contentBody = new FormUrlEncodedContent(Postbody);

                using (HttpClient client = _clientFactory.CreateClient()) {
                    using (HttpResponseMessage response = await client.PostAsync(url, contentBody)) {
                        using(HttpContent xmlcontent = response.Content) {
                            string value = await xmlcontent.ReadAsStringAsync();
                            return value;
                        }
                    }                
                } 

            } catch(HttpRequestException exception) {
                return exception.Message;
            }
        }
    }
}