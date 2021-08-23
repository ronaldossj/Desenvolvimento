using System.Linq;
using System.Net;
using System.Web.Mvc;
using Desenvolvimento.Context;
using Desenvolvimento.Models;
using Dapper;

namespace Desenvolvimento.Controllers
{
    public class ClientesController : Controller
    {
        private TContext db = new TContext();

        public ActionResult Index()
        {
            return View(db.ClientesModels.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.ClientesModels.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nome_cliente,email,cpf,status")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                var ret = 0;
                using (var conexao = new System.Data.SqlClient.SqlConnection())
                {
                    conexao.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    conexao.Open();
                    if (db.ClientesModels.Where(x => x.cpf == clientes.cpf || x.email == clientes.email).Count() == 0)
                    {
                        var sql = "insert into clientes (nome_cliente, email, cpf, status) values (@nome_cliente, @email, @cpf, @status); select convert(int, scope_identity())";
                        ret = conexao.ExecuteScalar<int>(sql, clientes);
                    }
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.ClientesModels.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,nome_cliente,email,cpf,status")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                var ret = 0;
                using (var conexao = new System.Data.SqlClient.SqlConnection())
                {
                    conexao.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    conexao.Open();

                    var sql = "update clientes set nome_cliente=@nome_cliente, email=@email, cpf=@cpf, status=@status where ID = @ID";
                    ret = conexao.ExecuteScalar<int>(sql, clientes);
                    return RedirectToAction("Index");
                }
            }
            return View(clientes);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.ClientesModels.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
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

                var sql = "delete from clientes where (id = @id)";
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
