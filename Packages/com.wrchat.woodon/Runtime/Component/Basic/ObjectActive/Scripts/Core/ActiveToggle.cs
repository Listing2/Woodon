using UdonSharp;
using UnityEngine;

namespace WRC.Woodon
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public abstract class ActiveToggle : WBase
	{
		[Header("_" + nameof(ActiveToggle))]
		[SerializeField] private bool defaultActive;

		[Header("_" + nameof(ActiveToggle) + " - Options")]
		[SerializeField] private WBool wBool;

		private bool _active;
		public bool Active
		{
			get => _active;
			private set
			{
				MDebugLog($"{nameof(Active)} changed: {_active} -> {value}");
				_active = value;
				UpdateActive();
			}
		}

		private void Start()
		{
			Init();
		}

		protected virtual void Init()
		{
			if (wBool != null)
			{
				wBool.RegisterListener(this, nameof(UpdateValueByWBool));
				UpdateValueByWBool();
			}
			else
			{
				SetActive(defaultActive);
			}

			UpdateActive();
		}

		protected abstract void UpdateActive();

		public void SetActive(bool newActive)
		{
			MDebugLog($"{nameof(SetActive)}({newActive})");

			if (wBool != null)
			{
				wBool.SetValue(newActive);
			}
			else
			{
				Active = newActive;
			}
		}

		public void UpdateValueByWBool()
		{
			if (wBool != null)
				Active = wBool.Value;
		}

		public void SetWBool(WBool wBool)
		{
			this.wBool = wBool;
			UpdateValueByWBool();
		}

		#region HorribleEvents
		[ContextMenu(nameof(ToggleActive))]
		public void ToggleActive() => SetActive(!Active);

		[ContextMenu(nameof(SetActiveTrue))]
		public void SetActiveTrue() => SetActive(true);

		[ContextMenu(nameof(SetActiveFalse))]
		public void SetActiveFalse() => SetActive(false);
		#endregion
	}
}