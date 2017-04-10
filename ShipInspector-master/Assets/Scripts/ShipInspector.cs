using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Ship))]
public class ShipInspector : Editor {

    Ship shipInspector;
    int pointPool = 100;
    int tab;

    private void OnEnable()
    {
        shipInspector = (Ship)target;
    }

    public override void OnInspectorGUI()
    {
        int pointRemaining = 100;

        serializedObject.Update();
        tab = GUILayout.Toolbar(tab, new string[] { "Ship", "Crew" });
        switch (tab)
        {
            case 0:
                EditorGUILayout.LabelField("Ship Stats");
                EditorGUILayout.BeginVertical("Box");
                shipInspector.hitPoints = EditorGUILayout.IntField("Hit Points(hp)", shipInspector.hitPoints);
                EditorGUILayout.Space();
                shipInspector.armor = EditorGUILayout.IntField("Armor", shipInspector.armor);
                shipInspector.attack = EditorGUILayout.IntField("Attack", shipInspector.attack);
                shipInspector.agility = EditorGUILayout.IntField("Agility", shipInspector.agility);
                pointRemaining = pointPool - shipInspector.armor - shipInspector.attack - shipInspector.agility;
                if (shipInspector.hitPoints <= 0)
                    shipInspector.hitPoints = 1;
                if (shipInspector.armor < 10 || pointRemaining < 0)
                    shipInspector.armor = 10;
                if (shipInspector.attack < 10 || pointRemaining < 0)
                    shipInspector.attack = 10;
                if (shipInspector.agility < 10 || pointRemaining < 0)
                    shipInspector.agility = 10;

                EditorGUILayout.LabelField("Points Remaining", pointRemaining.ToString());
                EditorGUILayout.Space();
                SerializedProperty shipGuns = serializedObject.FindProperty("shipGuns");

                EditorGUILayout.PropertyField(shipGuns, true);
                EditorGUILayout.EndVertical();
                break;
            case 1:
                EditorGUILayout.LabelField("Crew Info");
                EditorGUILayout.BeginVertical("Box");
                SerializedProperty crewStats = serializedObject.FindProperty("crewMembers");

                EditorGUILayout.PropertyField(crewStats, true);
                EditorGUILayout.EndVertical();
                break;
        }
       

        serializedObject.ApplyModifiedProperties();
        //base.OnInspectorGUI();
    }
}
