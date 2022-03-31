using System.Linq;
using System.Reflection;
using UnityEngine;

namespace YeetDab.Utilities
{
	public static class SpriteUtils
	{
		public const string resourceRoot = "YeetDab.Resources.";
		public static Sprite LoadSpriteFromResources(string fileName, float PixelsPerUnit = 100.0f)
		{
			string resourcePath = resourceRoot + fileName;
			byte[] image = GetResource(Assembly.GetCallingAssembly(), resourcePath);
			if (image.Count() <= 0)
				return null;

			Texture2D texture = GetTexture(image);
			if (texture == null)
				return null;

			return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), PixelsPerUnit);
		}

		public static byte[] GetResource(Assembly asm, string ResourceName)
		{
			System.IO.Stream stream = asm.GetManifestResourceStream(ResourceName);
			byte[] data = new byte[stream.Length];
			stream.Read(data, 0, (int)stream.Length);
			return data;
		}

		public static Texture2D GetTexture(byte[] image)
		{
			Texture2D texture = new Texture2D(2, 2);
			ImageConversion.LoadImage(texture, image);
			return texture;
		}
	}
}
