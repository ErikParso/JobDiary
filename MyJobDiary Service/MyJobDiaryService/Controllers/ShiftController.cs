using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using MyJobDiaryService.DataObjects;
using MyJobDiaryService.Models;

namespace MyJobDiaryService.Controllers
{
    [Authorize]
    public class ShiftController : TableController<Shift>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MyJobDiaryContext context = new MyJobDiaryContext();
            DomainManager = new EntityDomainManager<Shift>(context, Request);
        }

        // GET tables/TodoItem
        public IQueryable<Shift> GetAllTodoItems()
        {
            string userId = GetUserId(User);
            return Query().Where(s => s.UserId == userId);
        }

        // GET tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Shift> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Shift> PatchTodoItem(string id, Delta<Shift> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/TodoItem
        public async Task<IHttpActionResult> PostTodoItem(Shift item)
        {
            item.UserId = GetUserId(User);
            Shift current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/TodoItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }

        private string GetUserId(IPrincipal user)
        {
            ClaimsPrincipal claimsUser = (ClaimsPrincipal)user;
            string sid = claimsUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return sid;
        }
    }
}