namespace AniMedia.Application.Common.Interfaces;

public interface IDirectoryService {

    /// <summary>
    /// Директория приложения
    /// </summary>
    /// <returns>Абсолютный путь к папке приложения</returns>
    public string GetBaseDirectory();

    /// <summary>
    /// Директория бинарных файлов
    /// </summary>
    /// <returns>Абсолютный путь к папке бинарных файлов</returns>
    public string GetBinaryFilesDirectory();

    /// <summary>
    /// Создание пути до несуществующего файла в папке бинарных файлов
    /// </summary>
    /// <returns>Абсолютный путь к несуществующему файлу</returns>
    public string GetNewRandomPathBinaryFile(string extension);
}