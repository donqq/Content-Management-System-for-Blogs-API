/* Copyright (C) 2021/January Badde Liyanage Don Dilanga ( github@dilanga.com ) - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the Simple non code license (SNCL)
 * https://tldrlegal.com/license/simple-non-code-license-(sncl)
 * You should have received a copy of the Simple non code license (SNCL) with
 * this file. If not, please write to: github@dilanga.com , or visit : https://github.com/donqq/Content-Management-System-for-Blogs
 */

namespace contentMngApp.api.DataTransferObject
{
    public class PlagiarismCheckingMeta
    {
        private readonly string _username = "USERNAME";
        private readonly string _apikey = "API KEY";
        private readonly string _format = "xml";
        private readonly string _encoding = "UTF-8";
        private readonly string _searchtype = "csearch";

        public string t { get; set; } // content = to store the content to be checked for plagiarism

        public string c { get; set; } //depth= copyscape API value that indicates the depth of the plagiarism checking. start from 0 to 10.

        public string u { //username
            get => _username;
        }

        public string k { //apikey
            get => _apikey;
        }

        public string e { //encoding
            get => _encoding;
        }

        public string f { //format
            get => _format;
        }

        public string o { //searchtype
            get => _searchtype;
        }

        // --------- FOR DTO (data transfer object)
        public string content { get; set; } 
        public string depth { get; set; } 
    }
}