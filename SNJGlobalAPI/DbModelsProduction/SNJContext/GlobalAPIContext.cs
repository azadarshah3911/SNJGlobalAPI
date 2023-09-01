using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog.Context;
using SNJGlobalAPI.DbModelsProduction;
using System.Reflection.Emit;

namespace SNJGlobalAPI.DbModels.SNJContext
{
    public partial class GlobalAPIContext : DbContext
    {
        public GlobalAPIContext(DbContextOptions<GlobalAPIContext> options)
           : base(options)
        {
        }
      
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<SubProduct> SubProducts { get; set; } = null!;
        #region Lead
        public virtual DbSet<Lead> Leads { get; set; } = null!;
        public virtual DbSet<LeadSubProduct> LeadSubProducts { get; set; } = null!;
        public virtual DbSet<LeadProduct> LeadProducts { get; set; } = null!;
        public virtual DbSet<LeadComments> LeadComments { get; set; } = null!;
        public virtual DbSet<LeadFile> LeadFiles { get; set; } = null!;
        public virtual DbSet<LeadStatus> LeadStatuses { get; set; } = null!;
        #endregion
        public virtual DbSet<User> Users { get; set; } = null!; 
        public virtual DbSet<Eligibility> Eligibilities  { get; set; } = null!;
        public virtual DbSet<State> States  { get; set; } = null!;
        public virtual DbSet<PatientLogs> PatientLogs { get; set; } = null!;

