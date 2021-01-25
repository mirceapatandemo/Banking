using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Banking.Api.OAuth2
{
    public class OAuth2Client : IOAuth2Client
    {
        public string RequestAccessToken()
        {           
            string payloadDigest = CalculatePayloadDigest();
            string formattedDate = FormatCurrentDateToRfc7231();

            string signingString = GetSigninString(formattedDate, payloadDigest);
            string signature = Sign(signingString);

            return null;
        }

        /// <returns>
        ///     payload = "grant_type=client_credentials"
        ///     `echo -n "$payload" | openssl dgst -binary -sha256 | openssl base64`        
        /// </returns>
        private string CalculatePayloadDigest()
        {            
            const string clientCredentials = "client_credentials";
            var payload = $"grant_type={clientCredentials}";

            var encoding = new ASCIIEncoding();
            var payloadBytes = encoding.GetBytes(payload);
            using (var sha256 = new SHA256Managed())
            {
                var hash = sha256.ComputeHash(payloadBytes);
                return Convert.ToBase64String(hash);
            }
        }

        /// <returns>
        ///     $(LC_TIME=en_US.UTF-8 date -u "+%a, %d %b %Y %H:%M:%S GMT") ,see RFC7231
        /// </returns>        
        private string FormatCurrentDateToRfc7231() =>
            $"{ DateTime.UtcNow:ddd, dd MMM yyyy HH:mm:ss} GMT";

        /// <returns>        
        ///     (request-target): $httpMethod $reqPath
        ///     date: $reqDate
        ///     digest: $digest"        
        /// </returns>
        private string GetSigninString(string formattedDate, string payloadDigest)
        {
            const string httpMethod = "post";
            const string reqPath = "/oauth2/token";

            return $"(request-target): {httpMethod} {reqPath}\ndate:{formattedDate}\ndigest: SHA-256={payloadDigest}";
        }

        private string Sign(string signingString)
        {
            var signingKeyFileName = GetSigningKeyFileName();
            string privateKey = File.ReadAllText(signingKeyFileName);

            //TODO: Convert PKCS#1 PEM RSA private key for RSACrypto service
            // privateKey

            RSAParameters rsap = new RSAParameters { Modulus = Encoding.ASCII.GetBytes(privateKey) };
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsap);
            byte[] encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(signingString), false);
            string base64Encrypted = Convert.ToBase64String(encryptedData);

            return base64Encrypted;
        }

        private string GetSigningKeyFileName()
        {
            const string signingKey = "example_eidas_client_signing.key";

            var currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string certsPath = Path.Combine(currentDir, "OAuth2", "Certs");

            return Path.Combine(certsPath, signingKey);
        }
    }
}
