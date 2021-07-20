namespace DataErrorInfo
{
    public class PersonEdit : ErrorBase
    {
        public PersonEdit()
        {
            Name = string.Empty;
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    AddError(nameof(Name), "Name is required");
                else
                    RemoveError(nameof(Name), "Name is required");
                _name = value;
            }
        }
    }
}
