using Microsoft.EntityFrameworkCore.Migrations;

namespace PayFlex.Identity.Infrastructure.Migrations
{
    public partial class seed_data_adds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO [identity].[Tenants] ([Name],[Description]) VALUES ('Default','Test')");

            //Username: emre.yazici  
            //Password: 123456.

            migrationBuilder.Sql($"INSERT INTO [identity].[Users]"+
           "([UserName]"+
           ",[NormalizedUserName]"+
           ",[Email]"+
           ",[NormalizedEmail]"+
           ",[EmailConfirmed]"+
           ",[PasswordHash]"+
           ",[SecurityStamp]"+
           ",[ConcurrencyStamp]" +
           ",[PhoneNumberConfirmed]" +
           ",[TwoFactorEnabled]" +
           ",[LockoutEnabled]" +
           ",[AccessFailedCount]" +
           ",[IsDeleted])" +
        "VALUES"+
           "('emre.yazici'"+
           ", 'EMRE.YAZICI'"+
           ", 'iyazici@innova.com.tr'"+
           ", 'IYAZICI@INNOVA.COM.TR'"+
           ", 0"+
           ", 'AQAAAAEAACcQAAAAEPLwdN10BB/JkIiQ3V6MuIKYKQlKjSqgLhFWn5jIf0yMF8M5qYbGtlWWtv/PocoJ+w=='"+
           ", 'IC6FMWR5W6MLXGHYF66DACSO6LN7CVKJ'"+
           ", '976a51ac-7e81-47c9-91c7-5bdd3863a3fa'" +
           ", 1" +
           ", 0" +
           ", 0" +
           ", 0" +
           ", 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
