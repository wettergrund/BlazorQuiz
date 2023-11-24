# BlazorQuiz
## Intro
This is a fullstack quiz application using Blazor.
The program handles all logic serverside and presents the data to the razor-client via our shared-viewmodels to minimize data exposure.
We've decided to do abstraction-layers on serverside in the Services folder example: 
Controller -> IGameService -> GameService.

<div align="center">
 
![showcase](https://github.com/wettergrund/BlazorQuiz/assets/50584818/25c4ea3b-f7b5-48da-bd8d-ae081ab7e49f)

</div>


## Setup
 - Blazor / ASP.NET Core
 - Authorization with JWT
 - Entity Framework Core
 - Code First with SQL Server

## Key Features
- ❓ User can create quizzes and let other users take the quiz
- 🖼 A quiz question can contain a picture, video or just text
- ⌛ User can set time limit for the quiz (timer applies to each question)
  - User score is affected by elapsed time
- 🥇 User can list their quizzes and see who have answered them

## The Code
|**Services**|**Breakdown**|
|-|-|
|GameService|Game logic|
|MediaService|Media logic, upload and retrive img/vid-files|
|ProfileService|User page logic (ex. display data on who played your quizzes)|
<br>

|**Data Base**|**Breakdown**|
|-|-|
|ApplicationUser|Table contains all user data so we can implement autorization with JWT|
|QuizModel|Table contains set atributes for quiz and has a collection to questions and FK to User|
|QuestionModel|Questions is created for specific quiz so there are FK to QuizModel and also <br> have a FK to MediaModel|
|UserQuizModel|Is created when a user joins a quiz. This contains the score of user. FK tu Quiz and User|
<br>


|**Shared/ViewModels**|**Breakdown**|
|-|-|
|GameQuizViewModel|Quiz data to play quiz. Including list of questions|
|QuestionSharedViewModel|Questions data. This is embeded in GameQuizViewModel|
|GuessCheckViewModel|Guess data. Send to be verified and api returns bool|
|UserCreatedQuizViewModel|UserName and QuizId. Used to print list of quizzes|
|SubmitQuizViewModel|Sends finished game. UserQuizID and list of GuessCheckViewModel|
|NewMediaViewModel|Upload media via URL|
|NewQuestionViewModel|Create new question. Contains imgURL, question and answer|
|NewQuizViewModel|Create new quiz. Contains atributes and NewQuestionViewModel|

## Database Diagram
![blazorquiz (1)](https://github.com/wettergrund/BlazorQuiz/assets/112638774/ff41f5d3-715d-4ea2-867b-b88f6d6a737c)

## Contributors

<table>
  <tr>
    <td align="center"><a href="https://github.com/berkowicz"><img src="https://avatars.githubusercontent.com/u/112638774?v=4" width="100px;" alt="Daniel Bekowicz"/><br /><sub><b>Daniel Berkowicz</b></sub></a><br /></td>
    <td align="center"><a href="https://github.com/wettergrund"><img src="https://avatars.githubusercontent.com/u/50584818?v=4" width="100px;" alt="Jonas Wettergrund"/><br /><sub><b>Jonas Wettergrund</b></sub></a><br />
  </tr>
</table>
