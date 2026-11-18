# Changelog

Todas as mudanças relevantes deste projeto ficam registradas aqui.
O formato segue o [Keep a Changelog](https://keepachangelog.com/pt-BR/1.1.0/) e o projeto usa [Versionamento Semântico](https://semver.org/lang/pt-BR/).

## [1.0.0] - 2026-11-18

Primeira versão entregue na Global Solution 2026.2.

### Adicionado
- Domínio do AlertaEncosta com encapsulamento e validações: `Setor`, `LeituraSensor`, `PontoApoio`, `Morador`, `Alerta`, `Ocorrencia`.
- Cálculo de risco com os mesmos limiares da estação ESP32, regra combinada (chuva + solo saturado) e motivos em texto simples.
- Histerese "sobe na hora, desce devagar" e análise de tendência por setor.
- Emissão e escalonamento de alertas com escolha automática do ponto de apoio (vagas, acessibilidade e distância de Haversine).
- Fila de busca ativa priorizando pedidos de ajuda e pessoas vulneráveis.
- Indicadores de impacto: % de moradores em local seguro (meta 80%) e antecedência média do alerta (meta ≥ 2 h).
- Importação de leituras no contrato JSON da estação (objeto, array, NDJSON ou linhas do Serial Monitor do Wokwi) e busca na API REST.
- Persistência em JSON com gravação atômica e dados de exemplo (simulados).
- Painel Windows Forms com seis abas, faixa de "alerta recomendado", controles desenhados com GDI+ e modo demonstração.
- 49 testes de unidade (xUnit) para domínio, serviços, integração e persistência.
- README com a Ficha da CitizenTech, instruções de execução e evidências.

## [0.1.0] - 2026-11-18

### Adicionado
- Estrutura inicial da solução (`AlertaEncosta.Core` e `AlertaEncosta.Core.Tests`), README com a Ficha da CitizenTech e lista de integrantes.

[1.0.0]: ../../compare/v0.1.0...v1.0.0
[0.1.0]: ../../releases/tag/v0.1.0
