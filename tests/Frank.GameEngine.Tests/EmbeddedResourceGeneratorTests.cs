using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Frank.GameEngine.Assets;
using Frank.GameEngine.Generators.AssetsGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Xunit.Abstractions;

namespace Frank.GameEngine.Tests;

public class EmbeddedResourceGeneratorTests
{
    private readonly ITestOutputHelper _outputHelper;

    public EmbeddedResourceGeneratorTests(ITestOutputHelper outputHelper)
    {
        _outputHelper = outputHelper;
    }

    [Fact]
    public void Use()
    {
        var thing = AdditionalResources2.Models.teapot;
        _outputHelper.WriteLine(thing.Length.ToString());
    }

    [Fact]
    public void Generate()
    {
        var inputCompilation = CSharpCompilation.Create("compilation",
            new[] { CSharpSyntaxTree.ParseText("public class R { }") },
            new[] { MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location) });

        var generator = new AdditionalFilesHelperGenerator();

        var generators = new ISourceGenerator[] { generator };
        var additionalFiles = new[]
        {
            new AnalyzerAdditionalText(@"C:\repos\frankhaugen\Frank.GameEngine\src\Frank.GameEngine.Assets\Teapot.obj",
                SourceText.From("Hello world")),
            new AnalyzerAdditionalText(
                @"C:\repos\frankhaugen\Frank.GameEngine\src\Frank.GameEngine.Assets\Models\Teapot.obj",
                SourceText.From("Hello world")),
            new AnalyzerAdditionalText(
                @"C:\repos\frankhaugen\Frank.GameEngine\src\Frank.GameEngine.Assets\Models\My Hole\State.obj",
                SourceText.From("Hello world")),
            new AnalyzerAdditionalText(
                @"C:\repos\frankhaugen\Frank.GameEngine\src\Frank.GameEngine.Assets\Models\Bob\MtL\Teapot.mtl",
                SourceText.From("Hello world")),
            new AnalyzerAdditionalText(
                @"C:\repos\frankhaugen\Frank.GameEngine\src\Frank.GameEngine.Assets\Sap\Bob\MtL\Teapot.mtl",
                SourceText.From("Hello world")),
            new AnalyzerAdditionalText(
                @"C:\repos\frankhaugen\Frank.GameEngine\src\Frank.GameEngine.Assets\Sap\Bob\MtL\Ashpot.mtl",
                SourceText.From("Hello world"))
        };

        CSharpGeneratorDriver
            .Create(generators, optionsProvider: new TestOptionsProvider(), additionalTexts: additionalFiles)
            .RunGeneratorsAndUpdateCompilation(inputCompilation, out var outputCompilation, out var diagnostics);

        _outputHelper.WriteLine(string.Join(Environment.NewLine, diagnostics.Select(x => x.GetMessage())));
        var syntaxTree = outputCompilation.SyntaxTrees.ElementAt(1);
        var text = syntaxTree.ToString();

        _outputHelper.WriteLine(text);
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

    public TestOptionsProvider()
    {
        _options.Add("build_property.rootnamespace", "Frank.GameEngine.Assets");
        _options.Add("build_property.projectdir", @"C:\repos\frankhaugen\Frank.GameEngine\src\Frank.GameEngine.Assets");
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