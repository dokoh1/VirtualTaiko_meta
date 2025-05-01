using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class Sprite_UI_Text_ColorSetter : EditorWindow
{
    private Color targetColor = Color.white;

    [MenuItem("Tools/All Color Setter")]
    public static void ShowWindow()
    {
        GetWindow<Sprite_UI_Text_ColorSetter>("All Color Setter");
    }

    void OnGUI()
    {
        GUILayout.Label("씬 안의 SpriteRenderer, Image, Text 색상 일괄 변경", EditorStyles.boldLabel);
        targetColor = EditorGUILayout.ColorField("변경할 색상", targetColor);

        if (GUILayout.Button("적용하기"))
        {
            ApplyColorToAll(targetColor);
        }
    }

    void ApplyColorToAll(Color color)
    {
        int count = 0;

        // 1. SpriteRenderer
        var sprites = FindObjectsOfType<SpriteRenderer>();
        foreach (var sr in sprites)
        {
            Undo.RecordObject(sr, "Change SpriteRenderer Color");
            sr.color = color;
            EditorUtility.SetDirty(sr);
            count++;
        }

        // 2. UI Image
        var images = FindObjectsOfType<Image>(true); // 비활성 포함
        foreach (var img in images)
        {
            Undo.RecordObject(img, "Change UI Image Color");
            img.color = color;
            EditorUtility.SetDirty(img);
            count++;
        }

        // 3. UI Text (Legacy Text)
        var texts = FindObjectsOfType<Text>(true);
        foreach (var txt in texts)
        {
            Undo.RecordObject(txt, "Change UI Text Color");
            txt.color = color;
            EditorUtility.SetDirty(txt);
            count++;
        }

        // 씬 저장 가능하게 표시
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

        Debug.Log($"총 {count}개의 컴포넌트 색상 변경 완료!");
    }
}
