using HTTPUtils;
using System.Text.Json;
using AnsiTools;
using Colors = AnsiTools.ANSICodes.Colors;
using Task;

Console.Clear();
Console.WriteLine("Starting Assignment 2");

// SETUP 
const string myPersonalID = "eb08d53019bf7dce93296d6f668b87789a2fc31943170b10f2d990b94f8651e7"; // GET YOUR PERSONAL ID FROM THE ASSIGNMENT PAGE https://mm-203-module-2-server.onrender.com/
const string baseURL = "https://mm-203-module-2-server.onrender.com/";
const string startEndpoint = "start/"; // baseURl + startEndpoint + myPersonalID
const string taskEndpoint = "task/";   // baseURl + taskEndpoint + myPersonalID + "/" + taskID

// Creating a variable for the HttpUtils so that we dont have to type HttpUtils.instance every time we want to use it
HttpUtils httpUtils = HttpUtils.instance;

//#### REGISTRATION
// We start by registering and getting the first task
Response startRespons = await httpUtils.Get(baseURL + startEndpoint + myPersonalID);
Console.WriteLine($"Start:\n{Colors.Magenta}{startRespons}{ANSICodes.Reset}\n\n"); // Print the response from the server to the console

//#### FIRST TASK 
// Fetch the details of the task from the server.
Response task1Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/psu31_4"); // Get the task from the server
Console.WriteLine(task1Response);


Task.InfoFromAPI task1 = JsonSerializer.Deserialize<Task.InfoFromAPI>(task1Response.content);

Console.WriteLine($"Task 1: {task1.title}");
Console.WriteLine($"{task1.description}");
Console.WriteLine($"Parameters: {task1.parameters}");


int[] numbers = task1.parameters.Split(',').Select(int.Parse).ToArray();

int sum = numbers.Sum();

Console.WriteLine($"Sum of all numbers: {sum}");


Response task1AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + task1.taskID, sum.ToString());
Console.WriteLine($"Response: {task1AnswerResponse.content}");

//#### SECOND TASK 
Response task2Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/otYK2"); // Get the task from the server
Console.WriteLine(task2Response);

Task.InfoFromAPI task2 = JsonSerializer.Deserialize<Task.InfoFromAPI>(task2Response.content);

Console.WriteLine($"Task 2: {task2.title}");
Console.WriteLine($"{task2.description}");
Console.WriteLine($"Parameters: {task2.parameters}");


string[] words = task2.parameters.Split(",");
string[] uniqueWords = words.Distinct(StringComparer.OrdinalIgnoreCase).OrderBy(word => word).ToArray();
string resultTask2 = string.Join(",", uniqueWords);
Console.WriteLine($"Result: {resultTask2}");


Response task2AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + task2.taskID, resultTask2);
Console.WriteLine($"Response: {task2AnswerResponse.content}");

//#### THIRD TASK 
Task.InfoFromAPI task3 = new Task.InfoFromAPI();
task3.taskID = "aLp96";
Response task3Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + task3.taskID); 
task3 = JsonSerializer.Deserialize<Task.InfoFromAPI>(task3Response.content);

Console.WriteLine($"Task 3: {task3.title}");
Console.WriteLine($"{task3.description}");
Console.WriteLine($"Parameters: {task3.parameters}");

int numberTask3 = int.Parse(task3.parameters);
string oddOrEven(int numberTask3)
{
    if (numberTask3 % 2 == 0)
    {
        return "even";
    }
    else
    {
        return "odd";
    }
}

string resultTask3 = oddOrEven(numberTask3);
Console.WriteLine($"Result: {resultTask3}");


Response task3AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + task3.taskID, resultTask3);
Console.WriteLine($"Response: {task3AnswerResponse.content}");

//#### FOURTH TASK 
Task.InfoFromAPI task4 = new Task.InfoFromAPI();
task4.taskID = "kuTw53L";
Response task4Response = await httpUtils.Get(baseURL + taskEndpoint + myPersonalID + "/" + task4.taskID); 
task4 = JsonSerializer.Deserialize<Task.InfoFromAPI>(task4Response.content);

Console.WriteLine($"Task 4: {task4.title}");
Console.WriteLine($"{task4.description}");
Console.WriteLine($"Parameters: {task4.parameters}");

int[] sequence = task4.parameters.Split(',').Select(int.Parse).ToArray();


bool IsPrime(int number)
{
    if (number <= 1)
        return false;
    if (number <= 3)
        return true;

    if (number % 2 == 0 || number % 3 == 0)
        return false;

    for (int i = 5; i * i <= number; i += 6)
    {
        if (number % i == 0 || number % (i + 2) == 0)
            return false;
    }

    return true;
}
List<int> primeNumbers = new List<int>();
foreach (int number in sequence)
{
    if (IsPrime(number))
    {
        primeNumbers.Add(number);
    }
}


string resultTask4 = string.Join(",", primeNumbers);
Response task4AnswerResponse = await httpUtils.Post(baseURL + taskEndpoint + myPersonalID + "/" + task4.taskID, resultTask4);
Console.WriteLine($"Response: {task4AnswerResponse.content}");
