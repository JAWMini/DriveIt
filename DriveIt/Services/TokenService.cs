using Microsoft.AspNetCore.DataProtection;

namespace DriveIt.Services
{
    public class TokenService
    {
        private readonly IDataProtector _protector;

        public TokenService(IDataProtectionProvider dataProtectionProvider)
        {
            _protector = dataProtectionProvider.CreateProtector("CarRentalOfferProtector");
        }

        public string GenerateToken(string offerId, int offerExpirationInMinutes)
        {
            var data = $"{offerId}|{DateTime.UtcNow}|{offerExpirationInMinutes}";
            var token = _protector.Protect(data);
            return token;
        }

        public bool ValidateToken(string token, out string offerId)
        {
            offerId = string.Empty;
            try
            {
                var data = _protector.Unprotect(token);
                var parts = data.Split('|');
                offerId = parts[0];
                var timeStamp = DateTime.Parse(parts[1]);
                var offerExpirationInMinutes = int.Parse(parts[2]);

                if ((DateTime.UtcNow - timeStamp).TotalMinutes > offerExpirationInMinutes)
                {
                    // Token has expired
                    return false;
                }
                return true;
            }
            catch
            {
                // Token is invalid or has expired
                return false;
            }
        }
    }
}
