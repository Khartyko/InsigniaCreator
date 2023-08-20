namespace Khartyko.InsigniaCreator.Library.Interfaces;

public interface IJournal
{
}

public interface IJournal<in T> : IJournal
{
    void Log(T item, bool newline = true);
}