FlashMessage
============


| ASP.NET version    | Package       |
| ------------------ |:-------------:|
| ASP.NET Core 2 and up			| [![Nuget version](https://img.shields.io/nuget/v/Vereyon.Web.FlashMessage)](https://www.nuget.org/packages/Vereyon.Web.FlashMessage/) |
| ASP.NET Classic and Core 1	| [![NuGet version](https://img.shields.io/badge/nuget-1.2.0-green)](https://www.nuget.org/packages/Vereyon.Web.FlashMessage/1.2.0) |

FlashMessage provides easy cross request notifications for ASP.NET MVC based on Twitter Bootstrap. It solves the problem with flashing the user a notification or message when using the Post/Redirect/Get pattern and ```RedirectToAction()``` method.

Quickstart
----------

Install the [FlashMessage NuGet package](https://www.nuget.org/packages/Vereyon.Web.FlashMessage/) :

```
Install-Package Vereyon.Web.FlashMessage
```

Register the flash message service in your startup class:

```C#
// Add services required for flash message to work.
services.AddFlashMessage();
```

Inject ```IFlashMessage``` in your controllers:

```C#
public HomeController(IFlashMessage flashMessage)
```

Queue some messages in your action method:

```C#
_flashMessage.Confirmation("Your confirmation message");
```

Register the tag helper in your view and have the messages rendered using ```<flash />```:

```C#
@addTagHelper *, Vereyon.Web.FlashMessage
```

```HTML
<flash dismissable="true" />
```

Upgrading to version 2.0
------------------------

* Only supports .NET Standard 2.0 and up
* Some short hand methods have been changed
 * Queue removed
 * Methods accepting a string format args removed
 * Consistently put message parameter first, title second
* HtmlHelper replaced with Tag Helper.

Usage
-----

### ASP.NET Core

Install the [FlashMessage NuGet package](https://www.nuget.org/packages/Vereyon.Web.FlashMessage/) and import the ```Vereyon.Web``` namespace where you need it.

```
Install-Package Vereyon.Web.FlashMessage
```

#### Registering required services for depencency injection

Register the required services during startup of you application:

```C#
// Add services required for flash message to work.
services.AddFlashMessage();
```

In case you are using ASP.NET Core 3 or newer you'll need to make sure the Newtonsoft serializer is used:

```
Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson
```

Next make to add a call to `AddNewtonsoftJson()` directly after registering MVC, Controller or RazorPage services, like in one of the following lines:

```C#
// Old style ASP .NET Core
services.AddMvc().AddNewtonsoftJson();

// ASP .NET Core 3+ style
services.AddControllers().AddNewtonsoftJson();
services.AddControllersWithViews().AddNewtonsoftJson();
services.AddRazorPages().AddNewtonsoftJson();
```

#### Rendering flash messages

Typically you want to render all queued flash messages in your Layout Razor template using the following code:

```C#
@Vereyon.Web.FlashMessageHtmlHelper.RenderFlashMessages(Html)
```

Optionally you can disable the dismiss icon passing `dismissable: false`:

```C#
@Vereyon.Web.FlashMessageHtmlHelper.RenderFlashMessages(Html, false)
```

#### Queuing flash messages

In order to be able to queue flash messages you'll need a reference to the ```IFlashMessage``` interface. Ask for it to be injected in your controller:


```C#
public HomeController(IFlashMessage flashMessage)
```

Queuing a confirmation message for display on the next request after for example user login is done as follows, assuming that ```FlashMessage``` is a property of type ```IFlashMessage```:

```C#
// User successfully logged in
FlashMessage.Confirmation("You have been logged in as: {0}", user.Name);
return RedirectToLocal(returnUrl);
```

Different types of messages can be scheduled using different methods on the IFlashMessage interface:

```C#
FlashMessage.Info("Your informational message");
FlashMessage.Confirmation("Your confirmation message");
FlashMessage.Warning("Your warning message");
FlashMessage.Danger("Your danger alert");
FlashMessage.Danger("Message title", "Your danger alert");
```

### ASP.NET 5 and earlier

Install the [FlashMessage NuGet package](https://www.nuget.org/packages/Vereyon.Web.FlashMessage/) and import the ```Vereyon.Web``` namespace where you need it.

#### Rendering flash messages

Typically you want to render all queued flash messages in your Layout Razor template using the following code:

```C#
@Html.RenderFlashMessages()
```

#### Queuing flash messages

Queuing a confirmation message for display on the next request after for example user login is done as follows:

```C#
// User successfully logged in
FlashMessage.Confirmation("You have been logged in as: {0}", user.Name);
return RedirectToLocal(returnUrl);
```

Different types of messages can be scheduled using different static methods on the FlashMessage object:

```C#
FlashMessage.Info("Your informational message");
FlashMessage.Confirmation("Your confirmation message");
FlashMessage.Warning("Your warning message");
FlashMessage.Danger("Your danger alert");
FlashMessage.Danger("Message title", "Your danger alert");
```

#### Advanced options

Using the FlashMessage.Queue() method advanced options are available:

```C#
FlashMessage.Queue(string.Format("You have been logged in as: {0}", user.Name), "Title", FlashMessageType.Confirmation, false);
```

The ```FlashMessage``` class allows you to queue messages anywhere in your code where a HttpContext is available and the response has not yet been sent out. You can thus also use FlashMessage outside your MVC actions or with WebForms applications.

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
