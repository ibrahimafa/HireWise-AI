namespace HireWise_AI.DTOs.AI.InterviewQuestions
{
	public class GenerateInterviewQuestionsRequestDto
	{
		public int CandidateId { get; set; }
		public int JobId { get; set; }
		public int QuestionCount { get; set; } = 5;
	}
}
