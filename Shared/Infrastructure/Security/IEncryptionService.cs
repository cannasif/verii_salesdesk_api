namespace salesdesk_api.Shared.Infrastructure.Security;

public interface IEncryptionService
{
    string Encrypt(string plain);
    string Decrypt(string cipher);
}
