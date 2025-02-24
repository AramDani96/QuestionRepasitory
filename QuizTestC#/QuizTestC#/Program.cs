namespace QuizTestC_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            QuestionsService questionsService = new QuestionsService();
            questionsService.LoadFromFile();
           
            int id =0;
            while (true)
            {
                Console.WriteLine("1. Add |2.Updete | 3.Delete | 4.Test | 0. Exit");
                Console.Write("Input location number: ");
                int location = int.Parse(Console.ReadLine());
                Console.WriteLine();
                switch (location)
                {
                    case 1: AddQuestion();
                        questionsService.SaveToFile();
                        break;
                    case 2:
                        Console.Write("Enter Id: ");
                         id = int.Parse(Console.ReadLine());
                        Console.Write("Enter Question Text");
                        string questionText = Console.ReadLine();
                        string[] answers = new string[4];
                        for (int i = 0; i < answers.Length; i++)
                        {
                            Console.Write($"{i + 1} Input Answers:");
                            answers[i] = Console.ReadLine();
                        }
                        Console.Write("Input Correct Index: ");
                        int correctAnswerIndex = int.Parse(Console.ReadLine());
                        Questions questions = new Questions(id, questionText, answers, correctAnswerIndex);
                        questionsService.Updete(id,questions);
                        break;
                    case 3:
                        Console.Write("Enter ID: ");
                         id = int.Parse (Console.ReadLine());
                        questionsService.RemoveQuestion(id);break;
                    case 4: questionsService.QuizStart(); break;
                    case 0:
                        return;
                    default:Console.WriteLine("Error number"); break;

                }
            }
                
        }
        static void AddQuestion()
        {
            QuestionsService questionsService = new QuestionsService();
            Questions questions;
            int id = questionsService.GetEndId();
            id++;
            Console.Write("Enter question text: ");
            string questionText = Console.ReadLine();

            string[] answers = new string[4];
            for (int i = 0; i < 4; i++)
            {
                Console.Write($"Enter your answer {i + 1}: ");
                answers[i] = Console.ReadLine();
            }

            int correctAnswerIndex;
            Console.Write("Enter the number of the correct answer (1-4): ");
            while (!int.TryParse(Console.ReadLine(), out correctAnswerIndex) || correctAnswerIndex < 1 || correctAnswerIndex > 4)
            {
                Console.Write("Invalid entry. Enter the number of the correct answer (1-4): ");
            }
            correctAnswerIndex--; // Преобразуем в индекс массива (начинается с 0)

            questions = new Questions(id, questionText, answers, correctAnswerIndex);

            questionsService.AddQuestion(questions);
            
        }
    }
}
