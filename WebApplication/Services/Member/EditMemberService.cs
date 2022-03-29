using System;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using WebApplication.Controllers.Base.Entity;
using WebApplication.Controllers.Member.Entity;
using WebApplication.Model;
using WebApplication.Model.Member;

namespace WebApplication.Services.Member
{
    public class EditMemberService: IEditMemberService
    {
        
        private readonly ILogger<EditMemberService> _logger;
        private readonly IStringLocalizer<EditMemberService> _stringLocalizer;

        private readonly SqlServerContext _sqlServerContext;

        public EditMemberService(
            ILogger<EditMemberService> logger,
            IStringLocalizer<EditMemberService> stringLocalizer,
            SqlServerContext sqlServerContext)
        {
            _logger = logger;
            _stringLocalizer = stringLocalizer;
            _sqlServerContext = sqlServerContext;
        }
        
        public ResponseDto Request(int memberId, EditMemberDo editMemberDo)
        {
            _logger.LogInformation($"memberId = {memberId}, editMemberDo = {JsonSerializer.Serialize(editMemberDo)}");
            MemberDo member = FindMember(memberId);
            if (member == null)
            {
                return new()
                {
                    Message = _stringLocalizer["AccountDoesNotExist"],
                    Status = -1
                };
            }
            if (!String.IsNullOrEmpty(editMemberDo.Name))
            {
                member.Name = editMemberDo.Name;
            }
            if (!String.IsNullOrEmpty(editMemberDo.Password))
            {
                member.Password = BCrypt.Net.BCrypt.HashPassword(editMemberDo.Password);
            }
            _sqlServerContext.Member.Update(member);
            _sqlServerContext.SaveChanges();
            return new()
            {
                Status = 1
            };
        }

        private MemberDo FindMember(int id)
        {
            return (from member in _sqlServerContext.Member
                where member.Id == id
                select member).SingleOrDefault();
        }
    }
}