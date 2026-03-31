using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Frank.GameEngine.Assets;
using Frank.GameEngine.Generators.AssetsGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using TUnit.Core;

namespace Frank.GameEngine.Tests.Generators;

/// <summary>
/// Roslyn source generator smoke tests; useful for local diagnostics when changing <see cref="AdditionalFilesHelperGenerator"/>.
/// </summary>
public class EmbeddedResourceGeneratorTests
{
    [Test]
    public void Use()
    {
        var thing = AdditionalResources2.Models.teapot;
        TestContext.Current!.Output.WriteLine(thing.Length.ToString());
    }

    [Test]
    public void Generate()
    {
        // Paths must use the OS separator so Path.GetRelativePath matches the real generator on Linux CI.
        var projectDir = Path.Combine(Path.GetTempPath(), "Frank.GameEngine.Tests", "Frank.GameEngine.Assets");
        projectDir = Path.GetFullPath(projectDir);

        var inputCompilation = CSharpCompilation.Create("compilation",
            new[] { CSharpSyntaxTree.ParseText("public class R { }") },
            new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) });

        var generator = new AdditionalFilesHelperGenerator();

        var generators = new ISourceGenerator[] { generator };
        var additionalFiles = new[]
        {
            new AnalyzerAdditionalText(Path.Combine(projectDir, "Teapot.obj"), SourceText.From("Hello world")),
            new AnalyzerAdditionalText(Path.Combine(projectDir, "Models", "Teapot.obj"), SourceText.From("Hello world")),
            new AnalyzerAdditionalText(Path.Combine(projectDir, "Models", "My Hole", "State.obj"),
                SourceText.From("Hello world")),
            new AnalyzerAdditionalText(Path.Combine(projectDir, "Models", "Bob", "MtL", "Teapot.mtl"),
                SourceText.From("Hello world")),
            new AnalyzerAdditionalText(Path.Combine(projectDir, "Sap", "Bob", "MtL", "Teapot.mtl"),
                SourceText.From("Hello world")),
            new AnalyzerAdditionalText(Path.Combine(projectDir, "Sap", "Bob", "MtL", "Ashpot.mtl"),
                SourceText.From("Hello world"))
        };

        CSharpGeneratorDriver
            .Create(generators, optionsProvider: new TestOptionsProvider(projectDir), additionalTexts: additionalFiles)
            .RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

        TestContext.Current!.Output.WriteLine(string.Join(Environment.NewLine, diagnostics.Select(x => x.GetMessage())));

        var trees = outputCompilation.SyntaxTrees.ToList();
        TestContext.Current.Output.WriteLine($"Syntax tree count: {trees.Count}");
        trees.Should().NotBeEmpty();

        // Do not use a fixed index: generator output ordering/count can vary by Roslyn version / host.
        foreach (var tree in trees)
            TestContext.Current.Output.WriteLine(tree.ToString());
    }
}

file class AnalyzerAdditionalText : AdditionalText
{
    private readonly SourceText _content;

    internal AnalyzerAdditionalText(string path, SourceText content)
    {
        Path = path;
        _content = content;
    }

    public override string Path { get; }

    public override SourceText GetText(CancellationToken cancellationToken = default)
    {
        return _content;
    }
}

file class TestOptionsProvider : AnalyzerConfigOptionsProvider
{
    private readonly TestAnalyzerConfigOptions _options = new();

    public TestOptionsProvider(string projectDir)
    {
        _options.Add("build_property.rootnamespace", "Frank.GameEngine.Assets");
        _options.Add("build_property.projectdir", projectDir);
    }

    public override AnalyzerConfigOptions GlobalOptions => _options;

    public override AnalyzerConfigOptions GetOptions(SyntaxTree tree)
    {
        return _options;
    }

    public override AnalyzerConfigOptions GetOptions(AdditionalText textFile)
    {
        return _options;
    }
}

file class TestAnalyzerConfigOptions : AnalyzerConfigOptions
{
    private readonly Dictionary<string, string> _options = new();

    public override bool TryGetValue(string key, [NotNullWhen(true)] out string? value)
    {
        if (_options.TryGetValue(key, out var result))
        {
            value = result;
            return true;
        }

        value = null;
        return false;
    }

    public void Add(string key, string value)
    {
        _options.Add(key, value);
    }
}
