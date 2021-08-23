namespace Desenvolvimento.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Desenvolvimento : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        nome_cliente = c.String(),
                        email = c.String(),
                        cpf = c.String(),
                        status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Imoveis",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        tipo_negocio = c.String(),
                        descricao = c.String(),
                        cpf_cliente = c.String(),
                        nome_cliente = c.String(),
                        id_cliente = c.String(),
                        valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Imoveis");
            DropTable("dbo.Clientes");
        }
    }
}
