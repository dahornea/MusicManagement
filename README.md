# V1

Choose any programming language and a **backend** framework and setup a development environment in it. Create an application with CRUD functionalities for a single entity of your choice. The entity should have at least 5 attributes, not counting the ID. You do not need any validations.

Remarks:
- Your application should either expose a REST API (like Django REST Framework, Flask) or a webpage that can be accessed in the browser (like Django)
- Console apps are **not** allowed
- Frontend frameworks / libraries (React, Angular) are **not** allowed
- You should be able to explain basic things about your framework of choice (where routes are defined, how the models are handled etc.) 
- You will also be marked on good coding practices and design principles, as they have been taught in Fundamentals of Programming and Object Oriented Programming and as they apply to your choice of framework

Suggestions:
- You can probably find libraries in your favorite language that help you build Web applications and REST APIs
- I suggest you make a REST API, as that will become mandatory later on anyway
- Java has Spring, Python has Django, C# has WebApi etc. The keywords to search for are your language + rest api
- Make sure you get comfortable with reading your choice's documentation and that you pick something with an active community, as you will be spending a lot of time studying stuff on your own


# V2
- Add at least 2 more entities with at least `5` attributes each: you should have at least `3` entities in total.
- Add a **1 to many** relation between two of your entities. Some of the entities can be unrelated, but be prepared to also add a many to many relation soon. Choose your entities accordingly.
- Add storage to a database if you don't already have it: any database type is allowed. Rerunning the application should keep previously existing data. It's ok if changes to the data model drops and recreates the database (you do not need migrations yet).
- Implement CRUD for at least one more entity and filtering on a numeric field for at least one entity. The filtering should return all entities with the numeric field higher than a given value.
- Have a REST API usable in Postman.


# V3

You will need to:
- Add a **many to many** relation between `2` entities. This should be added as a separate entity containing the two related entities or their IDs and at least 2 additional attributes. For example: `Transaction` with `Product` and `Client` as the related entities and `Date` and `Volume` as the additional attributes.
- Make sure all of your entities are part of at least one `1 to 1`, `1 to many` or `many to many` relation.
- Add CRUD for all entities.
- Add at least `10` realistic-looking records for each of your models / tables.
- Use Trello or ClickUp and set up a Kanban board (with at least `Todo`, `Doing`, `Done` or equivalent lists). You will need to update this for all functionalities you do from now on. Your lab instructor will give you feedback on how to improve its setup.
- Create a UML diagram for your application and a database diagram for your database. 
- Add a statistical report involving two entities. For example: show all movies ordered by the average age of their actors. Such reports **need** to show the computed field: the average age of the actors in this case. You probably want to use a `DTO` class for this.

For this and all future assignments, unless otherwise specified, you are not allowed to write any raw SQL queries: it must all be done through an ORM.
