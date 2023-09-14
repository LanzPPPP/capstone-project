using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Color = UnityEngine.Color;

public class InputFieldSyntaxHighlighter : MonoBehaviour
{
    public TMP_InputField inputField;
    public TextMeshProUGUI inputText;
    public TextMeshProUGUI displayText;

    [Header("Highlight Colors")]
    public Color keywordColor;
    public Color stringColor;
    public Color numericColor;

    private string keywordHexColor;
    private string stringHexColor;
    private string numericHexColor;

    private static string[] keywords = {
        "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const",
        "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern",
        "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface",
        "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params",
        "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc",
        "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked",
        "unsafe", "ushort", "using", "virtual", "void", "volatile", "while"
    };

    void Awake()
    {
        inputField.onValueChanged.AddListener((s) => SyntaxHighlight(s));

        keywordHexColor = "#" + keywordColor.ToHexString().Substring(0, keywordColor.ToHexString().Length-2) ;
        stringHexColor = "#" + stringColor.ToHexString().Substring(0, stringColor.ToHexString().Length-2) ;
        numericHexColor = "#" + numericColor.ToHexString().Substring(0, numericColor.ToHexString().Length-2) ;
        SyntaxHighlight(inputField.text);
    }

    public void SyntaxHighlight(string text)
    {
        text = HighlightKeywords(text);
        text = HighlightNumerics(text);
        text = HighlightStrings(text);

        displayText.text = text;
    }

    private string HighlightKeywords(string text)
    {
        string pattern = @"\b(?<![\w@])(" + string.Join("|", keywords) + @")(?![\w@])\b";

        return Regex.Replace(text, pattern, $"<color={keywordHexColor}>$&</color>");
    }

    private string HighlightNumerics(string text)
    {
        string pattern = @"[+-]?\b\d+(\.\d+)?[dDfFbBlL]?\b";

        return Regex.Replace(text, pattern, $"<color={numericHexColor}>$&</color>");
    }

    private string HighlightStrings(string text)
    {
        string pattern = "\"([^\"]*)\""; // Match content inside double quotes

        string result = Regex.Replace(text, pattern, match =>
        {
            string content = match.Groups[1].Value; // Get content within quotes
            string[] words = content.Split(' '); // Split words

            string parenthesizedWords = string.Join(" ", words.Select(word =>
            {
                word = word.Replace($"<color={keywordHexColor}>", "");
                word = word.Replace($"<color={numericHexColor}>", "");
                word = word.Replace($"</color>", "");

                return word;
            })); 

            return $"<color={stringHexColor}>\"{parenthesizedWords}\"</color>";
        });

        return result;
    }
}
