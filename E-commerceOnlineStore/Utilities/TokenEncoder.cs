using System.Text;

namespace E_commerceOnlineStore.Utilities
{
    /// <summary>
    /// Provides methods to encode and decode tokens using a URL-safe Base64 encoding scheme.
    /// </summary>
    public class TokenEncoder
    {
        /// <summary>
        /// Encodes a given token string into a URL-safe Base64 encoded string.
        /// </summary>
        /// <param name="token">The token string to be encoded.</param>
        /// <returns>A URL-safe Base64 encoded string representation of the token.</returns>
        public static string EncodeToken(string token)
        {
            // Convert the token into a byte array using UTF-8 encoding.
            var tokenBytes = Encoding.UTF8.GetBytes(token);

            // Convert the byte array into a Base64 encoded string.
            var base64String = Convert.ToBase64String(tokenBytes);

            // Make the Base64 string URL-safe by replacing certain characters.
            var urlSafeBase64String = base64String.Replace('+', '-').Replace('/', '_').Replace("=", "");

            // Return the URL-safe Base64 encoded string.
            return urlSafeBase64String;
        }

        /// <summary>
        /// Decodes a URL-safe Base64 encoded token string back into its original form.
        /// </summary>
        /// <param name="encodedToken">The URL-safe Base64 encoded token string to be decoded.</param>
        /// <returns>The original token string.</returns>
        public static string DecodeToken(string encodedToken)
        {
            // Replace URL-safe characters back to their original Base64 characters.
            var base64String = encodedToken.Replace('-', '+').Replace('_', '/');

            // Pad the Base64 string with '=' characters if necessary for valid decoding.
            switch (base64String.Length % 4)
            {
                case 2: base64String += "=="; break;
                case 3: base64String += "="; break;
            }

            // Convert the Base64 string back into a byte array.
            var tokenBytes = Convert.FromBase64String(base64String);

            // Convert the byte array back into the original string using UTF-8 encoding.
            return Encoding.UTF8.GetString(tokenBytes);
        }
    }

}
