using UnityEngine;
using UnityEditor;

public class EnemySpawningTool : EditorWindow
{
    private SerializedObject so;
    public GameObject SpawnPrefab;
    [Tooltip("Offset for y component of the world position, will not spawn above this value")]
    public float SpawnOffset;
    private Mesh mesh;
    private Material[] materials;
    private SerializedProperty spawnPrefabProp;
    private SerializedProperty spawnOffsetProp;

    [MenuItem("Tools/EnemySpawningTool")]
    private static void ShowWindow()
    {
        var window = GetWindow<EnemySpawningTool>();
        window.titleContent = new GUIContent("EnemySpawningTool");
        window.Show();
    }
    private void OnEnable()
    {
        so = new SerializedObject(this);
        
        spawnPrefabProp = so.FindProperty("SpawnPrefab");
        spawnOffsetProp = so.FindProperty("SpawnOffset");

        SceneView.duringSceneGui += DuringSceneGUI;
    }
    private void OnDisable()
    {
        SceneView.duringSceneGui -= DuringSceneGUI;
    }
    private void OnGUI()
    {
        so.Update();
        EditorGUILayout.PropertyField(spawnPrefabProp);
        EditorGUILayout.PropertyField(spawnOffsetProp);
        if (SpawnPrefab != null)
        {
            mesh = SpawnPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
            materials = SpawnPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials;
        }

        so.ApplyModifiedProperties();
    }
    private void DuringSceneGUI(SceneView sceneView)
    {
        if (Event.current.control)
        {
            sceneView.Repaint();
        }

        Vector3? point = null;
        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        if (Physics.Raycast(r, out RaycastHit hitInfo))
        {
            point = hitInfo.point.y > SpawnOffset ? null : hitInfo.point;
            
        }
        if (point == null) return;
        if (SpawnPrefab == null) return;

        if (Event.current.control)
        {
            PreviewGOMeshAt(point);
            var LMB = Event.current.type == EventType.MouseDown && Event.current.button == 0;
            if (LMB)
            {
                InstantiateGOAt(point);
            }
        }
    }
    private void InstantiateGOAt(Vector3? point)
    {
        GameObject instance = PrefabUtility.InstantiatePrefab(SpawnPrefab) as GameObject;
        instance.transform.position = (Vector3)point;
        Undo.RegisterCreatedObjectUndo(instance, $"Spawned {instance}");
    }
    private void PreviewGOMeshAt(Vector3? point)
    {
        Vector3 p = (Vector3)point;
        foreach (Material m in materials)
        {
            m.SetPass(0);
        }
        Graphics.DrawMeshNow(mesh, p, Quaternion.identity);
        Handles.DrawWireDisc(p + Vector3.up, Vector3.up, 1f);
    }
}