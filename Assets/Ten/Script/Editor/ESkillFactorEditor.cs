using Enemy;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnemySkillFactor))]
public class ESkillFactorEditor : PropertyDrawer
{
    private class PropertyData
    {
        public SerializedProperty Direction;
        public SerializedProperty AimKind;
        public SerializedProperty AimCount;
        public SerializedProperty Coordinate;
        public SerializedProperty IsDelay;
        public SerializedProperty DelayTime;
        public SerializedProperty ReverseMode;
        public SerializedProperty IsHeal;
        public SerializedProperty HealPoint;
        public int RowCount;
    }

    private Dictionary<string, PropertyData> _propertyDataPerPropertyPath = new Dictionary<string, PropertyData>();
    private PropertyData _property;
    private float LineHeight { get { return EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing; } }

    private void Init(SerializedProperty property)
    {
        if (_propertyDataPerPropertyPath.TryGetValue(property.propertyPath, out _property))
        {
            return;
        }

        _property = new PropertyData();
        _property.Direction = property.FindPropertyRelative("_direction");
        _property.AimKind = property.FindPropertyRelative("_aimKind");
        _property.AimCount = property.FindPropertyRelative("_aimCount");
        _property.Coordinate = property.FindPropertyRelative("_coordinate");
        _property.IsDelay = property.FindPropertyRelative("_isDelay");
        _property.DelayTime = property.FindPropertyRelative("_delayTime");
        _property.ReverseMode = property.FindPropertyRelative("_reverseMode");
        _property.IsHeal = property.FindPropertyRelative("_isHeal");
        _property.HealPoint = property.FindPropertyRelative("_healPoint");
        _propertyDataPerPropertyPath.Add(property.propertyPath, _property);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Init(property);
        var fieldRect = position;
        var indentedFieldRect = EditorGUI.IndentedRect(fieldRect);
        fieldRect.height = LineHeight;

        using (new EditorGUI.PropertyScope(fieldRect, label, property))
        {
            property.isExpanded = EditorGUI.Foldout(new Rect(fieldRect), property.isExpanded, label);
            if (property.isExpanded)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    _property.RowCount = 1;
                    fieldRect.y += LineHeight;
                    EditorGUI.LabelField(new Rect(fieldRect), "�U�����o", EditorStyles.boldLabel);
                    fieldRect.y += LineHeight;
                    EditorGUI.PropertyField(new Rect(fieldRect), _property.Direction);
                    _property.RowCount += 2;

                    fieldRect.y += LineHeight;
                    EditorGUI.LabelField(new Rect(fieldRect), "�G�C�����", EditorStyles.boldLabel);
                    fieldRect.y += LineHeight;
                    EditorGUI.PropertyField(new Rect(fieldRect), _property.AimKind);
                    _property.RowCount += 2;

                    AimKindEnemyAttack aimKind;
                    if (Enum.TryParse(_property.AimKind.enumValueIndex.ToString(), out aimKind) && Enum.IsDefined(typeof(AimKindEnemyAttack), aimKind))
                    {
                        switch (aimKind)
                        {
                            case AimKindEnemyAttack.RandomPoint:
                            case AimKindEnemyAttack.RandomHorizontal:
                            case AimKindEnemyAttack.RandomVertical:
                                fieldRect.y += LineHeight;
                                EditorGUI.LabelField(new Rect(fieldRect), "�^�[�Q�b�g��(�� or �s or ��)", EditorStyles.boldLabel);
                                fieldRect.y += LineHeight;
                                EditorGUI.PropertyField(new Rect(fieldRect), _property.AimCount);
                                _property.RowCount += 2;
                                break;

                            case AimKindEnemyAttack.Select:
                                fieldRect.y += LineHeight;
                                EditorGUI.LabelField(new Rect(fieldRect), "���W�w��(Select)", EditorStyles.boldLabel);
                                fieldRect.y += LineHeight;
                                EditorGUI.PropertyField(new Rect(fieldRect), _property.Coordinate);
                                if (_property.Coordinate.isExpanded)
                                {
                                    fieldRect.y += LineHeight * (_property.Coordinate.arraySize + 1);
                                    _property.RowCount += _property.Coordinate.arraySize + 3;
                                }
                                else
                                {
                                    _property.RowCount += 2;
                                }
                                break;
                        }
                    }

                    fieldRect.y += LineHeight;
                    EditorGUI.LabelField(new Rect(fieldRect), "�U�������܂ł̒x���̗L��", EditorStyles.boldLabel);
                    fieldRect.y += LineHeight;
                    EditorGUI.PropertyField(new Rect(fieldRect), _property.IsDelay);
                    _property.RowCount += 2;

                    if (_property.IsDelay.boolValue)
                    {
                        fieldRect.y += LineHeight;
                        EditorGUI.LabelField(new Rect(fieldRect), "�x���̒���", EditorStyles.boldLabel);
                        fieldRect.y += LineHeight;
                        EditorGUI.PropertyField(new Rect(fieldRect), _property.DelayTime);
                        _property.RowCount += 2;

                        if(aimKind != AimKindEnemyAttack.RandomPoint)
                        {
                            fieldRect.y += LineHeight;
                            EditorGUI.LabelField(new Rect(fieldRect), "�U���̕���(True�ō��W���傫����)", EditorStyles.boldLabel);
                            fieldRect.y += LineHeight;
                            EditorGUI.PropertyField(new Rect(fieldRect), _property.ReverseMode);
                            _property.RowCount += 2;
                        }
                    }

                    fieldRect.y += LineHeight;
                    EditorGUI.LabelField(new Rect(fieldRect), "�񕜌��ʂ̗L��", EditorStyles.boldLabel);
                    fieldRect.y += LineHeight;
                    EditorGUI.PropertyField(new Rect(fieldRect), _property.IsHeal);
                    _property.RowCount += 2;

                    if (_property.IsHeal.boolValue)
                    {
                        fieldRect.y += LineHeight;
                        EditorGUI.LabelField(new Rect(fieldRect), "�񕜗�", EditorStyles.boldLabel);
                        fieldRect.y += LineHeight;
                        EditorGUI.PropertyField(new Rect(fieldRect), _property.HealPoint);
                        _property.RowCount += 2;
                    }
                }
            }
        }

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        Init(property);
        // (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing) x �s�� �ŕ`��̈�̍��������߂�
        return LineHeight * (property.isExpanded ? _property.RowCount : 1);
    }
}