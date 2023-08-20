namespace Khartyko.InsigniaCreator.Library.Interfaces;

public interface IConfiguration
{
    void Load(string filepath = "");
    void Save(string filepath = "");

    void Register<T>(IAlchemist<string, T> conversionAlchemist);
    void Register<T>(IAlchemist<T, string> conversionAlchemist);
    
    T Get<T>(string name);
    void Set<T>(string name);
    
    object this[string name] { get; set; }
}