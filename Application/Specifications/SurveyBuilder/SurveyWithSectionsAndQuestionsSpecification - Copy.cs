using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Specifications.SurveyBuilder
{
    public class SurveyWithSectionsAndQuestionsSpecification : BaseSpecification<Survey>
    {
        public SurveyWithSectionsAndQuestionsSpecification(int surveyId)
            : base(s => s.Id == surveyId)
        {
            AddInclude(s => s.QuestionSections);
            AddInclude(s => s.Questions);
            AddInclude("QuestionSections.Questions");
            AddInclude("Questions.Options");
        }
    }
}
