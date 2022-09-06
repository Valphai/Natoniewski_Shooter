using UnityEngine;
using UnityEditor;
using Chocolate4.PersistantThroughLevels;
using Chocolate4.Entities;

public class EnemySpawningTool : EditorWindow
{
    private SerializedObject so;
    public float ChaseRange;
    public EntityManager EntityManager;
    [Tooltip("Offset for y component of the world position, will not spawn above this value")]
    public float SpawnOffset;
    private Entity spawnPrefab;
    private Mesh mesh;
    private Material[] materials;
    private SerializedProperty entityManagerProp;
    private SerializedProperty spawnOffsetProp;
    private SerializedProperty chaseRangeProp;

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
        chaseRangeProp = so.FindProperty("ChaseRange");

        SpawnOffset = EditorPrefs.GetFloat("ENEMY_SPAWNING_TOOL_SpawnOffset", .15f);
        ChaseRange = EditorPrefs.GetFloat("ENEMY_SPAWNING_TOOL_ChaseRange", .15f);

        SceneView.duringSceneGui += DuringSceneGUI;
    }
    private void OnDisable()
    {
        EditorPrefs.SetFloat("ENEMY_SPAWNING_TOOL_SpawnOffset", SpawnOffset);
        EditorPrefs.SetFloat("ENEMY_SPAWNING_TOOL_ChaseRange", ChaseRange);

        SceneView.duringSceneGui -= DuringSceneGUI;
    }
    private void OnGUI()
    {
        so.Update();
        EditorGUILayout.PropertyField(entityManagerProp);
        EditorGUILayout.PropertyField(spawnOffsetProp);
        EditorGUILayout.PropertyField(chaseRangeProp);
        
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
        if (Application.isPlaying) return;

        if (Event.current.control)
        {
            sceneView.Repaint();
            if (Event.current.type == EventType.ScrollWheel)
            {
                ScrollWheelSetsChaseRange();
            }
        }

        Vector3? point = FindPointByMousePosition();
        if (point == null) return;
        if (spawnPrefab == null) return;

        if (Event.current.control)
        {
            PreviewMeshAt(point);
            var LMB = Event.current.type == EventType.MouseDown && Event.current.button == 0;
            if (LMB)
            {
                InstantiateAt(point);
            }
        }
    }

    private Vector3? FindPointByMousePosition()
    {
        Vector3? point = null;
        Ray r = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        if (Physics.Raycast(r, out RaycastHit hitInfo))
        {
            point = hitInfo.point.y > SpawnOffset ? null : hitInfo.point;
        }

        return point;
    }

    private void ScrollWheelSetsChaseRange()
    {
        float scrollDir = Mathf.Sign(Event.current.delta.y);

        so.Update();
        chaseRangeProp.floatValue += scrollDir * 1f;
        chaseRangeProp.floatValue = Mathf.Max(0, chaseRangeProp.floatValue);
        so.ApplyModifiedProperties();

        Repaint();
        Event.current.Use();
    }

    private void InstantiateAt(Vector3? point)
    {
        Enemy instance = EntityManager.SpawnEnemy() as Enemy;
        
        instance.ChaseRange = chaseRangeProp.floatValue;
        instance.transform.position = (Vector3)point;

        Undo.RegisterCreatedObjectUndo(instance.gameObject, $"Spawned {instance.gameObject}");
    }
    private void PreviewMeshAt(Vector3? point)
    {
        Vector3 p = (Vector3)point;
        foreach (Material m in materials)
        {
            // Forward pass. Shades all light in a single pass.
            m.SetPass(0);
        }
        Handles.color = Color.cyan;
        Graphics.DrawMeshNow(mesh, p, Quaternion.identity);
        Handles.DrawWireDisc(
            p + Vector3.up, 
            Vector3.up, 
            chaseRangeProp.floatValue
        );
    }
}