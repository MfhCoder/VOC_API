using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.Delivery
{
    public class DeliverySpecParams : PagingParams
    {
        public int? BatchId { get; set; }
        public string? SurveyType { get; set; }
        public string? Channel { get; set; }
        public string? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
