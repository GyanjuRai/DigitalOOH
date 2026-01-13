using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigitalOOH.API.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    MediaType = table.Column<int>(type: "int", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(300)", nullable: false),
                    DurationSeconds = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ads", x => x.Id);
                    table.CheckConstraint("check_ad_durationSeconds", "[DurationSeconds] > 0");
                });

            migrationBuilder.CreateTable(
                name: "campaigns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("campaign_id", x => x.Id);
                    table.CheckConstraint("check_campaign_timeframe", "[EndTime] > [StartTime]");
                });

            migrationBuilder.CreateTable(
                name: "screens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Resolution = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_screens", x => x.Id);
                    table.CheckConstraint("checK_screen_resolution", "Resolution LIKE '[0-9][0-9][0-9]x%' OR Resolution LIKE '[0-9][0-9][0-9][0-9]x%'");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "campaign_ads",
                columns: table => new
                {
                    CampaignId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campaign_ads", x => new { x.CampaignId, x.AdId });
                    table.ForeignKey(
                        name: "FK_campaign_ads_ads_AdId",
                        column: x => x.AdId,
                        principalTable: "ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_campaign_ads_campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "campaign_screens",
                columns: table => new
                {
                    CampaignId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScreenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_campaign_screens", x => new { x.CampaignId, x.ScreenId });
                    table.ForeignKey(
                        name: "FK_campaign_screens_campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_campaign_screens_screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "proof_of_plays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScreenId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proof_of_plays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_proof_of_plays_ads_AdId",
                        column: x => x.AdId,
                        principalTable: "ads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_proof_of_plays_campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_proof_of_plays_screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "screens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_campaign_ads_AdId",
                table: "campaign_ads",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_campaign_screens_ScreenId",
                table: "campaign_screens",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_proof_of_plays_AdId",
                table: "proof_of_plays",
                column: "AdId");

            migrationBuilder.CreateIndex(
                name: "IX_proof_of_plays_CampaignId",
                table: "proof_of_plays",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_proof_of_plays_ScreenId",
                table: "proof_of_plays",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "campaign_ads");

            migrationBuilder.DropTable(
                name: "campaign_screens");

            migrationBuilder.DropTable(
                name: "proof_of_plays");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "ads");

            migrationBuilder.DropTable(
                name: "campaigns");

            migrationBuilder.DropTable(
                name: "screens");
        }
    }
}
