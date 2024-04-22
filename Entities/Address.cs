using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MyBoardsApp.Entities;

public class Address
{
    public Guid AddressId { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}