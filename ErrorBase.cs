using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DataErrorInfo
{
    public class ErrorBase : IDataErrorInfo, INotifyDataErrorInfo
    {
        public bool EnableIDataErrorInfo { get; set; } = true;
        public bool EnableINotifyDataErrorInfo { get; set; } = true;

        private Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();

        private string GetError(string propertyName)
        {
            var builder = new StringBuilder();
            if (_errors.ContainsKey(propertyName))
            {
                foreach (string error in _errors[propertyName])
                    builder.Append($"{error},");
            }
            if (builder.Length > 0)
                return builder.ToString().Substring(0, builder.Length - 1);
            else
                return string.Empty;
        }

        private string GetErrors()
        {
            var builder = new StringBuilder();
            foreach (var error in _errors)
                builder.AppendLine(error.Key + ": " + GetError(error.Key));
            if (builder.Length > 0)
                return builder.ToString().Substring(0, builder.Length - 2);
            else
                return string.Empty;
        }

        private bool HasErrors()
        {
            return _errors.Count > 0;
        }

        protected void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
                _errors.Add(propertyName, new List<string>());
            _errors[propertyName].Add(error);
            if (EnableINotifyDataErrorInfo)
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void RemoveError(string propertyName, string error)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors[propertyName].Remove(error);
                if (_errors[propertyName].Count == 0)
                    _errors.Remove(propertyName);
                if (EnableINotifyDataErrorInfo)
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                if (EnableINotifyDataErrorInfo)
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                if (EnableIDataErrorInfo)
                    return GetError(columnName);
                else
                    return null;
            }
        }

        string IDataErrorInfo.Error
        {
            get
            {
                if (EnableIDataErrorInfo)
                    return GetErrors();
                else
                    return null;
            }
        }

        bool INotifyDataErrorInfo.HasErrors
        {
            get
            {
                if (EnableINotifyDataErrorInfo)
                    return HasErrors();
                else
                    return false;
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyName)
        {
            if (EnableINotifyDataErrorInfo)
            {
                if (_errors.ContainsKey(propertyName))
                    return _errors[propertyName];
                else
                    return new List<string>();
            }
            else
            {
                return null;
            }
        }
    }
}
