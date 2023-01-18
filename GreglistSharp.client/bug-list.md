# Bug List

Car Controller File
1. In the get by id route I took off the id part so I changed it from [HttpGet("{id}")] to [HttpGet]
2. On my delete car I took out the [Authorize] and the             Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext) line
3. Took out my [FromBody] from my Edit Car route in my parameters

Startup File
1. Took out my cars service transient line to show off dependency error

Service File
1. I took return car in my GetById function to emphasize that you need to return something or your other files will be angry. Also a good time to show you can return right on the line or you can alias out and then return that.
2. changed my original.description to update.description only

Repo File
1. Took WHERE id = @Id; on the edit route that way it edits every single car when you perform an edit
2. Create took out ExecuteScalar to just Execute
3. Get Cars took off toList() to show what happens when it cant convert type
4. GetById took off FirstOrDefault() so it would throw an error for the whole sql statement
5. Tricky One In my GetById I changed  return _db.Query<Car, Account, Car>(sql, (car, account) => to  return _db.Query<Car, Account, Car>(sql, (account, car) =>

In my parentheses this shows that order matters and you need to be consistent so your naming doesn't get all messed up with you are mapping an object
