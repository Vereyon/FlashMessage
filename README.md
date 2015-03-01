FlashMessage
============

FlashMessage provides easy cross request notifications for ASP.NET mvc based on Twitter Bootstrap.

Usage
-----

### Rendering flash messages

Typically you want to render all queued flash messages in your Layout Razor template using the following code:

```C#
@Html.RenderFlashMessages()
```

### Queuing flash messages

Queuing a notification for display on the next request after for example user login is done as follows:

```C#
// User successfully logged in
FlashMessage.Queue(string.Format("You have been logged in as: {0}", user.Name), FlashMessageType.Confirmation);
return RedirectToLocal(returnUrl);
```