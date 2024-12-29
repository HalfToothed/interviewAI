# 💻 InterviewAI  

**InterviewAI** is an AI-powered tool designed to simplify the interview process by generating tailored interview questions from resumes. Built using Angular for the frontend, .NET Web API for the backend, and Google Gemini for AI integration, InterviewAI makes it easy to prepare personalized and insightful questions for candidates.

---

## 🎯 Features  

- **Tailored Questions**: Automatically generates customized interview questions based on the uploaded resume.  
- **Google Gemini Integration**: Leverages the power of Google Gemini for AI-driven insights.  
- **User-Friendly Interface**: Intuitive Angular-based UI for effortless interaction.  

---

## 🛠️ Technologies Used

- **Frontend**: Angular
- **Backend**: .Net Web API
- **AI Integration**: Google-Gemini

---

## 🚀 Quick Start Guide

To run this project locally:

### 1.📂 Clone the Repository  
```bash
git clone https://github.com/your-repo/InterviewAI.git
cd InterviewAI
```

### 2.📦 Setup Backend

- #### Navigate to the folder

    ```bash
    cd interviewApi
    ```

- #### Set API key

    ```plaintext
    "environmentVariables": {
            ...
            "Gemini-API-Key": "API-KEY"
        }     
    ```
- #### Start the Backend server 

    ```bash
    dotnet run
    ```

### 3.📱 Setup Frontend

- #### Navigate to the folder

    ```bash
    cd interviewAI
    ```
- #### Install Dependencies

    ```bash
    npm install
    ```
- #### Start the Angular development server

    ```bash
    ng serve
    ```

Open [http://localhost:4200](http://localhost:4200) in your browser! 🌐

---

## 📝 Usage

1. Upload a candidate's resume using the frontend interface.
2. Let the tool analyze the resume and generate custom interview questions.
3. Review and save the generated questions for use during the interview.

---


## 🤝 Contributing

Contributions are welcome! Please feel free to submit issues or pull requests to help this project.
