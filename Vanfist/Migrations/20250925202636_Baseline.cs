using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vanfist.Migrations
{
    public partial class Baseline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // intentionally left blank
            // mục tiêu: chỉ ghi nhận baseline vào __EFMigrationsHistory, không thay đổi schema
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // intentionally left blank
        }
    }
}
