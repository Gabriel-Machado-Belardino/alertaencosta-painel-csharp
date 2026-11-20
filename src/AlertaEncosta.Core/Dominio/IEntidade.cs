namespace AlertaEncosta.Core.Dominio;

/// <summary>Toda entidade persistida tem um identificador único em texto (ex.: "BRAS-S01", "ALT-0003").</summary>
public interface IEntidade
{
    string Id { get; }
}
