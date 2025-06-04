# Cybersecurity Awareness Chatbot – Part 2

**Student Name:** Luc de Marillac St Julein  
**Student Number:** ST10382638  
**Module:** PROG6221 – Programming 2A  
**Institution:** The Independent Institute of Education  
**Academic Year:** 2025  

---

## Project Overview

This is the second phase of the console-based **Cybersecurity Awareness Chatbot**, enhancing the original system to provide more realistic, intelligent, and supportive interactions. Developed for the PROG6221 Portfolio of Evidence, **Part 2** introduces dynamic responses, simple sentiment detection, and basic user memory — improving educational engagement on cybersecurity topics.

---

## New Features in Part 2

- **Keyword Recognition** – Recognises key topics such as "VPN", "phishing", and "passwords" even when mentioned in longer sentences.
- **Randomised Responses** – Certain topics now respond with a random tip or fact to simulate natural variation.
- **Sentiment Detection** – Detects emotional input like "worried", "frustrated", or "curious", and replies with empathy.
- **Memory Feature** – Stores what the user says they’re interested in (e.g., “I’m interested in VPN”) and recalls it when asked.
- **Expanded Input Validation** – Prevents blank input and handles vague questions with supportive fallback messages.
- **Conversation Flow** – Politely prompts the user if they’d like to continue before exiting.
- **Modular Code Design** – Separated into `ChatBot`, `ResponseBank`, `Memory`, and `StartUp` classes for better organisation.

---

## Cybersecurity Topics Recognised

- Password Safety
- Phishing
- Malware
- Antivirus Software
- VPNs
- Encryption
- Social Engineering
- Identity Theft
- Firewalls
- Safe Browsing

---

## Setup Instructions (Using Visual Studio Only)

### Requirements

Make sure you have:

- [Visual Studio 2022 or newer](https://visualstudio.microsoft.com/downloads/)
  - Install the “.NET desktop development” workload
- [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)

---

### How to Download the Project (Without Git)

1. Visit the repository:  
   [https://github.com/VCCT-PROG2A-2025-G1/Cybersecurity_Chatbot_ST10382638](https://github.com/VCCT-PROG2A-2025-G1/Cybersecurity_Chatbot_ST10382638)

2. Click the green **Code** button → **Download ZIP**

3. Right-click the ZIP file → **Extract All**

4. Inside the extracted folder, you should see:
   - `.cs` files (source code)
   - `Greeting Message.wav`
   - `.csproj` file

---

### Opening and Running the Project in Visual Studio

1. Open **Visual Studio**

2. Click **Open a project or solution**

3. Navigate to the extracted folder and open **CybersecurityChatbot.csproj**

4. From the top menu, click **Build > Build Solution**  
   - Wait for the “Build succeeded” message

5. Press the green **Start button** (or hit `F5`) to run the chatbot

---

### What to Expect

- A voice greeting will play  
- ASCII art will appear  
- You'll be prompted to enter your name  
- The chatbot will start the conversation  
- You can ask about cybersecurity or say how you’re feeling  
- You can type “I’m interested in [topic]” and ask “What did I say earlier?”

---

## Continuous Integration (CI)

- GitHub Actions runs on every push to verify the project builds successfully.  
- Includes a `.yml` workflow for .NET compilation check.  
- Screenshot of passing CI (green checkmark) is included in the submission folder.

---

## Demonstration Video

Watch the updated bot in action:  
[Video for project](https://youtu.be/_d8Ma3ey9tc)

---

## Post-Video Code Enhancement

> This feature was added **after the video demonstration** was recorded.

A new feature was implemented to improve the chatbot’s follow-up logic when a user says `"yes"`, `"sure"`, or `"yeah"` in response to a previously remembered topic — as well as if they say `"no"` to decline.

The chatbot now retrieves the previously stored **interest topic** from memory and loops through all known response keywords to find a matching topic. If a match is found, it selects and returns a random response related to that topic. If the user says "no", the bot politely ends that line of conversation.

### New Logic Example

```csharp
if ((cleanedInput.Contains("yes") || cleanedInput.Contains("yeah") || cleanedInput.Contains("sure")) && previousQuestion)
{
    string topic = Memory.Recall("interest")?.ToLower().Trim();

    foreach (var keyword in responses.Keys)
    {
        if (topic.Contains(keyword))
        {
            previousQuestion = false;
            List<string> possibleResponses = responses[keyword];
            return possibleResponses[rand.Next(possibleResponses.Count)];
        }
    }

    previousQuestion = false;
    return "I'm sorry, I don't understand. Try asking me about something cybersecurity-related.";
}

// Handle negative response if previous question was asked
if ((cleanedInput.Contains("no") || cleanedInput.Contains("nope") || cleanedInput.Contains("nah")) && previousQuestion)
{
    previousQuestion = false;
    return "Alright! Let me know if you'd like help with anything else.";
}

```
## References

- [W3Schools C# Tutorials](https://www.w3schools.com/cs/index.php)
- [ChatGPT](https://chat.openai.com)
- [ASCII Art Generator](https://ascii.co.uk/)
- [TTSMP3 - Text to Speech Tool](https://ttsmp3.com)

---

## Disclaimer

This project is developed for educational purposes only, submitted as part of the Programming 2A module for The Independent Institute of Education. All rights reserved.

---
