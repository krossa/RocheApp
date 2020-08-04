# RocheApp
Roche InterReview_v1.2

Hi! I hope that it will not be too boring :)

Also, I would be happy to discuss these topics or any other with you.

## Running Tests

Before running tests you need to update `appsettings.json` in RocheApp.Integration.Tests project.
Please set the correct database connection string `ConnectionString` *(Microsoft SQL Server, DB does not have to exist)*.
It should be a different database than the one used for the console application.

You can run tests in your IDE or using dotnet core cli.
> dotnet test -v n

## Running Console Application

Before running the application please update `appsettings.json` in RocheApp.Database.Runner and ConsoleClient projects.
You need to provide the correct connection string *(Microsoft SQL Server, db does not have to exist)*. The same in both files, preferably different then in test project :)

You need to also run RocheApp.Database.Runner once to set up the database. Console application with a simple interface is in the ConsoleClient project.

## Validation

The client should not call Domain. API would expose the Domain. Each type of client should have its own API. 
WebApplication Client, Mobile Phone Client, or REST Client. This is loosely based on Trinity Architecture (https://medium.com/oregor/the-trinity-architecture-c89ed5743c1e)
Each type of user could have his own API (user, superuser, admin). All those clients can require different types of validation.
For example, MobilePhoneClient can't add pets but WebApp can. Users can't add points but admin can etc.
It can be implemented in appropriate API layer.
The interface could look something like this:
```
public interface IValidator
{
    ValidationResult Validate();

    bool IsValid();
}

public class ValidationResult
{
    public bool Success { get; set; }
    public IEnumerable<Error> Errors { get; set; }
}

public class Error
{
    public string Message { get; set; }
    public string Property { get; set; }
}
```

## Remarks

#### Update functionality

##### update
This is an interesting case. Update function needs to update all records from one table n-time (n is a parameter).
I could handle it with different approaches.

I choose to updated records per iteration at once as pure SQL (dapper). If other users would run the function at the same time iterations can interlace. All function executions will increment values by the correct amount (iteration * row index). No data will be overwritten. It requires only n-factor DB calls.
I don't have detailed business requirements so I chose this approach.

Another option would be to update row by row but again updates would have to interlace. This would be also more time consuming as we would have many more calls to DB (number of rows * n-factor)

We could also try a different approach. Each update function would calculate all iterations in memory. After that write the whole result to DB. Now we would have to lock the whole table for the process of calculation. If not, then different calls could overwrite the result of the previous call.
I would not recommend this approach even tho it requires only one call.

##### deleting pets

Deletion is first handled by PetDeleter. It makes sure that call to DB is done only when necessary.
```
user.PetsDeleted == 0 && 
user.Pets.Any() &&
user.ExperiencePoints > _settings.PointsThresholdForDeletingPets
```

When a call is made to DB locking transaction ensures integrity of the process.
```
EXEC sp_getapplock @Resource='DeletePets', @LockMode='Exclusive', @LockOwner='Transaction', @LockTimeout = 10000
```
after which I again check whether pets weren't already deleted
```
INSERT INTO @PetIdsToDelete
SELECT p.PetId FROM [dbo].[Pet] as p
INNER JOIN [dbo].[User] as u on p.UserId = u.UserId
WHERE p.PetId IN @PetIds
AND u.PetsDeleted = 0 
```

#### Async

I have implemented async programming. I am aware that it lacks some functionality.
For example optional passing of Canncelation Token. Also after learning how I should use this code. I could add handling of Synchronization Context (ConfigureAwait(false))

#### Schema changes

- Add a relation between Pet and User with a foreign key.
- Reorder creation of tables so FK can be created.
- Add index on Pet.UserId. For better performance.
- Change TimeStampValue to RowVerion. Those are the same thing but TimeStamp is deprecated.
- Add column ExperiencePoints. To store experience points.
- Add column PetsDeleted. To store information on whether pets where deleted.


