using System;
using Newtonsoft.Json;

namespace NoteTaker
{
	public static class Extensions
	{
		public static T Clone<T>(this T toBeCloned)
		{
			var cloneString = JsonConvert.SerializeObject(toBeCloned);
			return JsonConvert.DeserializeObject<T>(cloneString);
		}
	}
}
