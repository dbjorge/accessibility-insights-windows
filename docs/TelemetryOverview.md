# Telemetry Overview

Accessibility Insights for Windows uses telemetry to better understand what features are most helpful to users, as well as to help identify potential issues that users are experiencing.

## Reporting a telemetry action
Telemetry actions typically indicate a user-initiated action (starting the app, clicking a button, etc.). They are also used to report the result of a user action (for examplle, a user attempt to open a file failed because the file was in an unrecognized format). At this time, telemetry actions are *not* supported in extensions. The telemetry classes are defined in the `AccessibilityInsights.Desktop.Telemetry` namespace. The `Logger` class provides 2 variants of the static `Logger.PublishTelemetryEvent` method:

### Single-property action
This version reports an action with a single associated property:
```
Logger.PublishTelemetryEvent(
    TelemetryAction.Mainwindow_Timer_Started,       // Name of action 
    TelemetryProperty.Seconds,                      // Name of property
    count.ToString(CultureInfo.InvariantCulture));  // Value of property (must be a locale-agnostic string)
```

### Multi-property action
This version reports an action with multiple associated properties:
```
Logger.PublishTelemetryEvent(TelemetryActions.Bug_Save, new Dictionary<TelemetryProperty, string>
    {
        { TelemetryProperty.RuleId, ruleId },  // values are locale-agnostic strings
        { TelemetryProperty.Url, url},
    });
```

The `TelemetryAction` and `TelemetryProperty` enumerations can be extended to add new actions or properties.

### Context Properties
There are some proeprties that are desired on every reported telemetry action. The `Logger.AddOrUpdateContextProperty` method exists for this purpose. When a context property is set, it will be automatically added as a property to each subsequent call to `Logger.PublishTelemetryEvent`. This sample adds a context property:
```
Logger.AddOrUpdateContextProperty(TelemetryProperty.Version, GetAppVersion());  // value is a locale-agnostic string
```

## Reporting Exceptions
A dedicated mechanism exists to report an `Exception` caught by the code. It is implemented as an extension method to make the code simpler to read. In non-extension code, this method is defined in the `AccessibilityInsights.Desktop.Telemetry` namespace. For extension code, this method is defined in the `AccessibilityInsights.Extensions.Helpers` namespace. Here is a typical usage where an Exception is caught, reported, then eaten:
```
try
{
    // Code that might throw
}
catch (Exception e)
{
    e.ReportException();
}
```

## User control
All telemetry (including reported exceptions) is under user control. When the application starts the first time, the user is allowed to enable or disable telemetry. The user can change this option at any time via the settings page.