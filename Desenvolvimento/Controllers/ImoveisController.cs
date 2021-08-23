using System.Linq;
using System.Net;
using System.Web.Mvc;
using Desenvolvimento.Context;
using Desenvolvimento.Models;
using Dapper;

namespace Desenvolvimento.Controllers
{
    public class ImoveisController : Controller
    {
        private TContext db = new TContext();

        public ActionResult Index()
        {
            return View(db.ImoveisModels.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imoveis imoveis = db.ImoveisModels.Find(id);
            if (imoveis == null)
            {
                return HttpNotFound();
            }
            return View(imoveis);
        }

        public ActionResult Create()
        {
            ViewBag.Nome = db.ClientesModels.Where(x => x.status == true).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,tipo_negocio,descricao,id_cliente,valor,ativo")] Imoveis imoveis)
        {
            if (ModelState.IsValid)
            {
                Clientes clientes = db.ClientesModels.Find(System.Convert.ToInt32(imoveis.id_cliente));
                var ret = 0;
                using (var conexao = new System.Data.SqlClient.SqlConnection())
                {
                    conexao.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    conexao.Open();
                    if (clientes == null)
                    {
                        var sql = "insert into imoveis (tipo_negocio,descricao,valor,ativo) values (@tipo_negocio, @descricao, @valor, @ativo); select convert(int, scope_identity())";
                        var parametros = new { tipo_negocio = imoveis.tipo_negocio, descricao = imoveis.descricao, valor = imoveis.valor, ativo = (imoveis.ativo ? 1 : 0) };
                        ret = conexao.ExecuteScalar<int>(sql, parametros);

                    }
                    else
                    {
                        var sql = "insert into imoveis (tipo_negocio,descricao,cpf_cliente,nome_cliente,id_cliente,valor,ativo) values (@tipo_negocio, @descricao, @cpf_cliente, @nome_cliente, @id_cliente, @valor, @ativo); select convert(int, scope_identity())";
                        var parametros = new { tipo_negocio = imoveis.tipo_negocio, descricao = imoveis.descricao, cpf_cliente = clientes.cpf, nome_cliente = clientes.nome_cliente, id_cliente = clientes.ID, valor = imoveis.valor, ativo = (imoveis.ativo ? 1 : 0) };
                        ret = conexao.ExecuteScalar<int>(sql, parametros);

                    }
                    return RedirectToAction("Index");
                }
            }

            return Redirect("Create");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imoveis imoveis = db.ImoveisModels.Find(id);
            if (imoveis == null)
            {
                return HttpNotFound();
            }
            ViewBag.Nome = db.ClientesModels.Where(x => x.status == true).ToList();
            return View(imoveis);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,tipo_negocio,descricao,id_cliente,valor,ativo")] Imoveis imoveis)
        {
            if (ModelState.IsValid)
            {
                Clientes clientes = db.ClientesModels.Find(System.Convert.ToInt32(imoveis.id_cliente));
                var ret = 0;
                using (var conexao = new System.Data.SqlClient.SqlConnection())
                {
                    conexao.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    conexao.Open();
                    if (clientes == null)
                    {
                        var sql = "update imoveis set tipo_negocio=@tipo_negocio, descricao=@descricao, cpf_cliente=@cpf_cliente, nome_cliente=@nome_cliente, id_cliente=@id_cliente, valor=@valor, ativo=@ativo where ID = @ID";
                        var parametros = new { tipo_negocio = imoveis.tipo_negocio, descricao = imoveis.descricao, cpf_cliente = "", nome_cliente = "", id_cliente = "", valor = imoveis.valor, ativo = (imoveis.ativo ? 1 : 0), ID = imoveis.ID };
                        ret = conexao.ExecuteScalar<int>(sql, parametros);

                    }
                    else
                    {
                        var sql = "update imoveis set tipo_negocio=@tipo_negocio, descricao=@descricao, cpf_cliente=@cpf_cliente, nome_cliente=@nome_cliente, id_cliente=@id_cliente, valor=@valor, ativo=@ativo where ID = @ID";
                        var parametros = new { tipo_negocio = imoveis.tipo_negocio, descricao = imoveis.descricao, cpf_cliente = clientes.cpf, nome_cliente = clientes.nome_cliente, id_cliente = clientes.ID, valor = imoveis.valor, ativo = (imoveis.ativo ? 1 : 0), ID = imoveis.ID };
                        ret = conexao.ExecuteScalar<int>(sql, parametros);

                    }
                    return RedirectToAction("Index");
                }
            }
            return View(imoveis);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imoveis imoveis = db.ImoveisModels.Find(id);
            if (imoveis == null)
            {
                return HttpNotFound();
            }
            return View(imoveis);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ret = false;
            using (var conexao = new System.Data.SqlClient.SqlConnection())
            {
                conexao.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conexao.Open();

                var sql = "delete from imoveis where (id = @id) AND (id_cliente IS NULL or id_cliente = '')";
                var parametros = new { id };
                ret = (conexao.Execute(sql, parametros) > 0);
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
