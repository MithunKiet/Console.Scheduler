Implement custom file-based logging with task scheduler settings for .NET application

- Configured settings loading from `appsettings.json`, enabling flexible log path configuration and integration with a task scheduler.
- Added `FileLogger` for file-based logging, creating daily rotating log files in a designated directory.
- Introduced the `FileLoggerProvider` and `AddFileLogger` extension methods, simplifying the logger's registration with `ILogger` services.
- Implemented a fallback mechanism for log path, defaulting to "Logs" if not provided in `appsettings.json`.
- Added task scheduler setting to allow automatic daily tasks, such as clearing old logs or sending daily summaries and notifications.
- Enhanced extensibility to support additional functionality, like integrating with notification services or scheduling maintenance tasks.
- Ensured thread safety with locking to prevent concurrent file access issues.
- Demonstrated various logging levels (information, warning) with detailed exception logging if applicable.

This update provides a robust, customizable logging solution with task scheduler settings. It allows daily maintenance tasks or notifications for efficient log management and monitoring.
Task scheduler Setting in Windows:-
![1](https://github.com/user-attachments/assets/9613361c-a904-4b67-9ffd-5ba013411aa0)
![2](https://github.com/user-attachments/assets/31aee41a-3474-4c84-9bf6-20fefe41e61e)
![3](https://github.com/user-attachments/assets/22e0e73b-7808-499a-92a8-5cb6240fc062)
![4](https://github.com/user-attachments/assets/d35c5d67-2b7d-4cd3-9cf4-21d7406d820c)
