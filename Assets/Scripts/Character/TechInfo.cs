using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
using System;

#endif

public enum TechType {Tech, Getup};
public enum TechAction {None, Roll, Neutral, Attack};
public enum RollDirection {Forward, Backward, None};
public enum GetupType {Back, Stomach, None};

[Serializable]
public class TechInfo : ScriptableObject{
    [SerializeField] private TechSheet fullTechSheet;
    public TechType techType = TechType.Tech;
    public TechAction techAction = TechAction.None;
    public RollDirection rollDirection = RollDirection.None;
    public GetupType getupType = GetupType.None;
    
    bool showTechData = true;
    bool showFrameData = true;

    public int totalFrames = 0;
    public int movementStartFrame = 0;
    public float rollDistance = 0;
    public Vector2Int invulnerableFrames;
    public Vector2Int hitFrontFrames;
    public Vector2Int hitBehindFrames;

    public TechSheet FullTechSheet { get => fullTechSheet; }

#region Editor
#if UNITY_EDITOR

    public void Initialize(TechSheet character, String _name, TechType _type, TechAction _action, RollDirection _direction, GetupType _getup)
    {
        fullTechSheet = character;
        this.name = _name;
        techType = _type;
        techAction = _action;
        rollDirection = _direction;
        getupType = _getup;
    }


    [CustomEditor(typeof(TechInfo))]
    public class TechInfoEditor : Editor
    {
        
        public override void OnInspectorGUI()
        {   
            //StartUp Stuff
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();
            //Local Reference
            TechInfo techInfo = (TechInfo)target;

            EditorGUILayout.LabelField(techInfo.name);
            
            techInfo.showTechData = EditorGUILayout.Foldout(techInfo.showTechData, "Tech Data", true);
            if(techInfo.showTechData)
            {
                EditorGUI.indentLevel++;

                //Tech Type Field
                techInfo.techType = (TechType)EditorGUILayout.EnumPopup("Tech Type", techInfo.techType);

                //Tech GetupType
                if(techInfo.techType == TechType.Getup)
                    techInfo.getupType = (GetupType)EditorGUILayout.EnumPopup("Getup Type", techInfo.getupType);

                //Tech Action Field
                GUIContent label = new GUIContent("Tech Action");
                techInfo.techAction = (TechAction)EditorGUILayout.EnumPopup(label, techInfo.techAction, DisplayAction, false);
                bool DisplayAction(Enum action) {
                    bool display = true;
                    if(techInfo.techType == TechType.Tech && action.Equals(TechAction.Attack)) 
                        display = false; 
                    return display;
                }

                //Tech Roll Direction Field
                if(techInfo.techAction == TechAction.Roll)
                    techInfo.rollDirection = (RollDirection)EditorGUILayout.EnumPopup("Tech Roll Direction", techInfo.rollDirection);
                
                EditorGUI.indentLevel--;
            }
    

            EditorGUILayout.Space();

            techInfo.showFrameData = EditorGUILayout.Foldout(techInfo.showFrameData, "Frame Data", true);
            if(techInfo.showFrameData)
            {
                EditorGUI.indentLevel++;

                techInfo.totalFrames = EditorGUILayout.IntField("Total Frames", techInfo.totalFrames);

                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Invulnerable Frames", GUILayout.MaxWidth(EditorGUIUtility.labelWidth - EditorGUI.indentLevel*15));
                    EditorGUILayout.LabelField("Start", GUILayout.MaxWidth(50));
                    techInfo.invulnerableFrames.x = EditorGUILayout.IntField(techInfo.invulnerableFrames.x);
                    EditorGUILayout.LabelField("End", GUILayout.MaxWidth(50));
                    techInfo.invulnerableFrames.y = EditorGUILayout.IntField(techInfo.invulnerableFrames.y);
                    EditorGUILayout.EndHorizontal();
                
                }
                if(techInfo.techAction == TechAction.Roll) {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("Roll Start Frame", GUILayout.MaxWidth(110));
                    techInfo.movementStartFrame = EditorGUILayout.IntField(techInfo.movementStartFrame);
                    EditorGUILayout.LabelField("Distance (feet)", GUILayout.MaxWidth(110));
                    techInfo.rollDistance = EditorGUILayout.FloatField(techInfo.rollDistance);
                    EditorGUILayout.EndHorizontal();
                }

                if(techInfo.techAction == TechAction.Attack) {
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Front Hit Frames", GUILayout.MaxWidth(EditorGUIUtility.labelWidth - EditorGUI.indentLevel*15));
                        EditorGUILayout.LabelField("Start", GUILayout.MaxWidth(50));
                        techInfo.hitFrontFrames.x = EditorGUILayout.IntField(techInfo.hitFrontFrames.x);
                        EditorGUILayout.LabelField("End", GUILayout.MaxWidth(50));
                        techInfo.hitFrontFrames.y = EditorGUILayout.IntField(techInfo.hitFrontFrames.y);
                        EditorGUILayout.EndHorizontal();
                    }
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Back Hit Frames", GUILayout.MaxWidth(EditorGUIUtility.labelWidth - EditorGUI.indentLevel*15));
                        EditorGUILayout.LabelField("Start", GUILayout.MaxWidth(50));
                        techInfo.hitBehindFrames.x = EditorGUILayout.IntField(techInfo.hitBehindFrames.x);
                        EditorGUILayout.LabelField("End", GUILayout.MaxWidth(50));
                        techInfo.hitBehindFrames.y = EditorGUILayout.IntField(techInfo.hitBehindFrames.y);
                        EditorGUILayout.EndHorizontal();
                    }
                }
                EditorGUI.indentLevel--;
            }

            //Validation
            if(EditorGUI.EndChangeCheck()) {
                if(techInfo.techType == TechType.Tech && techInfo.techAction == TechAction.Attack) techInfo.techAction = TechAction.None;
                if(techInfo.techAction != TechAction.Roll) techInfo.rollDirection = RollDirection.None;
                if(techInfo.techType == TechType.Tech) techInfo.getupType = GetupType.None;
            }
        }
    }
    
#endif
#endregion
}