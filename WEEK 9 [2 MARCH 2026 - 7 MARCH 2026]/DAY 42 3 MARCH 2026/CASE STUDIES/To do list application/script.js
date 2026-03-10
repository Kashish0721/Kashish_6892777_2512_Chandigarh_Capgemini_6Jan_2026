let tasks = JSON.parse(localStorage.getItem("tasks")) || [];

function displayTasks() {
  taskList.innerHTML = "";

  tasks.forEach((task, index) => {
    let li = document.createElement("li");

    li.innerHTML = `
      <span onclick="toggleTask(${index})" 
      style="text-decoration:${task.completed ? 'line-through' : 'none'}">
      ${task.text}</span>
      <button onclick="deleteTask(${index})">X</button>
    `;

    taskList.appendChild(li);
  });

  localStorage.setItem("tasks", JSON.stringify(tasks));
}

function addTask() {
  if(taskInput.value === "") return;

  tasks.push({ text: taskInput.value, completed: false });
  taskInput.value = "";
  displayTasks();
}

function toggleTask(index) {
  tasks[index].completed = !tasks[index].completed;
  displayTasks();
}

function deleteTask(index) {
  tasks.splice(index, 1);
  displayTasks();
}

displayTasks();