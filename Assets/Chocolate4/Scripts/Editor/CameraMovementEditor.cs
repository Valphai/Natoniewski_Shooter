using UnityEngine;
using UnityEditor.AnimatedValues;
using UnityEditor;
using Chocolate4.Level;

[CustomEditor(typeof(CameraMovement))]
public class CameraMovementEditor : Editor
{
    private AnimBool draggableMapAnim;
    private AnimBool canZoomAnim;
    private AnimBool canTwistAnim;
    private AnimBool canRotateAnim;
    private AnimBool canMoveByEdgeAnim;
    private SerializedProperty canMoveByDragMap;
    private SerializedProperty canZoom;
    private SerializedProperty canTwistZoom;
    private SerializedProperty canRotate;
    private SerializedProperty canMoveByEdge;
    private SerializedProperty mWheelDraggableLayer;
    private SerializedProperty minZoom, maxZoom;
    private SerializedProperty twistMinZoom, twistMaxZoom;
    private SerializedProperty moveSpeedMinZoom, moveSpeedMaxZoom;
    private SerializedProperty rotationSpeed;
    private SerializedProperty screenEdgePan;
    private CameraMovement cam;
    private Transform swivel, stick;

    private void OnEnable()
    {
        cam = (CameraMovement)target;
        swivel = cam.transform.GetChild(0);
        stick = swivel.GetChild(0);

        canMoveByDragMap = 
            serializedObject.FindProperty("canMoveByDragMap");
        canZoom = 
            serializedObject.FindProperty("canZoom");
        canTwistZoom = 
            serializedObject.FindProperty("canTwistZoom");
        canRotate = 
            serializedObject.FindProperty("canRotate");
        canMoveByEdge = 
            serializedObject.FindProperty("canMoveByEdge");
        mWheelDraggableLayer = 
            serializedObject.FindProperty("mouseWheelDraggable");
        minZoom = 
            serializedObject.FindProperty("minZoom");
        maxZoom = 
            serializedObject.FindProperty("maxZoom");
        twistMinZoom = 
            serializedObject.FindProperty("twistMinZoom");
        twistMaxZoom = 
            serializedObject.FindProperty("twistMaxZoom");
        moveSpeedMinZoom = 
            serializedObject.FindProperty("moveSpeedMinZoom");
        moveSpeedMaxZoom = 
            serializedObject.FindProperty("moveSpeedMaxZoom");
        rotationSpeed = 
            serializedObject.FindProperty("rotationSpeed");
        screenEdgePan = 
            serializedObject.FindProperty("screenEdgePan");

        draggableMapAnim = new AnimBool(canMoveByDragMap.boolValue);
        canZoomAnim = new AnimBool(canZoom.boolValue);
        canTwistAnim = new AnimBool(canTwistZoom.boolValue);
        canRotateAnim = new AnimBool(canRotate.boolValue);
        canMoveByEdgeAnim = new AnimBool(canMoveByEdge.boolValue);

        draggableMapAnim.valueChanged.AddListener(Repaint);
        canZoomAnim.valueChanged.AddListener(Repaint);
        canTwistAnim.valueChanged.AddListener(Repaint);
        canRotateAnim.valueChanged.AddListener(Repaint);
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.Space();
        GUILayout.Label("CAMERA OPTIONS", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        canMoveByEdgeAnim.target = EditorGUILayout.ToggleLeft("Can move by edge", canMoveByEdgeAnim.target);
        canMoveByEdge.boolValue = canMoveByEdgeAnim.target;

        if (EditorGUILayout.BeginFadeGroup(canMoveByEdgeAnim.faded))
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(
                screenEdgePan, new GUIContent("Screen edge pan")
            );
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        draggableMapAnim.target = EditorGUILayout.ToggleLeft("Can drag map", draggableMapAnim.target);
        canMoveByDragMap.boolValue = draggableMapAnim.target;

        if (EditorGUILayout.BeginFadeGroup(draggableMapAnim.faded))
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(
                mWheelDraggableLayer, new GUIContent("Draggable Layer")
            );

            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        canRotateAnim.target = EditorGUILayout.ToggleLeft("Can rotate map", canRotateAnim.target);
        canRotate.boolValue = canRotateAnim.target;

        if (EditorGUILayout.BeginFadeGroup(canRotateAnim.faded))
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(
                rotationSpeed, new GUIContent("Rotation Speed")
            );

            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        canZoomAnim.target = EditorGUILayout.ToggleLeft("Can zoom in", canZoomAnim.target);
        canZoom.boolValue = canZoomAnim.target;

        if (EditorGUILayout.BeginFadeGroup(canZoomAnim.faded))
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(
                minZoom, new GUIContent("Min Zoom")
            );
            EditorGUILayout.PropertyField(
                maxZoom, new GUIContent("Max Zoom")
            );

            EditorGUILayout.PropertyField(
                moveSpeedMinZoom, new GUIContent("Speed Farthest")
            );
            EditorGUILayout.PropertyField(
                moveSpeedMaxZoom, new GUIContent("Speed Closest")
            );

            canTwistAnim.target = EditorGUILayout.ToggleLeft("Twist camera during zoom", canTwistAnim.target);
            canTwistZoom.boolValue = canTwistAnim.target;

            if (EditorGUILayout.BeginFadeGroup(canTwistAnim.faded))
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(
                    twistMinZoom, new GUIContent("Twist Min Zoom")
                );
                EditorGUILayout.PropertyField(
                    twistMaxZoom, new GUIContent("Twist Max Zoom")
                );

                EditorGUI.indentLevel--;
            }
            EditorGUILayout.EndFadeGroup();

            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        if (canZoom.boolValue)
        {
            Quaternion stickRot = stick.localRotation;
            Vector3 pos = cam.transform.position;

            Vector3 stickStart = stick.forward * minZoom.floatValue;
            Vector3 stickEnd = stick.forward * maxZoom.floatValue;

            Handles.color = Color.magenta;

            Vector3 p1 = pos + stickRot * stickStart;
            Vector3 p2 = pos + stickRot * stickEnd;
            Handles.DrawAAPolyLine(p1, p2);
        }
    }
}