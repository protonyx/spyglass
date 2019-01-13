// Install modules
#module nuget:?package=Cake.DotNetTool.Module&version=0.1.0

// Addins
#addin "Cake.Incubator"
#addin "Cake.Yarn"
#addin "Cake.Npm"

// .NET Core Global tools.
#tool dotnet:?package=GitVersion.Tool&version=4.0.1-beta1-58

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var output = Argument("output", "./dist");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var distDirectory = Directory(output);
var sln = File("./src/Spyglass.sln");
var web = Directory("./src/Spyglass.Web");

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

Task("Clean-Solution")
    .Does(() =>
    {
        CleanDirectory(distDirectory);
        DotNetCoreClean(sln);
    });

Task("Restore-Solution")
    .Does(() =>
    {
        DotNetCoreRestore(sln);
    });

Task("Build-Solution")
    .IsDependentOn("Clean-Solution")
    .IsDependentOn("Restore-Solution")
    .IsDependentOn("GitVersion")
    .Does(() =>
    {
        var msbuild = new DotNetCoreMSBuildSettings();
        msbuild = msbuild
            .SetVersion(version.AssemblySemVer)
            .SetFileVersion(version.AssemblySemFileVer)
            .SetInformationalVersion(version.InformationalVersion);

        DotNetCoreBuild(sln, new DotNetCoreBuildSettings()
        {
            Configuration = configuration,
            NoRestore = true,
            MSBuildSettings = msbuild
        });
    });

Task("Test-Solution")
    .IsDependentOn("Build-Solution")
    .Does(() =>
    {
        var projects = GetFiles("./src/*.Tests/*.csproj");
        var settings = new DotNetCoreTestSettings()
        {
            Configuration = configuration,
            NoBuild = true
        };

        foreach (var project in projects)
        {
            var projName = project.Segments.Last().Replace(".csproj", "");

            DotNetCoreTest(project.ToString(), settings);
        }
    });

Task("Publish-Solution")
    .IsDependentOn("Build-Solution")
    .Does(() =>
    {
        DotNetCorePublish("./src/Spyglass.Server/Spyglass.Server.csproj",
            new DotNetCorePublishSettings()
            {
                Configuration = configuration,
                OutputDirectory = distDirectory,
                NoBuild = true
            });
    });

Task("Package-Solution")
    .IsDependentOn("Build-Solution")
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

Task("Clean-Web")
    .Does(() =>
    {
        CleanDirectory($"{web.Path}/dist");
    });

Task("Restore-Web")
    .Does(() =>
    {
        Yarn.FromPath(web).Install();
    });

Task("Build-Web")
    .IsDependentOn("Clean-Web")
    .IsDependentOn("Restore-Web")
    .Does(() =>
    {
        NpmRunScript("build", opt =>
        {
            opt.Arguments.Add("--prod");
            opt.Arguments.Add("--progress false");
            opt.FromPath(web);
        });
    });

Task("Test-Web")
    .IsDependentOn("Build-Web")
    .Does(() =>
    {
        NpmRunScript("test", opt =>
        {
            opt.FromPath(web);
        });
    });

Task("Publish-Web")
    .IsDependentOn("Build-Web")
    .Does(() =>
    {
        var webFiles = GetFiles($"{web.Path}/dist/*");
        CreateDirectory($"{distDirectory.Path}/wwwroot");
        CopyFiles(webFiles, $"{distDirectory.Path}/wwwroot");
        // CopyDirectory($"{web.Path}/dist/assets/", $"{distDirectory.Path}/wwwroot/assets");
    });

Task("Clean")
    .IsDependentOn("Clean-Solution")
    .IsDependentOn("Clean-Web");

Task("Restore")
    .IsDependentOn("Restore-Solution")
    .IsDependentOn("Restore-Web");

Task("Build")
    .IsDependentOn("Build-Solution")
    .IsDependentOn("Build-Web");

Task("Test")
    .IsDependentOn("Test-Solution")
    .IsDependentOn("Test-Web");

Task("Publish")
    .IsDependentOn("Publish-Solution")
    .IsDependentOn("Publish-Web");

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Publish");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
