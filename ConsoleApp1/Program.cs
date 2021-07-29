using Library;
using System;
using System.Collections;
using System.ComponentModel;
using System.Text;

namespace DataErrorInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new PersonEdit();
            var idei = person as IDataErrorInfo;
            var indei = person as INotifyDataErrorInfo;

            indei.ErrorsChanged += (sender, e) => Console.WriteLine("### ErrorsChanged");

            person.EnableINotifyDataErrorInfo = false;
            Console.WriteLine("*** IDataErrorInfo ***");
            Console.WriteLine("Should be error");
            Console.WriteLine(idei.Error);

            person.Name = "Fred";
            Console.WriteLine("Should be null");
            Console.WriteLine(idei.Error);

            person.Name = "";
            Console.WriteLine("Should be error");
            Console.WriteLine(idei.Error);

            person.EnableIDataErrorInfo = false;
            person.EnableINotifyDataErrorInfo = true;
            Console.WriteLine();
            Console.WriteLine("*** INotifyDataErrorInfo ***");
            Console.WriteLine("Should be error");
            Console.WriteLine(GetErrors(indei.GetErrors("Name")));

            person.Name = "Fred";
            Console.WriteLine("Should be null");
            Console.WriteLine(GetErrors(indei.GetErrors("Name")));

            person.Name = "";
            Console.WriteLine("Should be error");
            Console.WriteLine(GetErrors(indei.GetErrors("Name")));
        }

        private static string GetErrors(IEnumerable errors)
        {
            var builder = new StringBuilder();
            foreach (var error in errors)
                builder.Append($"{error},");
            if (builder.Length > 2)
                return builder.ToString().Substring(0, builder.Length - 1);
            else
                return string.Empty;
        }
    }
}
