using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using System.Text.RegularExpressions;

namespace Frank.GameEngine.Generators.AssetsGenerator;

[Generator]
public class AdditionalFilesHelperGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
    }

    public void Execute(GeneratorExecutionContext context)
    {
        GenerateResourceHelperClass(context);
    }

    private static void GenerateResourceHelperClass(GeneratorExecutionContext context)
    {
        const string rootClassName = "AdditionalResources";

        var sourceBuilder = new StringBuilder();
        var additionalFiles = context.AdditionalFiles;

        if (TryGetProjectDir(context, out var projectDir)) return;
        if (TryGetRootNamespace(context, out var rootNamespace)) return;

        var classes = new List<ClassDeclarationSyntax>();

        var members = additionalFiles.Select(file => GenerateMember(file, projectDir)).ToList();
        
        var tree = CreateTree(rootClassName, members);
        
        WalkTree(tree, node =>
        {
            try
            {
                if (node.Name == rootClassName)
                    return;

                var className = ConvertPathToClassName(node.Name);
                var members = node.Children.Select(x => x.MemberData.MemberDeclarationSyntax).Where(x => x != null).Select(x => x!).ToArray();
                classes.Add(GenerateClass(className, members));
            }
            catch (Exception e)
            {
                context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("FRANK0003", "Error", e.Message, "Error", DiagnosticSeverity.Error, true), Location.None));
            }
        });

        var rootClass = GenerateClass(rootClassName, classes.ToArray());

        var compilationUnit = SyntaxFactory.CompilationUnit()
                .AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Reflection")))
                .AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.IO")))
                .AddMembers(SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(rootNamespace)).AddMembers(rootClass))
            ;

        sourceBuilder.Append(compilationUnit.NormalizeWhitespace().ToFullString());

        var sourceString = sourceBuilder.ToString();
        var fileName = rootClassName + ".g.cs";

        var sourceText = SourceText.From(sourceString, Encoding.UTF8);
        context.AddSource(fileName, sourceText);

        WriteToFile(fileName, sourceString, projectDir, rootClassName);
    }
    
    private static void WalkTree(TreeNode node, Action<TreeNode> action, int depth = 0)
    {
        // Execute the provided action on the current node
        action(node);

        // Recursively call this function on all children of the node.
        foreach (var child in node.Children)
        {
            WalkTree(child, action, depth + 1);
        }
    }
    
    private static Member GenerateMember(AdditionalText file, string projectDir)
    {
        var relativePath = GetRelativePath(file, projectDir);
        var directoryPath = relativePath.Replace(Path.GetFileName(relativePath), "");
        var directories = directoryPath.Split('/').ToList();
        var memberDeclarationSyntax = GeneratePropertyDeclarationSyntax(GetMethodName(file), relativePath);
        
        return new Member
        {
            MemberDeclarationSyntax = memberDeclarationSyntax,
            Directories = directories,
            RelativePath = relativePath
        };
    }

    private struct Member
    {
        public MemberDeclarationSyntax MemberDeclarationSyntax { get; set; }
        public List<string> Directories { get; set; }
        public string RelativePath { get; set; }
    }
    
    private static bool TryGetRootNamespace(GeneratorExecutionContext context, out string? rootNamespace)
    {
        if (!context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.rootnamespace", out rootNamespace))
        {
            context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("FRANK0002", "Failed to generate resource helper class, no root", "Failed to generate resource helper class, no root", "Frank", DiagnosticSeverity.Error, true), Location.None));
            return true;
        }

        return false;
    }

    private static bool TryGetProjectDir(GeneratorExecutionContext context, out string? projectDir)
    {
        if (!context.AnalyzerConfigOptions.GlobalOptions.TryGetValue("build_property.projectdir", out projectDir))
        {
            context.ReportDiagnostic(Diagnostic.Create(new DiagnosticDescriptor("FRANK0001", "Failed to generate resource helper class", "Failed to generate resource helper class", "Frank", DiagnosticSeverity.Error, true), Location.None));
            return true;
        }
        
        return false;
    }

    private static void WriteToFile(string fileName, string sourceString, string projectDir, string rootClassName)
    {
#pragma warning disable RS1035
        File.WriteAllText(Path.Combine(projectDir, fileName), sourceString.Replace(rootClassName, rootClassName + "2"));
#pragma warning restore RS1035
    }

    private static string GetRelativePath(AdditionalText additionalText, string projectPath) => Path.GetRelativePath(projectPath, additionalText.Path).Replace("\\", "/");
    
    public static List<string> ExtractFolderHierarchy(string filePath)
    {
        List<string> folders = new List<string>();
        var directory = Path.GetDirectoryName(filePath);

        while (!string.IsNullOrEmpty(directory))
        {
            var folderName = Path.GetFileName(directory);
            folders.Insert(0, folderName); // Insert at the beginning to maintain the correct order
            directory = Path.GetDirectoryName(directory);
        }

        return folders;
    }

    private static PropertyDeclarationSyntax GeneratePropertyDeclarationSyntax(string name, string relativeAssetFilePath)
    {
        var syntaxTree = SyntaxFactory
            .PropertyDeclaration(
                SyntaxFactory.ParseTypeName("byte[]"), name)
            .WithExpressionBody(
                SyntaxFactory.ArrowExpressionClause(SyntaxFactory.InvocationExpression(
                        SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName("File"), SyntaxFactory.IdentifierName("ReadAllBytes"))
                    ).WithArgumentList(
                        SyntaxFactory.ArgumentList(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.Argument(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        SyntaxFactory.Literal(relativeAssetFilePath)
                                    )
                                )
                            )
                        )
                    )
                )
            )
            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
            .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword)));
        return syntaxTree ;
    }

    private static (MemberDeclarationSyntax? SyntaxTree, string? Directory) GenerateMemberFrom(AdditionalText additionalText, string projectPath)
    {
        var relativeAssetFilePath = GetRelativePath(additionalText, projectPath);

        var syntaxTree = SyntaxFactory
            .PropertyDeclaration(
                SyntaxFactory.ParseTypeName("byte[]"), GetMethodName(additionalText))
            .WithExpressionBody(
                SyntaxFactory.ArrowExpressionClause(SyntaxFactory.InvocationExpression(
                        SyntaxFactory.MemberAccessExpression(SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory.IdentifierName("File"), SyntaxFactory.IdentifierName("ReadAllBytes"))
                    ).WithArgumentList(
                        SyntaxFactory.ArgumentList(
                            SyntaxFactory.SingletonSeparatedList(
                                SyntaxFactory.Argument(
                                    SyntaxFactory.LiteralExpression(
                                        SyntaxKind.StringLiteralExpression,
                                        SyntaxFactory.Literal(relativeAssetFilePath)
                                    )
                                )
                            )
                        )
                    )
                )
            )
            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
            .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword)));
        var directory = Path.GetDirectoryName(relativeAssetFilePath)?.Replace("\\", "/");

        return (syntaxTree, directory);
    }

    private static ClassDeclarationSyntax GenerateClass(string name, MemberDeclarationSyntax[] members)
    {
        return SyntaxFactory
            .ClassDeclaration(name)
            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword), SyntaxFactory.Token(SyntaxKind.StaticKeyword))
            .AddMembers(members);
    }

    private static string ConvertPathToClassName(string path)
    {
        var className = Path.GetFileName(path);
        className = Regex.Replace(className, @"[\W]", "_");
        className = char.ToUpper(className[0]) + (className.Length > 1 ? className.Substring(1) : "");
        return className;
    }

    private static string GetMethodName(AdditionalText additionalText)
    {
        var name = Path.GetFileNameWithoutExtension(additionalText.Path);
        var nameParts = name.Split(".");
        var methodName = nameParts.Last();
        return methodName;
    }
    
    private static TreeNode CreateTree(string rootName, IEnumerable<Member> members)
    {
        var root = new TreeNode { Name = rootName };
    
        foreach (var member in members)
        {
            var currentNode = root;
            foreach (var dir in member.Directories)
            {
                var nextNode = currentNode.Children.FirstOrDefault(x => x.Name == dir);
                if (nextNode == null)
                {
                    nextNode = new TreeNode { Name = dir };
                    currentNode.Children.Add(nextNode);
                }
    
                currentNode = nextNode;
            }
    
            // You can assign the Member data to the leaf node, if needed.
            currentNode.MemberData = member;
        }
    
        return root;
    }
    
    private class TreeNode
    {
        public string Name { get; set; }
        public List<TreeNode> Children { get; set; } = new List<TreeNode>();
        public Member MemberData { get; set; } // optional: hold the related Member data in leaves only.
    }
}