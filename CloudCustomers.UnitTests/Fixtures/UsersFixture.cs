using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTests.Fixtures
{
	public static class UsersFixture
	{
		public static List<User> GetTestUsers() {
			return new() {
				new User() {
					Id = 1,
					Name = "John Doe",
					email = "johnDoe@example.com",
					Address = new Address() {
						City = "New York",
						Street = "Main Street",
						ZipCode = "12345"
					}
				},
				new User() {
					Id = 2,
					Name = "Jane Doe",
					email = "janeDoe@example.com",
						Address = new Address() {
						City = "New York",
						Street = "Main Street",
						ZipCode = "12346"
					}
				},
				new User() {
					Id = 3,
					Name = "John Smith",
					email = "johnSmith@example.com",
						Address = new Address() {
						City = "New York",
						Street = "Main Street",
						ZipCode = "12347"
					}
				}
			};
		}
	}
}