### LandMarkPin Application 

#### program design  and approach 

This application is designed into two de-coupled projects. 
- #### Landmark.API 
    - This project is a .Net core Web API  using SQL server localDb as its persistence  storage. It provides RESTFul API to front-end application.
    It is using .Net Core entity framework to perform CRUD operations.
    It is divided into different folders structure for maintain-ability purposes
        - <b>Data</b> : It is Entity Framework Database context class. This class is injected as depednecy into API's repository
        - <b>Entities</b> : This folder contains data model's classes. This Application has two data entity classes : ` Note` & ` Position`
        - <b>Repository</b> : It is repository layer of the API that performs  CRUD operation on local database. It has dependency on `DbConext` class and is injected as dependency into WebApi controller.The repository's methods is designed as  Interface `INoteRepository` and `NoteRepository` implements that interface.
        - <b>Migrations</b> : This folder contains the auto-generated Db operation performed by entity framework
        - <b> Dependencies</b> : This folder contains external dll that is used in the application. `LinqKit` is .Net search predicate builder utility.
        - <b> Controller </b> : This folder contains API controller class : ` NoteController`. It provides RESTFull API end points to front end application. End points are as follow :
          - `POST /api/note` It post a new note to the backend  and return `201` http status code
          - `GET  /api/note?lat='{<user position's latitude'}&lng='{user's position longitude}'` .It returns all the notes which are posted in that particular position. 
          -  `GET /api/note/{user}?lat='{<user position's latitude'}&lng='{user's position longitude}'` .It returns all the notes which are posted in that particular position for a particular user.
          -  `GET /api/note/search?query='{search query}'`.It  searches all the notes in its user and  content based on the query.
        - <b> Program.cs & Startup.cs </b> : These are .Net core standard classes. `Startup.cs` resolves the dependencies, get the connection string and enables CORS for APIs. 
        `Program.cs` creates an empty database if it does not exist and initiate the secure pipeline.

        
- #### Landmark.Front
    - This project is front end of the application. It is implemented as single page application using `AngularJS`. It consumes the backend APIs using angular js http and perform data binding to view.The project structure is as follow.
        - `app\ core` : This folder contains custom javascript of application 
          - <b>app.js</b> : This JS file initializes the angular app module and register different angular route 
          - <b>LandMarkController.js</b> : This JS file calls API endpoint via Angular $http cobject and binds it to angular view
          - <b>searchController.js</b> This JS file calls search API and binds the result to search view
          - <b>map.js</b> This JS file provider map-related functionalities like drawing on map, closing map info window etc.
        - `app\views`: This folder contains html templates which loads via angular route into index.html
        - `app\lib` : Contains Javascript libraries like angular & bootstrap. They are copied as part of NPM build process.

### how to run the project 
1. First run the backend API project from <b> LandMark.API</b> folder  
   - from visual studio, run <b>LandMark.API</b> project 
2.  Then build front-end packages and run the front-end by  following  below steps
   - From root folder, go to <b> LandMark.Front</b> folder
   - run `npm install` from command line to install node packages 
   - run `npm start` to start http server 
   - browse `localhost:8000\index.html ` for the application main page
   - For the first time , application asks to enable browser geo-location and enter the name. It saves the name to cookies for new notes.

### WorK Breakdown Structure 
Time spent on each part of application 
  - Backend : 7 hours including database creation 
  - Frontend : 12 hour : AngularJS + HTML+ CSS markup 
  - Documentation : 2 hours : Write README file and comment source code

### Limitation 
- I ran out of time and could not implement test coverage 
- It uses SQL server local db 
- It is relying on browser cookie for user session / name

### Application functionalities demonstrated
1. When loaded show all the notes 

![File](./Img/allNotes.png)

2. Filter note based on location or user 

![File](./Img/locationNote.png)
 
3. Search : By default Notes are not displayed when search page is viwed. user can show a specific note on Map from table of result.

![File](./Img/search.png)