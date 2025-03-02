using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using WRC.Woodon;

namespace WRC
{
#if UNITY_EDITOR
	public class CustomValueUIMapping : MonoBehaviour
	{
		[SerializeField] private Transform wBoolsParent;
		[SerializeField] private Transform uiWBoolsParent;

		[ContextMenu(nameof(MappingWBool2UI))]
		public void MappingWBool2UI()
		{
			WBool[] wBools = wBoolsParent.GetComponentsInChildren<WBool>(true);
			UIWBool[] uiWBools = uiWBoolsParent.GetComponentsInChildren<UIWBool>(true);

			for (int i = 0; i < wBools.Length; i++)
			{
				WBool wBool = wBools[i];
				UIWBool uiWBool = uiWBools[i];

				uiWBool.SetWBool(wBool);
				EditorUtility.SetDirty(uiWBool);
			}

			AssetDatabase.SaveAssets();
		}

		[SerializeField] private Transform mValuesParent;
		[SerializeField] private Transform uimValuesParent;

		[ContextMenu(nameof(MappingMValue2UI))]
		public void MappingMValue2UI()
		{
			MValue[] mValues = mValuesParent.GetComponentsInChildren<MValue>(true);
			UIMValue[] uiMValues = uimValuesParent.GetComponentsInChildren<UIMValue>(true);

			for (int i = 0; i < mValues.Length; i++)
			{
				MValue mValue = mValues[i];
				UIMValue uiMValue = uiMValues[i];

				uiMValue.SetMValue(mValue);
				EditorUtility.SetDirty(uiMValue);
			}

			AssetDatabase.SaveAssets();
		}
	}
#endif
}