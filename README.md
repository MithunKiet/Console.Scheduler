Implement custom file-based logging with extensibility options in .NET application

- Set up configuration for loading settings from `appsettings.json`, allowing flexible log path configuration.
- Implemented a custom `FileLogger` that writes log messages to daily rotating log files in a specified directory.
- Created `FileLoggerProvider` and extension method `AddFileLogger` to enable easy registration of the logger with `ILogger` services.
- Added fallback mechanism for log path, defaulting to "Logs" if not specified in `appsettings.json`.
- Enhanced logging functionality to support custom additions, such as sending daily notifications or integrating with monitoring services.
- Ensured thread safety with locking to avoid concurrent file access issues in logging operations.
- Demonstrated logging at various levels, including informational and warning messages with exception details if present.

This commit enhances the application’s logging system by adding flexible, file-based logging with room for additional custom functionality, aiding in diagnostics, monitoring, and notifications.
