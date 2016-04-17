#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using System.Linq;

[CustomEditor(typeof(AnimationTester))]
public class AnimationTesterEditor : Editor
{
    private ReorderableList list;
    private List<AnimationGroup> animationClips;
    private UnityEditor.Animations.AnimatorController animatorController;

    private void OnEnable()
    {
        list = new ReorderableList(serializedObject,
                serializedObject.FindProperty("animationClips"),
                true, true, true, true);

        list.drawElementCallback = DrawElementCallbackWithThreeColumns;

        animationClips = ((AnimationTester)target).animationClips;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();

        if (GUILayout.Button("Update Animation Clips"))
        {
            animationClips.Clear();
            animatorController = ((AnimationTester)target).runtimeAnimatorController as UnityEditor.Animations.AnimatorController;
            AddAllStates(animatorController.layers[0].stateMachine);
            animationClips.Sort((o1, o2) => (o1.substate + o1.name).CompareTo(o2.substate + o2.name));
        }
        if (GUILayout.Button("CSV to Console"))
        {
            string output = "";
            foreach (var animationClip in animationClips)
                output += string.Format("{0},{1},{2}\n", animationClip.substate, animationClip.name, animationClip.animationClip.name);
            Debug.Log(output);
        }
    }

    private void AddAllStates(UnityEditor.Animations.AnimatorStateMachine stateMachine, string substate = "")
    {
        foreach (var state in stateMachine.states)
            if (state.state.motion is AnimationClip)
                animationClips.Add(new AnimationGroup() { substate = substate, name = state.state.name, animationClip = state.state.motion as AnimationClip });
            else if (state.state.motion is UnityEditor.Animations.BlendTree)
                AddBlendTreeClips(state.state.motion as UnityEditor.Animations.BlendTree, state.state.name, substate);

        foreach (var childStateMachine in stateMachine.stateMachines)
            AddAllStates(childStateMachine.stateMachine, childStateMachine.stateMachine.name);
    }

    private void AddBlendTreeClips(UnityEditor.Animations.BlendTree blendTree, string name, string substate)
    {
        foreach (var child in blendTree.children)
            if (child.motion is AnimationClip)
                animationClips.Add(new AnimationGroup() { substate = substate, name = name, animationClip = child.motion as AnimationClip });
            else if (child.motion is UnityEditor.Animations.BlendTree)
                AddBlendTreeClips(child.motion as UnityEditor.Animations.BlendTree, name, substate);
    }

    private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        Rect nameRectangle = new Rect(rect.x, rect.y, rect.width / 3, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(nameRectangle, element.FindPropertyRelative("name"), GUIContent.none);
        Rect animRectangle = new Rect(rect.x + rect.width / 3, rect.y, rect.width * 2 / 3, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(animRectangle, element.FindPropertyRelative("animationClip"), GUIContent.none);
    }

    private void DrawElementCallbackWithThreeColumns(Rect rect, int index, bool isActive, bool isFocused)
    {
        var element = list.serializedProperty.GetArrayElementAtIndex(index);
        rect.y += 2;
        Rect substateRectangle = new Rect(rect.x, rect.y, rect.width / 3, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(substateRectangle, element.FindPropertyRelative("substate"), GUIContent.none);
        Rect nameRectangle = new Rect(rect.x + rect.width / 3, rect.y, rect.width / 3, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(nameRectangle, element.FindPropertyRelative("name"), GUIContent.none);
        Rect animRectangle = new Rect(rect.x + rect.width * 2 / 3, rect.y, rect.width / 3, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(animRectangle, element.FindPropertyRelative("animationClip"), GUIContent.none);
    }
}
#endif