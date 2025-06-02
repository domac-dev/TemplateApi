using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Security");

            migrationBuilder.EnsureSchema(
                name: "Translation");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Claim",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValue: "system"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CultureType",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValue: "system"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CultureType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValue: "system"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClaimTranslation",
                schema: "Translation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CultureName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimTranslation_Claim_ClaimId",
                        column: x => x.ClaimId,
                        principalSchema: "Security",
                        principalTable: "Claim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CultureTypeTranslation",
                schema: "Translation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CultureTypeId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CultureName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CultureTypeTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CultureTypeTranslation_CultureType_CultureTypeId",
                        column: x => x.CultureTypeId,
                        principalSchema: "dbo",
                        principalTable: "CultureType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailConfirmationToken = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValueSql: "NEWID()"),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CultureTypeId = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValue: "system"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_CultureType_CultureTypeId",
                        column: x => x.CultureTypeId,
                        principalSchema: "dbo",
                        principalTable: "CultureType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleTranslation",
                schema: "Translation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CultureName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleTranslation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleTranslation_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "Security",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedByIp = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    RevokedBy = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ReasonRevoked = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RevokedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false, defaultValue: "system"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => new { x.ClaimId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserClaim_Claim_ClaimId",
                        column: x => x.ClaimId,
                        principalSchema: "Security",
                        principalTable: "Claim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserClaim_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "Security",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Security",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Claim",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Name", "Type", "ValidUntil" },
                values: new object[,]
                {
                    { 1, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), null, null, "FullAccess", 1, null },
                    { 2, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), null, null, "ManageUsers", 2, null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "CultureType",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Type", "ValidUntil", "Value" },
                values: new object[,]
                {
                    { 1, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), null, null, 1, null, "hr-HR" },
                    { 2, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), null, null, 2, null, "en-US" }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Role",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Name", "Type", "ValidUntil" },
                values: new object[,]
                {
                    { 1, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), null, null, "Administrator", 1, null },
                    { 2, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), null, null, "Business", 2, null },
                    { 3, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), null, null, "Client", 3, null }
                });

            migrationBuilder.InsertData(
                schema: "Translation",
                table: "ClaimTranslation",
                columns: new[] { "Id", "ClaimId", "Content", "CreatedBy", "CreatedOn", "CultureName", "ModifiedBy", "ModifiedOn", "ValidUntil" },
                values: new object[,]
                {
                    { new Guid("62eca795-8510-46fc-b578-9505cc624d7a"), 2, "Managing users", "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 274, DateTimeKind.Utc).AddTicks(9489), "en-US", null, null, null },
                    { new Guid("96cb1977-adae-461e-8a77-f33abc1a0cfc"), 1, "Full access", "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 274, DateTimeKind.Utc).AddTicks(9489), "en-US", null, null, null },
                    { new Guid("b6c60634-d99d-4712-af4a-43d8cd4018e6"), 1, "Puno pravo", "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 274, DateTimeKind.Utc).AddTicks(9489), "hr-HR", null, null, null },
                    { new Guid("be76947b-cbd2-4de6-b4a1-0f2fa5e42434"), 2, "Upravljanje korisnicima", "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 274, DateTimeKind.Utc).AddTicks(9489), "hr-HR", null, null, null }
                });

            migrationBuilder.InsertData(
                schema: "Translation",
                table: "CultureTypeTranslation",
                columns: new[] { "Id", "Content", "CreatedBy", "CreatedOn", "CultureName", "CultureTypeId", "ModifiedBy", "ModifiedOn", "ValidUntil" },
                values: new object[,]
                {
                    { new Guid("9d4933c0-f86a-4cf5-9e05-01c7422545d0"), "Hrvatski", "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 274, DateTimeKind.Utc).AddTicks(9489), "hr-HR", 1, null, null, null },
                    { new Guid("f212e54b-9290-445f-821d-de477d419963"), "English", "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 274, DateTimeKind.Utc).AddTicks(9489), "en-US", 2, null, null, null }
                });

            migrationBuilder.InsertData(
                schema: "Translation",
                table: "RoleTranslation",
                columns: new[] { "Id", "Content", "CreatedBy", "CreatedOn", "CultureName", "ModifiedBy", "ModifiedOn", "RoleId", "ValidUntil" },
                values: new object[,]
                {
                    { new Guid("003a4449-fc03-4c38-abf7-34c304a04fa2"), "Bussiness", "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 274, DateTimeKind.Utc).AddTicks(9489), "hr-HR", null, null, 1, null },
                    { new Guid("27876906-c25c-4a41-a24c-2877c502a3b8"), "Client", "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 274, DateTimeKind.Utc).AddTicks(9489), "en-US", null, null, 3, null },
                    { new Guid("2d28eea5-d192-4fa1-a8ef-f41d69217fe0"), "Administrator", "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 274, DateTimeKind.Utc).AddTicks(9489), "hr-HR", null, null, 1, null },
                    { new Guid("b12d4617-04aa-46fb-be4c-40cf663c13b9"), "Klijent", "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 274, DateTimeKind.Utc).AddTicks(9489), "hr-HR", null, null, 3, null },
                    { new Guid("b8be5252-44da-4c7e-addc-e4334322574c"), "Poslovni", "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 274, DateTimeKind.Utc).AddTicks(9489), "en-US", null, null, 1, null },
                    { new Guid("dde0d4ba-f47f-4782-af24-5ba2d55d8420"), "Administrator", "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 274, DateTimeKind.Utc).AddTicks(9489), "en-US", null, null, 1, null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "CultureTypeId", "Email", "FullName", "ModifiedBy", "ModifiedOn", "PasswordHash", "Telephone", "ValidUntil", "City", "Country", "PostalCode", "Street" },
                values: new object[,]
                {
                    { 1, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), 1, "admin@email.hr", "Administrator User", null, null, "$2a$11$ss/EKhbB/Sb0F6adJotyZ.Ngna/8myEOIeO6U3xeM3xeZbl4loAW.", "+385955535353", null, "Zagreb", "Croatia", "10000", "Ilica 10" },
                    { 2, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), 2, "client@email.hr", "Client User", null, null, "$2a$11$bu35699st1JFhR7YUrrgS.q4I5MUTih16XVlhzpodGfD12TKf2VUq", "+385955585353", null, "Zagreb", "Croatia", "10000", "Ilica 255" },
                    { 3, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), 2, "bussiness@email.hr", "Business user", null, null, "$2a$11$3Bd1YIlXpaBxhfIOKS57rugf.3hCrroWMMc3Fe1PStfbCS1o.URzC", "+385956685353", null, "Zagreb", "Croatia", "10000", "Taborska 15" }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserClaim",
                columns: new[] { "ClaimId", "UserId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "ValidUntil" },
                values: new object[] { 2, 2, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), null, null, null });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserRole",
                columns: new[] { "RoleId", "UserId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "ValidUntil" },
                values: new object[,]
                {
                    { 1, 1, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), null, null, null },
                    { 2, 1, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), null, null, null },
                    { 2, 3, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), null, null, null },
                    { 3, 1, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), null, null, null },
                    { 3, 2, "Migration", new DateTime(2025, 6, 2, 17, 18, 11, 275, DateTimeKind.Utc).AddTicks(5643), null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claim_Name",
                schema: "Security",
                table: "Claim",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClaimTranslation_ClaimId",
                schema: "Translation",
                table: "ClaimTranslation",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_CultureTypeTranslation_CultureTypeId",
                schema: "Translation",
                table: "CultureTypeTranslation",
                column: "CultureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_Token",
                schema: "Security",
                table: "RefreshToken",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                schema: "Security",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Name",
                schema: "Security",
                table: "Role",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleTranslation_RoleId",
                schema: "Translation",
                table: "RoleTranslation",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CultureTypeId",
                schema: "dbo",
                table: "User",
                column: "CultureTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                schema: "dbo",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                schema: "Security",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                schema: "Security",
                table: "UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimTranslation",
                schema: "Translation");

            migrationBuilder.DropTable(
                name: "CultureTypeTranslation",
                schema: "Translation");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "RoleTranslation",
                schema: "Translation");

            migrationBuilder.DropTable(
                name: "UserClaim",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Claim",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Security");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CultureType",
                schema: "dbo");
        }
    }
}
