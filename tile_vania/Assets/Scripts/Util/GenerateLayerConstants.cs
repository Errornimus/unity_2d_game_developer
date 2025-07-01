using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class GenerateLayerConstants
{
    [MenuItem("Tools/Generate Layer Constants")]
    private static void Generate()
    {
        string pathForSaving = EditorUtility.SaveFilePanel("Speichere LayerConstants-Klasse", "Assets", "LayerConstants" + ".cs", "cs");
        if (string.IsNullOrEmpty(pathForSaving)) return;

        var sb = new StringBuilder();
        sb.AppendLine("// Auto-generated Layer Constants");
        sb.AppendLine("public static class LayerConstants");
        sb.AppendLine("{");

        for (int i = 0; i < 32; i++)
        {
            string layerName = LayerMask.LayerToName(i);
            if (string.IsNullOrEmpty(layerName)) continue;

            string safeName = MakeSafe(layerName);
            sb.AppendLine($"    public const int {safeName} = {i};");
            sb.AppendLine($"    public static readonly int {safeName}Mask = 1 << {i};");
        }

        sb.AppendLine("}");

        Directory.CreateDirectory(Path.GetDirectoryName(pathForSaving));
        File.WriteAllText(pathForSaving, sb.ToString());
        AssetDatabase.Refresh();

        Debug.Log($"✅ LayerConstants generiert unter: {pathForSaving}");
    }

    private static string MakeSafe(string name)
    {
        // Entfernt ungültige Zeichen und macht Namen C#-kompatibel
        string clean = name.Replace(" ", "_").Replace("-", "_");
        if (char.IsDigit(clean[0])) clean = "_" + clean;
        return clean;
    }
}
