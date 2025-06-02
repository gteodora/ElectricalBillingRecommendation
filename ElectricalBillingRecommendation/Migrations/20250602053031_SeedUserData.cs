using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElectricalBillingRecommendation.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "asp_net_user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { "1", "3183125d-eeb5-4ed4-ac1d-31e3e17c80a1" });

            migrationBuilder.DeleteData(
                table: "asp_net_users",
                keyColumn: "id",
                keyValue: "3183125d-eeb5-4ed4-ac1d-31e3e17c80a1");

            migrationBuilder.InsertData(
                table: "asp_net_users",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[,]
                {
                    { "68095d59-8fd0-4857-b11f-f9509f481cf7", 0, "55e7189a-42f4-4039-91d4-4976e401c642", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEAaPKwgOQaKutF52ubOWQB1E1GwzrFYF9XuNyyHvAvxxAelNC0baGwVBae1sfgYiog==", "1234567890", true, "1ad8de28-7331-4160-a225-825beb08cf3c", false, "admin@admin.com" },
                    { "ab2577ed-fed1-4a94-90f6-e11d39dcba51", 0, "afe3facc-99e5-4c29-9e92-93b06f84fc65", "user@user.com", true, false, null, "USER@USER.COM", "USER@USER.COM", "AQAAAAIAAYagAAAAEG2VVsXw3sz3WblbD/LwCgG4oqOAGbWO8etVzH8st0RUSmwhRx27XgRKMMDzNgLJqQ==", "1234567890", true, "c33989d2-f10a-47ee-bb3e-58e070160def", false, "user@user.com" }
                });

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 1,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 30, 29, 764, DateTimeKind.Utc).AddTicks(9184));

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 2,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 30, 29, 764, DateTimeKind.Utc).AddTicks(9191));

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 3,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 30, 29, 764, DateTimeKind.Utc).AddTicks(9194));

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 4,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 30, 29, 764, DateTimeKind.Utc).AddTicks(9195));

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 5,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 30, 29, 764, DateTimeKind.Utc).AddTicks(9197));

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 6,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 30, 29, 764, DateTimeKind.Utc).AddTicks(9199));

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 7,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 30, 29, 764, DateTimeKind.Utc).AddTicks(9201));

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 8,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 30, 29, 764, DateTimeKind.Utc).AddTicks(9203));

            migrationBuilder.InsertData(
                table: "asp_net_user_roles",
                columns: new[] { "role_id", "user_id" },
                values: new object[,]
                {
                    { "1", "68095d59-8fd0-4857-b11f-f9509f481cf7" },
                    { "2", "ab2577ed-fed1-4a94-90f6-e11d39dcba51" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "asp_net_user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { "1", "68095d59-8fd0-4857-b11f-f9509f481cf7" });

            migrationBuilder.DeleteData(
                table: "asp_net_user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { "2", "ab2577ed-fed1-4a94-90f6-e11d39dcba51" });

            migrationBuilder.DeleteData(
                table: "asp_net_users",
                keyColumn: "id",
                keyValue: "68095d59-8fd0-4857-b11f-f9509f481cf7");

            migrationBuilder.DeleteData(
                table: "asp_net_users",
                keyColumn: "id",
                keyValue: "ab2577ed-fed1-4a94-90f6-e11d39dcba51");

            migrationBuilder.InsertData(
                table: "asp_net_users",
                columns: new[] { "id", "access_failed_count", "concurrency_stamp", "email", "email_confirmed", "lockout_enabled", "lockout_end", "normalized_email", "normalized_user_name", "password_hash", "phone_number", "phone_number_confirmed", "security_stamp", "two_factor_enabled", "user_name" },
                values: new object[] { "3183125d-eeb5-4ed4-ac1d-31e3e17c80a1", 0, "18ce7af1-3048-41ed-94e8-5ae26e0890a1", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEKEQQl1mDC42xHc7fH5z1wZaeFB4E68UK2999+T4aqSGo7vqZPdqpiva4qWqoXe/JQ==", "1234567890", true, "982ff7fc-a2f8-4b4b-8754-6dcd7a7bbf32", false, "admin@admin.com" });

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 1,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 22, 26, 644, DateTimeKind.Utc).AddTicks(7671));

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 2,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 22, 26, 644, DateTimeKind.Utc).AddTicks(7677));

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 3,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 22, 26, 644, DateTimeKind.Utc).AddTicks(7680));

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 4,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 22, 26, 644, DateTimeKind.Utc).AddTicks(7682));

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 5,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 22, 26, 644, DateTimeKind.Utc).AddTicks(7684));

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 6,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 22, 26, 644, DateTimeKind.Utc).AddTicks(7686));

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 7,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 22, 26, 644, DateTimeKind.Utc).AddTicks(7688));

            migrationBuilder.UpdateData(
                table: "pricing_tiers",
                keyColumn: "id",
                keyValue: 8,
                column: "updated_at",
                value: new DateTime(2025, 6, 2, 5, 22, 26, 644, DateTimeKind.Utc).AddTicks(7690));

            migrationBuilder.InsertData(
                table: "asp_net_user_roles",
                columns: new[] { "role_id", "user_id" },
                values: new object[] { "1", "3183125d-eeb5-4ed4-ac1d-31e3e17c80a1" });
        }
    }
}
