using Azure.Storage.Blobs;

namespace E_commerceOnlineStore.Azure
{
    /// <summary>
    /// Provides methods for interacting with Azure Blob Storage.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="BlobStorageService"/> class.
    /// </remarks>
    /// <param name="blobServiceClient">The <see cref="BlobServiceClient"/> instance to interact with Azure Blob Storage.</param>
    /// <param name="configuration">The configuration instance to retrieve settings from.</param>
    public class BlobStorageService(BlobServiceClient blobServiceClient, IConfiguration configuration) : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient = blobServiceClient ?? throw new ArgumentNullException(nameof(blobServiceClient));
        private readonly string _containerName = configuration["BlobContainerName"] ?? throw new ArgumentNullException(nameof(configuration));

        /// <summary>
        /// Uploads a blob to Azure Blob Storage.
        /// </summary>
        /// <param name="content">The content stream of the file to upload.</param>
        /// <param name="fileName">The name of the file to upload.</param>
        /// <returns>The URL of the uploaded blob.</returns>
        public async Task<string> UploadBlobAsync(Stream content, string fileName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.UploadAsync(content, true);
            return blobClient.Uri.ToString();
        }
    }
}
