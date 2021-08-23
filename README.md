# Desenvolvimento

Desenvolvido para atender um teste solicitado

Criar um CRUD de Cliente utilizando ASP NET MVC e persistência de dados utilizando (EF, Dapper).
 * Listar cliente
 * Incluir um cliente
   Campos: Nome, Email, CPF, ativo
 * Editar um cliente
 * Excluir um cliente

Criar um CRUD de Imóvel
 * Listar Imóvel
 * Incluir um Imóel
   Campos: Tipo de Negócio (venda ou aluguel), valor, descrição, ativo
           Somente a descrição não é obrigatória
 * Editar um Imóvel
 * Excluir um Imóvel

Regras:
- Não permitir a inclusão de mais de um cliente com o mesmo CPF ou mesmo e-mail
- Não permitir relacionar mais de um cliente ao mesmo imóvel
- No cadastro de imóveis, não listar clientes inativos
- Não permitir a exclusão de um imóvel que esteja relacionado a um cliente
