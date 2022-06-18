namespace App.Services.Data.UpdateRecords
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using App.Common;
    using App.Data.Common.Repositories;
    using App.Data.Models;
    using App.Services.Data.DTOs;
    using App.Services.Mapping;
    using App.Web.ViewModels.BankEmployee;

    using Microsoft.AspNetCore.Identity;

    public class BankEmployeesService : IBankEmployeesService
    {
        private readonly IRepository<Discount> discounts;
        private readonly IRepository<DiscountVote> discountsVotes;
        private readonly IRepository<BankEmployee> bankEmployees;
        private readonly IRepository<Shopkeeper> shopkeepers;
        private readonly IRepository<Terminal> terminals;
        private readonly UserManager<ApplicationUser> userManager;

        public BankEmployeesService(
            IRepository<Discount> discounts,
            IRepository<BankEmployee> bankEmployees,
            IRepository<Shopkeeper> shopkeepers,
            IRepository<DiscountVote> discountsVotes,
            IRepository<Terminal> terminals,
            UserManager<ApplicationUser> userManager)
        {
            this.discounts = discounts;
            this.bankEmployees = bankEmployees;
            this.shopkeepers = shopkeepers;
            this.discountsVotes = discountsVotes;
            this.userManager = userManager;
            this.terminals = terminals;
        }

        public async Task AddNewRecordsAsync(IEnumerable<BankEmployeeNewRecordDTO> newRecords)
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            foreach (var record in newRecords)
            {
                var user = new ApplicationUser
                {
                    UserName = record.Username,
                    Email = record.Email,
                    PasswordHash = record.Password,
                };
                users.Add(user);

                var bankEmployee = new BankEmployee
                {
                    Id = record.Id,
                    User = user,
                };

                await this.bankEmployees.AddAsync(bankEmployee);
            }

            await this.bankEmployees.SaveChangesAsync();

            foreach (var user in users)
            {
                await this.userManager.AddToRoleAsync(user, GlobalConstants.BankEmployeeRoleName);
            }
        }

        public async Task ChangeDiscountStatusAsync(string discountId, string bankEmployeeUserId, sbyte vote)
        {
            if (!(vote == -1 || vote == 1))
            {
                throw new ArgumentException("Vote can only be -1 or 1");
            }

            if (this.HasBankEmployeeVoted(discountId, bankEmployeeUserId))
            {
                throw new ArgumentException("You have already voted for this discount");
            }

            var discountVote = new DiscountVote
            {
                DiscountId = discountId,
                Vote = vote,
                BankEmployeeUserId = bankEmployeeUserId,
            };
            await this.discountsVotes.AddAsync(discountVote);
            await this.discountsVotes.SaveChangesAsync();

            var votesSum = this.discountsVotes
                .AllAsNoTracking()
                .Where(x => x.DiscountId == discountId)
                .Sum(x => x.Vote);
            if (votesSum >= 2 || votesSum <= -2)
            {
                var discount = this.discounts
                    .All()
                    .Where(x => x.Id == discountId)
                    .Single();

                discount.Status = votesSum >= 2 ? DiscountStatus.Active : DiscountStatus.Rejected;
                await this.discounts.SaveChangesAsync();
            }
        }

        public IEnumerable<DiscountViewModel> GetAllDiscounts(string bankEmployeeUserId)
        {
            var employeeVotedDiscounds = this.discountsVotes
                .AllAsNoTracking()
                .Where(x => x.BankEmployeeUserId == bankEmployeeUserId)
                .Select(x => x.DiscountId)
                .ToHashSet();

            return this.discounts
                .AllAsNoTracking()
                .To<DiscountViewModel>()
                .ToList()
                .Select(x =>
                {
                    x.HasVoted = employeeVotedDiscounds.Contains(x.Id);
                    return x;
                });
        }

        public IEnumerable<T> GetAllShopkeepers<T>()
        {
            return this.shopkeepers
                .AllAsNoTracking()
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllTerminals<T>()
        {
            return this.terminals
                .AllAsNoTracking()
                .To<T>()
                .ToList();
        }

        private bool HasBankEmployeeVoted(string discountId, string bankEmployeeUserId)
        {
            return this.discountsVotes
                .AllAsNoTracking()
                .Where(x => x.DiscountId == discountId)
                .Any(x => x.BankEmployeeUserId == bankEmployeeUserId);
        }
    }
}
