﻿using UdonSharp;
using UnityEngine;

namespace WRC.Woodon
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class UISeatDataSetter : WBase
	{
		[SerializeField] private MSeat mSeat;
		[SerializeField] private MValue mValue;

		public void SetSeatDataByMValue()
		{
			mSeat.IntData = mValue.Value;
			mSeat.SerializeData();
		}
	}
}