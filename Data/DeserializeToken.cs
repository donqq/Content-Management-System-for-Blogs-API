/* Copyright (C) 2021/January Badde Liyanage Don Dilanga ( github@dilanga.com ) - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the Simple non code license (SNCL)
 * https://tldrlegal.com/license/simple-non-code-license-(sncl)
 * You should have received a copy of the Simple non code license (SNCL) with
 * this file. If not, please write to: github@dilanga.com , or visit : https://github.com/donqq/Content-Management-System-for-Blogs
 */

using contentMngApp.api.DataTransferObject;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace contentMngApp.api.Data
{
    public class DeserializeToken : IDeserializeToken
    {
        public DeserializedToken deserializedToken(string tokenStr) {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadJwtToken(tokenStr); // get the actual token as JWT object TYPED JwtSecurityToken.
            string tokenJSON = token.Payload.SerializeToJson(); // convert the token to JSON string.
            return JsonSerializer.Deserialize<DeserializedToken>(tokenJSON); // get the UID only.
        }
    }
}