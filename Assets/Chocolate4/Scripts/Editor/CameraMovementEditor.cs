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
    private AnimBool canLockCharacterAnim;
    private SerializedProperty canMoveByDragMapProp;
    private SerializedProperty canZoomProp;
    private SerializedProperty canTwistZoomProp;
    private SerializedProperty canRotateProp;
    private SerializedProperty canMoveByEdgeProp;
    private SerializedProperty canLockCharacterProp;
    private SerializedProperty mWheelDraggableLayerProp;
    private SerializedProperty minZoomProp, maxZoomProp;
    private SerializedProperty twistMinZoomProp, twistMaxZoomProp;
    private SerializedProperty moveSpeedMinZoomProp, moveSpeedMaxZoomProp;
    private SerializedProperty rotationSpeedProp;
    private SerializedProperty screenEdgePanProp;
    private SerializedProperty lockedTransformProp;
    private CameraMovement cam;
    private Transform swivel, stick;

    private void OnEnable()
    {
        cam = (CameraMovement)target;
        swivel = cam.transform.GetChild(0);
        stick = swivel.GetChild(0);

        canMoveByDragMapProp = 
            serializedObject.FindProperty("canMoveByDragMap");
        canZoomProp = 
            serializedObject.FindProperty("canZoom");
        canTwistZoomProp = 
            serializedObject.FindProperty("canTwistZoom");
        canRotateProp = 
            serializedObject.FindProperty("canRotate");
        canMoveByEdgeProp = 
            serializedObject.FindProperty("canMoveByEdge");
        mWheelDraggableLayerProp = 
            serializedObject.FindProperty("mouseWheelDraggable");
        minZoomProp = 
            serializedObject.FindProperty("minZoom");
        maxZoomProp = 
            serializedObject.FindProperty("maxZoom");
        twistMinZoomProp = 
            serializedObject.FindProperty("twistMinZoom");
        twistMaxZoomProp = 
            serializedObject.FindProperty("twistMaxZoom");
        moveSpeedMinZoomProp = 
            serializedObject.FindProperty("moveSpeedMinZoom");
        moveSpeedMaxZoomProp = 
            serializedObject.FindProperty("moveSpeedMaxZoom");
        rotationSpeedProp = 
            serializedObject.FindProperty("rotationSpeed");
        screenEdgePanProp = 
            serializedObject.FindProperty("screenEdgePan");
        canLockCharacterProp = 
            serializedObject.FindProperty("canLockCharacter");
        lockedTransformProp = 
            serializedObject.FindProperty("lockedTransform");

        draggableMapAnim = new AnimBool(canMoveByDragMapProp.boolValue);
        canZoomAnim = new AnimBool(canZoomProp.boolValue);
        canTwistAnim = new AnimBool(canTwistZoomProp.boolValue);
        canRotateAnim = new AnimBool(canRotateProp.boolValue);
        canMoveByEdgeAnim = new AnimBool(canMoveByEdgeProp.boolValue);
        canLockCharacterAnim = new AnimBool(canLockCharacterProp.boolValue);

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

        canLockCharacterAnim.target = EditorGUILayout.ToggleLeft("Can lock character", canLockCharacterAnim.target);
        canMoveByEdgeProp.boolValue = canLockCharacterAnim.target;

        if (EditorGUILayout.BeginFadeGroup(canLockCharacterAnim.faded))
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(
                lockedTransformProp, new GUIContent("Character to lock onto")
            );
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        canMoveByEdgeAnim.target = EditorGUILayout.ToggleLeft("Can move by edge", canMoveByEdgeAnim.target);
        canMoveByEdgeProp.boolValue = canMoveByEdgeAnim.target;

        if (EditorGUILayout.BeginFadeGroup(canMoveByEdgeAnim.faded))
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(
                screenEdgePanProp, new GUIContent("Screen edge pan")
            );
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        draggableMapAnim.target = EditorGUILayout.ToggleLeft("Can drag map", draggableMapAnim.target);
        canMoveByDragMapProp.boolValue = draggableMapAnim.target;

        if (EditorGUILayout.BeginFadeGroup(draggableMapAnim.faded))
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(
                mWheelDraggableLayerProp, new GUIContent("Draggable Layer")
            );

            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        canRotateAnim.target = EditorGUILayout.ToggleLeft("Can rotate map", canRotateAnim.target);
        canRotateProp.boolValue = canRotateAnim.target;

        if (EditorGUILayout.BeginFadeGroup(canRotateAnim.faded))
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(
                rotationSpeedProp, new GUIContent("Rotation Speed")
            );

            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFadeGroup();

        canZoomAnim.target = EditorGUILayout.ToggleLeft("Can zoom in", canZoomAnim.target);
        canZoomProp.boolValue = canZoomAnim.target;

        if (EditorGUILayout.BeginFadeGroup(canZoomAnim.faded))
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(
                minZoomProp, new GUIContent("Min Zoom")
            );
            EditorGUILayout.PropertyField(
                maxZoomProp, new GUIContent("Max Zoom")
            );

            EditorGUILayout.PropertyField(
                moveSpeedMinZoomProp, new GUIContent("Speed Farthest")
            );
            EditorGUILayout.PropertyField(
                moveSpeedMaxZoomProp, new GUIContent("Speed Closest")
            );

            canTwistAnim.target = EditorGUILayout.ToggleLeft("Twist camera during zoom", canTwistAnim.target);
            canTwistZoomProp.boolValue = canTwistAnim.target;

            if (EditorGUILayout.BeginFadeGroup(canTwistAnim.faded))
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.PropertyField(
                    twistMinZoomProp, new GUIContent("Twist Min Zoom")
                );
                EditorGUILayout.PropertyField(
                    twistMaxZoomProp, new GUIContent("Twist Max Zoom")
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
        if (canZoomProp.boolValue)
        {
            Quaternion stickRot = stick.localRotation;
            Vector3 pos = cam.transform.position;

            Vector3 stickStart = stick.forward * minZoomProp.floatValue;
            Vector3 stickEnd = stick.forward * maxZoomProp.floatValue;

            Handles.color = Color.magenta;

            Vector3 p1 = pos + stickRot * stickStart;
            Vector3 p2 = pos + stickRot * stickEnd;
            Handles.DrawAAPolyLine(p1, p2);
        }
    }
}