# LightLog
Lightweight application log library

(under slow lazy development)

Inspired by NLog, but I wanted to create simple solution by myself.

Now only logging to file working.

You can put a line 
```csharp
Logger.Debug("Program started"); 
```

in your code, and get the following line in your log file:

```
2020-05-23T22:41:22.5116388+03:00||PrintInfo|MainViewModel.cs|17|Debug|Program started
```
Where you will find time of event, name of function, source file name and line, severity of the message, and your message.
