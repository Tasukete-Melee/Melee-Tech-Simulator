using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;
using System;

#endif

[CreateAssetMenu(fileName = "Character", menuName = "Create/TechSheet", order = 1)]
public class TechSheet : ScriptableObject
{
    [SerializeField] private List<TechInfo> _techs = new List<TechInfo>();


    #if UNITY_EDITOR

        [ContextMenu("Reset Tech Options")]
        private void ResetTechOptions() {
            CreateTechOption("Tech Roll Forward", TechType.Tech, TechAction.Roll, RollDirection.Forward, GetupType.None);
            CreateTechOption("Tech Roll Backward", TechType.Tech, TechAction.Roll, RollDirection.Backward, GetupType.None);
            CreateTechOption("Tech Neutral", TechType.Tech, TechAction.Neutral, RollDirection.None, GetupType.None);
            CreateTechOption("Getup Back Roll Forward", TechType.Getup, TechAction.Roll, RollDirection.Forward, GetupType.Back);
            CreateTechOption("Getup Back Roll Backward", TechType.Getup, TechAction.Roll, RollDirection.Backward, GetupType.Back);
            CreateTechOption("Getup Back Attack", TechType.Getup, TechAction.Attack, RollDirection.None, GetupType.Back);
            CreateTechOption("Getup Back Neutral", TechType.Getup, TechAction.Neutral, RollDirection.None, GetupType.Back);
            CreateTechOption("Getup Stomach Roll Forward", TechType.Getup, TechAction.Roll, RollDirection.Forward, GetupType.Stomach);
            CreateTechOption("Getup Stomach Roll Backward", TechType.Getup, TechAction.Roll, RollDirection.Backward, GetupType.Stomach);
            CreateTechOption("Getup Stomach Attack", TechType.Getup, TechAction.Attack, RollDirection.None, GetupType.Stomach);
            CreateTechOption("Getup Stomach Neutral", TechType.Getup, TechAction.Neutral, RollDirection.None, GetupType.Stomach);
        }

        private void CreateTechOption(String _name, TechType _type, TechAction _action, RollDirection _direction, GetupType _getup) {
            TechInfo tech = ScriptableObject.CreateInstance<TechInfo>();
            tech.name = this.name + " " + _name;
            tech.Initialize(this, _name, _type, _action, _direction, _getup);
            _techs.Add(tech);

            AssetDatabase.AddObjectToAsset(tech, this);
            AssetDatabase.SaveAssets();

            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(tech);
        }

    #endif
}