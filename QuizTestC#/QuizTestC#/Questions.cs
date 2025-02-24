using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTestC_
{
    internal class Questions
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string[] Answers { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public Questions(int id,string quesstionText, string[] answers, int correctAnswerIndex)
        {
            Id = id;
            QuestionText = quesstionText;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;
        }

    }
}
