namespace TableStorageApp
{
    public class ModelFactory
    {
        public CustomerEntity Create(CustomerForm form)
        {
            return new CustomerEntity(form.LastName, form.FirstName)
            {
                Email = form.EmailAddress,
                PhoneNumber = form.PhoneNumer
            };
        }
    }
}