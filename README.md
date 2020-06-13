[![Build Status](https://travis-ci.org/u1035/LightLog.svg?branch=master)](https://travis-ci.org/u1035/LightLog)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/LightLog)
![Nuget](https://img.shields.io/nuget/dt/LightLog)

# LightLog
Lightweight application log library

(under slow lazy development)

Inspired by NLog, but I wanted to create simple solution by myself.

You can put a line in your ViewModel:
```csharp
public Logger Logger { get; } = LogManager.Instance;
```
and bind to collection of log records in XAML with something like this:
```xaml
<ListView Grid.Row="2" ItemsSource="{Binding Logger.LogRecords}">
    <ListView.ItemTemplate>
        <DataTemplate>
            <StackPanel Orientation="Horizontal">
                <TextBlock Margin="4 0 0 0" Text="{Binding Time}"/>
                <TextBlock Margin="4 0 0 0" Text="{Binding Level}"/>
                <TextBlock Margin="4 0 0 0" Text="{Binding CallerMemberName}"/>
                <TextBlock Margin="4 0 0 0" Text="{Binding Message}"/>
            </StackPanel>
        </DataTemplate>
    </ListView.ItemTemplate>
</ListView>
```
UserControl for displaying log records will appear in next versions.

In code you can use logger like this:
```csharp
Logger.Debug("Program started"); 
```

and get the following line in your log file:

```
2020-05-23T22:41:22.5116388+03:00||PrintInfo|MainViewModel.cs|17|Debug|Program started
```
Where you will find time of event, name of function, source file name and line, severity of the message, and your message.
