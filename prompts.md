# prompts.md · Uso de IA como copiloto

A GS permite IA para aprender e revisar, desde que registrada e que o grupo saiba explicar cada linha. Esta parte (C# Software Development) usou um assistente de IA (Claude, via Hermes) para acelerar a montagem; o grupo revisou, testou e deve saber explicar o resultado.

| # | Pedido (resumo) | O que foi gerado | O que foi conferido por humanos |
|---|---|---|---|
| 1 | "Fazer a parte de C# da Global Solution" a partir do enunciado da GS, do PDF de arquitetura TOGAF do AlertaEncosta e do repositório da estação ESP32 | Proposta do Painel da Defesa Civil (Windows Forms), domínio, serviços, persistência JSON, telas e testes | Coerência com o componente "Painel Defesa Civil (C# Windows Forms)" da camada de Aplicação e com os critérios da disciplina (lógica, POO, interface) |
| 2 | Reaproveitar o contrato JSON e os limiares de risco do firmware ESP32 | `ImportadorLeituras`, `LimiaresRisco` e importação das linhas do Serial Monitor do Wokwi | Mesmo formato e mesmos limiares do `config.h` |
| 3 | Verificar build, testes e telas | Build sem avisos, 49 testes xUnit e capturas geradas pelo próprio executável (`--capturar-telas`) | Execução do painel e do roteiro de demonstração |

> Os limiares de risco e todos os dados (moradores, telefones, abrigos, leituras) são **simulados** para a demonstração. Nenhum dado pessoal real é usado.
