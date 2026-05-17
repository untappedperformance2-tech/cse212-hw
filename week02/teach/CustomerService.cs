/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Add one customer and then serve them
        // Expected Result: The customer that was added should be displayed
        Console.WriteLine("Test 1");
        var cs = new CustomerService(5);
        cs.AddNewCustomer();
        cs.ServeCustomer();
        // Defect(s) Found: ServeCustomer was removing the customer before reading it

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Add two customers and serve them both
        // Expected Result: Customers should be served in the order they were added
        Console.WriteLine("Test 2");
        cs = new CustomerService(5);
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        cs.ServeCustomer();
        cs.ServeCustomer();
        // Defect(s) Found: None once defect from Test 1 was fixed

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Try to serve a customer from an empty queue
        // Expected Result: An error message should be displayed
        Console.WriteLine("Test 3");
        cs = new CustomerService(5);
        cs.ServeCustomer();
        // Defect(s) Found: No check for empty queue, it crashed instead of showing an error

        Console.WriteLine("=================");

        // Test 4
        // Scenario: Fill the queue to max size and try to add one more
        // Expected Result: Error message should display when trying to add the extra customer
        Console.WriteLine("Test 4");
        cs = new CustomerService(3);
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        cs.AddNewCustomer();
        Console.WriteLine(cs);
        // Defect(s) Found: Used > instead of >= so one extra customer could be added

        Console.WriteLine("=================");

        // Test 5
        // Scenario: Create a queue with an invalid max size of 0
        // Expected Result: Max size should default to 10
        Console.WriteLine("Test 5");
        cs = new CustomerService(0);
        Console.WriteLine(cs);
        // Defect(s) Found: None, the default logic was already correct
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        if (_queue.Count <= 0) {
            Console.WriteLine("No customers in the queue.");
            return;
        }

        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}