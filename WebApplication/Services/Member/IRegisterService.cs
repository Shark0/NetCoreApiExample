using WebApplication.Controllers.Base.Entity;
using WebApplication.Controllers.Member.Entity;

namespace WebApplication.Services.Member
{
    public interface IRegisterService
    {
        public ResponseDataDto<LoginDto> Request(RegisterDo registerDo);
    }
}