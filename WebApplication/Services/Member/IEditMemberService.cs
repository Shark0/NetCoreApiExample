using WebApplication.Controllers.Base.Entity;
using WebApplication.Controllers.Member.Entity;

namespace WebApplication.Services.Member
{
    public interface IEditMemberService
    {
        public ResponseDto Request(int memberId, EditMemberDo editMemberDo);
    }
}