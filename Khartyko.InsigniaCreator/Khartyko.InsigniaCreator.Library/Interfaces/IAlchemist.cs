namespace Khartyko.InsigniaCreator.Library.Interfaces;

public interface IAlchemist
{
}

public interface IAlchemist<in TInput, out TResult> : IAlchemist
{
    TResult Transmute(TInput input);
}