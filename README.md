# MovieRental Exercise

This is a dummy representation of a movie rental system.
Can you help us fix some issues and implement missing features?

 * The app is throwing an error when we start, please help us. Also, tell us what caused the issue.

    - **There is no need to instantiate the dbcontext in the program file. Since the db context was previously added, only a require srevice was necessary.**

    ```
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<MovieRentalDbContext>();
        dbContext.Database.EnsureCreated();
    }
    ```

 * The rental class has a method to save, but it is not async, can you make it async and explain to us what is the difference?
     - **Sure. In sync methods the program awaits until the method completes the execution, so it´s a sequencial execution. Async methods allow to start a task and continues executing the program, under a no blocking execution approach. It´s usually harder to maitain an async strategy but it brings a lot of beneficts. The save method was refactored to async.**
 * Please finish the method to filter rentals by customer name, and add the new endpoint.
    - **Done!**
 * We noticed we do not have a table for customers, it is not good to have just the customer name in the rental.
   Can you help us add a new entity for this? Don't forget to change the customer name field to a foreign key, and fix your previous method!
    - **Done!**
 * In the MovieFeatures class, there is a method to list all movies, tell us your opinion about it.
    - **Since it is only intended to only read data, it should be called with AsNoTracking();**
   - **Instead of returning movies as List, it is better to return IEnumerable<Movie> for performance and separation of concerns;**
    - **Also, as the Movies table can increase in size, it is better to use pagination. A method was create as an example of a pretty simple pagination:**
    ```
    public IEnumerable<Movie> GetAllPaginated(int skip = 0, int take = 1000)
    {
        return _movieRentalDb.Movies
            .AsNoTracking()
            .Skip(skip)
            .Take(take)
            .ToList();
    }
    ```
 * No exceptions are being caught in this api, how would you deal with these exceptions?
    - **Try-Catch approach can be spread across the program in the core functionalities but a Global Exception Handler was implemented to catch all untreaded exceptions. For this purpose, a test endpoint was implemented:**
    ```
    GET {{MovieRental_HostAddress}}/error/ErrorHandlerHealthCheck
    ```
    

	## Challenge (Nice to have)
We need to implement a new feature in the system that supports automatic payment processing. Given the advancements in technology, it is essential to integrate multiple payment providers into our system.

Here are the specific instructions for this implementation:

* Payment Provider Classes:
    * In the "PaymentProvider" folder, you will find two classes that contain basic (dummy) implementations of payment providers. These can be used as a starting point for your work.
        - **Right. A Interface was implemented to provide extensibility and flexibility.**
* RentalFeatures Class:
    * Within the RentalFeatures class, you are required to implement the payment processing functionality.
        - **An async Payment Process method was implemented. Also a PriceCalculator was created and injected in way to keep responsabilities isolated.**
* Payment Provider Designation:
    * The specific payment provider to be used in a rental is specified in the Rental model under the attribute named "PaymentMethod".
        - **As more than one classes are configured for the new IPaymentProvider, the Payment Process make use of the PaymentMethod to choose between them.**
* Extensibility:
    * The system should be designed to allow the addition of more payment providers in the future, ensuring flexibility and scalability.
        - **Both MBWay provider and PayPal provider rely on scability provided by DI and IPaymentProvider. Also a new FailPaymentProvider was added and is only intended to throw a not implemented exception.**
* Payment Failure Handling:
    * If the payment method fails during the transaction, the system should prevent the creation of the rental record. In such cases, no rental should be saved to the database.
        - **A TransactionScope could be implemented but it does not deal with SQLite. So a transaction based in boolean return was a simple solution.**

- **The MovieRental.http file is ready with some requests.**
- **In order to have the database prepared for this solution, please run migrations:**


    ```
    dotnet ef database update
    ```