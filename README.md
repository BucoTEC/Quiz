# Quiz API

This repository contains an ASP.NET Core Web API for managing Quizzes. It serves
as a showcase of skills in designing and building a RESTful API, implementing
various patterns and techniques that are for a simple application as this
overengineering. The business logic of the selected requirements is not complex,
so a traditional layered architecture approach was chosen instead of
domain-driven design.

## Project Structure

The main project code is located under the `src` directory and is divided into
the following components:

- `Quiz.Api`: .NET Core Web API project
- `Quiz.Bll`: Class library representing the business logic layer
- `Quiz.Dal`: Class library representing the data access layer
- `Quiz.UnitTest`: Class library containing unit tests

Unit test coverage primarily focuses on the services in the business logic
layer.

## Database

The application uses a code-first approach for database access and design,
leveraging EF Core with a PostgreSQL driver. In development mode, it
automatically applies the initial migration.

## CI-CD

The repository at its current state is not configured to deploy the code to any
specific infrastructure but it contains a build and test pipeline ( builds the
dotnet application and runs the unit tests ) that triggers on creation of pull
request and merge to give basic continues integration advantages to the
application.

## Containerization

The application is containerized for ease of cross-platform deployment and
testing.

## Run Locally with Docker and docker-compose

If your machine has the Docker daemon installed, you can run the application
using the following steps:

1. Navigate to the root directory of the project.
2. Execute the command `docker-compose up -d --build`.
   - This command will spin up the containerized application based on the
     Dockerfile located under the `Quiz.Api` directory.
   - It will also start PostgreSQL and pgAdmin for database management.
3. Additional configuration details for the container instances can be found in
   the `docker-compose.yml` file.
4. Note that volumes for the postgres container are configured by default to
   write to the same directory and the volume data will not be registered by
   version control due to the configuration of the gitignore.

The initial run up of the docker-compose wil take a bit of time to pull all the
needed images, after that all other runs will be much faster. Note that in the
execute command listed at the begging of this section the flag `--build` is
included to rebuild the docker image for the quiz api container just to make
sure its alway up to date.

After docker finishes spinning up the needed containers you can access the API
on `http://localhost:5075/swagger/index.html`

When you are finished testing be sure to remember to run `docker-compose down`
so that the docker demon and containers to not use excess resurges of you
machine.

## Run Locally on Machine

To run the application locally without Docker, follow these steps:

1. Set up .NET 8.0 runtime on your local machine.
2. Install and configure PostgreSQL.
3. Update the `appsettings.json` file to point to the correct PostgreSQL
   database of your choice.

## Documentation

Bellow is a basic overview of the endpoints exposed by the API for a more
detailed information of the apis capabilities you can run it locally and access
the swagger ui containing more information about the API its capabilities and
how to use it.

## Endpoints

### Questions

#### Retrieve a Question

- **GET** `/api/Questions/{id}`
  - Retrieves a question by its unique identifier.

#### Update a Question

- **PUT** `/api/Questions/{id}`
  - Updates an existing question.

#### Delete a Question

- **DELETE** `/api/Questions/{id}`
  - Deletes a question by its unique identifier.

#### Search for Questions

- **GET** `/api/Questions`
  - Searches for questions based on the provided search criteria.

#### Create a Question

- **POST** `/api/Questions`
  - Creates a new question.

### Quizzes

#### Retrieve a Quiz

- **GET** `/api/Quizzes/{id}`
  - Retrieves a quiz by its unique identifier.

#### Update a Quiz

- **PUT** `/api/Quizzes/{id}`
  - Updates an existing quiz.

#### Delete a Quiz

- **DELETE** `/api/Quizzes/{id}`
  - Deletes a quiz by its unique identifier.

#### Search for Quizzes

- **GET** `/api/Quizzes`
  - Searches for quizzes based on the provided search criteria.

#### Create a Quiz

- **POST** `/api/Quizzes`
  - Creates a new quiz.
