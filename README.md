# Cybersecurity Awareness Chatbot – Part 3 (GUI with WPF)

**Student Name:** Luc de Marillac St Julein  
**Student Number:** ST10382638  
**Module:** PROG6221 – Programming 2A  
**Institution:** The Independent Institute of Education  
**Academic Year:** 2025  

---

## 🚀 Project Overview

This is the final WPF-based version of the **Cybersecurity Awareness Chatbot**, completing the Portfolio of Evidence (Part 3) for PROG6221. This version transforms the console chatbot into a full Windows GUI application using **WPF (.NET 4.8)**. It includes animations, audio, interaction logging, and mini-apps like a task manager and cybersecurity quiz — all themed around educating users on cybersecurity best practices.

---

## 🧠 Key Features (New in Part 3)

- **WPF GUI Interface** – Replaces console with a sleek modern UI using XAML.
- **ASCII Art with Voice Greeting** – Displays skull-themed art with a greeting sound effect.
- **Typing Effect** – Chatbot types out each response character-by-character.
- **"ChatBot is thinking..." Animation** – Simulates a realistic delay before responding.
- **Activity Logging** – Tracks interactions like quiz answers and task creation.
- **Quiz Module** – 10-question cybersecurity quiz with scoring and input validation.
- **Task Assistant** – GUI form to create, complete, and delete tasks with reminders.
- **Memory System** – Remembers topics the user is interested in.
- **Confirmation-Based Exit** – Confirms before exiting the app.

---

## 💬 Recognized Cybersecurity Topics

- VPNs
- Passwords
- Phishing
- Safe Browsing
- Antivirus
- Malware
- Firewalls
- Social Engineering
- Encryption
- Identity Theft

---

## 🛠 Setup Instructions

### Prerequisites

- **Visual Studio 2022** with **.NET Desktop Development**
- **.NET Framework 4.8**

### How to Run

1. **Download ZIP:**  
   [https://github.com/VCCT-PROG2A-2025-G1/Cybersecurity_Chatbot_ST10382638_Part_3](https://github.com/VCCT-PROG2A-2025-G1/Cybersecurity_Chatbot_ST10382638_Part_3)

2. Extract the contents.

3. Open `Cybersecurity_Chatbot.sln` in Visual Studio.

4. Press `F5` to run.

---

## 🧪 How to Use

| Command | Action |
|---------|--------|
| `start quiz` | Launches the 10-question cybersecurity quiz |
| `remind me to...` | Opens the Task Assistant window |
| `view log` | Shows all recent activity |
| `goodbye` → `no` | Triggers shutdown confirmation |
| `what is phishing?` | Asks the bot anything cybersecurity-related |

---

## 📹 Demo Video

Watch the chatbot in action:  
🔗 [https://youtu.be/_d8Ma3ey9tc](https://youtu.be/_d8Ma3ey9tc)

---

## ✅ Post-Video Enhancements

- Improved exit logic (confirmation before shutdown)
- Typing animation
- Thinking animation using dot animation
- Memory enhancement: recalls prior interest topic
- Input is blocked during startup animation

---

## 📁 Included Files

- `ChatWindow.xaml` – Main WPF chat UI
- `WpfChatBot.cs` – Handles all input and logic
- `CyberQuiz.cs` – Quiz logic with validation
- `TaskWindow.xaml` – GUI task assistant
- `ActivityLog.cs` – Logging system
- `ResponseBank.cs` – Cybersecurity responses
- `Greeting Message.wav` – Voice greeting
- `StartUp.cs` – Plays greeting and prints ASCII art
- `Memory.cs` – Stores user interests

---

## ✅ Continuous Integration

- GitHub Actions checks every commit
- Project builds must pass before being merged
- CI `.yml` workflow included

---

## References

- [W3Schools C#](https://www.w3schools.com/cs/)
- [ASCII Generator](https://ascii.co.uk/)
- [TTSMP3](https://ttsmp3.com)
- [ChatGPT](https://chat.openai.com)

---

## 📌 Disclaimer

This application is a coursework submission for the IIE's Programming 2A module. It is intended for educational use only.
