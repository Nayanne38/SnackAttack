# Snack Attack (estilo Pou) – protótipo em C#

Jogo em **Windows Forms (.NET 8)**: o personagem anda e pula pegando as comidas que caem.
Alimentos saudáveis dão pontos e vida. Fast foods tiram vida e enchem a barra de estômago.
Em cada fase, pegue o item dourado (Semente, Manual, Água), responda o quiz e avance.
No fim, volte à cidade e cultive a cenoura.

## Como rodar

Precisa do **Windows** e do **.NET 8 SDK**: https://dotnet.microsoft.com/download

```
dotnet run
```

> O jogo usa Windows Forms: **não compila nem abre no Codespaces** (Linux). Lá dá só para editar o código.

Controles: setas ou A/D mover, **Espaço** pular, P pausar, 1/2/3 no quiz, Enter confirma.
Menu: 1 novo jogo, 2 instruções, 3 configurações (som), Esc sair.

## Já está pronto (não precisa fazer)

- Menu, instruções completas e configurações (som ligado/desligado)
- Mover e pular, pausa, vida, estômago, pontos, itens e habilidades na tela
- 3 fases com item da missão, quiz com **perguntas sorteadas** e Boss na fase 3
- **Ataque especial do Boss**: chuva de 5 fast foods a cada 10 s, com aviso "!"
- **Visão aprimorada** deixa o personagem mais largo (vale no próximo jogo)
- Vitória e derrota

## O que falta (pouca coisa!)

Cada grupo só edita o seu arquivo e **procura por `TODO`** (Ctrl + F).

| Grupo | Arquivo | Tarefa |
|---|---|---|
| **1** | `Fabrica.cs` | Adicionar **2 alimentos saudáveis** |
| **2** | `Fabrica.cs` | Adicionar **2 fast foods** |
| **3** | `Jogo.cs` | Escrever **mais 2 perguntas por fase** (ficar com 4) e ajustar textos e dificuldade |

---

## GRUPO 1 – 2 alimentos novos

📍 **ONDE:** `Fabrica.cs` → lista `_alimentos`.

❌ **APAGUE** só esta linha:

```csharp
// TODO (Grupo 1): adicionar mais 2 alimentos saudáveis (ex.: Banana, Uva)
```

✅ **COLE** no lugar:

```csharp
//   nome     cor        forma           pontos  vida
("Banana", "#F7D340", Forma.Circulo,    10,     3),
("Uva",    "#7B3FA0", Forma.Circulo,    12,     4),
```

💡 Cada linha é um alimento: "pontos" é o que o jogador ganha e "vida" é quanto cura. O jogo sorteia sozinho entre todos da lista.

⚠️ Toda linha termina com **vírgula**.

🧪 **TESTE:** jogar a Fase 1 e ver Banana e Uva caindo.

- [ ] Banana e Uva aparecem · [ ] Jogo abre sem erro · [ ] Enviado no branch `grupo1`

---

## GRUPO 2 – 2 fast foods novos

📍 **ONDE:** `Fabrica.cs` → lista `_fastFoods` (logo abaixo da de alimentos).

❌ **APAGUE** só esta linha:

```csharp
// TODO (Grupo 2): adicionar mais 2 fast foods (ex.: Pizza, Donut)
```

✅ **COLE** no lugar:

```csharp
//   nome     cor        forma             dano  estômago
("Pizza", "#E4572E", Forma.Triangulo,      9,    13),
("Donut", "#F4A6C0", Forma.Circulo,        7,    12),
```

💡 "dano" é a vida que o jogador perde e "estômago" é quanto a barra enche.

⚠️ Toda linha termina com **vírgula**. O Grupo 1 mexe na lista de cima: não mexam na lista um do outro.

🧪 **TESTE:** jogar e ver Pizza e Donut tirando vida. Na Fase 3 eles também entram na chuva do Boss.

- [ ] Pizza e Donut aparecem · [ ] Jogo abre sem erro · [ ] Enviado no branch `grupo2`

---

## GRUPO 3 – Mais perguntas e textos

📍 **ONDE:** `Jogo.cs` → método `CriarFases`. Cada fase já tem 2 perguntas.

✅ **FAÇA:** dentro de cada `new List<Pergunta> { ... }`, **acrescente mais 2 perguntas** (ficar com 4 por fase). Modelo:

```csharp
//             pergunta                       alternativas                                      certa
new Pergunta("Qual vitamina a laranja tem?", new[] { "Vitamina B12", "Vitamina K", "Vitamina C" }, 2),
```

💡 O último número é **qual é a certa, começando do ZERO** (0 = primeira, 1 = segunda, 2 = terceira).

**Regras**

- Sempre **3 alternativas** (a tela só comporta 3)
- **Variem a posição da certa** (0, 1 e 2)
- Cada pergunta termina com **vírgula**
- Temas: Fase 1 = frutas e vitaminas | Fase 2 = alimentação saudável | Fase 3 = fast food, açúcar e gordura

**Textos e dificuldade** (no final de cada `new Fase(...)`):

- `intervaloSpawn` = segundos entre cada coisa que cai (**menor = mais difícil**)
- `chanceFastFood` = chance de cair fast food (0.30 = 30%)
- A dificuldade deve **crescer** da Fase 1 para a 3. Mude também os textos de descrição.

🧪 **TESTE:** jogar cada fase até o quiz e ver se as perguntas novas aparecem.

- [ ] 4 perguntas por fase · [ ] Dificuldade cresce · [ ] Jogo abre sem erro · [ ] Enviado no branch `grupo3`

---

## PASSO A PASSO NO GITHUB

Tudo pelo site, sem instalar nada.

1. **Criar o branch** (uma pessoa por grupo): clique no botão **main** → digite `grupo1` (ou `grupo2`, `grupo3`) → **Create branch**. Confira se o botão mostra o nome do seu grupo.
2. **Abrir o arquivo** da sua tarefa (no seu branch) e clicar no **lápis** (Edit).
3. **Fazer a mudança** (Ctrl + F para achar o `TODO`). Confira vírgulas, aspas e chaves.
4. **Salvar:** **Commit changes...** → resumo curto → marque **Commit directly to the grupoX branch** → **Commit changes**.
5. **Pull Request** (quando terminar): **Compare & pull request** → base `main` ← compare seu branch → **Create pull request**.
6. **Juntar (uma pessoa só):** ordem **Grupo 1 → 2 → 3**: abra o PR → **Merge pull request** → **Confirm merge**. Se der conflito na `Fabrica.cs`, mantenha as linhas novas dos dois grupos e apague só as marcas `<<<<<<<`, `=======` e `>>>>>>>`.
7. **Teste final (quem tem Windows):** na main, **Code → Download ZIP**, extraia, instale o .NET 8 SDK e rode `dotnet run`.

## Teste final (todos juntos)

- [ ] O jogo abre e o menu funciona
- [ ] Banana, Uva, Pizza e Donut aparecem
- [ ] Cada fase sorteia entre 4 perguntas
- [ ] O Boss faz a chuva a cada 10 s
- [ ] Vitória ao cultivar a cenoura; derrota com vida zerada ou estômago cheio

> Os códigos não foram compilados em um Windows. Se der erro vermelho, leia a mensagem e corrija o detalhe (vírgula, aspas, chave) ou peça ajuda.
