namespace ASP.NetMVC_Tutorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'722b0051-ef2a-43ca-9b63-3a262be8a985', N'guest@asp.net', 0, N'AO5GA3I/+2rKbEeXSVkxUYbDzmgCUJ1DNyxurxzLz0Svr47uWvt98EXbk4uPYLy+gw==', N'b35226c0-17f2-4ac9-b9fd-4d58e645c844', NULL, 0, 0, NULL, 1, 0, N'guest@asp.net')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'8fa6ef12-82ac-4880-8778-8b2e4d14e0e3', N'admin@asp.net', 0, N'AO8yq6fJkDYZxzIUD+HXK9SKg/ZBP1hWUOD5tbQPAVSGF7NdAs/NRXfXIT0O4NNDgQ==', N'cfa6fea4-0676-4901-81f4-651e0211f067', NULL, 0, 0, NULL, 1, 0, N'admin@asp.net')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a21ea258-69bb-4798-ab3f-6b04b77d7868', N'CanManageMovies')
INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8fa6ef12-82ac-4880-8778-8b2e4d14e0e3', N'a21ea258-69bb-4798-ab3f-6b04b77d7868')
");
        }
        
        public override void Down()
        {
        }
    }
}
