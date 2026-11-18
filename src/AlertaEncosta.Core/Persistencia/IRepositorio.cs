using AlertaEncosta.Core.Dominio;

namespace AlertaEncosta.Core.Persistencia;

/// <summary>Contrato de persistência. As telas e os serviços só conhecem esta interface.</summary>
public interface IRepositorio<T> where T : class, IEntidade
{
    int Quantidade { get; }
    IReadOnlyList<T> Listar();
    T? ObterPorId(string id);
    bool Existe(string id);
    void Adicionar(T entidade);
    void Remover(string id);
    void Salvar();
}

/// <summary>
/// Repositório em memória (usado nos testes). Guarda os objetos numa lista e
/// garante que o Id seja único (sem diferenciar maiúsculas).
/// </summary>
public class RepositorioMemoria<T> : IRepositorio<T> where T : class, IEntidade
{
    protected readonly List<T> Itens = new();
    protected readonly object Trava = new();

    public int Quantidade { get { lock (Trava) return Itens.Count; } }

    public IReadOnlyList<T> Listar()
    {
        lock (Trava) return Itens.ToList();
    }

    public T? ObterPorId(string id)
    {
        if (string.IsNullOrWhiteSpace(id)) return null;
        lock (Trava) return Itens.FirstOrDefault(i => string.Equals(i.Id, id, StringComparison.OrdinalIgnoreCase));
    }

    public bool Existe(string id) => ObterPorId(id) is not null;

    public void Adicionar(T entidade)
    {
        ArgumentNullException.ThrowIfNull(entidade);
        lock (Trava)
        {
            if (Itens.Any(i => string.Equals(i.Id, entidade.Id, StringComparison.OrdinalIgnoreCase)))
                throw new DominioException($"Já existe um registro com o código {entidade.Id}.");
            Itens.Add(entidade);
        }
    }

    public void Remover(string id)
    {
        lock (Trava)
        {
            var removidos = Itens.RemoveAll(i => string.Equals(i.Id, id, StringComparison.OrdinalIgnoreCase));
            if (removidos == 0) throw new DominioException($"Registro {id} não encontrado.");
        }
    }

    /// <summary>Em memória não há o que gravar. O repositório JSON sobrescreve este método.</summary>
    public virtual void Salvar() { }
}
