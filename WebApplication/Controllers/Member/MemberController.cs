using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Controllers.Base.Entity;
using WebApplication.Controllers.Member.Entity;
using WebApplication.Model;
using WebApplication.Services.Member;

namespace WebApplication.Controllers.Member
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly ILoginService _loginService;
        private readonly IEditMemberService _editMemberService;

        public MemberController(
            IRegisterService registerService, 
            ILoginService loginService,
            IEditMemberService editMemberService)
        {
            _registerService = registerService;
            _loginService = loginService;
            _editMemberService = editMemberService;
        }

        [HttpPost("Login")]
        public ResponseDataDto<LoginDto> Login(LoginDo login)
        {
            return _loginService.Request(login);
        }
        
        [HttpPost("Register")]
        public ResponseDataDto<LoginDto> Register(RegisterDo register)
        {
            return _registerService.Request(register);
        }
        
        [Authorize]
        [HttpPost("Edit")]
        public ResponseDto Edit(EditMemberDo editMember)
        {
            
            var claimList = HttpContext.AuthenticateAsync().Result.Principal.Claims;
            string memberId = claimList.FirstOrDefault(t => t.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            Console.WriteLine("memberId = " + memberId);

            return _editMemberService.Request(Int32.Parse(memberId), editMember);
        }
        
        
    }
}