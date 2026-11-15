namespace Ordering.Infrastructure.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Customer> Customers => new List<Customer>
    {
        Customer.Create(CustomerId.Of(new Guid("08570594-1d2f-402b-987a-0d6b7fb4656f")),"Rahul","rahul@gmail.com"),
        Customer.Create(CustomerId.Of(new Guid("20a67ffa-3f38-4f2a-839b-32ea74bc192b")),"Ravi","ravi@gmail.com")
    };

    public static IEnumerable<Product> Products => new List<Product>
    {
        Product.Create(ProductId.Of(new Guid("63aec511-cc98-43a6-98e4-40d08426cd4a")),"IPhone X",1000),
        Product.Create(ProductId.Of(new Guid("d77b6ed3-fe17-40a6-b43c-989e5093deb2")),"Samsumg S26",800),
        Product.Create(ProductId.Of(new Guid("cac8b799-8723-4d0c-8670-5db5be31d044")),"POCO X5 Pro 5g",400),
        Product.Create(ProductId.Of(new Guid("cb16cc25-e18d-416f-9850-19561201c869")),"Nokia 1122",200)
    };

    public static IEnumerable<Order> OrderItems
    {
        get
        {
            var address1 = Address.Of("Rahul", "Macha", "rahul@gmail.com", "hometown", "India", "Indian state", "0007");
            var address2 = Address.Of("Ravi", "Macha", "ravi@gmail.com", "hometown", "India", "Indian state", "0007");

            var payment1 = Payment.Of("SBI Cards", "9876543210122", "12/30", "123", 1);
            var payment2 = Payment.Of("Kotak Cards", "2222666448833", "12/30", "321", 2);

            var order1 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("08570594-1d2f-402b-987a-0d6b7fb4656f")),
                OrderName.Of("Ord_1"),
                shippingAddress: address1,
                billingAddress: address1,
                payment1
                );
            order1.Add(ProductId.Of(new Guid("63aec511-cc98-43a6-98e4-40d08426cd4a")), 1, 1000);
            order1.Add(ProductId.Of(new Guid("d77b6ed3-fe17-40a6-b43c-989e5093deb2")), 2, 800);

            var order2 = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(new Guid("20a67ffa-3f38-4f2a-839b-32ea74bc192b")),
                OrderName.Of("Ord_2"),
                shippingAddress: address2,
                billingAddress: address2,
                payment2
                );
            order2.Add(ProductId.Of(new Guid("cac8b799-8723-4d0c-8670-5db5be31d044")), 1, 400);
            order2.Add(ProductId.Of(new Guid("cb16cc25-e18d-416f-9850-19561201c869")), 2, 200);

            return new List<Order> { order1, order2 };
        }
    }
}
