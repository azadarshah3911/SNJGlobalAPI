using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SNJGlobalAPI.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MailsTrackings",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotSentReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailsTrackings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stages",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    StageNo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stages", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(275)", maxLength: 275, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(275)", maxLength: 275, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(275)", maxLength: 275, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(275)", maxLength: 275, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Fk_BranchId = table.Column<int>(type: "int", nullable: true),
                    Fk_CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoginFailedCount = table.Column<byte>(type: "tinyint", nullable: false),
                    LockedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoginFailedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fk_UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    ActDctAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fk_ActDctBy = table.Column<int>(type: "int", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fk_DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NicNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JoiningDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Branches_Fk_BranchId",
                        column: x => x.Fk_BranchId,
                        principalTable: "Branches",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Users_Users_Fk_ActDctBy",
                        column: x => x.Fk_ActDctBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Users_Users_Fk_CreatedBy",
                        column: x => x.Fk_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Users_Users_Fk_DeletedBy",
                        column: x => x.Fk_DeletedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Users_Users_Fk_UpdatedBy",
                        column: x => x.Fk_UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_StageId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Statuses_Stages_Fk_StageId",
                        column: x => x.Fk_StageId,
                        principalTable: "Stages",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ForgetPasswords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Fk_UserId = table.Column<int>(type: "int", nullable: true),
                    RequestedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgetPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ForgetPasswords_Users_Fk_UserId",
                        column: x => x.Fk_UserId,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicareID = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Ssn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(275)", maxLength: 275, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(275)", maxLength: 275, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(275)", maxLength: 275, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Suffix = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    City = table.Column<string>(type: "nvarchar(575)", maxLength: 575, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(575)", maxLength: 575, nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(575)", maxLength: 575, nullable: true),
                    FK_StateId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FK_UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FK_DeletedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Patients_States_FK_StateId",
                        column: x => x.FK_StateId,
                        principalTable: "States",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Patients_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Patients_Users_FK_DeletedBy",
                        column: x => x.FK_DeletedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Patients_Users_FK_UpdatedBy",
                        column: x => x.FK_UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true),
                    AllowedStages = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fk_UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Fk_DeletedBy = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Products_Users_Fk_DeletedBy",
                        column: x => x.Fk_DeletedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Products_Users_Fk_UpdatedBy",
                        column: x => x.Fk_UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Products_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UserBonus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Fk_BonusTo = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fk_BonusFrom = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBonus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserBonus_Users_Fk_BonusFrom",
                        column: x => x.Fk_BonusFrom,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UserBonus_Users_Fk_BonusTo",
                        column: x => x.Fk_BonusTo,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_UserId = table.Column<int>(type: "int", nullable: false),
                    Dated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsLoggedIn = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_Fk_UserId",
                        column: x => x.Fk_UserId,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_UserId = table.Column<int>(type: "int", nullable: true),
                    Fk_RoleId = table.Column<int>(type: "int", nullable: true),
                    Fk_CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersRoles_Roles_Fk_RoleId",
                        column: x => x.Fk_RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_Fk_CreatedBy",
                        column: x => x.Fk_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_UsersRoles_Users_Fk_UserId",
                        column: x => x.Fk_UserId,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_ParentId = table.Column<int>(type: "int", nullable: true),
                    Fk_PatientId = table.Column<int>(type: "int", nullable: true),
                    Fk_StatusId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsDuplicate = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Leads_Leads_Fk_ParentId",
                        column: x => x.Fk_ParentId,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Leads_Patients_Fk_PatientId",
                        column: x => x.Fk_PatientId,
                        principalTable: "Patients",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Leads_Statuses_Fk_StatusId",
                        column: x => x.Fk_StatusId,
                        principalTable: "Statuses",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Leads_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ProductQuestions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_ProductId = table.Column<int>(type: "int", nullable: true),
                    FK_StageId = table.Column<int>(type: "int", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuestions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductQuestions_Products_FK_ProductId",
                        column: x => x.FK_ProductId,
                        principalTable: "Products",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductQuestions_Stages_FK_StageId",
                        column: x => x.FK_StageId,
                        principalTable: "Stages",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SubProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FK_ProductId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true),
                    IsParent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SubProducts_Products_FK_ProductId",
                        column: x => x.FK_ProductId,
                        principalTable: "Products",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SubProducts_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "AgentPenalty",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fk_StageId = table.Column<int>(type: "int", nullable: true),
                    Fk_LeadID = table.Column<int>(type: "int", nullable: true),
                    Fk_PenaltyTo = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fk_PenaltyFrom = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgentPenalty", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AgentPenalty_Leads_Fk_LeadID",
                        column: x => x.Fk_LeadID,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AgentPenalty_Stages_Fk_StageId",
                        column: x => x.Fk_StageId,
                        principalTable: "Stages",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AgentPenalty_Users_Fk_PenaltyFrom",
                        column: x => x.Fk_PenaltyFrom,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_AgentPenalty_Users_Fk_PenaltyTo",
                        column: x => x.Fk_PenaltyTo,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Chassing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_LeadId = table.Column<int>(type: "int", nullable: true),
                    Fk_StatusId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chassing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chassing_Leads_Fk_LeadId",
                        column: x => x.Fk_LeadId,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Chassing_Statuses_Fk_StatusId",
                        column: x => x.Fk_StatusId,
                        principalTable: "Statuses",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Chassing_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ChassingVerifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fk_LeadId = table.Column<int>(type: "int", nullable: true),
                    Fk_StatusId = table.Column<int>(type: "int", nullable: true),
                    Fk_UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChassingVerifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChassingVerifications_Leads_Fk_LeadId",
                        column: x => x.Fk_LeadId,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ChassingVerifications_Statuses_Fk_StatusId",
                        column: x => x.Fk_StatusId,
                        principalTable: "Statuses",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ChassingVerifications_Users_Fk_UserId",
                        column: x => x.Fk_UserId,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Eligibilities",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_LeadID = table.Column<int>(type: "int", nullable: true),
                    FK_StatusId = table.Column<int>(type: "int", nullable: false),
                    PrimaryInsurance = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HCPSCode = table.Column<string>(type: "nvarchar(275)", maxLength: 275, nullable: true),
                    ElgibilityRemarks = table.Column<string>(type: "nvarchar(575)", maxLength: 575, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eligibilities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Eligibilities_Leads_FK_LeadID",
                        column: x => x.FK_LeadID,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Eligibilities_Statuses_FK_StatusId",
                        column: x => x.FK_StatusId,
                        principalTable: "Statuses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Eligibilities_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LeadAssigneds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_LeadId = table.Column<int>(type: "int", nullable: false),
                    FK_AgentId = table.Column<int>(type: "int", nullable: true),
                    FK_SupervisorId = table.Column<int>(type: "int", nullable: true),
                    FK_StageId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadAssigneds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadAssigneds_Leads_FK_LeadId",
                        column: x => x.FK_LeadId,
                        principalTable: "Leads",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeadAssigneds_Stages_FK_StageId",
                        column: x => x.FK_StageId,
                        principalTable: "Stages",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeadAssigneds_Users_FK_AgentId",
                        column: x => x.FK_AgentId,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadAssigneds_Users_FK_SupervisorId",
                        column: x => x.FK_SupervisorId,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LeadComments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fk_StageId = table.Column<int>(type: "int", nullable: true),
                    FK_LeadID = table.Column<int>(type: "int", nullable: true),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeadComments_Leads_FK_LeadID",
                        column: x => x.FK_LeadID,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadComments_Stages_Fk_StageId",
                        column: x => x.Fk_StageId,
                        principalTable: "Stages",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadComments_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LeadFiles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FK_LeadID = table.Column<int>(type: "int", nullable: true),
                    FK_StageId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadFiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeadFiles_Leads_FK_LeadID",
                        column: x => x.FK_LeadID,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadFiles_Stages_FK_StageId",
                        column: x => x.FK_StageId,
                        principalTable: "Stages",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadFiles_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LeadStatuses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_LeadId = table.Column<int>(type: "int", nullable: true),
                    FK_StatusId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadStatuses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeadStatuses_Leads_FK_LeadId",
                        column: x => x.FK_LeadId,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadStatuses_Statuses_FK_StatusId",
                        column: x => x.FK_StatusId,
                        principalTable: "Statuses",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadStatuses_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "PatientLogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_StateId = table.Column<int>(type: "int", nullable: true),
                    FK_PatientID = table.Column<int>(type: "int", nullable: true),
                    Fk_StageId = table.Column<int>(type: "int", nullable: true),
                    Fk_LeadId = table.Column<int>(type: "int", nullable: true),
                    MedicareID = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Ssn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(275)", maxLength: 275, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(275)", maxLength: 275, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(275)", maxLength: 275, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Suffix = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    City = table.Column<string>(type: "nvarchar(575)", maxLength: 575, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(575)", maxLength: 575, nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(575)", maxLength: 575, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientLogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PatientLogs_Leads_Fk_LeadId",
                        column: x => x.Fk_LeadId,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PatientLogs_Patients_FK_PatientID",
                        column: x => x.FK_PatientID,
                        principalTable: "Patients",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PatientLogs_Stages_Fk_StageId",
                        column: x => x.Fk_StageId,
                        principalTable: "Stages",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PatientLogs_States_FK_StateId",
                        column: x => x.FK_StateId,
                        principalTable: "States",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_PatientLogs_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "QAs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fk_StatusId = table.Column<int>(type: "int", nullable: true),
                    Fk_LeadID = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QAs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QAs_Leads_Fk_LeadID",
                        column: x => x.Fk_LeadID,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_QAs_Statuses_Fk_StatusId",
                        column: x => x.Fk_StatusId,
                        principalTable: "Statuses",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_QAs_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SNS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_StatusID = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FK_LeadID = table.Column<int>(type: "int", nullable: true),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FK_UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SNS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SNS_Leads_FK_LeadID",
                        column: x => x.FK_LeadID,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SNS_Statuses_FK_StatusID",
                        column: x => x.FK_StatusID,
                        principalTable: "Statuses",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SNS_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_SNS_Users_FK_UpdatedBy",
                        column: x => x.FK_UpdatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ProductQuestionAnswers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_LeadId = table.Column<int>(type: "int", nullable: true),
                    FK_QuestionId = table.Column<int>(type: "int", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuestionAnswers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductQuestionAnswers_Leads_FK_LeadId",
                        column: x => x.FK_LeadId,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductQuestionAnswers_ProductQuestions_FK_QuestionId",
                        column: x => x.FK_QuestionId,
                        principalTable: "ProductQuestions",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductQuestionAnswers_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LeadProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_LeadID = table.Column<int>(type: "int", nullable: true),
                    FK_ProductId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true),
                    StageID = table.Column<int>(type: "int", nullable: true),
                    SubProductID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeadProducts_Leads_FK_LeadID",
                        column: x => x.FK_LeadID,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadProducts_Products_FK_ProductId",
                        column: x => x.FK_ProductId,
                        principalTable: "Products",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadProducts_Stages_StageID",
                        column: x => x.StageID,
                        principalTable: "Stages",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadProducts_SubProducts_SubProductID",
                        column: x => x.SubProductID,
                        principalTable: "SubProducts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadProducts_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "LeadSubProducts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_LeadID = table.Column<int>(type: "int", nullable: true),
                    FK_SubProductId = table.Column<int>(type: "int", nullable: true),
                    FK_StagetId = table.Column<int>(type: "int", nullable: true),
                    StageCount = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ProductID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadSubProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LeadSubProducts_Leads_FK_LeadID",
                        column: x => x.FK_LeadID,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadSubProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadSubProducts_Stages_FK_StagetId",
                        column: x => x.FK_StagetId,
                        principalTable: "Stages",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadSubProducts_SubProducts_FK_SubProductId",
                        column: x => x.FK_SubProductId,
                        principalTable: "SubProducts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_LeadSubProducts_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ChassingFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FK_LeadID = table.Column<int>(type: "int", nullable: true),
                    Fk_ChassingId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ChassingFileId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChassingFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChassingFile_Chassing_Fk_ChassingId",
                        column: x => x.Fk_ChassingId,
                        principalTable: "Chassing",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChassingFile_ChassingFile_ChassingFileId",
                        column: x => x.ChassingFileId,
                        principalTable: "ChassingFile",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChassingFile_Leads_FK_LeadID",
                        column: x => x.FK_LeadID,
                        principalTable: "Leads",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ChassingFile_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "QAFiles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_QAId = table.Column<int>(type: "int", nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QAFiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QAFiles_QAs_FK_QAId",
                        column: x => x.FK_QAId,
                        principalTable: "QAs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_QAFiles_Users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "QaQuestionAnswers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_QaID = table.Column<int>(type: "int", nullable: true),
                    FK_QuestionID = table.Column<int>(type: "int", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FK_CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QaQuestionAnswers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QaQuestionAnswers_ProductQuestions_FK_QuestionID",
                        column: x => x.FK_QuestionID,
                        principalTable: "ProductQuestions",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_QaQuestionAnswers_QAs_FK_QaID",
                        column: x => x.FK_QaID,
                        principalTable: "QAs",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_QaQuestionAnswers_Users_FK_CreatedBy",
                        column: x => x.FK_CreatedBy,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1, "DHA" },
                    { 2, "PECHS" }
                });

            migrationBuilder.InsertData(
                table: "Stages",
                columns: new[] { "ID", "Name", "StageNo" },
                values: new object[,]
                {
                    { 1, "Lead", 1 },
                    { 2, "Eligibility Verification", 2 },
                    { 3, "SNS Check", 3 },
                    { 4, "QA Verification", 4 },
                    { 5, "Proccesed Lead", 5 },
                    { 6, "Confirmation", 6 },
                    { 7, "Chassing Verification", 7 }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "ID", "Fk_StageId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "New Lead" },
                    { 2, 1, "SNS Pending" },
                    { 3, 1, "QA Pending" },
                    { 4, 2, "Ev Error" },
                    { 5, 2, "Medicare B" },
                    { 6, 2, "HMO" },
                    { 7, 2, "PPO" },
                    { 8, 2, "PPD" },
                    { 9, 2, "Part B In Active" },
                    { 10, 2, "In Active(PT Dead)" },
                    { 11, 3, "Pass" },
                    { 12, 3, "Fail" },
                    { 13, 3, "By Pass" },
                    { 14, 4, "QA Master" },
                    { 15, 1, "Chassing Pending" },
                    { 16, 4, "Zero Processed" },
                    { 17, 4, "QA Qualified" },
                    { 18, 1, "QA Re-Examine" },
                    { 19, 1, "SNS Fail" },
                    { 20, 5, "Pending" },
                    { 21, 5, "Approved" },
                    { 22, 5, "Reject" },
                    { 23, 1, "Confirmation Pending" },
                    { 24, 3, "SNS Error" },
                    { 25, 4, "QA Error" },
                    { 26, 1, "Chassing Fail" },
                    { 27, 4, "Not Qualified" },
                    { 28, 1, "Call Verification Pending" },
                    { 29, 7, "Start Chassing" },
                    { 30, 7, "Denied Chassing" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgentPenalty_Fk_LeadID",
                table: "AgentPenalty",
                column: "Fk_LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_AgentPenalty_Fk_PenaltyFrom",
                table: "AgentPenalty",
                column: "Fk_PenaltyFrom");

            migrationBuilder.CreateIndex(
                name: "IX_AgentPenalty_Fk_PenaltyTo",
                table: "AgentPenalty",
                column: "Fk_PenaltyTo");

            migrationBuilder.CreateIndex(
                name: "IX_AgentPenalty_Fk_StageId",
                table: "AgentPenalty",
                column: "Fk_StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Chassing_FK_CreatedBy",
                table: "Chassing",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Chassing_Fk_LeadId",
                table: "Chassing",
                column: "Fk_LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_Chassing_Fk_StatusId",
                table: "Chassing",
                column: "Fk_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ChassingFile_ChassingFileId",
                table: "ChassingFile",
                column: "ChassingFileId");

            migrationBuilder.CreateIndex(
                name: "IX_ChassingFile_Fk_ChassingId",
                table: "ChassingFile",
                column: "Fk_ChassingId");

            migrationBuilder.CreateIndex(
                name: "IX_ChassingFile_FK_CreatedBy",
                table: "ChassingFile",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ChassingFile_FK_LeadID",
                table: "ChassingFile",
                column: "FK_LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_ChassingVerifications_Fk_LeadId",
                table: "ChassingVerifications",
                column: "Fk_LeadId",
                unique: true,
                filter: "[Fk_LeadId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ChassingVerifications_Fk_StatusId",
                table: "ChassingVerifications",
                column: "Fk_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ChassingVerifications_Fk_UserId",
                table: "ChassingVerifications",
                column: "Fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Eligibilities_CreatedBy",
                table: "Eligibilities",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Eligibilities_FK_LeadID",
                table: "Eligibilities",
                column: "FK_LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_Eligibilities_FK_StatusId",
                table: "Eligibilities",
                column: "FK_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ForgetPasswords_Fk_UserId",
                table: "ForgetPasswords",
                column: "Fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssigneds_FK_AgentId",
                table: "LeadAssigneds",
                column: "FK_AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssigneds_FK_LeadId",
                table: "LeadAssigneds",
                column: "FK_LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssigneds_FK_StageId",
                table: "LeadAssigneds",
                column: "FK_StageId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadAssigneds_FK_SupervisorId",
                table: "LeadAssigneds",
                column: "FK_SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadComments_FK_CreatedBy",
                table: "LeadComments",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LeadComments_FK_LeadID",
                table: "LeadComments",
                column: "FK_LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadComments_Fk_StageId",
                table: "LeadComments",
                column: "Fk_StageId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadFiles_CreatedBy",
                table: "LeadFiles",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LeadFiles_FK_LeadID",
                table: "LeadFiles",
                column: "FK_LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadFiles_FK_StageId",
                table: "LeadFiles",
                column: "FK_StageId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadProducts_FK_CreatedBy",
                table: "LeadProducts",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LeadProducts_FK_LeadID",
                table: "LeadProducts",
                column: "FK_LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadProducts_FK_ProductId",
                table: "LeadProducts",
                column: "FK_ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadProducts_StageID",
                table: "LeadProducts",
                column: "StageID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadProducts_SubProductID",
                table: "LeadProducts",
                column: "SubProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_FK_CreatedBy",
                table: "Leads",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_Fk_ParentId",
                table: "Leads",
                column: "Fk_ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_Fk_PatientId",
                table: "Leads",
                column: "Fk_PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Leads_Fk_StatusId",
                table: "Leads",
                column: "Fk_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadStatuses_FK_CreatedBy",
                table: "LeadStatuses",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LeadStatuses_FK_LeadId",
                table: "LeadStatuses",
                column: "FK_LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadStatuses_FK_StatusId",
                table: "LeadStatuses",
                column: "FK_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadSubProducts_FK_CreatedBy",
                table: "LeadSubProducts",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_LeadSubProducts_FK_LeadID",
                table: "LeadSubProducts",
                column: "FK_LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_LeadSubProducts_FK_StagetId",
                table: "LeadSubProducts",
                column: "FK_StagetId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadSubProducts_FK_SubProductId",
                table: "LeadSubProducts",
                column: "FK_SubProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadSubProducts_ProductID",
                table: "LeadSubProducts",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientLogs_FK_CreatedBy",
                table: "PatientLogs",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PatientLogs_Fk_LeadId",
                table: "PatientLogs",
                column: "Fk_LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientLogs_FK_PatientID",
                table: "PatientLogs",
                column: "FK_PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientLogs_Fk_StageId",
                table: "PatientLogs",
                column: "Fk_StageId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientLogs_FK_StateId",
                table: "PatientLogs",
                column: "FK_StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_FK_CreatedBy",
                table: "Patients",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_FK_DeletedBy",
                table: "Patients",
                column: "FK_DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_FK_StateId",
                table: "Patients",
                column: "FK_StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_FK_UpdatedBy",
                table: "Patients",
                column: "FK_UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuestionAnswers_FK_CreatedBy",
                table: "ProductQuestionAnswers",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuestionAnswers_FK_LeadId",
                table: "ProductQuestionAnswers",
                column: "FK_LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuestionAnswers_FK_QuestionId",
                table: "ProductQuestionAnswers",
                column: "FK_QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuestions_FK_ProductId",
                table: "ProductQuestions",
                column: "FK_ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductQuestions_FK_StageId",
                table: "ProductQuestions",
                column: "FK_StageId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FK_CreatedBy",
                table: "Products",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Fk_DeletedBy",
                table: "Products",
                column: "Fk_DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Fk_UpdatedBy",
                table: "Products",
                column: "Fk_UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserID",
                table: "Products",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_QAFiles_CreatedBy",
                table: "QAFiles",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_QAFiles_FK_QAId",
                table: "QAFiles",
                column: "FK_QAId");

            migrationBuilder.CreateIndex(
                name: "IX_QaQuestionAnswers_FK_CreatedBy",
                table: "QaQuestionAnswers",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_QaQuestionAnswers_FK_QaID",
                table: "QaQuestionAnswers",
                column: "FK_QaID");

            migrationBuilder.CreateIndex(
                name: "IX_QaQuestionAnswers_FK_QuestionID",
                table: "QaQuestionAnswers",
                column: "FK_QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_QAs_FK_CreatedBy",
                table: "QAs",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_QAs_Fk_LeadID",
                table: "QAs",
                column: "Fk_LeadID");

            migrationBuilder.CreateIndex(
                name: "IX_QAs_Fk_StatusId",
                table: "QAs",
                column: "Fk_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_SNS_FK_CreatedBy",
                table: "SNS",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SNS_FK_LeadID",
                table: "SNS",
                column: "FK_LeadID",
                unique: true,
                filter: "[FK_LeadID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SNS_FK_StatusID",
                table: "SNS",
                column: "FK_StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_SNS_FK_UpdatedBy",
                table: "SNS",
                column: "FK_UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_Fk_StageId",
                table: "Statuses",
                column: "Fk_StageId");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_FK_CreatedBy",
                table: "SubProducts",
                column: "FK_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SubProducts_FK_ProductId",
                table: "SubProducts",
                column: "FK_ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBonus_Fk_BonusFrom",
                table: "UserBonus",
                column: "Fk_BonusFrom");

            migrationBuilder.CreateIndex(
                name: "IX_UserBonus_Fk_BonusTo",
                table: "UserBonus",
                column: "Fk_BonusTo");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_Fk_UserId",
                table: "UserLogins",
                column: "Fk_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Fk_ActDctBy",
                table: "Users",
                column: "Fk_ActDctBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Fk_BranchId",
                table: "Users",
                column: "Fk_BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Fk_CreatedBy",
                table: "Users",
                column: "Fk_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Fk_DeletedBy",
                table: "Users",
                column: "Fk_DeletedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Fk_UpdatedBy",
                table: "Users",
                column: "Fk_UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_Fk_CreatedBy",
                table: "UsersRoles",
                column: "Fk_CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_Fk_RoleId",
                table: "UsersRoles",
                column: "Fk_RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_Fk_UserId",
                table: "UsersRoles",
                column: "Fk_UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgentPenalty");

            migrationBuilder.DropTable(
                name: "ChassingFile");

            migrationBuilder.DropTable(
                name: "ChassingVerifications");

            migrationBuilder.DropTable(
                name: "Eligibilities");

            migrationBuilder.DropTable(
                name: "ForgetPasswords");

            migrationBuilder.DropTable(
                name: "LeadAssigneds");

            migrationBuilder.DropTable(
                name: "LeadComments");

            migrationBuilder.DropTable(
                name: "LeadFiles");

            migrationBuilder.DropTable(
                name: "LeadProducts");

            migrationBuilder.DropTable(
                name: "LeadStatuses");

            migrationBuilder.DropTable(
                name: "LeadSubProducts");

            migrationBuilder.DropTable(
                name: "MailsTrackings");

            migrationBuilder.DropTable(
                name: "PatientLogs");

            migrationBuilder.DropTable(
                name: "ProductQuestionAnswers");

            migrationBuilder.DropTable(
                name: "QAFiles");

            migrationBuilder.DropTable(
                name: "QaQuestionAnswers");

            migrationBuilder.DropTable(
                name: "SNS");

            migrationBuilder.DropTable(
                name: "UserBonus");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UsersRoles");

            migrationBuilder.DropTable(
                name: "Chassing");

            migrationBuilder.DropTable(
                name: "SubProducts");

            migrationBuilder.DropTable(
                name: "ProductQuestions");

            migrationBuilder.DropTable(
                name: "QAs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Leads");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Stages");

            migrationBuilder.DropTable(
                name: "Branches");
        }
    }
}
