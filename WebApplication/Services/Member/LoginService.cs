using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using WebApplication.Controllers.Base.Entity;
using WebApplication.Controllers.Member.Entity;
using WebApplication.Helper;
using WebApplication.Model;
using WebApplication.Model.Member;

namespace WebApplication.Services.Member
{
    public class LoginService: ILoginService
    {
        private readonly ILogger<LoginService> _logger;
        private readonly IStringLocalizer<LoginService> _stringLocalizer;
        
        private readonly IConfiguration _configuration;
        private readonly SqlServerContext _sqlServerContext;

        public LoginService(
            ILogger<LoginService> logger,
            IStringLocalizer<LoginService> stringLocalizer,
            IConfiguration configuration,
            SqlServerContext sqlServerContext)
        {
            _logger = logger;
            _configuration = configuration;
            _stringLocalizer = stringLocalizer;
            _sqlServerContext = sqlServerContext;
        }
        
        public ResponseDataDto<LoginDto> Request(LoginDo loginDo)
        {
            _logger.LogInformation($"loginDo = {JsonSerializer.Serialize(loginDo)}");
            MemberDo member = new MemberHelper().FindMember(_sqlServerContext, loginDo.Account);
            if (member == null)
            {
                return new ResponseDataDto<LoginDto>
                {
                    Status = -1,
                    Message = _stringLocalizer["AccountDoesNotExist"]
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDo.Password, member.Password))
            {
                return new ResponseDataDto<LoginDto>
                {
                    Status = -2,
                    Message = _stringLocalizer["WrongPassword"]
                };
            }
            
            string jwt = new MemberHelper().GenerateJwt(_configuration, member.Id);
            LoginDto loginDto = new LoginDto
            {
                Id = member.Id,
                Name =  member.Name,
                Jwt = jwt
            };

            return new()
            {
                Status = 0,
                Data = loginDto
            };
        }
    }
}