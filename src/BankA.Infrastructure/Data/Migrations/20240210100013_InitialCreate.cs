using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankA.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FileFormat = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FileFormatConfiguration = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Closed = table.Column<bool>(type: "bit", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChangedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CategoryType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSystem = table.Column<bool>(type: "bit", nullable: false),
                    ChangedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChangedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankCategory_BankCategory_ParentId",
                        column: x => x.ParentId,
                        principalTable: "BankCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankMerchant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChangedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChangedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankMerchant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ChangedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChangedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankFile_BankAccount_AccountId",
                        column: x => x.AccountId,
                        principalTable: "BankAccount",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Keywords = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    MerchantId = table.Column<int>(type: "int", nullable: true),
                    ChangedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChangedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankRule_BankCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BankCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankRule_BankMerchant_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "BankMerchant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BankTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<int>(type: "int", nullable: true),
                    TransactionDate = table.Column<DateTime>(type: "date", nullable: false),
                    TransactionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DebitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    MerchantId = table.Column<int>(type: "int", nullable: true),
                    FileId = table.Column<int>(type: "int", nullable: true),
                    ChangedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChangedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankTransaction_BankAccount_AccountId",
                        column: x => x.AccountId,
                        principalTable: "BankAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankTransaction_BankCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BankCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankTransaction_BankFile_FileId",
                        column: x => x.FileId,
                        principalTable: "BankFile",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankTransaction_BankMerchant_MerchantId",
                        column: x => x.MerchantId,
                        principalTable: "BankMerchant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankCategory_ParentId",
                table: "BankCategory",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_BankFile_AccountId",
                table: "BankFile",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BankRule_CategoryId",
                table: "BankRule",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BankRule_MerchantId",
                table: "BankRule",
                column: "MerchantId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransaction_AccountId",
                table: "BankTransaction",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransaction_CategoryId",
                table: "BankTransaction",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransaction_FileId",
                table: "BankTransaction",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_BankTransaction_MerchantId",
                table: "BankTransaction",
                column: "MerchantId");


            migrationBuilder.Sql(SqlViews.TransactionsView());
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankRule");

            migrationBuilder.DropTable(
                name: "BankTransaction");

            migrationBuilder.DropTable(
                name: "BankCategory");

            migrationBuilder.DropTable(
                name: "BankFile");

            migrationBuilder.DropTable(
                name: "BankMerchant");

            migrationBuilder.DropTable(
                name: "BankAccount");
        }
    }
}
