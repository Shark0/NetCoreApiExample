using System;
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
    public class RegisterService : IRegisterService
    {
        private readonly ILogger<RegisterService> _logger;
        private readonly IStringLocalizer<RegisterService> _stringLocalizer;

        private readonly IConfiguration _configuration;
        
        private readonly SqlServerContext _sqlServerContext;

        public RegisterService(
            ILogger<RegisterService> logger,
            IStringLocalizer<RegisterService> stringLocalizer,
            IConfiguration configuration,
            SqlServerContext sqlServerContext)
        {
            _logger = logger;
            _configuration = configuration;
            _stringLocalizer = stringLocalizer;
            _sqlServerContext = sqlServerContext;
        }

        public ResponseDataDto<LoginDto> Request(RegisterDo registerDo)
        {
            _logger.LogInformation($"registerDo = {JsonSerializer.Serialize(registerDo)}");
            MemberDo member = new MemberHelper().FindMember(_sqlServerContext, registerDo.Account);
            if (member != null)
            {
                return new ResponseDataDto<LoginDto>
                {
                    Status = -1,
                    Message = _stringLocalizer["AccountAlreadyExist"]
                };
            }

            MemberDo memberDo = InsertMember(registerDo);
            Console.WriteLine(JsonSerializer.Serialize(memberDo));
            string jwt = new MemberHelper().GenerateJwt(_configuration, memberDo.Id);
            LoginDto loginDto = new LoginDto
            {
                Id = memberDo.Id,
                Name =  memberDo.Name,
                Jwt = jwt
            };

            return new()
            {
                Status = 0,
                Data = loginDto
            };
        }

        private MemberDo InsertMember(RegisterDo registerDo)
        {
            string password = BCrypt.Net.BCrypt.HashPassword(registerDo.Password);
            MemberDo memberDo = new MemberDo()
            {
                Account = registerDo.Account,
                Password = password,
                Name = registerDo.Name
            };

            _sqlServerContext.Member.Add(memberDo);
            _sqlServerContext.SaveChanges();
            return memberDo;
        }
    }
}