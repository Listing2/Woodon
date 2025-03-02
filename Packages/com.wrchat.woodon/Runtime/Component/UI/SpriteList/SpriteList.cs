﻿using UnityEngine;
using UnityEngine.UI;
using UdonSharp;

namespace WRC.Woodon
{
	[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
	public class SpriteList : WBase
	{
		[Header("_" + nameof(SpriteList))]
		[SerializeField] private Sprite[] sprites;

		[Header("_" + nameof(SpriteList) + " - Targets")]
		[SerializeField] private Image[] images;
		[SerializeField] private SpriteRenderer[] spriteRenderers;

		[Header("_" + nameof(SpriteList) + " - Options")]
		[SerializeField] private SpriteListInitType initType = SpriteListInitType.FirstSprite;
		[SerializeField] private MValue mValue_SpriteIndex;

		private void Start()
		{
			Init();
		}

		private void Init()
		{
			if (mValue_SpriteIndex != null)
			{
				mValue_SpriteIndex.SetMinMaxValue(0, sprites.Length - 1);
				mValue_SpriteIndex.RegisterListener(this, nameof(SetAllByMValue));
				SetAllByMValue();
			}
			else
			{
				switch (initType)
				{
					case SpriteListInitType.FirstSprite:
						SetAll(0);
						break;
					case SpriteListInitType.EachIndex:
						InitSprites();
						break;
				}
			}
		}

		public void InitSprites()
		{
			MDebugLog(nameof(InitSprites));

			for (int i = 0; i < images.Length; i++)
				images[i].sprite = sprites[i];

			for (int i = 0; i < spriteRenderers.Length; i++)
				spriteRenderers[i].sprite = sprites[i];
		}

		[ContextMenu(nameof(SetAllByMValue))]
		public void SetAllByMValue()
		{
			MDebugLog(nameof(SetAllByMValue));
			if (mValue_SpriteIndex)
				SetAll(mValue_SpriteIndex.Value);
		}

		[ContextMenu(nameof(SetAll))]
		public void SetAll()
		{
			SetAll(0);
		}

		public void SetAll(int spriteIndex)
		{
			MDebugLog(nameof(SetAll));

			if (spriteIndex < 0 || spriteIndex >= sprites.Length)
			{
				MDebugLog($"{nameof(SetAll)}, Index out of range: {spriteIndex}", LogType.Error);
				return;
			}

			foreach (Image image in images)
				image.sprite = sprites[spriteIndex];

			foreach (SpriteRenderer spriteRenderer in spriteRenderers)
				spriteRenderer.sprite = sprites[spriteIndex];
		}
	}
}