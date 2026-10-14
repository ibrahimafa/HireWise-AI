using HireWise_AI.Interfaces;
using static System.Net.Mime.MediaTypeNames;
using UglyToad.PdfPig;

namespace HireWise_AI.Service
{
	public class PdfService : IPdfService
	{
		public PdfService() { }
		public string ExtractText(string filePath)
		{
			using var document = PdfDocument.Open(filePath);

			string text = "";

			foreach (var page in document.GetPages())
			{
				text += page.Text + " ";
			}

			return text;
		}
	}
}
