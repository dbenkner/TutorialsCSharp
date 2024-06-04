using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwtAuth.Migrations
{
    /// <inheritdoc />
    public partial class Customers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StateCode = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    RepFirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    RepLastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SalesTotal = table.Column<decimal>(type: "Decimal(11,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 183, 136, 92, 71, 18, 239, 57, 146, 18, 151, 192, 162, 128, 72, 195, 120, 143, 191, 115, 30, 70, 132, 177, 249, 4, 251, 217, 49, 153, 205, 88, 160 }, new byte[] { 106, 74, 181, 245, 157, 123, 106, 126, 109, 12, 227, 150, 57, 148, 71, 198, 190, 242, 38, 131, 208, 91, 227, 47, 88, 11, 192, 177, 86, 217, 55, 245, 239, 157, 4, 128, 253, 188, 57, 61, 33, 186, 181, 152, 150, 187, 128, 64, 43, 80, 27, 150, 86, 87, 237, 158, 103, 95, 5, 183, 201, 203, 116, 215 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 92, 135, 82, 36, 16, 21, 143, 157, 8, 54, 96, 130, 170, 109, 142, 188, 87, 96, 76, 236, 101, 60, 121, 57, 177, 159, 176, 208, 104, 49, 58, 78 }, new byte[] { 255, 148, 207, 58, 41, 6, 135, 93, 145, 31, 152, 227, 74, 39, 47, 214, 175, 144, 107, 89, 136, 106, 67, 213, 184, 198, 187, 254, 43, 35, 124, 179, 5, 18, 24, 133, 13, 78, 28, 173, 93, 139, 103, 131, 162, 25, 20, 2, 101, 161, 79, 3, 136, 38, 14, 174, 181, 80, 68, 72, 194, 1, 59, 49 } });
        }
    }
}
