using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Merchant
{
    public class MerchantDto
    {
        public string Name { get; set; }
        public int? TransactionCount { get; set; }
        public decimal? TransactionValue { get; set; }
        public string? Type { get; set; }
        public int? TerminalCount { get; set; }
        public string? License { get; set; }
        public string? PosMcc { get; set; }
        public string? Industry { get; set; }
        public string? Location { get; set; }
        public string? Product { get; set; }
        public string? Ledger { get; set; }
        public string? MerchantSettlementBank { get; set; }
        public string? Tenure { get; set; }
        public int? TenureInDays { get; set; }
        public DateTime? LastTransaction { get; set; }
        public DateTime? LastTicket { get; set; }
        public DateTime? LastSurvey { get; set; }
        public DateTime? LastFeedback { get; set; }
        public DateTime? LastEscalation { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? PhoneNo { get; set; }
    }
}
