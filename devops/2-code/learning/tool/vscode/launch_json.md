# launch_json

1. `PreLaunchTask`
---
runs the associated taskName in task.json before debugging your program

2. `Program`
---
the program field is set to the path of the application dll or .net core host executable to launch.

**this property normally takes the from : "${workspaceFolder}/bin/Debug/<target-framework>/<project name.dll>"**


- `<target-framework>` is the framework that the debugged project is being built for. This is normally found in the project file as the 'TargetFramework' property.

- `<project-name.dll>` is the name of debugged project's build output dll. This is normally the same as the project file name but with a '.dll' extension.


example : `"${workspaceFolder}/bin/Debug/netcoreapp1.1/MyProject.dll"`


3. `Cwd`
---
the working directory of the target process

4. `Args`
---
these are the arguments that will be passed to your program

5. `stopAtEntry` 
---
if you need to stop the entry point of the target, you can optionally set to be `true`.

**Starting a Web Browser**

---
the default launch.json template from ASP.NET Core projects will use the following to configure VS Code to launch a web browser when ASP.NET starts

```json
"serverReadyAction": {
    "action":"openExternally",
    "pattern":"\\bNow listening on:\\s+(https?://\\S+)"
}
```
1. if you do **Not** want the browser to automatically start. you can just delete this element( and a `launchBrowser` element if you launch.json has that instead)

 




1. `request` : launch(啟動)  /attach (附加)

2. `protocol`: 

3. `port`: 指定埠號

4. `localRoot`