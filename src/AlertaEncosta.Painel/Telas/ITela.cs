namespace AlertaEncosta.Painel.Telas;

/// <summary>Contrato das abas do painel: recebem o contexto uma vez e se atualizam quando os dados mudam.</summary>
public interface ITela
{
    void Inicializar(PainelContexto contexto);
    void Atualizar();
}
