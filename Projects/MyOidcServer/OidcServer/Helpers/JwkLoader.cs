using Microsoft.IdentityModel.Tokens;

namespace OidcServer.Helpers
{
    public class JwkLoader
    {
        private static readonly string DefaultFile = Path.Combine("OidcDiscovery", "default-jwk.json");
        private static JsonWebKey? defaultJwk = null;
        private static SpinLock spinLock = new SpinLock();

        public static JsonWebKey LoadFromFile(string file)
        {
            var fi = new FileInfo(file);
            if (fi.Exists)
            {
                using var reader = fi.OpenText();
                var json = reader.ReadToEnd();

                return new JsonWebKey(json);
            }
            else
            {
                throw new FileNotFoundException(file);
            }
        }

        public static JsonWebKey LoadFromDefault()
        {
            bool lockTaken = false;

            spinLock.Enter(ref lockTaken);
            if (lockTaken)
            {
                defaultJwk ??= LoadFromFile(DefaultFile);
                spinLock.Exit();
            }
            else
            {
                throw new InvalidOperationException(); // this will never happen I hope
            }

            return defaultJwk;
        }
    }
}
