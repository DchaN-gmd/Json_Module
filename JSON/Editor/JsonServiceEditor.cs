using System;
using JSON;
using UnityEditor;
using UnityEngine;

namespace JSON
{
    [CustomEditor(typeof(JsonService<>), true), CanEditMultipleObjects]
    public class JsonServiceEditor : Editor
    {
        private SerializedProperty _jsonTypePath;
        private SerializedProperty _path;
        private SerializedProperty _fileName;

        private void OnEnable()
        {
            _jsonTypePath = serializedObject.FindProperty("JsonTypePath");
            _path = serializedObject.FindProperty("Path");
            _fileName = serializedObject.FindProperty("FileName");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.SetIsDifferentCacheDirty();

            EditorGUILayout.PropertyField(_jsonTypePath);
            EditorGUILayout.PropertyField(_fileName);

            if (_jsonTypePath.enumValueIndex == (int)JsonTypePath.CustomPath)
            {
                EditorGUILayout.PropertyField(_path);
            }
            else if (_jsonTypePath.enumValueIndex == (int)JsonTypePath.StreamingAndCustomPath)
            {
                EditorGUILayout.PropertyField(_path);
            }
            EditorGUILayout.Space();

            if (GUILayout.Button("Clear JSON"))
            {
                IClearable jsonService = (IClearable)target;
                jsonService.Clear();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
