﻿using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace WRC.Woodon
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class MTeamManager : WEventPublisher
	{
		[field: Header("_" + nameof(MTeamManager))]
		[field: SerializeField] public MTeam[] MTeams { get; private set; }

		[SerializeField] private bool onlyOneTeam = true;

		private void Start()
		{
			Init();
		}

		private void Init()
		{
			foreach (MTeam mTeam in MTeams)
				mTeam.Init(this);
		}

		public void PlayerChanged(TeamType teamType, UIMTeamButton targetTeamButton)
		{
			WDebugLog(
				$"{nameof(PlayerChanged)} : {nameof(TeamType)} = {teamType}, {nameof(UIMTeamButton)} = {targetTeamButton}");

			if (targetTeamButton.WPlayer.TargetPlayerID == NONE_INT ||
				targetTeamButton.WPlayer.TargetPlayerID == Networking.LocalPlayer.playerId)
			{
				WDebugLog($"Invalid ID");
				return;
			}

			if (onlyOneTeam)
				foreach (MTeam mTeam in MTeams)
				{
					if (mTeam.TeamType == teamType)
						continue;

					foreach (UIMTeamButton teamButton in mTeam.TeamButtons)
						if (teamButton.IsPlayer())
							teamButton.WPlayer.SetTargetNone();
				}

			SendEvents();
		}

		public TeamType GetTargetPlayerTeamType(VRCPlayerApi targetPlayer = null)
		{
			if (targetPlayer == null)
				targetPlayer = Networking.LocalPlayer;

			foreach (MTeam mTeam in MTeams)
				if (mTeam.IsTargetPlayerTeam(targetPlayer))
					return mTeam.TeamType;

			return TeamType.None;
		}
	}
}