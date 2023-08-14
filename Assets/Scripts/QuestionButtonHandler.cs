using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionButtonHandler : MonoBehaviour
{
    public GameObject numberedButtonsPanel;
    public GameObject questionsPanel;
    public TextMeshProUGUI questionText;
    public Button[] answerChoiceButtons;
    public Button[] numberedButtons;

    public TextMeshProUGUI scoreText;

    private int currentQuestionIndex;
    private int[] questionAttempts;
    private int score;

    private void Start()
    {
        if (GameData.Instance != null)
        {
            score = GameData.Instance.score;
            for (int i = 0; i < numberedButtons.Length; i++)
            {
                // Update the color of buttons from GameData
                numberedButtons[i].GetComponent<Image>().color = GameData.Instance.buttonColors[i];

                // If the button color is green or red, set it to non-interactable
                if(GameData.Instance.buttonColors[i] == Color.green || GameData.Instance.buttonColors[i] == Color.red)
                {
                    numberedButtons[i].interactable = false;
                }
            }
        }
        else
        {
            score = 0;
        }

        questionAttempts = new int[numberedButtons.Length];
        UpdateScoreText();
    }

    public void OnNumberedButtonClicked(int questionIndex)
    {
        numberedButtonsPanel.SetActive(false);
        questionsPanel.SetActive(true);

        // Set the color of numbered button to default when first clicked
        numberedButtons[questionIndex].GetComponent<Image>().color = Color.white;

        string[] questions = new string[]
        {
            "What is one way Computing Professionals can have a negative impact?",
            "How can Computing Professionals have a positive impact?",
            "Which best describes what a Computing Professional is?",
            "Who is the ACM Code of Ethics & Professional Conduct designed to inspire?",
            "What is the role of the Code when violations occur, and how does it help address these issues?",
            "Why is it important for computing professionals to respect the privacy of others?",
            "According to the Code, what is the responsibility of computing professionals when it comes to society and human well-being?",
            "What does the principle about avoiding harm mean?",
            "What are some negative consequences that can be caused by computing?",
            "Which ethical principle from the Code requires computing professionals to disclose all relevant information and not deceive others?",
            "What best describes the principle 'Be fair and take action not to discriminate'?",
            "Which of the following best describes the ACM Code of Ethics principle related to creating opportunities for members of an organization or group to grow as professionals?",
            "What step should leaders take when changing an outdated system in order not to impact the quality and the productivity of the stakeholders/users they work with?",
            "What is the principle 'Respect the work required to produce new ideas, inventions, creative works, and computing artifacts' about?",
            "Which of the following is NOT a key principle to ensure special care of systems that become integrated into the infrastructure of society?",
            "From the Code, what is respecting privacy about?",
            "Keeping confidential information secure and not disclosing it to unauthorized parties describes which principle from the Code?",
            "According to the Code, which of the following best shows how a developer could promote public understanding of computers, related technologies, and their effects?",
            "When dealing with confidential information, what is the responsibility of a computing professional?",
            "Which of these is a part of the professional responsibility, as listed in the Code?",
        };


        string[][] answerChoices = new string[][]
        {
            new string[] { "By creating systems that discriminate.", "Computing professionals can not have a negative impact on society.", "By caring about the ethical consequences of their work.", "By designing systems that positively impact the people around them." },
            new string[] { "Designing and creating useful technologies that solve problems in society.", "Not respecting the privacy and confidentiality of users.", "Prioritizing security, reliability, and trustworthiness.", "Promoting policies that are ethical and responsible for the use of technology." },
            new string[] { "Someone who cares about computers.", "Someone who works with computers.", "Dealing with computers at a high level.", "A computing professional is someone who applies computer science principles to solve problems." },
            new string[] { "People who like computers.", "College Students.", "Computing Professionals.", "Professors." },
            new string[] { "The Code is used to punish individuals who commit violations.", "The Code serves as a reference point for identifying violations and initiating appropriate corrective actions.", "The Code has no impact on addressing violations.", "The Code is a list of violations that employees should avoid." },
            new string[] { "It helps to maximize profits.", "Because it promotes social responsibility.", "Because it is a personal preference.", "Because it helps to prevent cyber attacks and security breaches." },
            new string[] { "To benefit society with their skills and know that all people are stakeholders in computing.", "Think about their own interests.", "Consider only the needs of the people around them.", "It is not their job to benefit society." },
            new string[] { "Not making mistakes.", "Computing professionals aiming to minimize negative consequences of computing.", "The impact on others should not concern computing professionals.", "Computing professionals cannot cause harm." },
            new string[] { "Social well-being.", "Economic growth.", "Better personal privacy.", "Threats to health and safety, personal security and privacy." },
            new string[] { "Respecting privacy", "Not misrepresenting information", "Be honest and trustworthy", "Not using your position for personal gain" },
            new string[] { "Computing Professionals do not need to worry about being fair.", "This is not an ethical principle from the ACM Code of Ethics and Professional Conduct.", "Computing Professionals cannot discriminate.", "Computing Professionals should aim for equal access and treatment for all." },
            new string[] { "Provide opportunities for the professional development of colleagues.", "Avoid harm to others.", "Be honest and trustworthy.", "Strive to achieve high quality in both the processes and products of professional work." },
            new string[] { "Roll out changes without notifying stakeholders to avoid resistance.", "Notify users of an unsupported system only after the outdated system has been switched for an alternative.", "Assist system users in monitoring the operational viability of their new computing systems.", "Limit testing and documentation to save time and money." },
            new string[] { "Taking the ideas of others without giving them the proper credit.", "Recognizing and respecting others contributions in the creation of new ideas and computing artifacts.", "Computing professionals only have to respect the work, not the people.", "It is not important to acknowledge the contributions of others." },
            new string[] { "Considering societal impact of systems created.", "Follow ethical and legal guidelines.", "Ensuring safety and quality.", "Listening to stakeholders about the profits made by a system, and rolling it back if it doesn't meet that criteria." },
            new string[] { "The use of personal information is only for legitimate purposes that do not violate any rights.", "The use of personal information without any restrictions.", "Using personal information without permission is not an issue.", "Considering the potential consequences of systems on society." },
            new string[] { "Respect Privacy.", "Be fair and take action not to discriminate.", "Honor confidentiality.", "Be honest and trustworthy." },
            new string[] { "Conduct thorough testing and maintenance of systems.", "Follow ethical and legal guidelines while designing and implementing systems.", "Respect the property rights of others, including intellectual property rights and copyrights.", "Educate the public about computing and related technologies and their potential impact on society." },
            new string[] { "To sell confidential information.", "Computing professionals must keep information confidential except when disclosure is authorized.", "A computing professional can do whatever they would like to confidential information.", "Provide the confidential information to anyone who wants it." },
            new string[] { "Strive to achieve high quality in both the processes and products of professional work.", "Computing professionals do not have a professional responsibility.", "Professional responsibility simply means to get your work done.", "Using skill to achieve company goals." },
        };


        questionText.text = questions[questionIndex];

        for (int i = 0; i < answerChoiceButtons.Length; i++)
        {
            answerChoiceButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answerChoices[questionIndex][i];
        }

        // Keep track of the current question index for later
        currentQuestionIndex = questionIndex;
    }
    // Call this function from answer choice buttons
    public void OnAnswerButtonClicked(int answerIndex)
    {
        // Define the correct answers index for each question (0-based index)
        int[] correctAnswers = {0, 0, 3, 2, 1, 3, 0, 1, 3, 2, 3, 0, 2, 1, 2, 0, 2, 3, 1, 0};

        // Increment the number of attempts for this question
        questionAttempts[currentQuestionIndex]++;

        // Check if the selected answer is correct
        if (answerIndex == correctAnswers[currentQuestionIndex])
        {
            // Increment the score
            score++;

            //Increment Game Data Score 
            GameData.Instance.score = score;

            // Update the score text
            UpdateScoreText();

            // Set button color to green
            numberedButtons[currentQuestionIndex].GetComponent<Image>().color = Color.green;
            GameData.Instance.buttonColors[currentQuestionIndex] = Color.green;

            // Hide the corresponding numbered button (locked out)
            numberedButtons[currentQuestionIndex].interactable = false;
        }
        else if (questionAttempts[currentQuestionIndex] >= 2) // Check if this is the second time they got it wrong
        {
            // Set button color to red
            numberedButtons[currentQuestionIndex].GetComponent<Image>().color = Color.red;
            GameData.Instance.buttonColors[currentQuestionIndex] = Color.red;

            // Hide the corresponding numbered button (locked out)
            numberedButtons[currentQuestionIndex].interactable = false;
        }

        // Show the numbered buttons again and hide the question
        numberedButtonsPanel.SetActive(true);
        questionsPanel.SetActive(false);
    }
    //Method to update the score text
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}


