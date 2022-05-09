using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace ControllerPenalCodes.Shared
{
	public class Authentication
	{
		public SecurityKey Key { get; }

		public SigningCredentials SigningCredentials { get; }

        public Authentication()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }

        public static string GetUserId(IEnumerable<Claim> claimList)
        {
            if (claimList == null || claimList.Count() == 0)
                return null;

            return claimList
                .FirstOrDefault(i => !string.IsNullOrEmpty(i.Type)
                    && i.Type.Contains("nameidentifier"))?.Value;
		}
    }
}
