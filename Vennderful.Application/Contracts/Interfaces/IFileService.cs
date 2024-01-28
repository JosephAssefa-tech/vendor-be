using System.IO;

namespace Vennderful.Application.Contracts.Interfaces
{
    public interface IFileService
    {
        string GetMimeTypeForFileExtension(string filePath);
    }
}
