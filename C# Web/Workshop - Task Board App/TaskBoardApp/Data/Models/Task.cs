using Humanizer;

namespace TaskBoardApp.Data.Models
{
    public class Task
    {

    }
}

//•	Id – a unique integer, Primary Key
//•	Title – a string with min length 5 and max length 70 (required)
//•	Description – a string with min length 10 and max length 1000 (required)
//•	CreatedOn – date and time
//•	BoardId – an integer
//•	Board – a Board object
//•	OwnerId – an integer(required)
//•	Owner – an IdentityUser object