using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace site.Infrastructure
{
	public class CodeGenerator
	{
		private static readonly IEnumerable<char> AvailableCharacters;
		private static byte[] _privateKey;

		static CodeGenerator()
		{
			AvailableCharacters = Enumerable.Range(65, 26).ToCharArray()
							   .Union(
								   Enumerable.Range(97, 26).ToCharArray())
							   .Union(
								   Enumerable.Range(48, 10).ToCharArray());
		}

		public CodeGenerator(byte[] privateKey)
		{
			_privateKey = privateKey;
		}

		public string Generate(int maximumCharacters = 6)
		{
			var random = new Random((int)(DateTime.Now.Millisecond * DateTime.Now.Ticks));

			char[] result = new char[maximumCharacters];

            for (int index = 0; index < maximumCharacters; index++)
			{
                char characterToFetch;

                do
				{
					characterToFetch = AvailableCharacters.ElementAt(random.Next(0, AvailableCharacters.Count()));
				} while (result.Contains(characterToFetch));

				result[index] = characterToFetch;
			}

			return new string(result);
		}

		public string GenerateExitCode(GameCompleteStatus status)
		{
			byte[] publicKey = Encoding.UTF8.GetBytes(status.EntryCode);
			byte[] data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(status));

			string result = string.Empty;

			using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
			{
				aes.Key = publicKey;
				aes.IV = _privateKey;

				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
					{
						cs.Write(data, 0, data.Length);
						cs.FlushFinalBlock();
					}

					result = Convert.ToBase64String(ms.ToArray());
				}
			}

			return result;
		}

		public GameCompleteStatus DecodeExitCode(string entryCode, string code)
		{
			byte[] publicKey = Encoding.UTF8.GetBytes(entryCode);
			GameCompleteStatus result;

			using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
			{
				aes.Key = publicKey;
				aes.IV = _privateKey;

				byte[] fromBase64ToBytes = Convert.FromBase64String(code);
				
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

				byte[] decryptedBytes = decryptor.TransformFinalBlock(fromBase64ToBytes, 0, fromBase64ToBytes.Length);

				result = JsonConvert.DeserializeObject<GameCompleteStatus>(Encoding.UTF8.GetString(decryptedBytes));
			}

			return result;
		}
	}
}