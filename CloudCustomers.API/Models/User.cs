namespace CloudCustomers.API.Models;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? email { get; set; }
    public Address? Address { get; set; }
    public User()
    {
        
    }
}