using AutoMapper;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SNJGlobalAPI.DbModels;
using SNJGlobalAPI.DbModelsProduction;
using SNJGlobalAPI.DtoModelsProduction;
using System;
using System.Reflection.Metadata.Ecma335;

namespace SNJGlobalAPI.Mappers
{
    public class DashboardMapper
    {
        //daily each agents staatus
        public static MapperConfiguration DailyAgentStatusCount()
        {
            var crntDate = DateTime.UtcNow.Date;
            return new(
            c =>
            {
                c.CreateProjection<User, DailyAgentsReportDto>()
                .ForMember(d => d.AgentId, x => x.MapFrom(s => s.ID))
                .ForMember(d => d.AgentName, x => x.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(d => d.NewLead, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 1 && w.CreatedAt.Date == crntDate).Count()))
                .ForMember(d => d.QaPending, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 3 && w.CreatedAt.Date == crntDate).Count()))
                .ForMember(d => d.Processing, x => x.MapFrom(s => s.Lead.SelectMany(selector => selector.QA.Where(w => w.Fk_StatusId == 17 && w.CreatedAt.Date == crntDate).Select(subSelector => subSelector.Fk_StatusId)).Count()))
                .ForMember(d => d.EvError, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 4 && w.CreatedAt.Date == crntDate).Count()))
                .ForMember(d => d.QaReExamine, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 18 && w.CreatedAt.Date == crntDate).Count()))
                .ForMember(d => d.SnsFail, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 19 && w.CreatedAt.Date == crntDate).Count()))
                .ForMember(d => d.Total, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 1 || w.Fk_StatusId == 4 || w.Fk_StatusId == 3 || w.Fk_StatusId == 15 || w.Fk_StatusId == 2 && w.CreatedAt.Date == crntDate).Count()));

            });
        }

        public static MapperConfiguration MonthlyAgentStatusCount()
        {
            var crntDate = DateTime.UtcNow.Month;
            return new(
            c =>
            {
                c.CreateProjection<User, DailyAgentsReportDto>()
                .ForMember(d => d.AgentId, x => x.MapFrom(s => s.ID))
                .ForMember(d => d.AgentName, x => x.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(d => d.NewLead, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 1 && w.CreatedAt.Month == crntDate).Count()))
                .ForMember(d => d.QaPending, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 3 && w.CreatedAt.Month == crntDate).Count()))
                .ForMember(d => d.Processing, x => x.MapFrom(s => s.Lead.SelectMany(selector => selector.QA.Where(w => w.Fk_StatusId == 17 && w.CreatedAt.Month == crntDate).Select(subSelector => subSelector.Fk_StatusId)).Count()))
                .ForMember(d => d.EvError, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 4 && w.CreatedAt.Month == crntDate).Count()))
                .ForMember(d => d.QaReExamine, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 18 && w.CreatedAt.Month == crntDate).Count()))
                .ForMember(d => d.SnsFail, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 19 && w.CreatedAt.Month == crntDate).Count()))
                .ForMember(d => d.Total, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 1 || w.Fk_StatusId == 4 || w.Fk_StatusId == 3 || w.Fk_StatusId == 15 || w.Fk_StatusId == 2 && w.CreatedAt.Month == crntDate).Count()));

            });
        }

        public static MapperConfiguration DailyAgentsListReport()
        {
            var crntDate = DateTime.UtcNow.Date;
            return new(
            c =>
            {
                c.CreateProjection<User, DailyAgentsReportDto>()
                .ForMember(d => d.AgentId, x => x.MapFrom(s => s.ID))
                .ForMember(d => d.AgentName, x => x.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(d => d.NewLead, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 1 && w.CreatedAt.Date == crntDate).Count()))
                .ForMember(d => d.QaPending, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 3 && w.CreatedAt.Date == DateTime.Today.Date.Date).Count()))
                .ForMember(d => d.Processing, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 15 && w.CreatedAt.Date == crntDate).Count()))
                .ForMember(d => d.EvError, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 4 && w.CreatedAt.Date == crntDate).Count()))
                .ForMember(d => d.Total, x => x.MapFrom(s => s.Lead.Where(w => w.Fk_StatusId == 1 || w.Fk_StatusId == 4 || w.Fk_StatusId == 3 || w.Fk_StatusId == 15 || w.Fk_StatusId == 2 && w.CreatedAt.Date == crntDate).Count()))
                .ForMember(d => d.Penalty, x => x.MapFrom(s => s.PenaltyTo.Where(w => w.CreatedAt.Date == crntDate).Sum(sum => sum.Amount)))
                .ForMember(d => d.Bonus, x => x.MapFrom(s => s.BonusesTo
                    .Where(w => w.CreatedAt.Date == crntDate)
                    .OrderByDescending(m => m.ID).FirstOrDefault().Amount));
            });
        }

        public class AnnonymousType
        {
            public DateTime Date { get; set; }
            public int Count { get; set; }

        }

        public static MapperConfiguration MonthlyAgentListReport(int month,int year)
        {
            var sourceAnonymousType = new { Date = default(DateTime), Count = default(int) };
            var crnt = DateTime.UtcNow;
            var monthStart = new DateTime(year == 0 ? crnt.Year : year, month == 0 ? crnt.Month : month, 1);
            var monthEnd = monthStart.AddMonths(1);

            return new(c =>
            {
                c.CreateProjection<User, MonthlyAgentReportDto>()
                    .ForMember(d => d.AgentId, x => x.MapFrom(s => s.ID))
                    .ForMember(d => d.AgentName, x => x.MapFrom(s => s.FirstName + " " + s.LastName))
                    .ForMember(d => d.TotalMonth, x => x.MapFrom(s => s.Lead.Count(w => w.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId == 17 && w.CreatedAt.Date >= monthStart && w.CreatedAt.Date < monthEnd)))
                    .ForMember(d => d.days, x => x.MapFrom(s => s.Lead
                        .Where(w => w.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId == 17 && w.CreatedAt.Date >= monthStart && w.CreatedAt.Date < monthEnd)));

                c.CreateProjection<Lead, LeadCountDto>()
                    .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.CreatedAt.Day));
            });
        }

        public static MapperConfiguration GetExcelDetails = new
               (
                   c =>
                   {
                       //List For Lead And Patient Infoemation
                       c.CreateProjection<Lead, LeadFullDetailListDto>()
                       .ForMember(a => a.Patient, o => o.MapFrom(p => p.Patient))
                       .ForMember(a => a.Lead, o => o.MapFrom(p => p))
                       .ForMember(a => a.Eligibilities, o => o.MapFrom(p => p.Eligibilities.OrderByDescending(o => o.ID).FirstOrDefault()))
                       .ForMember(a => a.SubProducts, o => o.MapFrom(p => p.LeadSubProducts.Where(w => w.IsApproved)))
                       .ForMember(a => a.Sns, o => o.MapFrom(p => p.SNS))
                       .ForMember(a => a.Qas, o => o.MapFrom(p => p.QA.OrderByDescending(o => o.ID).FirstOrDefault()))
                       .ForMember(a => a.LeadFiles, o => o.MapFrom(p => p.LeadFiles.Select(s => s.File)))
                       .ForMember(a => a.QaFiles, o => o.MapFrom(p => p.QA.SelectMany(s => s.Files).Select(s => s.File)))
                       .ForMember(a => a.Chassing, o => o.MapFrom(p => p.Chassings.OrderByDescending(order => order.Id)))
                        .ForMember(a => a.FirstStageQuesAns, o => o.MapFrom(p => p.ProductQuestionAnswer))
                       .ForMember(a => a.ChassingFiles, o => o.MapFrom(p => p.ChassingFiles.SelectMany(s => s.ChassingFiles).Select(s => s.File)));

                       //Patient
                       c.CreateProjection<Patient, GetPatientForSnsDto>()
                       .ForMember(a => a.StateCode, o => o.MapFrom(m => m.State.Code))
                       .ForMember(a => a.State, o => o.MapFrom(m => m.State.Name));

                       //Lead
                       c.CreateProjection<Lead, GetLeadForSnsDto>()
                       .ForMember(a => a.AgentName, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName))
                       .ForMember(a => a.AgentBranch, o => o.MapFrom(m => m.CreatedBy.branch.Name))
                       .ForMember(a => a.AgentId, o => o.MapFrom(m => m.CreatedBy.ID))
                       .ForMember(a => a.LeadId, o => o.MapFrom(m => m.ID))
                       .ForMember(a => a.LeadStatus, o => o.MapFrom(m => m.Status.Name));

                       //Eligibilities
                       c.CreateProjection<Eligibility, GetEligibilityForDetailsDto>()
                       .ForMember(a => a.Status, o => o.MapFrom(m => m.Status.Name))
                       .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.User.FirstName + " " + m.User.LastName));

                       //SNS
                       c.CreateProjection<SNS, GetSnsForQaDto>()
                       .ForMember(a => a.Status, o => o.MapFrom(m => m.Status.Name))
                       .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName));

                       //Sub Product
                       c.CreateProjection<LeadSubProduct, GetSubProductsInLeadForQaDto>()
                       .ForMember(a => a.ID, o => o.MapFrom(m => m.SubProduct.ID))
                       .ForMember(a => a.Code, o => o.MapFrom(m => m.SubProduct.Code))
                       .ForMember(a => a.Name, o => o.MapFrom(m => m.SubProduct.Name))
                       .ForMember(a => a.ProductName, o => o.MapFrom(m => m.SubProduct.Product.Name));

                       //QA List
                       c.CreateProjection<QA, GetQAForLeadDto>()
                       .ForMember(a => a.Status, o => o.MapFrom(m => m.Status.Name))
                       .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName))
                       .ForMember(a => a.Answers, o => o.MapFrom(m => m.Answers));

                       //For Question Answer
                       c.CreateProjection<ProductQuestionAnswer, GetLeadQuestionAnsDto>()
                       .ForMember(a => a.QuestionId, o => o.MapFrom(p => p.ProductQuestion.ID))
                       .ForMember(a => a.AnswerId, o => o.MapFrom(p => p.ProductQuestion.ID))
                       .ForMember(a => a.Question, o => o.MapFrom(p => p.ProductQuestion.Question))
                       .ForMember(a => a.ProductName, o => o.MapFrom(p => p.ProductQuestion.Product.Name));


                       //QA Answer List
                       c.CreateProjection<QaQuestionAnswer, GetQaAnswerForLeadDto>()
                       .ForMember(a => a.QuestionId, o => o.MapFrom(m => m.Question.ID))
                       .ForMember(a => a.Question, o => o.MapFrom(m => m.Question.Question))
                       .ForMember(a => a.ProductName, o => o.MapFrom(m => m.Question.Product.Name));

                       //Chassing List
                       c.CreateProjection<Chassing, GetProcessedForExcelDto>()
                       .ForMember(a => a.Status, o => o.MapFrom(m => m.Status.Name))
                       .ForMember(a => a.Fk_StatusId, o => o.MapFrom(m => m.Status.ID))
                       .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName));

                   });


        public static MapperConfiguration DailyAssignQaLeadsReport()
        {
            var crntDate = DateTime.UtcNow.Date;
            return new(
            c =>
            {
                c.CreateProjection<User, GetDailyAssignedLeads>()
                .ForMember(d => d.AgentId, x => x.MapFrom(s => s.ID))
                .ForMember(d => d.AgentName, x => x.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(d => d.Branch, x => x.MapFrom(s => s.branch.Name))
                .ForMember(d => d.Assigned, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 4 && w.CreatedAt.Date == crntDate).Count()))
                .ForMember(d => d.Pending, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 4 && (w.Lead.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId == null) && w.CreatedAt.Date == crntDate).Count()))
                .ForMember(d => d.LastDayPending, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 4 && (w.Lead.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId == null) && w.CreatedAt.Date == crntDate.AddDays(-1)).Count()))
                .ForMember(d => d.Complete, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 4 && (w.Lead.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId != null) && w.CreatedAt.Date == crntDate).Count()));
            });
        }

        public static MapperConfiguration MonthlyAssignQaLeadsReport(MonthYearDto dto)
        {
            var sourceAnonymousType = new { Date = default(DateTime), Count = default(int) };
            var crnt = DateTime.UtcNow;
            var monthStart = new DateTime(dto.Year == null ? crnt.Year : dto.Year ?? 0, dto.Month == null ? crnt.Month : dto.Month ?? 0, 1);
            var monthEnd = monthStart.AddMonths(1);

            return new(
            c =>
            {
                c.CreateProjection<User, GetDailyAssignedLeads>()
                .ForMember(d => d.AgentId, x => x.MapFrom(s => s.ID))
                .ForMember(d => d.AgentName, x => x.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(d => d.Branch, x => x.MapFrom(s => s.branch.Name))
                .ForMember(d => d.Assigned, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 4 && w.CreatedAt.Date >= monthStart && w.CreatedAt.Date < monthEnd).Count()))
                .ForMember(d => d.Pending, x => x.MapFrom(s => 
                s.LeadAssignedsTo.Where(w => w.FK_StageId == 4 
                && (w.Lead.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId == null) 
                && w.CreatedAt.Date >= monthStart.AddMonths(-1) && w.CreatedAt.Date < monthEnd.AddMonths(-1)).Count()))

                .ForMember(d => d.LastDayPending, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 4 
                && (w.Lead.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId == null) 
                && w.CreatedAt.Date >= monthStart && w.CreatedAt.Date < monthEnd
                ).Count()))
                .ForMember(d => d.Complete, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 4 && (w.Lead.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId != null) && w.CreatedAt.Date >= monthStart && w.CreatedAt.Date < monthEnd).Count()));
            });
        }

        public static MapperConfiguration MonthlyAssignQaLeadsDetailReport()
        {
            var sourceAnonymousType = new { Date = default(DateTime), Count = default(int) };
            var crnt = DateTime.UtcNow;
            var monthStart = new DateTime(crnt.Year, crnt.Month, 1);
            var monthEnd = monthStart.AddMonths(1);

            return new(
            c =>
            {
                c.CreateProjection<User, GetAssignedLeadDetailDto>()
                .ForMember(d => d.Day, x => x.MapFrom(s => s.LeadAssignedsTo.FirstOrDefault().CreatedAt.Date))
                .ForMember(d => d.AgentId, x => x.MapFrom(s => s.ID))
                .ForMember(d => d.AgentName, x => x.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(d => d.Assigned, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 4 && w.CreatedAt.Date >= monthStart && w.CreatedAt.Date < monthEnd).Count()))
                .ForMember(d => d.Pending, x => x.MapFrom(s =>
                s.LeadAssignedsTo.Where(w => w.FK_StageId == 4
                && (w.Lead.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId == null)
                && w.CreatedAt.Date >= monthStart.AddMonths(-1) && w.CreatedAt.Date < monthEnd.AddMonths(-1)).Count()))

                .ForMember(d => d.LastDayPending, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 4
                && (w.Lead.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId == null)
                && w.CreatedAt.Date >= monthStart && w.CreatedAt.Date < monthEnd
                ).Count()))
                .ForMember(d => d.Complete, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 4 && (w.Lead.QA.OrderByDescending(order => order.ID).FirstOrDefault().Fk_StatusId != null) && w.CreatedAt.Date >= monthStart && w.CreatedAt.Date < monthEnd).Count())
                );
            });
        }


        public static MapperConfiguration DailyAssignChassingLeadsReport()
        {

            var crntDate = DateTime.UtcNow.Date;
            return new(
            c =>
            {
                c.CreateProjection<User, GetDailyAssignedLeads>()
                .ForMember(d => d.AgentId, x => x.MapFrom(s => s.ID))
                .ForMember(d => d.AgentName, x => x.MapFrom(s => s.FirstName + " " + s.LastName))
                .ForMember(d => d.Branch, x => x.MapFrom(s => s.branch.Name))
                .ForMember(d => d.Assigned, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 5 && w.CreatedAt.Date == crntDate).Count()))
                .ForMember(d => d.Pending, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 5 && (w.Lead.Chassings.OrderByDescending(order => order.Id).FirstOrDefault().Fk_StatusId == null) && w.CreatedAt.Date == crntDate).Count()))
                .ForMember(d => d.LastDayPending, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 5 && (w.Lead.Chassings.OrderByDescending(order => order.Id).FirstOrDefault().Fk_StatusId == null) && w.CreatedAt.Date == crntDate.AddDays(-1)).Count()))
                .ForMember(d => d.Complete, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 5 && (w.Lead.Chassings.OrderByDescending(order => order.Id).FirstOrDefault().Fk_StatusId != null) && w.CreatedAt.Date == crntDate).Count()));
            });
        }



        public static MapperConfiguration MonthlyAssignChassingLeadsReport(MonthYearDto dto)
        {
            var sourceAnonymousType = new { Date = default(DateTime), Count = default(int) };
            var crnt = DateTime.UtcNow;
            var monthStart = new DateTime(dto.Year == null ? crnt.Year : dto.Year ?? 0, dto.Month == null ? crnt.Month : dto.Month ?? 0, 1);
            var monthEnd = monthStart.AddMonths(1);
            return new(
              c =>
              {
                  c.CreateProjection<User, GetDailyAssignedLeads>()
                  .ForMember(d => d.AgentId, x => x.MapFrom(s => s.ID))
                  .ForMember(d => d.AgentName, x => x.MapFrom(s => s.FirstName + " " + s.LastName))
                  .ForMember(d => d.Branch, x => x.MapFrom(s => s.branch.Name))
                  .ForMember(d => d.Assigned, x => x.MapFrom(s => 
                  s.LeadAssignedsTo.Where(w => w.FK_StageId == 5 && w.CreatedAt.Date >= monthStart && w.CreatedAt.Date < monthEnd).Count()))
                  .ForMember(d => d.Pending, x => x.MapFrom(s =>
                  s.LeadAssignedsTo.Where(w => w.FK_StageId == 5
                  && (w.Lead.Chassings.OrderByDescending(order => order.Id).FirstOrDefault().Fk_StatusId == null)
                  && w.CreatedAt.Date >= monthStart.AddMonths(-1) && w.CreatedAt.Date < monthEnd.AddMonths(-1)).Count()))
                  .ForMember(d => d.LastDayPending, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 5
                  && (w.Lead.Chassings.OrderByDescending(order => order.Id).FirstOrDefault().Fk_StatusId == null)
                  && w.CreatedAt.Date >= monthStart && w.CreatedAt.Date < monthEnd
                  ).Count()))
                  .ForMember(d => d.Complete, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 5 && (w.Lead.Chassings.OrderByDescending(order => order.Id).FirstOrDefault().Fk_StatusId != null) && w.CreatedAt.Date >= monthStart && w.CreatedAt.Date < monthEnd).Count()));
              });
        }
        //public static MapperConfiguration DailyAssignChassingLeadsReport()
        //{
        //    var crntDate = DateTime.UtcNow.Date;
        //    return new MapperConfiguration(c =>
        //    {
        //        c.CreateProjection<User, GetDailyAssignedLeads>()
        //            .ForMember(d => d.AgentId, x => x.MapFrom(s => s.ID))
        //            .ForMember(d => d.AgentName, x => x.MapFrom(s => s.FirstName + " " + s.LastName))
        //            .ForMember(d => d.Branch, x => x.MapFrom(s => s.branch.Name))
        //            .ForMember(d => d.Assigned, x => x.MapFrom(s => s.LeadAssignedsTo.Count(w => w.FK_StageId == 5 && w.CreatedAt.Date == crntDate)))
        //            .ForMember(d => d.Pending, x => x.MapFrom(s => s.LeadAssignedsTo.Count(w => w.FK_StageId == 5 && w.Lead.Fk_StatusId != 20 && w.Lead.Fk_StatusId != 22 && w.CreatedAt.Date == crntDate)))
        //            .ForMember(d => d.Complete, x => x.MapFrom(s => s.LeadAssignedsTo.Count(w => w.FK_StageId == 5 && (w.Lead.Fk_StatusId == 20 || w.Lead.Fk_StatusId == 22) && w.CreatedAt.Date == crntDate)))
        //            .ForMember(d => d.Ratio, x => x.MapFrom(s => s.LeadAssignedsTo.Count() < 0 || s.LeadAssignedsTo == null ? 0 : CalculateDailyRatio(s, crntDate, s.LeadAssignedsTo))); // Pass the LeadAssignedsTo collection
        //    });
        //}

        //private static double CalculateDailyRatio(User user, DateTime currentDate, ICollection<LeadAssigned> leadAssignedsTo)
        //{
        //    // Calculate the remaining leads from the previous day
        //    var previousDay = currentDate.AddDays(-1);

        //    if (leadAssignedsTo.Where(predicate => predicate.CreatedAt.Date == previousDay).ToList() is null)
        //        return 0; // Return 0 if totalOrders is 0 to avoid division by zero


        //    var remainingLeadsPreviousDay = leadAssignedsTo.Where(w => w.FK_StageId == 5 && w.Lead.Fk_StatusId != 20 && w.Lead.Fk_StatusId != 22 && w.CreatedAt.Date == previousDay).Count();

        //    // Get the total orders for the current day
        //    var totalOrders = leadAssignedsTo.Count(w => w.FK_StageId == 5 && w.CreatedAt.Date == currentDate);

        //    // Calculate and return the daily ratio
        //    if (totalOrders > 0)
        //    {
        //        return (double)remainingLeadsPreviousDay / totalOrders;
        //    }
        //    return 0; // Return 0 if totalOrders is 0 to avoid division by zero
        //}



        //public static MapperConfiguration MonthlyAssignChassingLeadsReport()
        //{
        //    var crntDate = DateTime.UtcNow.Month;
        //    return new MapperConfiguration(c =>
        //    {
        //        c.CreateProjection<User, GetDailyAssignedLeads>()
        //            .ForMember(d => d.AgentId, x => x.MapFrom(s => s.ID))
        //            .ForMember(d => d.AgentName, x => x.MapFrom(s => s.FirstName + " " + s.LastName))
        //            .ForMember(d => d.Branch, x => x.MapFrom(s => s.branch.Name))
        //            .ForMember(d => d.Assigned, x => x.MapFrom(s => s.LeadAssignedsTo.Count(w => w.FK_StageId == 5 && w.CreatedAt.Month == crntDate)))
        //            .ForMember(d => d.Pending, x => x.MapFrom(s => s.LeadAssignedsTo.Count(w => w.FK_StageId == 5 && w.Lead.Fk_StatusId != 20 && w.Lead.Fk_StatusId != 22 && w.CreatedAt.Month == crntDate)))
        //            .ForMember(d => d.Complete, x => x.MapFrom(s => s.LeadAssignedsTo.Count(w => w.FK_StageId == 5 && w.Lead.Fk_StatusId == 20 || w.Lead.Fk_StatusId == 22) && w.CreatedAt.Month == crntDate)));
        //            //.ForMember(d => d.Ratio, x => x.MapFrom(s => CalculateMonthlyRatio(s, crntDate)));
        //    });
        //}

        //private static double CalculateMonthlyRatio(User user, int currentMonth)
        //{
        //    // Calculate the remaining leads from the last month that don't have status 20 or 22
        //    var remainingLeadsLastMonth = user.LeadAssignedsTo
        //        .Count(w => w.FK_StageId == 5 && w.Lead.Fk_StatusId != 20 && w.Lead.Fk_StatusId != 22 && w.CreatedAt.Month == currentMonth - 1);

        //    // Get the total orders for the current month
        //    var totalOrders = user.LeadAssignedsTo
        //        .Count(w => w.FK_StageId == 5 && w.CreatedAt.Month == currentMonth);

        //    // Calculate and return the ratio
        //    if (totalOrders > 0)
        //    {
        //        return (double)remainingLeadsLastMonth / totalOrders;
        //    }
        //    return 0; // Return 0 if totalOrders is 0 to avoid division by zero
        //}


        //public static MapperConfiguration DailyAssignProcessingLeadsReport()
        //{
        //    var crntDate = DateTime.Today.Date;
        //    return new(
        //    c =>
        //    {
        //        c.CreateProjection<User, GetDailyAssignedLeads>()
        //        .ForMember(d => d.AgentId, x => x.MapFrom(s => s.ID))
        //        .ForMember(d => d.AgentName, x => x.MapFrom(s => s.FirstName + " " + s.LastName))
        //        .ForMember(d => d.Branch, x => x.MapFrom(s => s.branch.Name))
        //        .ForMember(d => d.Assigned, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 5 && w.CreatedAt.Date == crntDate).Count()))
        //        .ForMember(d => d.Pending, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 5 && w.Lead.Fk_StatusId != 15 && w.CreatedAt.Date == crntDate).Count()))
        //        .ForMember(d => d.Complete, x => x.MapFrom(s => s.LeadAssignedsTo.Where(w => w.FK_StageId == 5 && w.Lead.Fk_StatusId == 15 && w.CreatedAt.Date == crntDate).Count()));
        //    });
        //}



        public static MapperConfiguration GetPdfDetails = new
               (
                   c =>
                   {
                       //List For Lead And Patient Infoemation
                       c.CreateProjection<Lead, PdfDto>()
                       .ForMember(a => a.Patient, o => o.MapFrom(p => p.Patient))
                       .ForMember(a => a.Eligibilities, o => o.MapFrom(p => p.Eligibilities.OrderByDescending(o => o.ID).FirstOrDefault()))
                       .ForMember(a => a.SubProducts, o => o.MapFrom(p => p.LeadSubProducts.Where(w => w.IsApproved)))
                       .ForMember(a => a.Qas, o => o.MapFrom(p => p.QA.OrderByDescending(o => o.ID).FirstOrDefault()))
                       .ForMember(a => a.FirstStageQuesAns, o => o.MapFrom(p => p.ProductQuestionAnswer));

                       //Patient
                       c.CreateProjection<Patient, GetPatientForSnsDto>()
                       .ForMember(a => a.StateCode, o => o.MapFrom(m => m.State.Code))
                       .ForMember(a => a.State, o => o.MapFrom(m => m.State.Name));

                       //Eligibilities
                       c.CreateProjection<Eligibility, GetEligibilityForDetailsDto>()
                       .ForMember(a => a.Status, o => o.MapFrom(m => m.Status.Name))
                       .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.User.FirstName + " " + m.User.LastName));

                       //Sub Product
                       c.CreateProjection<LeadSubProduct, GetSubProductsInLeadForQaDto>()
                       .ForMember(a => a.ID, o => o.MapFrom(m => m.SubProduct.ID))
                       .ForMember(a => a.Code, o => o.MapFrom(m => m.SubProduct.Code))
                       .ForMember(a => a.Name, o => o.MapFrom(m => m.SubProduct.Name))
                       .ForMember(a => a.ProductName, o => o.MapFrom(m => m.SubProduct.Product.Name));

                       //For Question Answer
                       c.CreateProjection<ProductQuestionAnswer, GetLeadQuestionAnsDto>()
                       .ForMember(a => a.QuestionId, o => o.MapFrom(p => p.ProductQuestion.ID))
                       .ForMember(a => a.AnswerId, o => o.MapFrom(p => p.ProductQuestion.ID))
                       .ForMember(a => a.Question, o => o.MapFrom(p => p.ProductQuestion.Question))
                       .ForMember(a => a.ProductName, o => o.MapFrom(p => p.ProductQuestion.Product.Name));


                       //QA List
                       c.CreateProjection<QA, GetQAForLeadDto>()
                       .ForMember(a => a.Status, o => o.MapFrom(m => m.Status.Name))
                       .ForMember(a => a.CreatedBy, o => o.MapFrom(m => m.CreatedBy.FirstName + " " + m.CreatedBy.LastName))
                       .ForMember(a => a.Answers, o => o.MapFrom(m => m.Answers));


                       //QA Answer List
                       c.CreateProjection<QaQuestionAnswer, GetQaAnswerForLeadDto>()
                       .ForMember(a => a.QuestionId, o => o.MapFrom(m => m.Question.ID))
                       .ForMember(a => a.Question, o => o.MapFrom(m => m.Question.Question))
                       .ForMember(a => a.ProductName, o => o.MapFrom(m => m.Question.Product.Name));
                   });


        public static MapperConfiguration AgentMonthlyDetailReport = new(
            (configurationEcpression) =>
            {
                configurationEcpression.CreateProjection<User, AgentMonthlyDetailReportDto>()
                .ForMember(destinationMember => destinationMember.AgentId, memberOptions => memberOptions.MapFrom(mapExpression => mapExpression.ID))
                .ForMember(destinationMember => destinationMember.AgentName, memberOptions => memberOptions.MapFrom(mapExpression => mapExpression.FirstName + " " + mapExpression.LastName))
                .ForMember(destinationMember => destinationMember.NewLeadDay, memberOptions => memberOptions.MapFrom(mapExpression => mapExpression.Lead.Where(predicate => predicate.Fk_StatusId == 1)))
                .ForMember(destinationMember => destinationMember.EvErrorDay, memberOptions => memberOptions.MapFrom(mapExpression => mapExpression.Lead.Where(predicate => predicate.Fk_StatusId == 4)))
                .ForMember(destinationMember => destinationMember.QaPendingDay, memberOptions => memberOptions.MapFrom(mapExpression => mapExpression.Lead.Where(predicate => predicate.Fk_StatusId == 3)))
                .ForMember(destinationMember => destinationMember.QaQualifiedDay, memberOptions => memberOptions.MapFrom(mapExpression =>
                mapExpression.Lead.Where(predicate => predicate.QA.Any(pr => pr.Fk_StatusId == 17))
                ))
                .ForMember(destinationMember => destinationMember.NotQualifiedDay, memberOptions => memberOptions.MapFrom(mapExpression => mapExpression.Lead.Where(predicate => predicate.QA.Any(any => any.Fk_StatusId == 16 || any.Fk_StatusId == 27))));

                configurationEcpression.CreateProjection<Lead, DatesDto>()
                .ForMember(destinationMember => destinationMember.Date, memberOptions => memberOptions.MapFrom(mapExpression => mapExpression.CreatedAt));
            }
            );


        public static MapperConfiguration GetAlreadyAssinedLead = new
              (
                  c =>
                  {
                      //List For Lead And Patient Infoemation
                      c.CreateProjection<LeadAssigned, GetLeadAssignedDto>()
                      .ForMember(a => a.FK_LeadId, o => o.MapFrom(p => p.FK_LeadId));
                  });

    }//class
}//namespace