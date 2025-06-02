using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Translation : Migration
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
                    { 1, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), null, null, "FullAccess", 1, null },
                    { 2, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), null, null, "ManageUsers", 2, null }
                });

            migrationBuilder.InsertData(
                schema: "Translation",
                table: "ClaimTranslation",
                columns: new[] { "Id", "ClaimId", "Content", "CreatedBy", "CreatedOn", "CultureName", "ModifiedBy", "ModifiedOn", "ValidUntil" },
                values: new object[,]
                {
                    { new Guid("204b62a4-80cc-4cec-9431-ce18adddbc3b"), 1, "Puno pravo", "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 952, DateTimeKind.Utc).AddTicks(3822), "hr-HR", null, null, null },
                    { new Guid("7c867940-8cfc-4a07-a1d5-bf910a88d559"), 2, "Upravljanje korisnicima", "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 952, DateTimeKind.Utc).AddTicks(3822), "hr-HR", null, null, null },
                    { new Guid("a176ee71-ea72-4d15-bec2-3416091abc76"), 2, "Managing users", "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 952, DateTimeKind.Utc).AddTicks(3822), "en-US", null, null, null },
                    { new Guid("c2961012-5ecf-44e1-a87e-da1e716034fb"), 1, "Full access", "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 952, DateTimeKind.Utc).AddTicks(3822), "en-US", null, null, null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "CultureType",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Type", "ValidUntil", "Value" },
                values: new object[,]
                {
                    { 1, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), null, null, 1, null, "hr-HR" },
                    { 2, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), null, null, 2, null, "en-US" }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "Role",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "Name", "Type", "ValidUntil" },
                values: new object[,]
                {
                    { 1, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), null, null, "Administrator", 1, null },
                    { 2, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), null, null, "Business", 2, null },
                    { 3, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), null, null, "Client", 3, null }
                });

            migrationBuilder.InsertData(
                schema: "Translation",
                table: "RoleTranslation",
                columns: new[] { "Id", "Content", "CreatedBy", "CreatedOn", "CultureName", "ModifiedBy", "ModifiedOn", "RoleId", "ValidUntil" },
                values: new object[,]
                {
                    { new Guid("09b1fcf0-5969-49f3-8aaf-dc6724f3c1e6"), "Client", "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 952, DateTimeKind.Utc).AddTicks(3822), "en-US", null, null, 3, null },
                    { new Guid("0e6894fe-26e4-4f82-9d19-6338f03f122f"), "Bussiness", "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 952, DateTimeKind.Utc).AddTicks(3822), "hr-HR", null, null, 1, null },
                    { new Guid("3f3e0e4c-6fc4-4c4d-bb66-11f65207e58c"), "Klijent", "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 952, DateTimeKind.Utc).AddTicks(3822), "hr-HR", null, null, 3, null },
                    { new Guid("69fe8877-a63e-4f08-98b5-62c7e6acf4cb"), "Poslovni", "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 952, DateTimeKind.Utc).AddTicks(3822), "en-US", null, null, 1, null },
                    { new Guid("90d85150-c088-4ec0-9488-40b1bf2f9874"), "Administrator", "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 952, DateTimeKind.Utc).AddTicks(3822), "hr-HR", null, null, 1, null },
                    { new Guid("b29728c4-55c5-4b2f-9e5a-1a1bc37bf337"), "Administrator", "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 952, DateTimeKind.Utc).AddTicks(3822), "en-US", null, null, 1, null }
                });

            migrationBuilder.InsertData(
                schema: "Translation",
                table: "CultureTypeTranslation",
                columns: new[] { "Id", "Content", "CreatedBy", "CreatedOn", "CultureName", "CultureTypeId", "ModifiedBy", "ModifiedOn", "ValidUntil" },
                values: new object[,]
                {
                    { new Guid("8567bd6c-bdc6-4c14-9bc3-d9c2a26de6f2"), "English", "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 952, DateTimeKind.Utc).AddTicks(3822), "en-US", 2, null, null, null },
                    { new Guid("ca109a4f-78de-4684-8b08-89c8f7a7a306"), "Hrvatski", "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 952, DateTimeKind.Utc).AddTicks(3822), "hr-HR", 1, null, null, null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "User",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "CultureTypeId", "Email", "FullName", "ModifiedBy", "ModifiedOn", "PasswordHash", "Telephone", "ValidUntil", "City", "Country", "PostalCode", "Street" },
                values: new object[,]
                {
                    { 1, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), 1, "admin@email.hr", "Administrator User", null, null, "$2a$11$JirWzDstghZJWJ6teyzeIuSQxz2ZIq9Xt67PDrYoGv8MinnPtXRMm", "+385955535353", null, "Zagreb", "Croatia", "10000", "Ilica 10" },
                    { 2, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), 2, "client@email.hr", "Client User", null, null, "$2a$11$wQOVMi39iC5ILUYSPFh69u431XPMaL9KJFDXX1OaBTRCEjhB6rmES", "+385955585353", null, "Zagreb", "Croatia", "10000", "Ilica 255" },
                    { 3, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), 2, "bussiness@email.hr", "Business user", null, null, "$2a$11$5FCSOG1dbgAenQDR4janPeHYweha9wxXnT55EuzUTS45z77viF9eS", "+385956685353", null, "Zagreb", "Croatia", "10000", "Taborska 15" }
                });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserClaim",
                columns: new[] { "ClaimId", "UserId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "ValidUntil" },
                values: new object[] { 2, 2, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), null, null, null });

            migrationBuilder.InsertData(
                schema: "Security",
                table: "UserRole",
                columns: new[] { "RoleId", "UserId", "CreatedBy", "CreatedOn", "ModifiedBy", "ModifiedOn", "ValidUntil" },
                values: new object[,]
                {
                    { 1, 1, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), null, null, null },
                    { 2, 1, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), null, null, null },
                    { 2, 3, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), null, null, null },
                    { 3, 1, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), null, null, null },
                    { 3, 2, "Migration", new DateTime(2025, 6, 2, 17, 11, 21, 953, DateTimeKind.Utc).AddTicks(14), null, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claim_Name",
                schema: "Security",
                table: "Claim",
                column: "Name",
                unique: true);

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
