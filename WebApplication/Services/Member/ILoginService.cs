using WebApplication.Controllers.Base.Entity;
using WebApplication.Controllers.Member.Entity;

namespace WebApplication.Services.Member
{
    public interface ILoginService
    {
        public ResponseDataDto<LoginDto> Request(LoginDo loginDo);
    }
}