#tool nuget:?package=GitVersion.CommandLine&version=4.0.0-beta0012
#addin "Cake.Incubator"
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var distDirectory = Directory("./dist");
var sln = File("./src/Spyglass.sln");

GitVersion version;

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("GitVersion")
    .Does(() =>
    {
        version = GitVersion();

        Information(version.Dump());
    });

Task("Clean")
    .Does(() =>
{
    CleanDirectory(distDirectory);
    DotNetCoreClean(sln);
});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore(sln);
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
{
    var msbuild = new DotNetCoreMSBuildSettings();
        msbuild = msbuild
            .SetVersion(version.AssemblySemVer)
            .SetFileVersion(version.SemVer)
            .SetInformationalVersion(version.InformationalVersion);

        DotNetCoreBuild(sln, new DotNetCoreBuildSettings()
        {
            Configuration = configuration,
            NoRestore = true,
            MSBuildSettings = msbuild
        });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var projects = GetFiles("./src/*.Tests/*.csproj");
        var settings = new DotNetCoreTestSettings()
        {
            Configuration = configuration,
            NoBuild = true,
            ArgumentCustomization = args => args.Append("--no-restore")
        };

        foreach (var project in projects)
        {
            var projName = project.Segments.Last().Replace(".csproj", "");

            DotNetCoreTest(project.ToString(), settings);
        }
    });

  Task("Package")
    .IsDependentOn("Build")
    .Does(() =>
    {
        var msbuild = new DotNetCoreMSBuildSettings();
        msbuild = msbuild
            .WithProperty("PackageVersion", version.NuGetVersion);

        var settings = new DotNetCorePackSettings()
        {
            Configuration = configuration,
            OutputDirectory = distDirectory,
            NoBuild = true,
            IncludeSymbols = true,
            IncludeSource = true,
            MSBuildSettings = msbuild
        };

        DotNetCorePack(sln, settings);
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
