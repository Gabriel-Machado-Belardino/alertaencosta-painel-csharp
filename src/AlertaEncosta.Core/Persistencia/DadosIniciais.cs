using AlertaEncosta.Core.Dominio;
using AlertaEncosta.Core.Servicos;

namespace AlertaEncosta.Core.Persistencia;

/// <summary>
/// Dados de exemplo para a primeira execução. TODOS SIMULADOS (regra da GS e LGPD):
/// nomes e telefones são fictícios; setores e pontos de apoio são aproximações didáticas
/// na Vila Brasilândia (zona norte de São Paulo).
/// </summary>
public static class DadosIniciais
{
    private static readonly string[] Nomes =
    {
        "Maria Aparecida", "José Carlos", "Ana Paula", "Antônio", "Francisca", "João Pedro",
        "Luzia", "Paulo Henrique", "Rosângela", "Sebastião", "Juliana", "Raimundo",
        "Benedita", "Marcos Vinícius", "Terezinha", "Edson", "Cláudia", "Severino",
        "Patrícia", "Valdir", "Joana", "Wellington", "Conceição", "Diego"
    };

    private static readonly string[] Sobrenomes =
    {
        "da Silva", "dos Santos", "Oliveira", "Souza", "Pereira", "Lima", "Ferreira", "Almeida"
    };

    private static readonly string[] Ruas =
    {
        "Rua da Encosta", "Viela do Córrego", "Travessa das Flores", "Rua do Morro", "Escadaria Azul", "Rua da Nascente"
    };

    public static void Popular(ContextoDados dados, DateTime agora, int semente = 2026)
    {
        if (!dados.Vazio) return;
        var aleatorio = new Random(semente);

        var setores = new[]
        {
            new Setor("BRAS-S01", "Córrego do Bananal", "Vila Brasilândia", "EST-BRAS-01", new Coordenada(-23.4470, -46.6950)),
            new Setor("BRAS-S02", "Encosta Jardim Damasceno", "Vila Brasilândia", "EST-BRAS-02", new Coordenada(-23.4420, -46.6800)),
            new Setor("BRAS-S03", "Vila Itaberaba (várzea)", "Vila Brasilândia", "EST-BRAS-03", new Coordenada(-23.4600, -46.6880)),
            new Setor("BRAS-S04", "Morro do Elisa Maria", "Vila Brasilândia", "EST-BRAS-04", new Coordenada(-23.4530, -46.7080)),
        };
        foreach (var s in setores) dados.Setores.Adicionar(s);

        dados.PontosApoio.Adicionar(new PontoApoio("PA-01", "Escola Municipal Bananal (simulada)", "Rua Exemplo, 100 - Vila Brasilândia",
            new Coordenada(-23.4500, -46.6920), 120, acessivel: true));
        dados.PontosApoio.Adicionar(new PontoApoio("PA-02", "Paróquia do Damasceno (simulada)", "Rua Exemplo, 250 - Jardim Damasceno",
            new Coordenada(-23.4440, -46.6830), 80, acessivel: false));
        dados.PontosApoio.Adicionar(new PontoApoio("PA-03", "CEU Brasilândia (simulado)", "Av. Exemplo, 1000 - Vila Brasilândia",
            new Coordenada(-23.4580, -46.6950), 200, acessivel: true));
        dados.PontosApoio.Adicionar(new PontoApoio("PA-04", "Associação de Moradores Elisa Maria (simulada)", "Rua Exemplo, 45 - Jardim Elisa Maria",
            new Coordenada(-23.4515, -46.7040), 60, acessivel: true));

        // 6 moradores por setor; cerca de 1 em cada 3 com condição especial.
        var condicoes = new[]
        {
            CondicaoEspecial.Nenhuma, CondicaoEspecial.Idoso, CondicaoEspecial.Nenhuma,
            CondicaoEspecial.MobilidadeReduzida, CondicaoEspecial.Nenhuma, CondicaoEspecial.Nenhuma,
            CondicaoEspecial.Gestante, CondicaoEspecial.Nenhuma, CondicaoEspecial.Acamado,
            CondicaoEspecial.Nenhuma, CondicaoEspecial.DeficienciaVisual, CondicaoEspecial.Nenhuma
        };
        for (var i = 0; i < Nomes.Length; i++)
        {
            var setor = setores[i % setores.Length];
            var nome = $"{Nomes[i]} {Sobrenomes[(i * 3) % Sobrenomes.Length]}";
            var endereco = $"{Ruas[i % Ruas.Length]}, {10 + i * 7} - {setor.Nome}";
            var telefone = $"11 9{aleatorio.Next(1000, 9999)}-{aleatorio.Next(1000, 9999)}";
            dados.Moradores.Adicionar(new Morador($"MOR-{i + 1:000}", nome, setor.Id, endereco, telefone, condicoes[i % condicoes.Length]));
        }

        // Histórico das últimas 2 horas, uma leitura a cada 10 minutos.
        var calculadora = new CalculadoraRisco();
        var monitoramento = new ServicoMonitoramento(dados, calculadora, () => agora);
        var simulador = new SimuladorLeituras(aleatorio);
        var cenarios = new[] { CenarioChuva.ChuvaModerada, CenarioChuva.Estiagem, CenarioChuva.Estiagem, CenarioChuva.Estiagem };

        for (var s = 0; s < setores.Length; s++)
        {
            LeituraSensor? anterior = null;
            for (var passo = 12; passo >= 1; passo--)
            {
                var cenario = passo <= 4 ? cenarios[s] : CenarioChuva.Estiagem;
                anterior = simulador.Gerar(setores[s], anterior, cenario, agora.AddMinutes(-10 * passo));
                monitoramento.Registrar(anterior, salvar: false);
            }
        }

        // Evento de ontem na várzea (BRAS-S03): alerta emitido às -28 h, pico do córrego às -25,5 h,
        // alerta encerrado às -22 h. Dá base real para o indicador "antecedência média do alerta".
        RegistrarEventoDeOntem(dados, monitoramento, setores[2], agora);

        dados.Ocorrencias.Adicionar(new Ocorrencia("OCO-0001", "BRAS-S02", TipoOcorrencia.Trinca, CanalOcorrencia.AppMorador,
            "Rua do Morro, 58 (fundos)", "Trinca nova na parede dos fundos depois da chuva de ontem, morador enviou foto.", false, agora.AddHours(-5)));
        var vistoriada = new Ocorrencia("OCO-0002", "BRAS-S03", TipoOcorrencia.Alagamento, CanalOcorrencia.LiderComunitario,
            "Viela do Córrego, altura do nº 20", "Água entrando nas casas da viela, nível na canela.", true, agora.AddHours(-26));
        vistoriada.IniciarAtendimento("Equipe Norte 2", agora.AddHours(-25.5));
        vistoriada.Resolver("Bueiro desobstruído pela subprefeitura; água baixou.", agora.AddHours(-23));
        dados.Ocorrencias.Adicionar(vistoriada);

        dados.SalvarTudo();
    }

