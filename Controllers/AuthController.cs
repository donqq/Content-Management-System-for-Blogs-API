/* Copyright (C) 2021/January Badde Liyanage Don Dilanga ( github@dilanga.com ) - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the Simple non code license (SNCL)
 * https://tldrlegal.com/license/simple-non-code-license-(sncl)
 * You should have received a copy of the Simple non code license (SNCL) with
 * this file. If not, please write to: github@dilanga.com , or visit : https://github.com/donqq/Content-Management-System-for-Blogs
 */

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using contentMngApp.api.DataTransferObject;
using contentMngApp.api.Data;
using System.Net.Http;
using System;

namespace contentMngApp.api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _auth;
        private readonly IDeserializeToken _deserializeToken;
        private readonly IPlagiarismChecking _plagiarismChecking;
        private readonly IXMLtoJSON _xMLtoJSON;

        public AuthController(IAuthRepository auth, IDeserializeToken deserializeToken, IPlagiarismChecking plagiarismChecking, IXMLtoJSON xMLtoJSON) {
            _auth = auth;
            _deserializeToken = deserializeToken;
            _plagiarismChecking = plagiarismChecking;
            _xMLtoJSON = xMLtoJSON;
        }

        [HttpPost("result")]
        public async Task<IActionResult> result(PlagiarismCheckingMeta dto) { // PlagiarismCheckingMeta dto
            try {
                var BearerToken = Request.Headers["Authorization"]; // get the bearer token
                string tokenStr = BearerToken.ToString().Split(" ")[1].ToString(); // separate bearer part from token, and only retrieve the token
                string uid = _deserializeToken.deserializedToken(tokenStr).user_id; // get the user id of the authenticated user.
                
                if (!(await _auth.UserExist(uid ,tokenStr))) {
                    return BadRequest("User doesn't exist");
                }

                string XMLResult = await _plagiarismChecking.plagiarismResult(dto.content, dto.depth);
                string jsonResult = _xMLtoJSON.getJson(XMLResult);

                return Ok(jsonResult);

            } catch (Exception error) {
                return BadRequest(error.Message);
            }
        }
    }
}
