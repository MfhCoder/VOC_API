using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.CustomerFeedback
{
    public class EditFeedbackDto
    {
        public List<int> TagIds { get; set; }

        /// <summary>
        /// Sentiment value: true for Positive, false for Negative.
        /// </summary>
        [SwaggerParameter("Sentiment value. Use true for Positive, false for Negative.")]
        public bool Sentiment { get; set; }
    }
}
