using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using System;

public class ObjectTemplateEditorWindow : EditorWindow
{
    private string jsonFilePath = "Assets/JSON files/generated.json";
    private List<ObjectTemplate> templates = new List<ObjectTemplate>();
    private Vector2 scrollPosition;

    private Canvas canvas;

    private void OnGUI()
    {
        GUILayout.Label("Object Template Editor", EditorStyles.boldLabel);

        if (GUILayout.Button("Load JSON Data"))
        {
            LoadJSONData();
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        for (int i = 0; i < templates.Count; i++)
        {
            EditorGUILayout.LabelField("Template Name: " + templates[i].Name);

            // This will allow users to edit template properties
            templates[i].Name = EditorGUILayout.TextField("Name:", templates[i].Name);
            templates[i].Position = EditorGUILayout.Vector3Field("Position:", templates[i].Position);
            templates[i].Rotation = EditorGUILayout.Vector3Field("Rotation:", templates[i].Rotation);
            templates[i].Scale = EditorGUILayout.Vector3Field("Scale:", templates[i].Scale);

            GUILayout.Space(10);

            // Button added to delete existing template
            if (GUILayout.Button("Delete Template"))
            {
                templates.RemoveAt(i);
                i--;
            }
        }
        EditorGUILayout.EndScrollView();

        // Button for new template
        if (GUILayout.Button("Create New Template"))
        {
            ObjectTemplate newTemplate = new ObjectTemplate();
            templates.Add(newTemplate);
        }

        if (GUILayout.Button("Save JSON Data"))
        {
            SaveJSONData();
        }

        // Button to instantiate the selected template in the scene
        if (GUILayout.Button("Instantiate Template"))
        {
            if (canvas == null)
            {

                GameObject canvasGO = Instantiate(templatePrefab);
                canvasGO.name = "CanvasInstance";
                canvas = canvasGO.GetComponent<Canvas>();
            }

            if (selectedTemplateIndex >= 0 && selectedTemplateIndex < templates.Count)
            {
                InstantiateTemplate(templates[selectedTemplateIndex]);
            }
        }
    }

    private void LoadJSONData()
    {       //error handling in case of no json file is provided or corrupted json is provided
        try
        {
            if (File.Exists(jsonFilePath))
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                templates = JsonUtility.FromJson<TemplateData>(jsonContent).Templates;
            }
            else
            {
                templates.Clear();
                Debug.Log("JSON file does not exist.");
            }
        }
        catch (Exception e)
        {
            templates.Clear();
            Debug.LogError("Error loading JSON data: " + e.Message);
        }
    }

    private void SaveJSONData()
    {   //error handled in case of wrong json format is provided
        try
        {
            TemplateData templateData = new TemplateData { Templates = templates };
            string jsonContent = JsonUtility.ToJson(templateData, true);
            File.WriteAllText(jsonFilePath, jsonContent);
            Debug.Log("JSON data saved successfully.");
        }
        catch (Exception e)
        {
            Debug.LogError("Error saving JSON data: " + e.Message);
        }
    }


    private int selectedTemplateIndex = -1;
    public GameObject templatePrefab;

    private void InstantiateTemplate(ObjectTemplate template)
    {
        if (templatePrefab != null)
        {

            GameObject templateGroup = Instantiate(templatePrefab);

            // properties which will customise the parent's position, rotation, and scale based on the template
            templateGroup.transform.SetParent(canvas.transform);
            templateGroup.transform.localPosition = template.Position;
            templateGroup.transform.localRotation = Quaternion.Euler(template.Rotation);
            templateGroup.transform.localScale = template.Scale;
        }
        else
        {
            Debug.LogError("Prefab reference (templatePrefab) is not assigned in the Inspector.");
        }
    }

    [System.Serializable]
    public class ObjectTemplate
    {
        public string Name = "canvasTemplate";
        public Vector3 Position;
        public Vector3 Rotation;
        public Vector3 Scale;
    }

    [System.Serializable]
    public class TemplateData
    {
        public List<ObjectTemplate> Templates = new List<ObjectTemplate>();
    }

    [MenuItem("Window/Object Template Editor")]
    public static void ShowWindow()
    {
        GetWindow<ObjectTemplateEditorWindow>("Object Template Editor");
    }
}
