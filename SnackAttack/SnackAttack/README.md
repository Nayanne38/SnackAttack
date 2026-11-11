# Snack Attack (estilo Pou) – protótipo em C#

Jogo em **Windows Forms (.NET 8)**: o personagem anda de um lado para o outro e pega as comidas que caem.
Alimentos saudáveis dão pontos e vida. Fast foods tiram vida e enchem a barra de estômago.
Em cada fase, pegue o item dourado (Semente, Manual, Água), responda o quiz e avance.
No fim, volte à cidade e cultive a cenoura.

## Como rodar (Windows)
```
dotnet run
```
Precisa do .NET 8 SDK: https://dotnet.microsoft.com/download

Controles: setas ou A/D para mover, P para pausar, 1/2/3 no quiz, Enter para confirmar.

## O que falta (pouca coisa!) – procure por `TODO`

| Grupo | Arquivos | Tarefas |
|---|---|---|
| **1 – Jogador e Itens** | `Personagens/Jogador.cs`, `Objetos/Item.cs`, `Alimento.cs`, `ItemChave.cs`, `Debuff.cs`, `Fabrica.cs` | 2 alimentos novos na `Fabrica`; dar efeito real à "Visão aprimorada" em `AdicionarHabilidade` |
| **2 – Inimigos** | `Objetos/Inimigo.cs`, `FastFood.cs`, `Personagens/Boss.cs`, `Fabrica.cs` | 2 fast foods novos na `Fabrica`; ataque especial do `Boss` |
| **3 – Jogo e Telas** | `Jogo.cs`, `Fase.cs`, `Pergunta.cs`, `Tela.cs` | mais perguntas e textos das fases; instruções completas e telas mais bonitas |

## Regras
- Não mudem nomes de métodos que outros grupos usam (`AoColetar`, `Atualizar`, `Fabrica.Criar...`).
- Trabalhem em branches (`grupo1`, `grupo2`, `grupo3`) e uma pessoa faz os merges.
- A regra do jogo fica em `Jogo.cs`. A `Tela.cs` só desenha.

## Herança
```
ObjetoQueCai ── Item ── Alimento / ItemChave / Debuff
             └─ Inimigo ── FastFood
Jogador, Boss, Fabrica, Fase, Pergunta, Jogo, Tela
```
