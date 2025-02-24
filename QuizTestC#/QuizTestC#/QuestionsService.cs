using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizTestC_
{
    internal class QuestionsService
    {
        private static List<Questions> _questions = new List<Questions>();
        private string FilePath = "questions.txt";
        

        public void AddQuestion(Questions questions)
        {
            if (questions == null)
            {
                throw new Exception("Object is Null");
            }
            _questions.Add(questions);
            SaveToFile();
        }
        public void RemoveQuestion(int id)
        {
            var question = GetById(id);

            if (question == null)
            {
                throw new Exception("Object is Null");
            }
            Console.WriteLine("Successfully deleted.");
            _questions.Remove(question);
            SaveToFile();
        }
        public void Updete(int id, Questions questions)
        {
            var quest = GetById(id);

            if (quest == null)
            {
                throw new Exception("Object is Null");
            }
            quest.QuestionText = questions.QuestionText;
            quest.Answers = questions.Answers;
            quest.CorrectAnswerIndex = questions.CorrectAnswerIndex;
            Console.WriteLine("Successfully updated.");
            SaveToFile();
        }

        private Questions GetById(int id)
        {
            Questions questions = _questions.FirstOrDefault(x => x.Id == id);

            if (questions == null)
            {
                return null;
            }
            return questions;
        }

        public int GetEndId()
        {
            if (_questions.Count == 0)
                return 0;

            return _questions.Max(x => x.Id);

        }

        private void SaveToFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    foreach (var question in _questions)
                    {
                        string questionLine = $"{question.Id}|{question.QuestionText}|{string.Join(",", question.Answers)}|{question.CorrectAnswerIndex}";
                        writer.WriteLine(questionLine);
                    }
                }
                Console.WriteLine("The questions are successfully saved to the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Messige: " + ex.Message);
            }
        }

        public void LoadFromFile()
        {
            if (!File.Exists(FilePath))
            {
                Console.WriteLine("Question file not found.");
                return; // Файл не найден, можно вывести сообщение
            }

            try
            {
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        _questions.Clear();
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split('|');
                            if (parts.Length != 4)
                                continue;
                            int id = int.Parse(parts[0]);
                            string questionText = parts[1];
                            string[] answers = parts[2].Split(',');
                            int correctAnswerIndex = int.Parse(parts[3]);

                            Questions questions = new Questions(id, questionText, answers,correctAnswerIndex);
                            AddQuestion(questions);
                        }
                    }
                    Console.WriteLine("successfully loaded data");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Load document: " + ex.Message);
            }
        }

        public void QuizStart()
        {
            int countIndex = 0;
            while (countIndex < _questions.Count)
            {
                var listQuestion = _questions[countIndex];
                Console.WriteLine($"{listQuestion.Id}. {listQuestion.QuestionText}");
                Console.WriteLine();

                for (int i = 0; i < listQuestion.Answers.Length; i++)
                {
                    Console.WriteLine($"{i + 1}) {listQuestion.Answers[i]}");
                }

                Console.Write("Input Number: ");
                int answerCount;

                while (!int.TryParse(Console.ReadLine(), out answerCount) || answerCount < 1 || answerCount > 4)
                {
                    Console.Write("Please enter the correct answer number (1-4): ");
                }

                if (answerCount-1 == listQuestion.CorrectAnswerIndex)
                {
                    Console.WriteLine("Correct answer");
                }
                else
                {
                    Console.WriteLine($"Wrong answer. Correct answer: {listQuestion.Answers[listQuestion.CorrectAnswerIndex]}");
                }
                countIndex++;
                Console.WriteLine();
            }
        }
    }
}
