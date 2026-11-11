# Snack Attack – Protótipo (Etapa 3)

Jogo educacional em **C# (.NET 8, console)**. Este projeto é a **base**: ele já compila e roda do menu até a vitória,
com versões mínimas de tudo. Cada grupo deve **melhorar e completar** as suas classes, procurando os comentários `TODO`.

## Como rodar
```
dotnet run
```
(precisa do .NET 8 SDK instalado: https://dotnet.microsoft.com/download)

## Quem mexe em quê

| Grupo | Arquivos | Procurar por |
|---|---|---|
| **1 – Jogador e Itens** | `Personagens/Jogador.cs`, `Itens/Item.cs`, `Itens/Espada.cs`, `Itens/Alimento.cs`, `Itens/ItensFactory.cs` | `TODO (Grupo 1` |
| **2 – Inimigos e Combate** | `Personagens/Inimigo.cs`, `Personagens/FastFood.cs`, `Personagens/Boss.cs`, `Combate.cs` | `TODO (Grupo 2` |
| **3 – Mundo, NPCs e Menu** | `Program.cs`, `Menu.cs`, `Jogo.cs`, `Fase.cs`, `Pergunta.cs`, `Npcs/*` | `TODO (Grupo 3` |

Dentro de cada grupo, a "Função 1/2/3" do TODO indica qual pessoa cuida daquele trecho.

Para achar todas as tarefas do seu grupo no VS Code: `Ctrl+Shift+F` e pesquise `TODO (Grupo 1`.

## Regras de convivência (para não quebrar o código dos outros)

1. **Não mudem o nome nem os parâmetros** de métodos que outro grupo usa. Se precisar mudar, avisem no grupo.
   Pode **adicionar** métodos e atributos novos à vontade.
2. Trabalhem em **branches**: `grupo1`, `grupo2`, `grupo3`. Façam commits pequenos.
3. Só mexam nos arquivos do seu grupo. Se precisar mexer em arquivo de outro grupo, combinem antes.
4. Antes do merge final, rodem `dotnet build` e joguem do menu até o fim.
5. Uma pessoa só faz os merges na `main`.

## Mapa das classes (herança)

```
Item ── Espada
     └─ Alimento

Inimigo (abstract) ── FastFood
                   └─ Boss

Npc ── NpcPrincipal
    ├─ NpcSabio
    └─ NpcVendedor

Jogador, Combate, Fase, Pergunta, Menu, Jogo
```

## Como os grupos se conectam

- `Jogo` (G3) cria o `Jogador` (G1), monta as `Fase`s e chama `Fase.Executar()`.
- `Fase` (G3) usa `Jogador.ColetarItem()` (G1) e `Combate.Iniciar()` (G2).
- `Combate` (G2) usa `Jogador.Atacar()` (G1) e `Inimigo.Atacar()` (G2).
- No fim, `Jogo` usa `Alimento.Cultivar()` (G1) e decide vitória/derrota.

## Requisitos da Etapa 3 (checklist)

- [x] Pelo menos 3 classes próprias
- [x] Instanciação de objetos (`new` em `Jogo.CriarMissao1`, `FastFood`, `ItensFactory`...)
- [x] Atributos e métodos
- [x] Sistema de pontuação (`Jogador.Pontos`)
- [x] Menu simples (`Menu`)
- [x] Condição de vitória ou derrota (`Jogo.FimDeJogo`, `Combate.VerificarDerrota`)
