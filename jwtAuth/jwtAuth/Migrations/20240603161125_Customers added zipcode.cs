using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jwtAuth.Migrations
{
    /// <inheritdoc />
    public partial class Customersaddedzipcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Customers",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 213, 136, 211, 168, 215, 52, 54, 27, 219, 217, 148, 222, 161, 83, 132, 243, 180, 252, 129, 244, 29, 176, 203, 2, 19, 72, 184, 193, 236, 205, 209, 209 }, new byte[] { 109, 58, 35, 104, 147, 35, 9, 250, 14, 38, 128, 21, 168, 114, 215, 62, 173, 211, 151, 199, 30, 125, 95, 216, 218, 243, 36, 177, 208, 154, 177, 98, 239, 58, 212, 131, 196, 147, 123, 11, 100, 124, 250, 209, 25, 78, 159, 153, 71, 80, 204, 147, 19, 86, 251, 150, 100, 80, 85, 54, 110, 9, 66, 53 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Customers");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 183, 136, 92, 71, 18, 239, 57, 146, 18, 151, 192, 162, 128, 72, 195, 120, 143, 191, 115, 30, 70, 132, 177, 249, 4, 251, 217, 49, 153, 205, 88, 160 }, new byte[] { 106, 74, 181, 245, 157, 123, 106, 126, 109, 12, 227, 150, 57, 148, 71, 198, 190, 242, 38, 131, 208, 91, 227, 47, 88, 11, 192, 177, 86, 217, 55, 245, 239, 157, 4, 128, 253, 188, 57, 61, 33, 186, 181, 152, 150, 187, 128, 64, 43, 80, 27, 150, 86, 87, 237, 158, 103, 95, 5, 183, 201, 203, 116, 215 } });
        }
    }
}
