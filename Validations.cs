using System.ComponentModel;
using System.Text.RegularExpressions;


namespace winui_cooler
{
   
    public class EmailValidation : ConcreteValidator
    {
        public string Message { get; set; } = "This email is not valid!";
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        public bool Validate(object value)
        {
            if (value is string text)
            {
               if (Regex.IsMatch(text, pattern))
                {
                    Valid = true;
                    return true;
                }
            }
            return false;
        }
    }

     public class PhoneValidation : ConcreteValidator
    {
        public string Message { get; set; } = "We can't call you using this!";
        string pattern = @"^(8|\+7)\d{10}$";
        public bool Validate(object value)
        {
            if (value is string text)
            {
               if (Regex.IsMatch(text, pattern))
                {
                    Valid = true;
                    return true;
                }
            }
            return false;
        }
    }

    
    public class PasswordValidation : ConcreteValidator
    {
        public string Message { get; set; } = "Your passwords don't match!";
        public bool Validate(object value, string original)
        {
            if (value is string text)
            {
                if (text == original)
                {
                    Valid = true;
                    return true;
                }
            }
            return false;
        }
    }

    public class OverrideRequiredValidation : ConcreteValidator
    {
        public string Message { get; set; } = "This email is not valid!";
        public bool Validate(object value)
        {
            if (value is string text)
            {
                if(text != string.Empty)
                {
                    Valid = true;
                    return true;
                }
            }
            return false;
        }
    }
    
    public class OverrideLenValidation : ConcreteValidator
    {
        public int MinLength {get; set; }
        public string Message { get; set; } = "This email is not valid!";
        public bool Validate(object value)
        {
            if (value is string text)
            {
                if(text.Count() >= MinLength)
                {
                    Valid = true;
                    return true;
                }
            }
            return false;
        }
    }

    public class ConcreteValidator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _valid;
        public bool Valid
        {
            get { return _valid; }
            set
            {
                if (_valid != value)
                {
                    _valid = value;
                    OnPropertyChanged(nameof(Valid));
                }
            }
        }

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}