# Read Me
.net core的API範例，實作了Register、Login、Edit Member這三隻API來展現Bcrypt、JWT、Sql、Mulit-Language、Log等技術應用

## Sql
Database為Sql Server
```
-- auto-generated definition
create table Member
(
    Id       int identity
        constraint Member_pk
            primary key nonclustered,
    Account  varchar(128) not null,
    Password varchar(128) not null,
    Name     varchar(128) not null
)
go

create unique index Member_Account_uindex
    on Member (Account)
go

```

## API Demo
執行就會打開Swagger UI，可以用Swagger UI來Demo

### Register API
註冊成功可以得到JWT，可去DB看Member看Password有被Bcrypt加密，再次註冊相同帳號可以看到Language Resource管理的訊息

### Login API
登入成功可以得到JWT，輸入錯帳號或密碼可以看到Language Resource管理的訊息

### Edit Member API
該API應該用REST Post，但是Demo用就不要太計較，需在Swagger的Authorize按鈕輸入JWT，JWT錯誤可以看到Http Status 401，因為JWT含有Member Id，所以Post參數不需要MemberId


## Reference
* Sql Server Entity Framework Core
	* https://blog.johnwu.cc/article/ironman-day24-asp-net-core-entity-framework-core.html
	* https://docs.microsoft.com/en-us/ef/ef6/saving/transactions
* Bcrypt
	* https://jasonwatmore.com/post/2020/07/16/aspnet-core-3-hash-and-verify-passwords-with-bcrypt
* JWT
	* https://talllkai.coderbridge.io/2021/09/05/JWT/
* Swagger Jwt Header
	* https://dev.to/raviranjanpandey/configure-authorization-in-openapi-swagger-ui-for-jwt-in-asp-net-core-web-api-1p1k
* Multi-Language
	* https://docs.microsoft.com/zh-tw/aspnet/core/fundamentals/localization?view=aspnetcore-6.0
* Log
	* https://docs.microsoft.com/zh-tw/aspnet/core/fundamentals/logging/?view=aspnetcore-6.0