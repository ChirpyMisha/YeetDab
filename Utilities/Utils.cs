﻿using System.Linq;
using System.Reflection;
using UnityEngine;

namespace YeetDab.Utilities
{
	public static class Utils
	{
		public static Sprite LoadSpriteFromResources(string resourcePath, float PixelsPerUnit = 100.0f)
		{
			return LoadSpriteRaw(GetResource(Assembly.GetCallingAssembly(), resourcePath), PixelsPerUnit);
		}

		public static Sprite LoadSpriteRaw(byte[] image, float PixelsPerUnit = 100.0f)
		{
			return LoadSpriteFromTexture(LoadTextureRaw(image), PixelsPerUnit);
		}

		public static Sprite LoadSpriteFromTexture(Texture2D SpriteTexture, float PixelsPerUnit = 100.0f)
		{
			if (SpriteTexture)
				return Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);
			return null;
		}

		public static Texture2D LoadTextureRaw(byte[] file)
		{
			if (file.Count() > 0)
			{
				Texture2D Tex2D = new Texture2D(2, 2);
				ImageConversion.LoadImage(Tex2D, file);
				return Tex2D;
			}
			return null;
		}

		public static byte[] GetResource(Assembly asm, string ResourceName)
		{
			System.IO.Stream stream = asm.GetManifestResourceStream(ResourceName);
			byte[] data = new byte[stream.Length];
			stream.Read(data, 0, (int)stream.Length);
			return data;
		}
	}
}