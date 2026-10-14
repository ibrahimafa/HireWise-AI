# HireWise AI

AI-powered recruitment platform that helps recruiters screen candidates faster. It parses PDF resumes, matches them against a job description using Google Gemini, and generates tailored interview questions for each candidate.

## Features

- Upload PDF resumes and extract their text (PdfPig)
- AI resume-to-job matching with a match score and reasoning (Google Gemini API)
- Automatic generation of interview questions based on the candidate's profile and the job requirements
- REST API with Swagger documentation

## Tech Stack

| Layer | Technology |
|-------|------------|
| Backend | ASP.NET Core, C# |
| AI | Google Gemini API |
| PDF parsing | PdfPig |
| Database | 
| Architecture | 

## Screenshots

![Resume matching](docs/matching.png)
![Interview questions](docs/questions.png)

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version used in the project, e.g. 8.0)
- A Google Gemini API key from [Google AI Studio](https://aistudio.google.com/)

### Setup

1. Clone the repository

   ```bash
   git clone https://github.com/ibrahimafa/<repo-name>.git
   cd <repo-name>
   ```

2. Add your API key (never commit it)

   ```bash
   dotnet user-secrets set "Gemini:ApiKey" "YOUR_API_KEY"
   ```

   Or set it in `appsettings.Development.json` (make sure this file is in `.gitignore`).

3. Run the project

   ```bash
   dotnet run
   ```

4. Open Swagger at `https://localhost:<port>/swagger`

## API Overview

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/...` | Upload a resume and match it to a job description |
| POST | `/api/...` | Generate interview questions for a candidate |


## How It Works

1. The recruiter uploads a resume (PDF) and provides a job description.
2. PdfPig extracts the resume text.
3. The text and job description are sent to Gemini with a structured prompt.
4. The API returns a match score, strengths/gaps, and suggested interview questions.

## Roadmap

- [ ] Authentication and recruiter accounts
- [ ] Candidate ranking across multiple resumes
- [ ] Frontend (Angular)

## Author

**Ibrahim Afaneh**
Backend / Full-Stack .NET Developer
[LinkedIn](https://linkedin.com/in/ibrahim-afaneh-dev) | [GitHub](https://github.com/ibrahimafa)
