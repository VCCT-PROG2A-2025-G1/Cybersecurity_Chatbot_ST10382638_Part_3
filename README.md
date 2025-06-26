# Cybersecurity Awareness Chatbot – Part 3 (GUI with WPF)

**Student Name:** Luc de Marillac St Julien
**Student Number:** ST10382638
**Module:** PROG6221 – Programming 2A
**Institution:** The Independent Institute of Education
**Academic Year:** 2025

---

## 🚀 Project Overview

This is the final WPF-based version of the **Cybersecurity Awareness Chatbot**, completing Part 3 of the PROG6221 Portfolio of Evidence (POE). It transforms the console prototype into a fully-featured Windows GUI application using **WPF (.NET 4.8)**, with interactive modules for task management, quizzes, and more, all themed around teaching cybersecurity best practices.

---

## 🧠 Key Features (New in Part 3)

* **WPF GUI Interface**: Modern XAML windows replace console I/O.
* **ASCII Art & Voice Greeting**: Displays themed ASCII art and plays a WAV welcome message at startup.
* **Typing & Thinking Animations**: Character-by-character typing effect and “ChatBot is thinking...” dot animation.
* **Task Assistant Window**: Create, view, complete, and delete tasks with:

  * **Title** and **multi‑line Description**
  * **DatePicker** for exact reminder date/time
  * “Days from now” fallback
* **Cybersecurity Quiz Window**: 10-question GUI quiz with:

  * Radio buttons for True/False or multiple‑choice
  * Immediate feedback + brief explanation per question
  * Final score screen with tailored encouragement
* **Activity Log Window**: Tracks every action (tasks, reminders, quiz runs) and shows the **last 10 entries** by default, with a **Show More** button to page older entries.
* **NLP & Keyword Detection**: Flexible phrase parsing (using regex and pattern lists) to recognize commands in complete sentences.
* **Memory & Sentiment System**: Stores user preferences (e.g. favorite topic) and detects simple moods ("worried", "frustrated") to tailor empathetic responses.
* **Confirmation-Based Exit**: Safety prompt before closing the application.

---

## 💬 Recognized Cybersecurity Topics

* VPNs
* Password safety
* Phishing awareness
* Safe browsing
* Antivirus protection
* Malware defense
* Firewalls
* Social engineering
* Encryption basics
* Identity theft prevention

---

## 🛠 Setup Instructions

### Prerequisites

* **Visual Studio 2022** with **.NET Desktop Development** workload
* **.NET Framework 4.8**

### How to Run

1. **Download ZIP:**
   [https://github.com/VCCT-PROG2A-2025-G1/Cybersecurity\_Chatbot\_ST10382638\_Part\_3](https://github.com/VCCT-PROG2A-2025-G1/Cybersecurity_Chatbot_ST10382638_Part_3)

2. Extract the contents.

3. Open `Cybersecurity_Chatbot.sln` in Visual Studio.

4. Press **F5** to run.

---

## 🧪 Usage Guide

| Command Phrase Examples              | Action                               |
| ------------------------------------ | ------------------------------------ |
| “start quiz” / “let’s take the quiz” | Opens the Quiz window                |
| “remind me to update password”       | Opens Task Assistant window          |
| “view log” / “show activity log”     | Opens Activity Log window            |
| “goodbye”                            | Prompts exit confirmation            |
| Any cybersecurity question           | Bot answers using ResponseBank logic |

---

## 📹 Demo Video & Submission Guidelines

* **Length:** Max 15 minutes (≤5 min code / ≤10 min demo)
* **Format:** Unlisted YouTube link
* **Requirements:** Include face/voice introduction, explain code logic (no line-by-line), showcase each Part 3 feature in-app

🔗 Watch: [https://youtu.be/\_d8Ma3ey9tc](https://youtu.be/_d8Ma3ey9tc)

---

## 📁 Project Structure (Key Files)

* **App.xaml / App.xaml.cs** – Application startup logic
* **ChatWindow\.xaml / .cs** – Main chat UI & input processing
* **TaskWindow\.xaml / .cs** – Task Assistant GUI with Title, Description, DatePicker, Reminder
* **QuizWindow\.xaml / .cs** – Cybersecurity Quiz GUI
* **ActivityLogWindow\.xaml / .cs** – Activity Log GUI
* **WpfChatBot.cs** – Core NLP, memory, sentiment, and window-launch logic
* **CyberQuiz.cs / Question.cs** – Quiz data and parsing
* **ActivityLog.cs** – Global log storage and paging
* **Memory.cs** – Persistent user preference store
* **Assets/** – `Greeting.wav`, ASCII art text file, etc.
* **.github/workflows/ci.yml** – CI build & lint checks

---

## ✅ Continuous Integration & Versioning

* **GitHub Actions** pipeline runs on every push: builds solution, checks for errors.
* **Semantic Versioning**: tags/releases created for Part 1, Part 2, Part 3 milestones.

---

## 📚 References

1. W3Schools. C# Guide. [https://www.w3schools.com/cs/](https://www.w3schools.com/cs/)
2. ASCII Art Generator. [https://ascii.co.uk/](https://ascii.co.uk/)
3. TTSMP3. [https://ttsmp3.com/](https://ttsmp3.com/)
4. ChatGPT. OpenAI (2025). [https://chat.openai.com/](https://chat.openai.com/)

---

## 📌 Disclaimer

This application is submitted as coursework for the IIE’s Programming 2A module and is intended for academic purposes only.