        //by zameer
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UsersRoles { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<ForgetPassword> ForgetPasswords { get; set; }
        public DbSet<MailsTracking> MailsTrackings { get; set; }


        public DbSet<Stage> Stages { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<ProductQuestion> ProductQuestions { get; set; }
        public DbSet<ProductQuestionAnswer> ProductQuestionAnswers { get; set; }
        public DbSet<QA> QAs { get; set; }
        public DbSet<QaQuestionAnswer> QaQuestionAnswers { get; set; }
        public DbSet<LeadAssigned> LeadAssigneds { get; set; }
        public DbSet<UserBonus> UserBonus { get; set; }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<ChassingVerification> ChassingVerifications { get; set; }
        public DbSet<Confirmation> Confirmations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
            //User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(o => o.creator)
               .WithMany(m => m.userCreators)
               .HasForeignKey(f => f.Fk_CreatedBy)
               .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.updator)
               .WithMany(m => m.userUpdators)
               .HasForeignKey(f => f.Fk_UpdatedBy)
               .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.deletor)
               .WithMany(m => m.userDeletors)
               .HasForeignKey(f => f.Fk_DeletedBy)
               .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.actDctBy)
               .WithMany(m => m.userActDcts)
               .HasForeignKey(f => f.Fk_ActDctBy)
               .OnDelete(DeleteBehavior.NoAction);
            });

            //UserRoles
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasOne(o => o.user)
               .WithMany(m => m.usersRoles)
               .HasForeignKey(f => f.Fk_UserId)
               .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.creator)
               .WithMany(m => m.userRolesCreators)
               .HasForeignKey(f => f.Fk_CreatedBy)
               .OnDelete(DeleteBehavior.NoAction);
            });


            //Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(o => o.CreatedBy)
                .WithMany(m => m.CreatedProducts)
                .HasForeignKey(f => f.FK_CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.UpdatedBy)
                .WithMany(m => m.UpdatedProducts)
                .HasForeignKey(f => f.Fk_UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.DeletedBy)
                .WithMany(m => m.DeletedProducts)
                .HasForeignKey(f => f.Fk_DeletedBy)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.HasOne(o => o.CreatedBy)
                .WithMany(m => m.CreatedPatients)
                .HasForeignKey(f => f.FK_CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.UpdatedBy)
                .WithMany(m => m.UpdatedPatients)
                .HasForeignKey(f => f.FK_UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.DeletedBy)
                .WithMany(m => m.DeletedPatients)
                .HasForeignKey(f => f.FK_DeletedBy)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<AgentPenalty>(entity =>
            {
                entity.HasOne(o => o.PenaltyFrom)
                .WithMany(m => m.PenaltyFrom)
                .HasForeignKey(f => f.Fk_PenaltyFrom)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.PenaltyTo)
                .WithMany(m => m.PenaltyTo)
                .HasForeignKey(f => f.Fk_PenaltyTo)
                .OnDelete(DeleteBehavior.NoAction);
            });
            modelBuilder.Entity<SNS>(entity =>
            {
                entity.HasOne(o => o.CreatedBy)
                .WithMany(m => m.CreatedSNS)
                .HasForeignKey(f => f.FK_CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.UpdateBy)
                .WithMany(m => m.UpdatedSNS)
                .HasForeignKey(f => f.FK_UpdatedBy)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<LeadAssigned>(entity =>
            {
                entity.HasOne(o => o.Agent)
                .WithMany(m => m.LeadAssignedsTo)
                .HasForeignKey(f => f.FK_AgentId)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.Supervisor)
                .WithMany(m => m.LeadAssignedsFrom)
                .HasForeignKey(f => f.FK_SupervisorId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<UserBonus>(entity =>
            {
                entity.HasOne(o => o.BonusTo)
                .WithMany(m => m.BonusesTo)
                .HasForeignKey(f => f.Fk_BonusTo)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.BonusFrom)
                .WithMany(m => m.BonusesFrom)
                .HasForeignKey(f => f.Fk_BonusFrom)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Lead>(entity =>
            {
                entity.HasOne(o => o.CreatedBy)
                .WithMany(m => m.Lead)
                .HasForeignKey(f => f.FK_CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(o => o.DeletedBy)
                .WithMany(m => m.DeletedByLead)
                .HasForeignKey(f => f.FK_DeletedBy)
                .OnDelete(DeleteBehavior.NoAction);
            });


            //OnModelCreatingPartial(modelBuilder);
            SeedStages(modelBuilder);
            SeedStatus(modelBuilder);
            SeedBranches(modelBuilder);
            GlobalQueryFilter(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        private static void SeedStages(ModelBuilder builder)
        {
            builder.Entity<Stage>().HasData(
                new Stage()
                {
                    ID = 1,
                    Name = "Lead",
                    StageNo = 1,
                },
                new Stage()
                {
                    ID = 2,
                    Name = "Eligibility Verification",
                    StageNo = 2,
                },
                new Stage()
                {
                    ID = 3,
                    Name = "SNS Check",
                    StageNo = 3,
                },
                new Stage()
                {ID = 4,
                    Name = "QA Verification",
                    StageNo = 4,
                },
                new Stage()
                {
                    ID = 5,
                    Name = "Chassing Lead",
                    StageNo = 5,
                },
                new Stage()
                {
                    ID = 6,
                    Name = "Confirmation",
                    StageNo = 6,
                },
                //This Stage Comes Befor Chassing 
                new Stage()
                {
                    ID = 7,
                    Name = "Chassing Verification",
                    StageNo = 7,
                },
                //After Confiramtion Pedning Stage
                new Stage()
                {
                    ID = 8,
                    Name = "Full Fill",
                    StageNo = 8,
                });
        }

        private static void SeedBranches(ModelBuilder builder)
        {
            builder.Entity<Branch>().HasData(
                new Branch()
                {
                    ID = 1,
                    Name = "DHA",
                },
                new Stage()
                {
                    ID = 2,
                    Name = "PECHS",
                });
        }
        private static void SeedStatus(ModelBuilder builder)
        {
            builder.Entity<Status>().HasData(
                #region Lead Status
                new Status()
                {
                    ID = 1,
                    Name = "New Lead",
                    Fk_StageId = 1,
                },new Status()
                {
                    ID = 2,
                    Name = "SNS Pending",
                    Fk_StageId = 1,
                }, new Status()
                {
                    ID = 3,
                    Name = "QA Pending",
                    Fk_StageId = 1,
                }, new Status()
                {
                    ID = 14,
                    Name = "QA Master",
                    Fk_StageId = 4,
                }, new Status()
                {
                    ID = 15,
                    Name = "Chassing Pending",
                    Fk_StageId = 1,
                }
                ,
                #endregion
                #region Eligibility Status
                 new Status()
                 {
                     ID = 4,
                     Name = "Ev Error",
                     Fk_StageId = 2,
                 },
                 new Status()
                 {
                     ID = 5,
                     Name = "Medicare B",
                     Fk_StageId = 2,
                 },
                 new Status()
                 {
                     ID = 6,
                     Name = "HMO",
                     Fk_StageId = 2,
                 },
                 new Status()
                 {
                     ID = 7,
                     Name = "PPO",
                     Fk_StageId = 2,
                 },
                 new Status()
                 {
                     ID = 8,
                     Name = "PPD",
                     Fk_StageId = 2,
                 },
                 new Status()
                 {
                     ID = 9,
                     Name = "Part B In Active",
                     Fk_StageId = 2,
                 },
                 new Status()
                 {
                     ID = 10,
                     Name = "In Active(PT Dead)",
                     Fk_StageId = 2,
                 }
                 #endregion
                 #region SNS Status
                 , new Status()
                 {
                     ID = 11,
                     Name = "Pass",
                     Fk_StageId = 3,
                 }, new Status()
                 {
                     ID = 12,
                     Name = "Fail",
                     Fk_StageId = 3,
                 },
                  new Status()
                  {
                      ID = 13,
                      Name = "By Pass",
                      Fk_StageId = 3,
                  }
                  #endregion
                  , new Status()
                  {
                      ID = 16,
                      Name = "Zero Processed",
                      Fk_StageId = 4,
                  }, new Status()
                  {
                      ID = 17,
                      Name = "QA Qualified",
                      Fk_StageId = 4,
                  },
                  new Status()
                  {
                      ID = 18,
                      Name = "QA Re-Examine",
                      Fk_StageId = 1,
                  },
                  new Status()
                  {
                      ID = 19,
                      Name = "SNS Fail",
                      Fk_StageId = 1,
                  },
                  new Status()
                  {
                      ID = 20,
                      Name = "Pending",
                      Fk_StageId = 5,
                  },
                  new Status()
                  {
                      ID = 21,
                      Name = "Approved",
                      Fk_StageId = 5,
                  },
                  new Status()
                  {
                      ID = 22,
                      Name = "Reject",
                      Fk_StageId = 5,
                  },
                  new Status()
                  {
                      ID = 23,
                      Name = "Confirmation Pending",
                      Fk_StageId = 1,
                  },
                  new Status()
                  {
                      ID = 24,
                      Name = "SNS Error",
                      Fk_StageId = 3,
                  },
                  new Status()
                  {
                      ID = 25,
                      Name = "QA Error",
                      Fk_StageId = 4,
                  },
                  new Status()
                  {
                      ID = 26,
                      Name = "Chassing Fail",
                      Fk_StageId = 1,
                  },
                  new Status()
                  {
                      ID = 27,
                      Name = "Not Qualified",
                      Fk_StageId = 4,
                  },
                  new Status()
                  {
                      ID = 28,
                      Name = "Call Verification Pending",
                      Fk_StageId = 1,
                  },
                  new Status()
                  {
                      ID = 29,
                      Name = "Start Chassing",
                      Fk_StageId = 7,
                  },
                  new Status()
                  {
                      ID = 30,
                      Name = "Denied Chassing",
                      Fk_StageId = 7,
                  },
                  new Status()
                  {
                      ID = 31,
                      Name = "Agent Approved",
                      Fk_StageId = 5,
                  }
                  ,
                  new Status()
                  {
                      ID = 32,
                      Name = "Done",
                      Fk_StageId = 6,
                  },
                  new Status()
                  {
                      ID = 33,
                      Name = "Denied",
                      Fk_StageId = 6,
                  },
                  new Status()
                  {
                      ID = 34,
                      Name = "Full Fill",
                      Fk_StageId = 1,
                  },
                  new Status()
                  {
                      ID = 35,
                      Name = "Other Plan",
                      Fk_StageId = 1,
                  },
                  new Status()
                  {
                      ID = 36,
                      Name = "Can't Process",
                      Fk_StageId = 5,
                  },
                  new Status()
                  {
                      ID = 37,
                      Name = "Chart Notes",
                      Fk_StageId = 5,
                  },
                  new Status()
                  {
                      ID = 38,
                      Name = "Chassing Halfway",
                      Fk_StageId = 1,
                  });
        }

        private static void GlobalQueryFilter(ModelBuilder builder)
        {
            builder.Entity<Lead>()
                  .HasQueryFilter(filter => filter.DeletedAt == null);

        }
    }
}
