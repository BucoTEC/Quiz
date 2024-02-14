# ToDo API

This is an ASP.NET Core Web API for managing ToDo items.

## Documentation

For detailed documentation of the API endpoints and schemas, please refer to the
[Swagger Documentation](http://localhost:5075/swagger/v1/swagger.json).

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
