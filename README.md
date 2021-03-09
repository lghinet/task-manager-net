# Task manager library 

With Task Manager we refer to a software
component that is designed for handling multiple
processes inside an operating system.

We want the Task Manager to expose the following functionality:
 - Add a process
 - List running processes
 - Kill/KillGroup/KillAll
 
## Installation
you have to install dotnet sdk fom https://dotnet.microsoft.com/download 

## Running tests 
```cmd
dotnet build task-manager.sln
dotnet test task-manager.sln
```

## Usage 
````c#
 var manager = new TaskManager(3, TaskManagerStoreStrategy.Priority);
 manager.Add(new Process("1", ProcessPriority.High));
 manager.Add(new Process("2", ProcessPriority.Medium));
 manager.Add(new Process("3", ProcessPriority.Medium));

 var p4 = new Process("4", ProcessPriority.High);
 manager.Add(p4);
 manager.Kill("4");
````
