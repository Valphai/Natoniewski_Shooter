using UnityEngine;
using UnityEditor;
using Chocolate4.PersistantThroughLevels;
using Chocolate4.Entities;

public class EnemySpawningTool : EditorWindow
{
    private SerializedObject so;
    public EntityManager EntityManager;
    [Tooltip("Offset for y component of the world position, will not spawn above this value")]
    public float SpawnOffset;
    private Entity spawnPrefab;
    private Mesh mesh;
    private Material[] materials;
    private SerializedProperty entityManagerProp;
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
        
        entityManagerProp = so.FindProperty("EntityManager");
        spawnOffsetProp = so.FindProperty("SpawnOffset");

        SpawnOffset = EditorPrefs.GetFloat("ENEMY_SPAWNING_TOOL_SpawnOffset", .15f);

        SceneView.duringSceneGui += DuringSceneGUI;
    }
    private void OnDisable()
    {
        EditorPrefs.SetFloat("ENEMY_SPAWNING_TOOL_SpawnOffset", SpawnOffset);

        SceneView.duringSceneGui -= DuringSceneGUI;
    }
    private void OnGUI()
    {
        so.Update();
        EditorGUILayout.PropertyField(entityManagerProp);
        EditorGUILayout.PropertyField(spawnOffsetProp);
        
        if (EntityManager != null)
        {
            spawnPrefab = null ?? EntityManager.EnemyPrefab;
            if (spawnPrefab != null)
            {
                mesh = spawnPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh;
                materials = spawnPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials;
            }
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
        if (spawnPrefab == null) return;

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
        GameObject instance = EntityManager.SpawnEnemy().gameObject;// PrefabUtility.InstantiatePrefab(spawnPrefab) as GameObject;
        instance.transform.position = (Vector3)point;
        Undo.RegisterCreatedObjectUndo(instance, $"Spawned {instance}");
    }
    private void PreviewGOMeshAt(Vector3? point)
    {
        Vector3 p = (Vector3)point;
        foreach (Material m in materials)
        {
            // Forward pass. Shades all light in a single pass.
            m.SetPass(0);
        }
        Graphics.DrawMeshNow(mesh, p, Quaternion.identity);
        Handles.DrawWireDisc(p + Vector3.up, Vector3.up, 1f);
    }
}