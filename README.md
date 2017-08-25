# dotVersioner
Keep track of all versions for a C# .Net solution

Usage

From command line run
```
VersionsCli.exe c:\projects\myawsomeapp
```

Your awsomeapp folder must contain following text files 
ReleaseTrack and VersionedFiles


ReleaseTrack must have following structure
```
Version|Year|Month|Day|Description
1.0.0.0|2017|08|24|Initial commit
1.0.1.0|2017|08|25|Allow multiple file types
```

VersionedFiles, example of a C# based solution
```
FileName|Preffix|Suffix
AssemblyInfo.cs|[assembly: AssemblyVersion("|")]
AssemblyInfo.cs|[assembly: AssemblyFileVersion("|")]
```
