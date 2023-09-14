using UnityEngine;
using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;
using System.Linq;
using System.IO;
using TMPro;
using System.Text;
using Unity.VisualScripting;
using UnityEngine.UI;
using System.Collections.Generic;

public class CodeCompiler : MonoBehaviour
{
    public TMP_InputField codeInputText;
    public ModalControl consoleModalControl;
    public TextMeshProUGUI outputText;

    [Header("Console Colors")]
    public Color errorColor;


    public void ExecuteCode()
    {
        CompileAndExecuteCode();
        consoleModalControl.Open();
    }

    public void CopyCode()
    {
        GUIUtility.systemCopyBuffer = codeInputText.text;
    }

    private void CompileAndExecuteCode()
    { 
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(codeInputText.text);
        string assemblyName = Path.GetRandomFileName();

        MetadataReference[] references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).GetTypeInfo().Assembly.Location),
        };

        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName,
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
        );

        using (MemoryStream ms = new MemoryStream())
        {
            EmitResult result = compilation.Emit(ms);

            if (!result.Success)
            {
                IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);

                StringBuilder sb = new StringBuilder();
                foreach (Diagnostic failure in failures)
                {
                    FileLinePositionSpan fileLinePositionSpan = failure.Location.GetMappedLineSpan();

                    int lineNumber = fileLinePositionSpan.StartLinePosition.Line;
                    int columnStart = fileLinePositionSpan.StartLinePosition.Character;
                    int columnEnd = fileLinePositionSpan.EndLinePosition.Character;

                    string line = codeInputText.text.Split('\n')[lineNumber];
                    line = line.Insert(columnEnd, "</u>");
                    line = line.Insert(columnStart, "<u>");

                    int codeFailureLineNumber = failure.Location.GetLineSpan().StartLinePosition.Line;
                    string codeFailureLine = codeInputText.text.Split('\n')[codeFailureLineNumber];

                    sb.AppendLine($"Error at (Line {lineNumber}, Column {columnStart})");
                    sb.AppendLine($"> {line.TrimStart()}");
                    sb.AppendLine($"<color=#{errorColor.ToHexString()}>error {failure.Id}: {failure.GetMessage()}</color>\n");
                }

                outputText.text = sb.ToString();

                return;
            }

            // Load the assembly into memory
            ms.Seek(0, SeekOrigin.Begin);
            Assembly assembly = Assembly.Load(ms.ToArray());
            
            // Find and execute the Main method
            Type programType = assembly
                .GetTypes()
                .FirstOrDefault(t => t.GetMethod("Main", BindingFlags.Static | BindingFlags.Public) != null);

            if (programType == null)
            {
                outputText.text = $"<color=#{errorColor.ToHexString()}>error CS5001: Program does not contain a public static 'Main' method suitable for an entry point</color>";
                return;
            }
            
            StringWriter outputWriter = new StringWriter();
            Console.SetOut(outputWriter);
            
            MethodInfo mainMethod = programType.GetMethod("Main", BindingFlags.Static | BindingFlags.Public);
            mainMethod.Invoke(null, new object[] { new string[] { } });
            outputText.text = outputWriter.ToString();

            GC.Collect();
        }
    }
}