    private static void RegistrarEventoDeOntem(ContextoDados dados, ServicoMonitoramento monitoramento, Setor setor, DateTime agora)
    {
        // (horas atrás, chuva na janela mm, solo %, nível do córrego cm)
        var serie = new (double Horas, double Chuva, int Solo, double Nivel)[]
        {
            (30, 2, 40, 15), (29, 8, 50, 30), (28, 13, 62, 55), (27, 19, 72, 90), (26.5, 26, 84, 125),
            (26, 31, 90, 150), (25.5, 24, 92, 162), (25, 15, 90, 140), (24, 6, 85, 95), (23, 2, 78, 60), (22, 0, 70, 35)
        };
        var total = 0.0;
        foreach (var (horas, chuva, solo, nivel) in serie)
        {
            total += chuva;
            monitoramento.Registrar(new LeituraSensor(setor.EstacaoId, setor.Id, agora.AddHours(-horas),
                chuva, total, solo, nivel, OrigemLeitura.EstacaoEsp32), salvar: false);
        }

        var moradores = dados.Moradores.Listar().Count(m => m.SetorId == setor.Id);
        var opcao = new SeletorPontoApoio().Selecionar(setor.Centro, dados.PontosApoio.Listar(), moradores, precisaAcessivel: true);
        var alerta = new Alerta("ALT-0001", setor.Id, NivelRisco.Medio,
            ServicoAlertas.MontarMensagem(setor, NivelRisco.Medio, opcao), opcao?.Ponto.Id, moradores,
            "defesa.civil", agora.AddHours(-28).AddMinutes(2));
        alerta.Escalar(NivelRisco.Alto, ServicoAlertas.MontarMensagem(setor, NivelRisco.Alto, opcao), opcao?.Ponto.Id,
            moradores, agora.AddHours(-26.5));
        alerta.Encerrar(agora.AddHours(-22));
        dados.Alertas.Adicionar(alerta);
    }
}
