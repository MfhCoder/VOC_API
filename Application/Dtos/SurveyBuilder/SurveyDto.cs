using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.SurveyBuilder
{
    public class SurveyDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime CreationDate { get; set; }
        public int ResponsesCount { get; set; }
    }
}
