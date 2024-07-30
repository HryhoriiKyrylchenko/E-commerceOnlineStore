namespace E_commerceOnlineStore.Azure
{
    /// <summary>
    /// Defines methods for interacting with Azure Blob Storage.
    /// </summary>
    public interface IBlobStorageService
    {
        /// <summary>
        /// Uploads a blob to Azure Blob Storage.
        /// </summary>
        /// <param name="content">The content stream of the file to upload.</param>
        /// <param name="fileName">The name of the file to upload.</param>
        /// <returns>The URL of the uploaded blob.</returns>
        Task<string> UploadBlobAsync(Stream content, string fileName);
    }
}
