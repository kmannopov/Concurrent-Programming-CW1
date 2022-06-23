# Coursework for Module 6BUIS011C-n - Concurrent Programming

Learning Objectives and Outcomes:
- Develop multithreaded applications efficiently.
- Design applications in which threads coordinate in the presence of dependencies.
- Validate the correctness of concurrent algorithms.
- Apply concepts of concurrency in practical applications (e.g. SOA, RPC, etc.).

# Introduction

The following report will analyze the application created for the
Concurrent Programming module and how concurrency was used to optimize
the application. The idea behind the application was to
read the card swipes from students entering and exiting a university and
save them to a database. The application consists of two parts: a
backend service which receives the swipes directly from the terminals
and saves the swipes to the database. The frontend Windows application
allows the user to start receiving the swipes. Once started, the
application updates and shows the status of each terminal sending
swipes. The user is also able to view all the swipes saved to the
database.

# Application Development

The aim of the project was to design a system which uses concurrency to
increase the performance of our application. In addition, recognizing
the potential pitfalls of concurrency and taking the necessary steps to
ensure proper functioning of the application was also key. These two
aims were reached with the use of multi-threading, and the use of a
semaphore to lock any (in our case only one) critical sections of code.

## Multi-threading

Multi-threading is used both within the UI and Web Service parts of the
application. In the UI, there is typically a main thread which monitors
user inputs, listens for events, etc. When updating the UI or running
background tasks, we must create a separate thread to ensure that the
entire application does not freeze and remains responsive. In addition,
objects such as a DataGridView, Window, etc. each have their own
separate handle. When working with these objects, we must execute code
on the thread that owns the handle of the object we are trying to
modify. For this purpose, we use a MethodInvoker to delegate an
anonymous function where we can proceed to modify the UI element.

In Addition, the Web Service uses multi-threading by creating a separate
thread for each batch of swipes coming from each terminal. This approach
gives us the flexibility of choosing whether an individual thread can
run, wait its turn, run simultaneously with other threads, etc.

## Semaphore

A semaphore is a synchronization primitive used to control access to a
pool of shared resources. The type of semaphore used in our application
is called a counting semaphore. This type of semaphore counts the number
of threads accessing a critical section, and restricts access based on
the limits set by the developer. Another type of semaphore is the binary
semaphore, which is extremely similar in function to a lock or mutex,
which only allows access to one thread at a time. Counting semaphores
rely on the accessing thread to release the pool on its own. A counting
semaphore is used within our Web Service, specifically within the
StartCollectingSwipes() method. This method collects the swipes from the
SynConnection library, and saves them to the database. The maximum
number of threads which can access the critical section of the method is
3, as is required by the coursework requirements.

Flow Diagram:

![image](https://user-images.githubusercontent.com/79659647/175179986-c8e1b54d-516a-4d4b-95fc-05759716a6b7.png)

## User Interface:

![image](https://user-images.githubusercontent.com/79659647/175180010-d2ba9f53-dc2f-451b-9299-bb898200b4ed.png)

User Interface. Processes are running.

![image](https://user-images.githubusercontent.com/79659647/175180026-ea99bd51-7de8-48f6-a1e2-a7e10dada019.png)

User Interface. Processes are finished

![image](https://user-images.githubusercontent.com/79659647/175180038-586d6c69-dcb5-4d05-bb73-d9bf12b7fe5c.png)

User Interface. Showing all swipes.

## Design Principles

The underlying reason behind the coursework was to gain practical
experience building applications using a service-oriented architecture
while utilizing concurrency/parallelism to increase the speed and
efficiency of the application.

Service oriented architecture, put simply, is a way to make software
components reusable and easy to integrate into existing services.
Services use a common interface standard to achieve this, often called a
"contract". The services are then exposed using common network protocols
such as SOAP or REST. In our case, we used Windows Communication
Foundation, creating the service using the .NET Framework.

## Project Architecture

The project is created and structured based on a system architecture
called "Clean Architecture". In this type of architecture, dependencies
point inwards towards the core of the system, the Entities, Models, etc.
This is housed within a layer commonly titled "Core" or "Domain", which
has no dependencies. The "Application" layer contains all of the
business logic of the program, and is only dependent on the Domain
layer. Infrastructure contains our Data Access and persistence, and is
dependent on Application and Domain. The UI and Web Service work with
all layers.

![image](https://user-images.githubusercontent.com/79659647/175180090-761aacc7-bfc6-4eae-90c4-b5f3384084d5.png)

Typical "Clean Architecture" model

Our project follows this model, and this can be seen with our Code Map:

![image](https://user-images.githubusercontent.com/79659647/175180107-7fd4d554-fa8e-4750-8a30-ab9bb697acc6.png)

By maintaining the dependency rules, we can structure our project in a
way that is easier to maintain, scale, and understand.

# Alternatives to synchronization techniques

In the previous section, we discussed using semaphores to prevent a race
condition within our code. There are several different types of
synchronization techniques we can use to achieve the same goal.

## Mutex

A mutex is another type of synchronization primitive which limits access
to a shared resource. The primary difference from a semaphore is that a
mutex can only grant access to one thread at a time. Once a thread
acquires access to the shared resource, it is the thread's
responsibility to release access. If a thread requests access to a mutex
currently in use, it must wait until the current thread releases access.
Mutexes can be of two types: local and named mutexes. Local mutexes are
limited to the current process, while named mutexes work on the OS
level, so that resources can be shared between different processes.

## Lock

A lock (in C#) is simply a wrapper around the functionality of the
Monitor class. Monitor is used to grant and release access to shared
resources by locking and releasing objects. Functionally, it is
virtually identical to a mutex, except that it can only operate within
the process of the application.

## Threads as synchronization primitives

Threads themselves can also be used as a synchronization primitive. This
can be achieved by instructing a thread to wait until another thread is
completed. With this method, there is no concept of an owner of a shared
resource, so it is up to the developer to implement properly.

## Optimal solution for the current use case

In our use case, where different terminals each need to write a batch of
swipes to the database, almost any synchronization technique would work.
This is due to the fact that we are only adding new data and not
modifying any existing data, and that each swipe is an individual entry
that does not affect any other entries. Therefore, multiple threads
should be able to access and write to the database without creating a
race condition. A semaphore is the optimal solution in this case because
there is no reason to limit access to a single thread. With the use of a
semaphore, we can utilize the benefits of multi-threading while still
maintaining the ability to limit the degree of multi-threading (in case
the hardware is unable to handle heavier loads, for example).

# Advantages to using concurrent programming techniques

Concurrency offers several benefits over single threaded programming:

## Less frequent context switching

When a single core processor needs to run multiple threads at the same
time, it switches between them by saving the state of the current thread
and loading the state of the next thread. This is called context
switching, and is resource heavy on the CPU. With concurrent
programming, we can utilize multiple cores to run different threads at
the same time. With the technological development of the last 10-20
years, single core CPUs are almost completely obsolete, and it's
necessary to take advantage of the full capabilities of the hardware to
improve the user experience.

## Better utilization of resources

As discussed in the previous point, modern day CPUs almost always have
multiple cores. With concurrent programming, we can better utilize the
resources of the system. While one core is busy with the main thread of
a certain application, background tasks can be run on a separate core.

## Improve application responsiveness

In the example of our application, we use separate threads in the UI to
update different elements. This is just one of many ways concurrent
programming can be used to improve the application responsiveness, and
therefore, the user experience.

# Conclusion

Concurrency is an important aspect of software development, and it has
numerous performance and useability benefits over tradition
applications. However, developers must ensure that it is structured and
used properly to avoid some of the common pitfalls.
