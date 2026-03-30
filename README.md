# Lab de Segurança Web - Newton Paiva
## Hackathon Defensivo: Encontrar e Corrigir

Este projeto é uma API .NET 8 intencionalmente vulnerável, criada para fins educacionais na disciplina de Arquitetura de Aplicações Web.

### 🛠️ Tecnologias
- .NET 8 / C#
- Entity Framework Core com SQLite
- JWT Authentication

### 🚀 Como Rodar
1. Certifique-se de ter o [.NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0) instalado.
2. Clone o repositório.
3. No terminal, execute:
   ```bash
   dotnet run
   ```
4. A API estará disponível em `http://localhost:5000` (ou similar, verifique o terminal).
5. O Swagger pode ser acessado em `http://localhost:5000/swagger`.

### 🛡️ Sua Missão
O sistema simula uma loja virtual com diversas falhas de segurança. Você deve atuar como auditor e desenvolvedor:

1. **Fase 1 — Recon**: Explore a API no Swagger ou Postman. Tente entender como os dados são trafegados.
2. **Fase 2 — Exploit**: Encontre e documente pelo menos 3 vulnerabilidades (ex: SQL Injection, XSS, IDOR).
3. **Fase 3 — Patch**: Corrija o código-fonte.
4. **Fase 4 — Verify**: Re-teste para confirmar que a falha foi mitigada.

### 🔑 Credenciais de Teste (Login em /api/login)
- **Admin**: `admin@loja.com` / `AQAAAAEAACcQAAAAE...[HASHED_PASSWORD]...`
- **Usuário 1**: `joao@email.com` / `secret123`
- **Usuário 2**: `maria@email.com` / `maria789`

---

### 🔍 Dicas de Vulnerabilidades Plantadas
- **SQL Injection**: Endpoint de busca de produtos.
- **XSS Refletido**: Endpoint de busca via View.
- **IDOR**: Acesso aos pedidos de outros usuários via ID na URL.
- **Exposição de Dados**: O perfil do usuário retorna dados que deveriam ser secretos.
- **Missing Auth**: Um endpoint administrativo que não exige privilégios.

---

### 📦 Entregável
Preencha o arquivo `SECURITY-REPORT.md` com suas descobertas e correções.
