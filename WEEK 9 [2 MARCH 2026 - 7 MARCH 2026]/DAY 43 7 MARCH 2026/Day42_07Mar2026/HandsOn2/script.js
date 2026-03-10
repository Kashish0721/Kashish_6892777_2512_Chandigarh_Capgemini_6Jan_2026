function addTask()
{
    let input = document.getElementById("taskInput");
    let taskText = input.value;

    if(taskText === "")
    {
        alert("Please enter a task");
        return;
    }

    let li = document.createElement("li");
    li.textContent = taskText;

    // Mark task as done when clicked
    li.onclick = function()
    {
        li.classList.toggle("done");
    };

    document.getElementById("taskList").appendChild(li);

    input.value = "";
}

// Clear all tasks

function clearTasks()
{
    document.getElementById("taskList").innerHTML = "";
}