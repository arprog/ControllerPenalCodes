using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace ControllerPenalCodes.Utils
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
    }
}
