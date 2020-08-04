namespace RocheApp.Domain.Services.User
{
    public class UserFilter
    {
        private string _firstName;

        public string FirstName
        {
            get => $"%{_firstName}%";
            set => _firstName = value;
        }

        public byte? Status { get; set; }

        public bool HasFirstNameValue => !string.IsNullOrWhiteSpace(_firstName);
        public bool HasStatusValue => !(Status is null);
        public bool HasAnyValues => HasFirstNameValue || HasStatusValue;

        public static UserFilter EmptyFilter => new UserFilter();
    }
}