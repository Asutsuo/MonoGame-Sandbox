# Devlog #000 - O Começo

> Este projeto nasceu antes deste devlog existir.
>
> Resolvi começar a documentar apenas agora. Este documento resume tudo o que foi desenvolvido até o momento e servirá como ponto de partida para os próximos registros.
>
> Meu objetivo nunca foi criar jogos comerciais. Vejo os jogos como excelentes projetos para estudar computação gráfica, arquitetura de software e programação orientada a objetos. No futuro pretendo aplicar esse conhecimento em projetos próprios.

---

# Filosofia do Projeto

Este projeto segue alguns princípios simples:

- Aprender antes de abstrair.
- Criar ferramentas apenas quando elas se mostrarem necessárias.
- Manter cada classe responsável por apenas uma tarefa.
- Priorizar simplicidade em vez de quantidade de funcionalidades.
- Documentar decisões importantes durante o desenvolvimento.

---

# Objetivo do Projeto

Decidi iniciar este projeto para aprender programação gráfica em C# utilizando o framework MonoGame.

Escolhi o MonoGame porque ele fornece apenas a infraestrutura necessária para criar jogos, deixando a arquitetura do restante da aplicação sob minha responsabilidade. Meu objetivo é entender como essas ferramentas funcionam internamente, em vez de depender de soluções prontas.

Este repositório serve como um ambiente de estudo, experimentação e documentação. Ao longo do caminho pretendo desenvolver pequenos jogos completos enquanto construo uma biblioteca reutilizável de componentes.

---

# Estrutura do Repositório

O projeto está organizado da seguinte forma:

```text
📂 MonoGame/
├── 📂 Docs/
│   ├── 📁 notes/
│   └── 📁 devlogs/
├── 📂 Engine/
│   ├── 📄 Timer.cs
│   ├── 📄 SpriteSheet.cs
│   └── 📄 Animation.cs
├── 📂 Estudos/
├── 📂 Projetos/
│   ├── 📂 Sandbox/
│   └── 📂 MineSweeper/
```

Cada diretório possui uma responsabilidade específica.

- **Engine** contém classes reutilizáveis e desacopladas do projeto principal.
- **Projetos** reúne os jogos e experimentos desenvolvidos utilizando a Engine.
- **Estudos** armazena materiais utilizados durante o aprendizado.
- **Docs** concentra anotações técnicas e os devlogs.

---

# Ferramentas Criadas

Até o momento a Engine possui os seguintes componentes:

- Engine (Class Library)
- Timer.cs
- SpriteSheet.cs
- Animation.cs

As ferramentas não foram planejadas antecipadamente. Cada uma surgiu conforme um problema concreto aparecia durante o desenvolvimento.

---

## Timer

Responsável pelo controle de tempo em qualquer sistema que necessite de atualizações temporizadas, como animações, cooldowns, estados ou outros comportamentos dependentes de tempo.

Seu funcionamento é simples: recebe um tempo limite e acumula continuamente o valor de `deltaTime` até atingir esse limite. Quando isso acontece, o timer expira e pode ser reiniciado ou reutilizado conforme necessário.

Durante sua implementação reaprendi conceitos fundamentais de C# e compreendi melhor como frameworks como o MonoGame utilizam `deltaTime` para tornar a lógica independente da taxa de quadros.

---

## SpriteSheet

Responsável por registrar sequências de animação e gerar automaticamente os `Rectangle` utilizados pelo MonoGame durante a renderização.

Cada sequência é registrada manualmente através de informações como origem, tamanho dos frames, espaçamento e quantidade de quadros.

Essa abordagem foi escolhida porque muitas spritesheets possuem espaçamentos irregulares entre linhas e colunas, tornando inviável assumir uma estrutura perfeitamente uniforme.

No futuro essa classe poderá receber novas responsabilidades relacionadas ao gerenciamento de spritesheets.

---

## Animation

A classe Animation foi projetada para ser extremamente simples.

Ela não conhece `Rectangle`, `Texture2D` ou qualquer outro tipo específico do MonoGame. Sua única responsabilidade é controlar qual índice da animação está ativo em determinado momento.

Internamente utiliza um `Timer` para controlar a troca entre os quadros.

Essa separação permite reutilizar a classe em diferentes contextos, inclusive em sistemas que não envolvem renderização gráfica.

---

# Decisões de Arquitetura

Algumas decisões importantes tomadas até o momento:

- Timer controla apenas tempo.
- SpriteSheet conhece apenas a estrutura da spritesheet.
- Animation controla apenas o índice do quadro atual.
- O jogo é responsável por integrar essas classes durante a renderização.

Essa separação mantém cada componente pequeno, reutilizável e fácil de modificar.

---

# Dificuldades Encontradas

Durante esta primeira etapa surgiram diversos desafios:

- Organizar múltiplos projetos em um único repositório.
- Compartilhar código entre diferentes projetos.
- Compreender o funcionamento de `deltaTime`.
- Estruturar uma biblioteca de classes reutilizáveis.
- Criar e configurar uma Solution do .NET.

---

# O que Aprendi

Algumas das principais lições até agora:

- Uma classe deve possuir apenas uma responsabilidade.
- Ferramentas reutilizáveis surgem naturalmente durante um projeto.
- Separar lógica e renderização simplifica bastante a arquitetura.
- Boas abstrações costumam aparecer depois que o problema já existe.

---

# Estado Atual

Atualmente o projeto possui:

- Biblioteca de classes funcionando.
- Timer concluído.
- Animation concluída.
- SpriteSheet funcional.
- Estrutura do repositório organizada.
- Desenvolvimento do MineSweeper iniciado.

---

# Próximos Passos

Os próximos objetivos são:

- Criar a estrutura do tabuleiro.
- Implementar as células.
- Aprender renderização de texto no MonoGame.
- Centralizar corretamente os elementos da interface.
- Finalizar a primeira versão jogável do MineSweeper.

---

# Reflexão

Até aqui o desenvolvimento tem sido extremamente divertido.

Meu objetivo não é apenas recriar um Campo Minado, mas utilizá-lo como um laboratório para descobrir quais ferramentas uma engine realmente precisa. Em vez de projetar uma arquitetura completa desde o início, pretendo deixar que cada necessidade apareça naturalmente durante o desenvolvimento.

Espero que, ao longo dos próximos devlogs, seja possível acompanhar não apenas a evolução dos jogos, mas também a evolução da própria Engine e das decisões de arquitetura tomadas durante essa jornada.
