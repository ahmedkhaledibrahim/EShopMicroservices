namespace Ordering.Persistence.Data
{
    public static class InitialCustomerData
    {
        public static IEnumerable<(Guid Id,string name, string email)> Customers => [
              (Guid.Parse("58c49479-ec65-4de2-86e7-033c546291aa"),"John", "john.doe@example.com"),
              (Guid.Parse("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"),"Jane", "jane.smith@example.com")
        ];
    }
}
