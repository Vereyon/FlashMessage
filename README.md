FlashMessage
============

FlashMessage provides easy cross request notifications for ASP.NET MVC based on Twitter Bootstrap. It solves the problem with flashing the user a notification or message when using the Post/Redirect/Get pattern.

Usage
-----

Install the [FlashMessage NuGet package](https://www.nuget.org/packages/Vereyon.Web.FlashMessage/).

### Rendering flash messages

Typically you want to render all queued flash messages in your Layout Razor template using the following code:

```C#
@Html.RenderFlashMessages()
```

### Queuing flash messages

Queuing a confirmation message for display on the next request after for example user login is done as follows:

```C#
// User successfully logged in
FlashMessage.Confirm("You have been logged in as: {0}", user.Name);
return RedirectToLocal(returnUrl);
```

Different types of messages can be scheduled using different static methods on the FlashMessage object:

```C#
FlashMessage.Info("Your informational message");
FlashMessage.Confirm("Your confirmation message");
FlashMessage.Warning("Your warning message");
FlashMessage.Danger("Your danger alert");
```

### Advanced options

Using the FlashMessage.Queue() method advanced options are available:

```C#
FlashMessage.Queue(string.Format("You have been logged in as: {0}", user.Name), "Title", FlashMessageType.Confirmation, false);
```

Tests
-----

Got tests? Yes, see the tests project. It uses xUnit.


More information
-----

 * [FlashMessage NuGet package](https://www.nuget.org/packages/Vereyon.Web.FlashMessage/)
 * [CodeProject article on FlashMessage](http://www.codeproject.com/Articles/987638/Post-Redirect-Get-user-notifications-for-ASP-NET-M)
 * [Used in AlertA Contract Management](http://www.alert.eu)

License
-------

[MIT X11](http://en.wikipedia.org/wiki/MIT_License)