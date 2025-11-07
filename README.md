# The Apprentice Adventure Game

This is a visual novel style decision-driven role playing game where the player lives the life of an apprentice software developer.
The player's choices influence their stats and progress including the time which affect the availability of choices. The game state
is fully managed using a finite state machine and saved using a persistent JSON save file.

## Project Deliverables

This project aims to deliver the following required components:

- A fully playable Game MVP (runnable locally using an .exe file).
- Detailed Decision Flow Chart showing the game design of decision making.
- Class Diagram (UML).
- A Comprehensive Test Scenario document, unit test results, and manual integration testing results.
- A Project Report detailing the software development process.

## Roles and Responsibilities

As a solo project, all responsibilities were handled by me.

| Role                            | Responsibilities                                                                                                             |
| :------------------------------ | :--------------------------------------------------------------------------------------------------------------------------- |
| **Developer / Project Manager** | Game Design, Flow Chart creation, Software Architecture (Class Diagram), Full-Stack Development, Testing, and Documentation. |

## How to Run the Game

1.  **Prerequisites:** Ensure you have a computer running the Windows 10 or 11 operating system.
2.  **Installation:** Clone this repository: `git clone [repository URL TBD]`, or download as a ZIP.
3.  **Setup:** Navigate to the project directory. If downloaded as a ZIP, unzip the folder using the Windows file explorer or use a third party tool such as 7zip
4.  **Run:** Open/Execute the main .exe file.

## Features

This initial MVP focuses on core gameplay and robust save/load functionality with minimal art, sound and user interface.
If the MVP is promising I want to expand the decision making tree with more sections and complex behaviour such as decision
based logic e.g If player does not brush in the morning, the effects of going to lunch with colleagues is reversed. I will
also consider adding the ability to undo choices.
