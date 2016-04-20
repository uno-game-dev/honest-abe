#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(SoundPlayer))]
public class SoundPlayerEditor : Editor
{
    private ReorderableList list;
    private List<SoundPlayer.NamedAudioClip> namedAudioClips;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("namedAudioList"),
                true, true, true, true);

        list.drawElementCallback = DrawElementCallbackWithThreeColumns;

        namedAudioClips = ((SoundPlayer)target).namedAudioList;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawElementCallbackWithThreeColumns(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        Rect substateRectangle = new Rect(rect.x, rect.y, rect.width / 3, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(substateRectangle, element.FindPropertyRelative("name"), GUIContent.none);
        Rect nameRectangle = new Rect(rect.x + rect.width / 3, rect.y, rect.width / 3, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(nameRectangle, element.FindPropertyRelative("clip"), GUIContent.none);
        Rect animRectangle = new Rect(rect.x + rect.width * 2 / 3, rect.y, rect.width / 3, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(animRectangle, element.FindPropertyRelative("volume"), GUIContent.none);
    }
}
#endif