#tool nuget:?package=GitVersion.CommandLine&version=5.6.8

var target = Argument("target", "Build");
var configuration = Argument("configuration", "Release");
var version = "0.0.1";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory($"./ConsoleUI/bin");
    CleanDirectory($"./ConsoleUI/obj");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var gitVersion = GitVersion();

    Information($"\n\n => Application version: {gitVersion.FullSemVer} \n\n");

    DotNetCoreBuild("./BetterConsoleApp.sln", new DotNetCoreBuildSettings
    {
        Configuration = configuration,
        ArgumentCustomization = args => args.Append($"/p:Version={gitVersion.FullSemVer}")
                                            .Append($"/p:SourceRevisionId={gitVersion.Sha.Substring(0,8)}")
    });
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);