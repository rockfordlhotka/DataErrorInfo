# DataErrorInfo

This is a simple implementation of both `IDataErrorInfo` and `INotifyDataErrorInfo` in a single class, with flags to enable/disable the _behavior_ of each interface:

* `EnableIDataErrorInfo`
* `EnableINotifyDataErrorInfo`

The console app does very basic interaction with the `PersonEdit` class to establish that rules are added and removed as data changes.

The goal of this bit of code is to provide a basis for testing all the existing UI frameworks to see if they can handle a class that _implements_ both interfaces, even if one or the other is "disabled".

* [ ] Windows Forms
* [ ] Web Forms
* [ ] ASP.NET MVC
* [ ] WPF
* [ ] UWP
* [ ] WinUI
* [ ] Xamarin
* [ ] Maui
* [ ] Blazor
