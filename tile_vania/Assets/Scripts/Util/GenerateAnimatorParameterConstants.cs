using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.IO;
using System.Text;

public class GenerateAnimatorParameterConstants : MonoBehaviour
{
    [MenuItem("Tools/Generate Animator Parameter Class")]
    private static void Generate()
    {
        var animatorController = Selection.activeObject as AnimatorController;
        if (animatorController == null)
        {
            Debug.LogError("Bitte einen AnimatorController im Project-Fenster auswählen.");
            return;
        }

        string className = animatorController.name + "Params";
        string path = EditorUtility.SaveFilePanel("Speichere Parameter-Klasse", "Assets", className + ".cs", "cs");
        if (string.IsNullOrEmpty(path)) return;

        var sb = new StringBuilder();
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine();
        sb.AppendLine($"public static class {className}");
        sb.AppendLine("{");

        foreach (var parameter in animatorController.parameters)
        {
            string paramName = parameter.name;
            string safeName = MakeSafe(paramName);

            sb.AppendLine($"    public static readonly int {safeName} = Animator.StringToHash(\"{paramName}\");");
        }

        sb.AppendLine("}");

        File.WriteAllText(path, sb.ToString());
        AssetDatabase.Refresh();
        Debug.Log($"Animator-Parameter-Klasse erzeugt: {path}");
    }

    private static string MakeSafe(string name)
    {
        // Entfernt ungültige Zeichen für C#-Bezeichner
        string clean = name.Replace(" ", "_").Replace("-", "_");
        if (char.IsDigit(clean[0])) clean = "_" + clean;
        return clean;
    }
}
