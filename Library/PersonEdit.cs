using System.ComponentModel;

namespace Library
{
    public class PersonEdit : ErrorBase, INotifyPropertyChanged
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
                OnPropertyChanged(nameof(Name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
