const questions = [
  {
    question: "What is JavaScript?",
    options: ["Programming Language", "Database", "Operating System", "Browser"],
    answer: 0
  },
  {
    question: "Which keyword declares a variable?",
    options: ["var", "int", "string", "float"],
    answer: 0
  },
  {
    question: "Which method saves data locally?",
    options: ["save()", "store()", "localStorage", "session()"],
    answer: 2
  }
];

let current = 0;
let score = 0;

function loadQuestion() {
  let q = questions[current];
  document.getElementById("question").innerText = q.question;

  let optionsHTML = "";
  q.options.forEach((opt, index) => {
    optionsHTML += `<button onclick="checkAnswer(${index})">${opt}</button>`;
  });

  document.getElementById("options").innerHTML = optionsHTML;
}

function checkAnswer(index) {
  if(index === questions[current].answer) {
    score++;
    alert("Correct Answer!");
  } else {
    alert("Wrong Answer!");
  }

  current++;

  if(current < questions.length) {
    loadQuestion();
  } else {
    document.querySelector(".quiz-container").innerHTML =
      `<h2>Quiz Finished!</h2>
       <p>Your Score: ${score} / ${questions.length}</p>`;
  }
}

loadQuestion();